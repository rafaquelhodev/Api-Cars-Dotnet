using System;
using System.Collections.Generic;
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


    }

}