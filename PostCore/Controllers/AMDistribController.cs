using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostCore.Models;

namespace InfoMorph.Controllers
{
    public class AMDistribController : Controller
    {
        private readonly ILogger<AMDistribController> _logger;
        private readonly D2glkvqrc1vuvsContext _context;

        public AMDistribController(ILogger<AMDistribController> logger, D2glkvqrc1vuvsContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contents = await _context.Amdistribs.ToListAsync();
            var compositionCollection = new CompositionCollection
            {
                IE_AM_D = contents
            };
            return View(compositionCollection);
        }

        [HttpGet]
        public async Task<IActionResult> _DistribCreate(Guid id)
        {
            var asset = await _context.AssetMgmts.FirstOrDefaultAsync(a => a.Uniqueassetid == id);
            if (asset == null)
            {
                return NotFound();
            }
            var viewModel = new CompositionCollection
            {
                AM = asset,
                AM_D = new Amdistrib(),
            };
            viewModel.AM_D.Uniquedistributionid = Guid.NewGuid();
            viewModel.AM_D.Uniqueassetiddistr = id;
            return PartialView("~/Views/Amdistrib/_DistribCreate.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _DistribCreate(CompositionCollection model, Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var parentEntity = await _context.AssetMgmts.FirstOrDefaultAsync(a => a.Uniqueassetid == id);

                    if (parentEntity != null)
                    {
                        Amdistrib childEntity = new Amdistrib
                        {
                            Uniquedistributionid = Guid.NewGuid(),
                            Uniqueassetiddistr = id,
                            Assetdistributionowner = model.AM_D.Assetdistributionowner,
                            Assetdistributionlocation = model.AM_D.Assetdistributionlocation,
                            Assetdistributionquantity = model.AM_D.Assetdistributionquantity,
                            Assetdistributiondateassigned = model.AM_D.Assetdistributiondateassigned
                        };

                        _context.Amdistribs.Add(childEntity);
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
        public async Task<IActionResult> _DistribUpdate(Guid id, Guid d_id)
        {
            var childRecord = await _context.Amdistribs.FirstOrDefaultAsync(x => x.Uniqueassetiddistr == id && x.Uniquedistributionid == d_id);

            if (id == Guid.Empty || d_id == Guid.Empty || childRecord == null)
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
                AM_D = childRecord,
            };

            return PartialView("~/Views/AMDistrib/_DistribUpdate.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _DistribUpdate(Guid id, Guid d_id, CompositionCollection model)
        {
            if (id != model.AM_D.Uniqueassetiddistr)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var parentRecord = await _context.AssetMgmts.FirstOrDefaultAsync(a => a.Uniqueassetid == id);
                    var childRecord = await _context.Amdistribs.FirstOrDefaultAsync(x => x.Uniqueassetiddistr == id && x.Uniquedistributionid == d_id);

                    if (childRecord == null || parentRecord == null)
                    {
                        return NotFound();
                    }

                    _context.Entry(childRecord).CurrentValues.SetValues(model.AM_D);
                    await _context.SaveChangesAsync();

                    //return RedirectToAction("AssetDetails", "AssetMgmt", new { id = parentRecord.Uniqueassetid });
                    return Json(new { success = true, data = model });
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
        public async Task<IActionResult> _DistribDelete(Guid? id, Guid? d_id)
        {
            if (d_id == null || id == null)
            {
                return NotFound();
            }

            var childRecord = await _context.Amdistribs.FirstOrDefaultAsync(x => x.Uniqueassetiddistr == id && x.Uniquedistributionid == d_id);

            if (childRecord == null)
            {
                return NotFound();
            }

            var model = new CompositionCollection
            {
                AM_D = childRecord
            };

            return PartialView("~/Views/AMDistrib/_DistribDelete.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _DistribDelete(Guid id, Guid d_id)
        {
            try
            {
                var parentRecord = await _context.AssetMgmts.FirstOrDefaultAsync(a => a.Uniqueassetid == id);
                var childRecord = await _context.Amdistribs.FirstOrDefaultAsync(x => x.Uniqueassetiddistr == id && x.Uniquedistributionid == d_id);

                if (childRecord == null || parentRecord == null)
                {
                    return NotFound();
                }

                _context.Amdistribs.Remove(childRecord);
                await _context.SaveChangesAsync();

                CompositionCollection viewModel = new CompositionCollection
                {
                    AM = parentRecord,
                    AM_D = childRecord,
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
            return await _context.Amdistribs.AnyAsync(e => e.Uniquedistributionid == id);
        }
    }
}
