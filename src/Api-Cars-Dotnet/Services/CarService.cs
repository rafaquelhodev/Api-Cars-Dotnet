using System.Collections.Generic;
using Api_Cars_Dotnet.Models;
using MongoDB.Driver;

namespace Api_Cars_Dotnet.Services
{
    public class CarService
    {
        private readonly IMongoCollection<Car> _cars;
        private readonly IMongoDatabase _database;

        public CarService(IMongoDatabase database, ICarStoreDatabaseSettings settings)
        {
            _database = database;

            _cars = _database.GetCollection<Car>(settings.CarsCollectionName);
        }

        public List<Car> Get() =>
            _cars.Find(car => true).ToList();

        public Car Get(string id) =>
            _cars.Find<Car>(car => car.Id == id).FirstOrDefault();

        public Car Create(Car car)
        {
            _cars.InsertOne(car);
            return car;
        }

        public void Update(string id, Car carIn) =>
            _cars.ReplaceOne(car => car.Id == id, carIn);

        public void Remove(Car carIn) =>
            _cars.DeleteOne(car => car.Id == carIn.Id);

        public void Remove(string id) =>
            _cars.DeleteOne(car => car.Id == id);
    }
}