using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nomad_MVC.Data.Interfaces;
using Nomad_MVC.Models;

namespace Nomad_MVC.Controllers
{
    [Authorize]
    public class MakesController : Controller
    {
        private readonly IMakeRepository _makeRepository;

        public MakesController(IMakeRepository makeRepository)
        {
            _makeRepository = makeRepository;
        }

        // GET: Makes
        public async Task<IActionResult> Index()
        {
            var makes = await _makeRepository.GetMakes();
            return View(makes);
        }

        // GET: Makes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var makeId = Convert.ToInt32(id);
            var make = await _makeRepository.GetMake(makeId);
            if (make == null)
            {
                return NotFound();
            }

            return View(make);
        }

        // GET: Makes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Makes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] Make make)
        {
            if (ModelState.IsValid)
            {
                await _makeRepository.SaveMake(make);
                return RedirectToAction(nameof(Index));
            }

            return View(make);
        }

        // GET: Makes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var makeId = Convert.ToInt32(id);
            var make = await _makeRepository.GetMake(makeId);
            if (make == null)
            {
                return NotFound();
            }

            return View(make);
        }

        // POST: Makes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] Make make)
        {
            if (id != make.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _makeRepository.SaveMake(make);
                return RedirectToAction(nameof(Index));
            }

            return View(make);
        }

        // GET: Makes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var makeId = Convert.ToInt32(id);
            var make = await _makeRepository.GetMake(makeId);
            if (make == null)
            {
                return NotFound();
            }

            return View(make);
        }

        // POST: Makes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var makeId = Convert.ToInt32(id);
            var make = await _makeRepository.GetMake(makeId);
            await _makeRepository.DeleteMake(make);
            return RedirectToAction(nameof(Index));
        }
    }
}
