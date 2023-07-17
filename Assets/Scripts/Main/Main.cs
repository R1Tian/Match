using QFramework;

public class Main : ISingleton
{
    public static Main instance
    {
        get { return SingletonProperty<Main>.Instance; }
    }

    private Main() { }
    private static int Turn;
    private static Tetromino[] tetrominoes;

    public void OnSingletonInit()
    {
        Turn = 0;
        InitShape();
    }

    private void InitShape()
    {
        tetrominoes = new Tetromino[]
        {
            new Tetromino("L型",new int[][] // L型方块
            {
                new int[] { 1, 0 },
                new int[] { 1, 0 },
                new int[] { 1, 1 }
            },0),

            new Tetromino("J型",new int[][] // J型方块
            {
                new int[] { 1, 0, 0 },
                new int[] { 1, 1, 1 }
                },1),

            new Tetromino("O型",new int[][] // O型方块
            {
                new int[] { 1, 1 },
                new int[] { 1, 1 }
            },2),

            new Tetromino("I型",new int[][] // I型方块
            {
                new int[] { 1, 1, 1, 1 }
            },3),

            new Tetromino("T型",new int[][] // T型方块
            {
                new int[] { 1, 0 },
                new int[] { 1, 1 },
                new int[] { 1, 0 }
            },4),

            new Tetromino("S型",new int[][] // S型方块
            {
                new int[] { 1, 0 },
                new int[] { 1, 1 },
                new int[] { 0, 1 }
            },5),

            new Tetromino("Z型",new int[][] // Z型方块
            {
                new int[] { 1, 1, 0 },
                new int[] { 0, 1, 1 }
            },6),
        };
    }

    public int GetTurn() { return Turn; }
    public void AddOne() { Turn++; }

    public int GeTetLen() { return tetrominoes.Length; }
    public Tetromino GetTetShape(string name) {
        foreach (var item in tetrominoes)
        {
            if (name.Equals(item.Name)) return item;
        }

        return null;
    }

    public void Dispose() { SingletonProperty<Main>.Instance.Dispose(); }
}
