using System.Collections.Generic;
using Api_Cars_Dotnet.Models;

namespace Api_Cars_Dotnet.Services
{
    public interface ICarService
    {
        List<Car> Get();

        Car Get(string id);

        Car Create(Car car);

        void Update(string id, Car carIn);

        void Remove(Car carIn);

        void Remove(string id);
    }
}