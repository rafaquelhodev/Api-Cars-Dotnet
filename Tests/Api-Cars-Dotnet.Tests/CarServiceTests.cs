using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Api_Cars_Dotnet.Models;
using Api_Cars_Dotnet.Services;
using AutoFixture;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace Api_Cars_Dotnet.Tests
{
    public class CarServiceTests
    {
        private Fixture _fixture = new Fixture();
        private readonly Mock<IDatabaseSettings> _dbSettingMock = new Mock<IDatabaseSettings>();
        private readonly Mock<IRepository<Car>> repositoryMock = new Mock<IRepository<Car>>();

        [Fact(DisplayName = "Create car in DB")]
        public void InsertOneShouldBeCalledOnceOnCreation()
        {
            var car = new Car
            {
                Age = 10
            };

            repositoryMock.Reset();

            repositoryMock.Setup(x => x.Insert(It.IsAny<Car>())).Returns(car);

            // Act
            var carService = new CarService(repositoryMock.Object);
            Assert.NotNull(carService);

            carService.Create(car);

            repositoryMock.Verify(x => x.Insert(It.IsAny<Car>()), Times.Once());
        }

        [Fact(DisplayName = "Create invalid car")]
        public void InsertCarWithInvalidAgeOnCreation()
        {
            var car = new Car
            {
                Age = -10
            };

            repositoryMock.Reset();

            // Act
            var carService = new CarService(repositoryMock.Object);

            Assert.NotNull(carService);

            var invalidCar = new Car
            {
                Age = -10
            };

            Assert.Throws<ApplicationException>(() => carService.Create(invalidCar));

            repositoryMock.Verify(x => x.Insert(It.IsAny<Car>()), Times.Never());
        }

        [Fact(DisplayName = "Get car by Id")]
        public void GetCarById()
        {
            var car = new Car
            {
                Id = "teste",
                Age = 10,
                Price = 1000.0m,
                Brand = "tesla",
                Color = "black"
            };

            repositoryMock.Reset();

            repositoryMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(car);

            var carService = new CarService(repositoryMock.Object);
            Assert.NotNull(carService);

            var carFound = carService.Get(car.Id);

            Assert.Equal(car, carFound);

            repositoryMock.Verify(x => x.GetById(It.IsAny<string>()), Times.Once());
        }

        [Fact(DisplayName = "Get all")]
        public void GetAllCars()
        {
            var cars = _fixture.CreateMany<Car>(5).ToList();

            repositoryMock.Reset();

            repositoryMock.Setup(x => x.GetAll()).Returns(cars);

            // Act
            var carService = new CarService(repositoryMock.Object);
            Assert.NotNull(carService);

            var result = carService.Get();

            Assert.Equal(cars, result);
        }

        [Fact(DisplayName = "Get by Id")]
        public void GetById()
        {
            var car = new Car
            {
                Id = "1",
                Color = "black",
                Brand = "tesla"
            };

            repositoryMock.Reset();

            repositoryMock.Setup(x => x.GetById(car.Id)).Returns(car);

            // Act
            var carService = new CarService(repositoryMock.Object);
            Assert.NotNull(carService);

            var result = carService.Get(car.Id);

            Assert.Equal(car, result);
        }

        [Fact(DisplayName = "Update")]
        public void Update()
        {
            var car = new Car
            {
                Id = "1",
                Color = "black",
                Brand = "tesla"
            };

            repositoryMock.Reset();

            repositoryMock.Setup(x => x.Update(It.IsAny<string>(), It.IsAny<Car>())).Verifiable();

            // Act
            var carService = new CarService(repositoryMock.Object);
            Assert.NotNull(carService);

            carService.Update(car.Id, car);

            repositoryMock.Verify(x => x.Update(It.IsAny<string>(), It.IsAny<Car>()), Times.Once());
        }

        [Fact(DisplayName = "Remove with object as input")]
        public void RemoveByObjectInput()
        {
            var car = new Car
            {
                Id = "1",
                Color = "black",
                Brand = "tesla"
            };

            repositoryMock.Reset();

            repositoryMock.Setup(x => x.Delete(It.IsAny<Car>())).Verifiable();

            // Act
            var carService = new CarService(repositoryMock.Object);
            Assert.NotNull(carService);

            carService.Remove(car);

            repositoryMock.Verify(x => x.Delete(It.IsAny<Car>()), Times.Once());
        }

        [Fact(DisplayName = "Remove with Id as input")]
        public void RemoveById()
        {
            var car = new Car
            {
                Id = "1",
                Color = "black",
                Brand = "tesla"
            };

            repositoryMock.Reset();

            repositoryMock.Setup(x => x.Delete(It.IsAny<string>())).Verifiable();

            // Act
            var carService = new CarService(repositoryMock.Object);
            Assert.NotNull(carService);

            carService.Remove(car.Id);

            repositoryMock.Verify(x => x.Delete(It.IsAny<string>()), Times.Once());
        }


    }

}