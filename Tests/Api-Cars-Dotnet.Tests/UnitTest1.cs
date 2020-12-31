using System;
using Api_Cars_Dotnet.Models;
using Api_Cars_Dotnet.Services;
using Xunit;

namespace Api_Cars_Dotnet.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            ICarStoreDatabaseSettings settings = new CarStoreDatabaseSettings();

            var _carservice = new CarService(settings);

        }
    }
}
