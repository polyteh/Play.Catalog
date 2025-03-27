namespace Play.Catalog.Service.Settings
{
    public class MongoDbSettings
    {
        public static readonly string ConfigurationName = "MongoDbSettings";
        public string Host { get; init; }
        public int Port { get; init; }
        public string ConnectionString => $"mongodb://{Host}:{Port}";
    }
}