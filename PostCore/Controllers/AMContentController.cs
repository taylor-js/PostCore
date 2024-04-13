using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostCore.Models;

namespace InfoMorph.Controllers
{
    public class AMContentController : Controller
    {
        private readonly ILogger<AMContentController> _logger;
        private readonly D2glkvqrc1vuvsContext _context;

        public AMContentController(ILogger<AMContentController> logger, D2glkvqrc1vuvsContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contents = await _context.Amcontents.ToListAsync();
            var compositionCollection = new CompositionCollection
            {
                IE_AM_C = contents
            };
            return View(compositionCollection);
        }

        [HttpGet]
        public async Task<IActionResult> _ContentCreate(Guid id)
        {
            var asset = await _context.AssetMgmts.FirstOrDefaultAsync(a => a.Uniqueassetid == id);
            if (asset == null)
            {
                return NotFound();
            }
            var viewModel = new CompositionCollection
            {
                AM = asset,
                AM_C = new Amcontent(),
            };
            viewModel.AM_C.Uniquecontentid = Guid.NewGuid();
            viewModel.AM_C.Uniqueassetidcont = id;
            return PartialView("~/Views/AMContent/_ContentCreate.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _ContentCreate(CompositionCollection model, Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var parentEntity = await _context.AssetMgmts.FirstOrDefaultAsync(a => a.Uniqueassetid == id);

                    if (parentEntity != null)
                    {
                        Amcontent childEntity = new Amcontent
                        {
                            Uniquecontentid = Guid.NewGuid(),
                            Uniqueassetidcont = id,
                            Assetcontentnumber = model.AM_C.Assetcontentnumber,
                            Assetcontentdescription = model.AM_C.Assetcontentdescription,
                            Assetcontentversion = model.AM_C.Assetcontentversion,
                            Assetcontentdateassigned = model.AM_C.Assetcontentdateassigned
                        };

                        _context.Amcontents.Add(childEntity);
                        await _context.SaveChangesAsync();

                        //return RedirectToAction("AssetDetails", "AssetMgmt", new { id = id });
                        return Json(new { success = true, data = model });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Parent entity not found.");
                    }
                }

                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { success = false, errors = errors });
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { success = false, errors = errors });
            }
        }

        [HttpGet]
        public async Task<IActionResult> _ContentUpdate(Guid id, Guid c_id)
        {
            var childRecord = await _context.Amcontents.FirstOrDefaultAsync(x => x.Uniqueassetidcont == id && x.Uniquecontentid == c_id);

            if (id == Guid.Empty || c_id == Guid.Empty || childRecord == null)
            {
                return NotFound();
            }
            var parentRecord = await _context.AssetMgmts.FindAsync(id);
            if (parentRecord == null)
            {
                return NotFound();
            }
            CompositionCollection viewModel = new CompositionCollection
            {
                AM = parentRecord,
                AM_C = childRecord,
            };

            return PartialView("~/Views/AMContent/_ContentUpdate.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _ContentUpdate(Guid id, Guid c_id, CompositionCollection model)
        {
            if (id != model.AM_C.Uniqueassetidcont)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var parentRecord = await _context.AssetMgmts.FirstOrDefaultAsync(a => a.Uniqueassetid == id);
                    var childRecord = await _context.Amcontents.FirstOrDefaultAsync(x => x.Uniqueassetidcont == id && x.Uniquecontentid == c_id);

                    if (childRecord == null || parentRecord == null)
                    {
                        return NotFound();
                    }

                    _context.Entry(childRecord).CurrentValues.SetValues(model.AM_C);
                    await _context.SaveChangesAsync();

                    return Json(new { success = true, data = model });// Update Content Post
                    //return RedirectToAction("AssetDetails", "AssetMgmt", new { id = parentRecord.Uniqueassetid });
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
        public async Task<IActionResult> _ContentDelete(Guid? id, Guid? c_id)
        {
            if (c_id == null || id == null)
            {
                return NotFound();
            }

            var childRecord = await _context.Amcontents.FirstOrDefaultAsync(x => x.Uniqueassetidcont == id && x.Uniquecontentid == c_id);

            if (childRecord == null)
            {
                return NotFound();
            }

            var model = new CompositionCollection
            {
                AM_C = childRecord
            };
            return PartialView("~/Views/AMContent/_ContentDelete.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _ContentDelete(Guid id, Guid c_id)
        {
            try
            {
                var parentRecord = await _context.AssetMgmts.FirstOrDefaultAsync(a => a.Uniqueassetid == id);
                var childRecord = await _context.Amcontents.FirstOrDefaultAsync(x => x.Uniqueassetidcont == id && x.Uniquecontentid == c_id);

                if (childRecord == null || parentRecord == null)
                {
                    return NotFound();
                }

                _context.Amcontents.Remove(childRecord);
                await _context.SaveChangesAsync();

                CompositionCollection viewModel = new CompositionCollection
                {
                    AM = parentRecord,
                    AM_C = childRecord,
                };

                //return RedirectToAction("AssetDetails", "AssetMgmt", new { id = id });
                return Json(new { success = true, data = viewModel });
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting the record: {ex.Message}");
                return Json(new { success = false, errors = new[] { "An error occurred while deleting the record." } });
            }
        }

        private async Task<bool> AssetManagementContentExists(Guid id)
        {
            return await _context.Amcontents.AnyAsync(e => e.Uniquecontentid == id);
        }
    }
}
