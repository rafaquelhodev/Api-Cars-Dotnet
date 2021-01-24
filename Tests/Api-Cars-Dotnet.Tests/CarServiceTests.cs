using System;
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
        private readonly Mock<ICarStoreDatabaseSettings> _dbSettingMock = new Mock<ICarStoreDatabaseSettings>();

        [Fact(DisplayName = "Create car in DB")]
        public void InsertOneShouldBeCalledOnceOnCreation()
        {
            var collectionMock = new Mock<IMongoCollection<Car>>();
            var dbMock = new Mock<IMongoDatabase>();

            dbMock.Setup(x => x.GetCollection<Car>(It.IsAny<string>(), null)).Returns(collectionMock.Object);
            collectionMock.Setup(x => x.InsertOne(It.IsAny<Car>(), null, default)).Verifiable();

            _dbSettingMock.Setup(m => m.ConnectionString).Returns(_fixture.Create<string>);
            _dbSettingMock.Setup(m => m.DatabaseName).Returns(_fixture.Create<string>);

            // Act
            var carService = new CarService(dbMock.Object, _dbSettingMock.Object);
            Assert.NotNull(carService);

            carService.Create(new Car
            {
                Age = 10
            });

            collectionMock.Verify(x => x.InsertOne(It.IsAny<Car>(), null, default), Times.Once());
        }

        [Fact(DisplayName = "Create invalid car")]
        public void InsertCarWithInvalidAgeOnCreation()
        {
            var collectionMock = new Mock<IMongoCollection<Car>>();
            var dbMock = new Mock<IMongoDatabase>();

            dbMock.Setup(x => x.GetCollection<Car>(It.IsAny<string>(), null)).Returns(collectionMock.Object);
            collectionMock.Setup(x => x.InsertOne(It.IsAny<Car>(), null, default)).Verifiable();

            _dbSettingMock.Setup(m => m.ConnectionString).Returns(_fixture.Create<string>);
            _dbSettingMock.Setup(m => m.DatabaseName).Returns(_fixture.Create<string>);

            // Act
            var carService = new CarService(dbMock.Object, _dbSettingMock.Object);
            Assert.NotNull(carService);

            var invalidCar = new Car
            {
                Age = -10
            };

            Assert.Throws<ApplicationException>(() => carService.Create(invalidCar));

            collectionMock.Verify(x => x.InsertOne(It.IsAny<Car>(), null, default), Times.Never());
        }
    }

}