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
        Turn = 1;
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
            },0,TetrominoType.LType),

            new Tetromino("J型",new int[][] // J型方块
            {
                new int[] { 1, 0, 0 },
                new int[] { 1, 1, 1 }
                },1,TetrominoType.JType),

            new Tetromino("O型",new int[][] // O型方块
            {
                new int[] { 1, 1 },
                new int[] { 1, 1 }
            },2,TetrominoType.OType),

            new Tetromino("I型",new int[][] // I型方块
            {
                new int[] { 1, 1, 1, 1 }
            },3,TetrominoType.IType),

            new Tetromino("T型",new int[][] // T型方块
            {
                new int[] { 1, 0 },
                new int[] { 1, 1 },
                new int[] { 1, 0 }
            },4,TetrominoType.TType),

            new Tetromino("S型",new int[][] // S型方块
            {
                new int[] { 1, 0 },
                new int[] { 1, 1 },
                new int[] { 0, 1 }
            },5,TetrominoType.SType),

            new Tetromino("Z型",new int[][] // Z型方块
            {
                new int[] { 1, 1, 0 },
                new int[] { 0, 1, 1 }
            },6,TetrominoType.ZType),
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

    /// <summary>
    /// 返回Tetromino的名称缩写，如T型返回T，L型与J型都返回LJ
    /// </summary>
    /// <param name="tetromino"></param>
    /// <returns></returns>
    public string GetTetAbbName(Tetromino tetromino)
    {
        string abb;
        switch (tetromino.TetrominoType)
        {
            case TetrominoType.JType:
                abb = "LJ";
                break;
            case TetrominoType.LType:
                abb = "LJ";
                break;
            case TetrominoType.SType:
                abb = "SZ";
                break;
            case TetrominoType.ZType:
                abb = "SZ";
                break;
            default:
                abb = tetromino.Name[0].ToString();
                break;
        }

        return abb;
    }
    public Tetromino GetTetType(TetrominoType tetrominoType)
    {
        foreach (var item in tetrominoes)
        {
            if (tetrominoType.Equals(item.TetrominoType)) return item;
        }

        return null;
    }

    public Tetromino[] GetTetrominoes()
    {
        return tetrominoes;
    }
    public void Dispose() { SingletonProperty<Main>.Instance.Dispose(); }
}
