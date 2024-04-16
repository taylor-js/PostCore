using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostCore.Models;

namespace PostCore.Controllers
{
    public class AssetMgmtController : Controller
    {
        private readonly ILogger<AssetMgmtController> _logger;
        private readonly D2glkvqrc1vuvsContext _context;

        public AssetMgmtController(ILogger<AssetMgmtController> logger, D2glkvqrc1vuvsContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var assetMgmts = await _context.AssetMgmts.ToListAsync();
            if (assetMgmts == null)
            {
                return NotFound();
            }

            var compositionCollection = new CompositionCollection
            {
                IE_AM = assetMgmts
            };

            return View(compositionCollection);
        }


        [HttpGet]
        public async Task<PartialViewResult> IndexGrid(string search)
        {
            var dataCollection = string.IsNullOrEmpty(search) ? await _context.AssetMgmts.ToListAsync() : await GetFilteredData(search);
            return PartialView("_IndexGrid", dataCollection);
        }

        public async Task<IEnumerable<AssetMgmt>> GetFilteredData(string search)
        {
            string lowerCaseSearch = search.ToLower();

            // Instead of discards, use actual variables
            bool isInt = int.TryParse(search, out int searchInt);
            bool isDouble = decimal.TryParse(search, out decimal searchDouble);
            bool isDateTime = DateTime.TryParse(search, out DateTime searchDateTime);
            DateOnly searchDateOnly = DateOnly.FromDateTime(searchDateTime);

            return await _context.AssetMgmts
                .Where(x =>
                    (isInt && (x.Assetid == searchInt ||
                               x.Assetworkordernumber == searchInt ||
                               x.Assetpurchaseordernumber == searchInt)) ||
                    (isDouble && x.Assetequipmentamount == searchDouble) || // Corrected to compare with searchDouble
                    (isDateTime && x.Assetdate == searchDateOnly) || // Adjust based on your database column type
                    (x.Assettype != null && x.Assettype.ToLower().Contains(lowerCaseSearch)) ||
                    (x.Assetname != null && x.Assetname.ToLower().Contains(lowerCaseSearch)) ||
                    (x.Assetmanufacturer != null && x.Assetmanufacturer.ToLower().Contains(lowerCaseSearch)) ||
                    (x.Assetcategory != null && x.Assetcategory.ToLower().Contains(lowerCaseSearch)) ||
                    (x.Assetprojectmanager != null && x.Assetprojectmanager.ToLower().Contains(lowerCaseSearch)) ||
                    (x.Assetdescription != null && x.Assetdescription.ToLower().Contains(lowerCaseSearch))
                )
                .ToListAsync();
        }


        [HttpGet]
        public async Task<ActionResult> AssetDetails(Guid? id)
        {
            if (id == null)
                return NotFound();

            var parent = await _context.AssetMgmts
                .Include(a => a.Amcontents)
                .Include(a => a.Amdistribs)
                .FirstOrDefaultAsync(a => a.Uniqueassetid == id);

            if (parent == null)
                return NotFound();

            var assetManagementContents = await _context.Amcontents.Where(c => c.Uniqueassetidcont == id).ToListAsync();
            var assetManagementDistributions = await _context.Amdistribs.Where(d => d.Uniqueassetiddistr == id).ToListAsync();

            var viewModel = new CompositionCollection
            {
                AM = parent,
                IE_AM_C = assetManagementContents,
                IE_AM_D = assetManagementDistributions
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> _AssetCreate()
        {
            var viewModel = new CompositionCollection
            {
                AM = new AssetMgmt(),
                IE_AM = await _context.AssetMgmts.ToListAsync()
            };

            return PartialView(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _AssetCreate(CompositionCollection model)
        {
            if (ModelState.IsValid)
            {
                AssetMgmt newAsset = new AssetMgmt
                {
                    Uniqueassetid = Guid.NewGuid(),
                    Assetid = model.AM.Assetid,
                    Assettype = model.AM.Assettype,
                    Assetname = model.AM.Assetname,
                    Assetmanufacturer = model.AM.Assetmanufacturer,
                    Assetcategory = model.AM.Assetcategory,
                    Assetworkordernumber = model.AM.Assetworkordernumber,
                    Assetpurchaseordernumber = model.AM.Assetpurchaseordernumber,
                    Assetdate = model.AM.Assetdate,
                    Assetprojectmanager = model.AM.Assetprojectmanager,
                    Assetequipmentamount = model.AM.Assetequipmentamount,
                    Assetdescription = model.AM.Assetdescription
                };

                _context.AssetMgmts.Add(newAsset);
                await _context.SaveChangesAsync();

                return Json(new { success = true, data = newAsset });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, errors = errors });
        }

        [HttpGet]
        public async Task<IActionResult> _AssetUpdate(Guid? id)
        {
            if (id == null)
                return NotFound();

            var assetMgmt = await _context.AssetMgmts.FindAsync(id);
            if (assetMgmt == null)
                return NotFound();

            var viewModel = new CompositionCollection
            {
                AM = assetMgmt
            };

            return PartialView("_AssetUpdate", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _AssetUpdate(Guid id, CompositionCollection viewModel)
        {
            if (id != viewModel.AM.Uniqueassetid)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingAsset = await _context.AssetMgmts.FirstOrDefaultAsync(a => a.Uniqueassetid == id);
                    if (existingAsset == null)
                        return NotFound();

                    _context.Entry(existingAsset).CurrentValues.SetValues(viewModel.AM);
                    await _context.SaveChangesAsync();

                    //return RedirectToAction("AssetDetails", "AssetMgmt", new { id = existingAsset.UniqueAssetId });
                    return Json(new { success = true, data = existingAsset });
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Concurrency conflict occurred. The record you attempted to edit was modified by another user.");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "An error occurred while updating the record.");
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, errors = errors });
        }

        [HttpGet]
        public async Task<IActionResult> _AssetDelete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var model = await _context.AssetMgmts
                .Include(a => a.Amcontents)
                .Include(a => a.Amdistribs)
                .FirstOrDefaultAsync(a => a.Uniqueassetid == id);

            if (model == null)
                return NotFound();

            var contents = model.Amcontents.Where(c => c.Uniqueassetidcont == model.Uniqueassetid).ToList();
            var distributions = model.Amdistribs.Where(d => d.Uniqueassetiddistr == model.Uniqueassetid).ToList();
            CompositionCollection compositionCollection = new CompositionCollection
            {
                AM = model,
                IE_AM_C = contents,
                IE_AM_D = distributions,
            };

            return PartialView("_AssetDelete", compositionCollection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _AssetDelete(Guid id)
        {
            var assetMgmtRelationship = await _context.AssetMgmts
                .Include(a => a.Amcontents)
                .Include(a => a.Amdistribs)
                .FirstOrDefaultAsync(a => a.Uniqueassetid == id);

            if (assetMgmtRelationship == null)
                return NotFound();

            try
            {
                _context.Amcontents.RemoveRange(assetMgmtRelationship.Amcontents);
                _context.Amdistribs.RemoveRange(assetMgmtRelationship.Amdistribs);
                _context.AssetMgmts.Remove(assetMgmtRelationship);
                await _context.SaveChangesAsync();

                //return RedirectToAction("Index", "AssetMgmt");
                return Json(new { success = true, assetId = id, url = Url.Action("Index", "AssetMgmt") });
            }
            catch (Exception)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { success = false, errors = errors });
            }
        }

        private async Task<bool> AssetMgmtExists(Guid id)
        {
            return await _context.AssetMgmts.AnyAsync(e => e.Uniqueassetid == id);
        }
    }
}
