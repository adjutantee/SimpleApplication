using Application.DTO;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SimpleApplication.Models;
using System.Diagnostics;

namespace SimpleApplication.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly IReceiptService _receiptService;
        private readonly IResourceService _resourceService;
        private readonly IUnitService _unitService;
        private readonly ILogger<ReceiptsController> _logger;

        public ReceiptsController(
            IReceiptService receiptService,
            IResourceService resourceService,
            IUnitService unitService,
            ILogger<ReceiptsController> logger)
        {
            _receiptService = receiptService;
            _resourceService = resourceService;
            _unitService = unitService;
            _logger = logger;
        }

        // GET: Receipts
        public async Task<IActionResult> Index(ReceiptFilterDto filter = null)
        {
            try
            {
                ViewBag.Resources = await _resourceService.GetAllResourcesAsync();
                ViewBag.Units = await _unitService.GetAllUnitsAsync();

                var receiptDtos = filter != null
                    ? await _receiptService.GetFilteredReceiptsAsync(filter)
                    : await _receiptService.GetAllReceiptsAsync();

                var receiptsWithItems = new List<ReceiptWithItemsDto>();
                foreach (var receiptDto in receiptDtos)
                {
                    var fullReceipt = await _receiptService.GetReceiptByIdAsync(receiptDto.Id);
                    receiptsWithItems.Add(fullReceipt);
                }

                return View(receiptsWithItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting receipts");
                return View("Error", new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
            }
        }

        // GET: Receipts/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.Resources = await _resourceService.GetAllResourcesAsync();
                ViewBag.Units = await _unitService.GetAllUnitsAsync();
                return View(new ReceiptWithItemsDto
                {
                    Receipt = new ReceiptDto { Date = DateTime.Today },
                    Items = new List<ReceiptItemDto> { new ReceiptItemDto() }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading create form");
                return View("Error");
            }
        }

        // POST: Receipts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReceiptWithItemsDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dto.Receipt.Date = DateTime.SpecifyKind(dto.Receipt.Date, DateTimeKind.Utc);

                    await _receiptService.CreateReceiptAsync(dto);
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Resources = await _resourceService.GetAllResourcesAsync();
                ViewBag.Units = await _unitService.GetAllUnitsAsync();
                return View(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating receipt");
                ModelState.AddModelError("", "Ошибка при создании поступления");
                ViewBag.Resources = await _resourceService.GetAllResourcesAsync();
                ViewBag.Units = await _unitService.GetAllUnitsAsync();
                return View(dto);
            }
        }

        // GET: Receipts/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var receipt = await _receiptService.GetReceiptByIdAsync(id);
                if (receipt == null) return NotFound();

                ViewBag.Resources = await _resourceService.GetAllResourcesAsync();
                ViewBag.Units = await _unitService.GetAllUnitsAsync();
                return View(receipt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error loading edit form for receipt ID: {id}");
                return View("Error");
            }
        }

        // POST: Receipts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ReceiptWithItemsDto dto)
        {
            try
            {
                if (id != dto.Receipt.Id) return NotFound();

                if (ModelState.IsValid)
                {
                    dto.Receipt.Date = DateTime.SpecifyKind(dto.Receipt.Date, DateTimeKind.Utc);

                    await _receiptService.UpdateReceiptAsync(dto);
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Resources = await _resourceService.GetAllResourcesAsync();
                ViewBag.Units = await _unitService.GetAllUnitsAsync();
                return View(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating receipt ID: {id}");
                ModelState.AddModelError("", "Ошибка при обновлении поступления");
                ViewBag.Resources = await _resourceService.GetAllResourcesAsync();
                ViewBag.Units = await _unitService.GetAllUnitsAsync();
                return View(dto);
            }
        }

        // GET: Receipts/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var receipt = await _receiptService.GetReceiptByIdAsync(id);
                if (receipt == null) return NotFound();

                return View(receipt.Receipt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error loading delete confirmation for receipt ID: {id}");
                return View("Error");
            }
        }

        // POST: Receipts/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _receiptService.DeleteReceiptAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting receipt ID: {id}");
                return View("Error");
            }
        }
    }
}
