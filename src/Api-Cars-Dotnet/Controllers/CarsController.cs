using System.Collections.Generic;
using Api_Cars_Dotnet.Models;
using Api_Cars_Dotnet.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api_Cars_Dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private CarService _carService;

        public CarsController(CarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public ActionResult<List<Car>> Get() =>
            _carService.Get();

        [HttpGet("{id:length(24)}", Name = "GetCar")]
        public ActionResult<Car> Get(string id)
        {
            var car = _carService.Get(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpPost]
        public ActionResult<Car> Create(Car car)
        {
            _carService.Create(car);

            return CreatedAtRoute("GetCar", new { id = car.Id.ToString() }, car);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Car carIn)
        {
            var car = _carService.Get(id);

            if (car == null)
            {
                return NotFound();
            }

            _carService.Update(id, carIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var car = _carService.Get(id);

            if (car == null)
            {
                return NotFound();
            }

            _carService.Remove(car.Id);

            return NoContent();
        }

        [HttpGet("find")]
        public IActionResult Find(string color)
        {
            //
            return NoContent();
        }
    }
}