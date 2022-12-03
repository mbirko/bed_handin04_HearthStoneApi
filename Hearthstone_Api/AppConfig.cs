namespace Hearthstone_Api
{
    public class AppConfig
    {
        public HearthstoneDb? HearthstoneDb { get; set; }
    }

    public class HearthstoneDb
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public Dictionary<string, string> CollectionNames { get; set; } = null!;
    }
}
