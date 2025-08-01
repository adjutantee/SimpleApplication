using Application.DTO;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SimpleApplication.Models;
using System.Diagnostics;

namespace SimpleApplication.Controllers
{
    public class ResourcesController : Controller
    {
        private readonly IResourceService _resourceService;
        private readonly ILogger<ResourcesController> _logger;

        public ResourcesController(
            IResourceService resourceService,
            ILogger<ResourcesController> logger)
        {
            _resourceService = resourceService;
            _logger = logger;
        }

        // GET: Resources
        public async Task<IActionResult> Index()
        {
            try
            {
                var resources = await _resourceService.GetAllResourcesAsync();
                return View(resources);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении ресурсов");
                return View("Error", new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
            }
        }

        // GET: Resources/Archive
        public async Task<IActionResult> Archive()
        {
            try
            {
                var archivedResources = await _resourceService.GetAllArchiveResources();
                return View(archivedResources);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении архивированных ресурсов");
                return View("Error");
            }
        }

        // GET: Resources/Create
        public IActionResult Create()
        {
            return View(new ResourceDto());
        }

        // POST: Resources/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResourceDto resourceDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _resourceService.AddResourceAsync(resourceDto);
                    return RedirectToAction(nameof(Index));
                }
                return View(resourceDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при создании ресурса");
                ModelState.AddModelError("", "Ошибка при создании ресурса");
                return View(resourceDto);
            }
        }

        // GET: Resources/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var resource = await _resourceService.GetResourceByIdAsync(id);
                if (resource == null) return NotFound();

                return View(resource);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при загрузке формы редактирования по ID ресурса: {id}");
                return View("Error");
            }
        }

        // POST: Resources/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ResourceDto resourceDto)
        {
            try
            {
                if (id != resourceDto.Id) return NotFound();

                if (ModelState.IsValid)
                {
                    await _resourceService.UpdateResourceByAsync(resourceDto);
                    return RedirectToAction(nameof(Index));
                }
                return View(resourceDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при обновлении ресурса по ID: {id}");
                ModelState.AddModelError("", "Ошибка при обновлении ресурса");
                return View(resourceDto);
            }
        }

        // GET: Resources/Delete/
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var resource = await _resourceService.GetResourceByIdAsync(id);
                if (resource == null) return NotFound();

                return View(resource);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при загрузке подтверждения удаления по ID ресурса: {id}");
                return View("Error");
            }
        }

        // POST: Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _resourceService.DeleteResourceAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении ресурса по ID: {id}");
                return View("Error");
            }
        }

        // POST: Resources/Archive/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Archive(int id)
        {
            try
            {
                await _resourceService.ArchiveResource(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка архивирования ресурса по ID: {id}");
                return View("Error");
            }
        }
    }
}
