using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Api_Cars_Dotnet.Models;
using MongoDB.Driver;

namespace Api_Cars_Dotnet.Services
{
    public class CarService
    {
        private readonly IRepository<Car> _repository;

        public CarService(IRepository<Car> repository) => _repository = repository;

        public List<Car> Get() =>
           _repository.GetAll();

        public Car Get(string id) =>
            _repository.GetById(id);

        public Car Create(Car car)
        {
            if (!car.IsValid())
                throw new ApplicationException("Invalid input");

            var InsertedCar = _repository.Insert(car);
            return InsertedCar;
        }

        public void Update(string id, Car carIn) =>
            _repository.Update(id, carIn);

        public void Remove(Car carIn) =>
            _repository.Delete(carIn);

        public void Remove(string id) =>
            _repository.Delete(id);
    }
}