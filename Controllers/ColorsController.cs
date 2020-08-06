using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nomad_MVC.Data.Interfaces;
using Nomad_MVC.Models;

namespace Nomad_MVC.Controllers
{
    [Authorize]
    public class ColorsController : Controller
    {
        private readonly IColorRepository _colorRepository;


        public ColorsController(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        // GET: Colors
        public async Task<IActionResult> Index()
        {
            var colors = await _colorRepository.GetColors();
            return View(colors);
        }

        // GET: Colors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colorId = Convert.ToInt32(id);
            var color = await _colorRepository.GetColor(colorId);
            
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // GET: Colors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Colors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] Color color)
        {
            if (ModelState.IsValid)
            {
                await _colorRepository.SaveColor(color);
                return RedirectToAction(nameof(Index));
            }
            return View(color);
        }

        // GET: Colors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colorId = Convert.ToInt32(id);
            var color = await _colorRepository.GetColor(colorId);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        // POST: Colors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] Color color)
        {
            if (id != color.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _colorRepository.SaveColor(color);
                return RedirectToAction(nameof(Index));
            }

            return View(color);
        }

        // GET: Colors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colorId = Convert.ToInt32(id);
            var color = await _colorRepository.GetColor(colorId);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // POST: Colors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var color = await _colorRepository.GetColor(id);
            await _colorRepository.DeleteColor(color);
            return RedirectToAction(nameof(Index));
        }
    }
}
