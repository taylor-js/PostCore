﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostCore.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

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
        public async Task<IActionResult> GetAssetMgmtData()
        {
            var dataCollection = await _context.AssetMgmts.ToListAsync();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            return Json(new { success = true, data = dataCollection }, options);
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
        public IActionResult _AssetCreate()
        {
            var viewModel = new AssetMgmt();

            return PartialView(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _AssetCreate(AssetMgmt model)
        {
            if (ModelState.IsValid)
            {
                model = new AssetMgmt
                {
                    Uniqueassetid = Guid.NewGuid(),
                    Assetid = model.Assetid,
                    Assettype = model.Assettype,
                    Assetname = model.Assetname,
                    Assetmanufacturer = model.Assetmanufacturer,
                    Assetcategory = model.Assetcategory,
                    Assetworkordernumber = model.Assetworkordernumber,
                    Assetpurchaseordernumber = model.Assetpurchaseordernumber,
                    Assetdate = model.Assetdate,
                    Assetprojectmanager = model.Assetprojectmanager,
                    Assetequipmentamount = model.Assetequipmentamount,
                    Assetdescription = model.Assetdescription
                };

                _context.AssetMgmts.Add(model);
                await _context.SaveChangesAsync();

                return Json(new { success = true, data = model });
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

            return PartialView("_AssetUpdate", assetMgmt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _AssetUpdate(Guid id, AssetMgmt viewModel)
        {
            if (id != viewModel.Uniqueassetid)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingAsset = await _context.AssetMgmts.FirstOrDefaultAsync(a => a.Uniqueassetid == id);
                    if (existingAsset == null)
                        return NotFound();

                    _context.Entry(existingAsset).CurrentValues.SetValues(viewModel);
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

            return PartialView("_AssetDelete", model);
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
