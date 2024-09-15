public class Database
{
    private static Database _instance;
    private static readonly object _lock = new object();

    private Database() { /* Private constructor to prevent instantiation */ }

    public static Database GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new Database();
                }
            }
        }
        return _instance;
    }
}