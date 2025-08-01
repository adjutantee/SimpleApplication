using Application.DTO;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SimpleApplication.Models;
using System.Diagnostics;

namespace SimpleApplication.Controllers
{
    public class UnitsController : Controller
    {
        private readonly IUnitService _unitService;
        private readonly ILogger<UnitsController> _logger;

        public UnitsController(
            IUnitService unitService,
            ILogger<UnitsController> logger)
        {
            _unitService = unitService;
            _logger = logger;
        }

        // GET: Units
        public async Task<IActionResult> Index()
        {
            try
            {
                var units = await _unitService.GetAllUnitsAsync();
                return View(units);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting units");
                return View("Error", new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
            }
        }

        // GET: Units/Archive
        public async Task<IActionResult> Archive()
        {
            try
            {
                var archivedUnits = await _unitService.GetAllArchiveAsync();
                return View(archivedUnits);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting archived units");
                return View("Error");
            }
        }

        // GET: Units/Create
        public IActionResult Create()
        {
            return View(new UnitDto());
        }

        // POST: Units/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UnitDto unitDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitService.AddUnitAsync(unitDto);
                    return RedirectToAction(nameof(Index));
                }
                return View(unitDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating unit");
                ModelState.AddModelError("", "Ошибка при создании единицы измерения");
                return View(unitDto);
            }
        }

        // GET: Units/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var unit = await _unitService.GetUnitByIdAsync(id);
                if (unit == null) return NotFound();

                return View(unit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error loading edit form for unit ID: {id}");
                return View("Error");
            }
        }

        // POST: Units/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UnitDto unitDto)
        {
            try
            {
                if (id != unitDto.Id) return NotFound();

                if (ModelState.IsValid)
                {
                    await _unitService.UpdateUnitByAsync(unitDto);
                    return RedirectToAction(nameof(Index));
                }
                return View(unitDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating unit ID: {id}");
                ModelState.AddModelError("", "Ошибка при обновлении единицы измерения");
                return View(unitDto);
            }
        }

        // GET: Units/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var unit = await _unitService.GetUnitByIdAsync(id);
                if (unit == null) return NotFound();

                return View(unit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error loading delete confirmation for unit ID: {id}");
                return View("Error");
            }
        }

        // POST: Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _unitService.DeleteUnitAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting unit ID: {id}");
                return View("Error");
            }
        }

        // POST: Units/Archive/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Archive(int id)
        {
            try
            {
                await _unitService.ArchiveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error archiving unit ID: {id}");
                return View("Error");
            }
        }
    }
}
