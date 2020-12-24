namespace Api_Cars_Dotnet.Models
{
    public class CarStoreDatabaseSettings : ICarStoreDatabaseSettings
    {
        public string CarsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ICarStoreDatabaseSettings
    {
        string CarsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}