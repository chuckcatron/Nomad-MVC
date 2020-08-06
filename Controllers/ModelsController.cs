using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nomad_MVC.Data.Interfaces;
using Nomad_MVC.Models;

namespace Nomad_MVC.Controllers
{
    [Authorize]
    public class ModelsController : Controller
    {
        private readonly IModelRepository _modelRepository;

        public ModelsController(IModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        // GET: Models
        public async Task<IActionResult> Index()
        {
            var models = await _modelRepository.GetModels();
            return View(models);
        }

        // GET: Models/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelId = Convert.ToInt32(id);
            var model = await _modelRepository.GetModel(modelId);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Models/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Models/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] Model model)
        {
            if (ModelState.IsValid)
            {
                await _modelRepository.SaveModel(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Models/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelId = Convert.ToInt32(id);
            var model = await _modelRepository.GetModel(modelId);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Models/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] Model model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _modelRepository.SaveModel(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Models/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelId = Convert.ToInt32(id);
            var model = await _modelRepository.GetModel(modelId);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await _modelRepository.GetModel(id);
            await _modelRepository.DeleteModel(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
