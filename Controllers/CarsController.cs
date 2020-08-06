using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Nomad_MVC.Data.Interfaces;
using Nomad_MVC.Models;
using Nomad_MVC.PageHelpers;
using X.PagedList;

namespace Nomad_MVC.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMakeRepository _makeRepository;
        private readonly IModelRepository _modelRepository;
        private readonly IColorRepository _colorRepository;
        private readonly long _fileSizeLimit;
        private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".png" };

        public CarsController(ICarRepository carRepository, ICategoryRepository categoryRepository, IMakeRepository makeRepository, IModelRepository modelRepository, IColorRepository colorRepository, IConfiguration config)
        {
            _carRepository = carRepository;
            _categoryRepository = categoryRepository;
            _makeRepository = makeRepository;
            _modelRepository = modelRepository;
            _colorRepository = colorRepository;
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
        }

        // GET: Cars
        public async Task<IActionResult> Index(string id, int? page)
        {
            var searchString = id;
            ViewBag.SearchString = searchString;

            if (!string.IsNullOrEmpty(searchString))
            {
                page = 1;
            }

            var carList = await _carRepository.GetCars();

            if (!string.IsNullOrEmpty(searchString))
            {
                carList = carList.Where(c =>
                    c.Category.Description.Contains(searchString) || c.Model.Description.Contains(searchString) ||
                    c.Color.Description.Contains(searchString) || c.Make.Description.Contains(searchString)).ToList();
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(carList.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult Clear()
        {
            RouteValueDictionary routeInfo = new RouteValueDictionary();
            routeInfo.Add("id", "");
            return RedirectToAction("Index", routeInfo);
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carId = Convert.ToInt32(id);
            var car = await _carRepository.GetCar(carId);

            return View(car);
        }

        [Authorize]
        // GET: Cars/Create
        public IActionResult Create()
        {
            var car = new Car()
            {
                Categories = Helpers.PopulateCategoryNameSL(_categoryRepository.GetCategories().Result.ToList()),
                Makes = Helpers.PopulateMakeSL(_makeRepository.GetMakes().Result.ToList()),
                Models = Helpers.PopulateModelSL(_modelRepository.GetModels().Result.ToList()),
                Colors = Helpers.PopulateColorSL(_colorRepository.GetColors().Result.ToList())
            };

            return View(car);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Category,Make,Model,Year,Color,Price,FormFile")] Car car)
        {
            var carImage =
                await FileHelpers.ProcessFormFile<Car>(
                    car.FormFile, ModelState, _permittedExtensions, _fileSizeLimit);
            car.Image = carImage.Image;
            ModelState.ClearValidationState("Image");
            TryValidateModel(car);

            if (ModelState.IsValid)
            {
                await _carRepository.SaveCar(car);
                return RedirectToAction(nameof(Index));
            }

            car.Categories = Helpers.PopulateCategoryNameSL(_categoryRepository.GetCategories().Result.ToList());
            car.Makes = Helpers.PopulateMakeSL(_makeRepository.GetMakes().Result.ToList());
            car.Models = Helpers.PopulateModelSL(_modelRepository.GetModels().Result.ToList());
            car.Colors = Helpers.PopulateColorSL(_colorRepository.GetColors().Result.ToList());


            return View(car);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carId = Convert.ToInt32(id);

            var car = await _carRepository.GetCar(carId);

            if (car == null)
            {
                return NotFound();
            }

            car.Categories = Helpers.PopulateCategoryNameSL(_categoryRepository.GetCategories().Result.ToList(), car.Category.Id);
            car.Makes = Helpers.PopulateMakeSL(_makeRepository.GetMakes().Result.ToList(), car.Make.Id);
            car.Models = Helpers.PopulateModelSL(_modelRepository.GetModels().Result.ToList(), car.Model.Id);
            car.Colors = Helpers.PopulateColorSL(_colorRepository.GetColors().Result.ToList(), car.Color.Id);

            return View(car);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category,Make,Model,Year,Color,Price,FormFile")]
            Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (car.FormFile != null)
            {
                var carImage =
                    await FileHelpers.ProcessFormFile<Car>(
                        car.FormFile, ModelState, _permittedExtensions, _fileSizeLimit);
                car.Image = carImage.Image;
            }
            else
            {
                var oldCar = await _carRepository.GetCar(car.Id);
                car.Image = oldCar.Image;
                car.FileName = oldCar.FileName;
            }

            if (ModelState.IsValid)
            {

                await _carRepository.SaveCar(car);
                return RedirectToAction(nameof(Index));
            }

            return View(car);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carId = Convert.ToInt32(id);
            var car = await _carRepository.GetCar(carId);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carId = Convert.ToInt32(id);
            var car = await _carRepository.GetCar(carId);
            await _carRepository.DeleteCar(car);
            return RedirectToAction(nameof(Index));
        }
    }
}
