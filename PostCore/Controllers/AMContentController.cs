using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostCore.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        public async Task<IActionResult> GetAMContentData(Guid? id)
        {
            if (id == null)
                return NotFound();

            var assetManagementContents = await _context.Amcontents.Where(c => c.Uniqueassetidcont == id).ToListAsync();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            return Json(new { success = true, data = assetManagementContents }, options);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAMContentData()
        {
            var assetManagementContents = await _context.Amcontents.ToListAsync();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            return Json(new { success = true, data = assetManagementContents }, options);
        }

        [HttpGet]
        public async Task<IActionResult> _ContentCreate(Guid id)
        {
            var asset = await _context.AssetMgmts.FirstOrDefaultAsync(a => a.Uniqueassetid == id);
            if (asset == null)
            {
                return NotFound();
            }
            var viewModel = new Amcontent();
            viewModel.Uniquecontentid = Guid.NewGuid();
            viewModel.Uniqueassetidcont = id;
            return PartialView("~/Views/AMContent/_ContentCreate.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _ContentCreate(Amcontent model, Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var parentEntity = await _context.AssetMgmts.FirstOrDefaultAsync(a => a.Uniqueassetid == id);

                    if (parentEntity != null)
                    {
                        var childEntity = new Amcontent
                        {
                            Uniquecontentid = Guid.NewGuid(),
                            Uniqueassetidcont = id,
                            Assetcontentnumber = model.Assetcontentnumber,
                            Assetcontentdescription = model.Assetcontentdescription,
                            Assetcontentversion = model.Assetcontentversion,
                            Assetcontentdateassigned = model.Assetcontentdateassigned
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

            return PartialView("~/Views/AMContent/_ContentUpdate.cshtml", childRecord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _ContentUpdate(Guid id, Guid c_id, Amcontent model)
        {
            if (id != model.Uniqueassetidcont)
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

                    _context.Entry(childRecord).CurrentValues.SetValues(model);
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
            return PartialView("~/Views/AMContent/_ContentDelete.cshtml", childRecord);
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

                return Json(new { success = true, data = parentRecord });
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
