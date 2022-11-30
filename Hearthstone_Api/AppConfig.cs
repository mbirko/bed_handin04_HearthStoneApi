namespace Hearthstone_Api
{
    public class AppConfig
    {
        public HearthstoneDB HearthstoneDB { get; set; }
    }

    public class HearthstoneDB
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public Dictionary<string, string> CollectionNames { get; set; } = null!;
    }
}
