/* SCRIPT WRITTEN BY
 * Singleton.cs
 * Wayway Studio
 * Singleton
 * inherit Class
 * nothing require
 * 2021.02.15 */
public class Singleton<T> where T : class, new()
{
    private static T instance;

    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            lock (_lock)
            {
                if (instance == null)
                {
                    instance = new T();
                }

                return instance;
            }
        }
    }

    protected Singleton() { }
}
