using QFramework;

public class Main : ISingleton
{
    public static Main instance
    {
        get { return SingletonProperty<Main>.Instance; }
    }

    private Main() { }
    private static int Turn;

    public void OnSingletonInit()
    {
        Turn = 0;
    }

    public int GetTurn() { return Turn; }
    public void AddOne() { Turn++; }
    public void Dispose() { SingletonProperty<Main>.Instance.Dispose(); }
}
