using UnityEngine;





public class TetrisStats2 : MonoBehaviour
{
    public GameObject cubePrefab;
    public int boardSize = 8; // 自定义棋盘大小
    private Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow };
    private string[] colorNames = { "红色", "绿色", "蓝色", "黄色" };
    private Tetromino[] tetrominoes;
    private string[] tetrominoNames = { "L型", "J型", "O型", "I型", "T型", "S型", "Z型" };
    private int[,] board;
    private int[] tetrominoCounts;

    public Transform boardContainer;

    //private void Start()
    //{
    //    float startTime = Time.realtimeSinceStartup;

    //    // 初始化棋盘和计数器
    //    board = new int[boardSize, boardSize];
    //    tetrominoCounts = new int[colors.Length * tetrominoNames.Length];

    //    // 初始化方块图形数组
    //    tetrominoes = new Tetromino[]
    //    {
    //        new Tetromino("L型",new int[][] // L型方块
    //        {
    //            new int[] { 1, 0 },
    //            new int[] { 1, 0 },
    //            new int[] { 1, 1 }
    //        },0),

    //        new Tetromino("J型",new int[][] // J型方块
    //        {
    //            new int[] { 1, 0, 0 },
    //            new int[] { 1, 1, 1 }
    //            },1),

    //        new Tetromino("O型",new int[][] // O型方块
    //        {
    //            new int[] { 1, 1 },
    //            new int[] { 1, 1 }
    //        },2),

    //        new Tetromino("I型",new int[][] // I型方块
    //        {
    //            new int[] { 1, 1, 1, 1 }
    //        },3),

    //        new Tetromino("T型",new int[][] // T型方块
    //        {
    //            new int[] { 1, 0 },
    //            new int[] { 1, 1 },
    //            new int[] { 1, 0 }
    //        },4),

    //        new Tetromino("S型",new int[][] // S型方块
    //        {
    //            new int[] { 1, 0 },
    //            new int[] { 1, 1 },
    //            new int[] { 0, 1 }
    //        },5),

    //        new Tetromino("Z型",new int[][] // Z型方块
    //        {
    //            new int[] { 1, 1, 0 },
    //            new int[] { 0, 1, 1 }
    //        },6),
    //    };




    //    // 生成棋盘颜色
    //    GenerateBoardColors();

    //    // 统计方块图形个数
    //    CountTetrominoes();

    //    // 输出统计结果
    //    PrintTetrominoCounts();

    //    // 输出程序执行时间


    //    float endTime = Time.realtimeSinceStartup;
    //    float elapsedTime = (endTime - startTime) * 1000f;
    //    Debug.Log("用时：" + elapsedTime.ToString("F2") + " ms");
    //}

    public void OnButtonClick()
    {
        float startTime = Time.realtimeSinceStartup;

        // 初始化棋盘和计数器
        board = new int[boardSize, boardSize];
        tetrominoCounts = new int[colors.Length * tetrominoNames.Length];

        // 初始化方块图形数组
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




        // 生成棋盘颜色
        GenerateBoardColors();

        // 统计方块图形个数
        CountTetrominoes();

        // 输出统计结果
        PrintTetrominoCounts();

        // 输出程序执行时间


        float endTime = Time.realtimeSinceStartup;
        float elapsedTime = (endTime - startTime) * 1000f;
        Debug.Log("用时：" + elapsedTime.ToString("F2") + " ms");
    }

    public void GenerateBoardColors()
    {
        float xOffset = 0.5f * (boardSize - 1);
        float yOffset = 0.5f * (boardSize - 1);

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                int randomColorIndex = Random.Range(0, colors.Length);
                Color randomColor = colors[randomColorIndex];
                board[i, j] = randomColorIndex;
                GameObject cube = Instantiate(cubePrefab, new Vector3(-j + xOffset, -i + yOffset, 0), Quaternion.identity);
                cube.GetComponent<SpriteRenderer>().color = randomColor;
                cube.transform.SetParent(boardContainer);
            }
        }

        boardContainer.transform.Rotate(new Vector3(0, 0, 90));
    }

    private int[][,] GenerateRotatedBoard(int[,] originalBoard)
    {
        int size = originalBoard.GetLength(0);
        int[][,] rotatedBoard = new int[4][,];

        // Store the original board as the first rotation
        rotatedBoard[0] = originalBoard;

        // Generate the three remaining rotations
        for (int i = 1; i < 4; i++)
        {
            int[,] rotated = new int[size, size];
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    switch (i)
                    {
                        case 1:
                            rotated[row, col] = originalBoard[col, size - row - 1];
                            break;
                        case 2:
                            rotated[row, col] = originalBoard[size - row - 1, size - col - 1];
                            break;
                        case 3:
                            rotated[row, col] = originalBoard[size - col - 1, row];
                            break;
                    }
                }
            }
            rotatedBoard[i] = rotated;
        }

        return rotatedBoard;
    }

    public void CountTetrominoes()
    {
        foreach (Tetromino tetromino in tetrominoes)
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    foreach (int[,] board in GenerateRotatedBoard(board))
                    {
                        if (CheckTetromino(board, tetromino, i, j))
                        {
                            int colorIndex = board[i, j];
                            int tetrominoCountIndex = tetromino.Index + colorIndex * tetrominoes.Length;
                            tetrominoCounts[tetrominoCountIndex]++;
                        }
                    }

                }
            }
        }
        //O型重复计算了四遍
        for (int i = 2; i < tetrominoCounts.Length; i += 7)
        {
            tetrominoCounts[i] /= 4;
        }
        //I型重复计算了两遍
        for (int i = 3; i < tetrominoCounts.Length; i += 7)
        {
            tetrominoCounts[i] /= 2;
        }
        //S型重复计算了两遍
        for (int i = 5; i < tetrominoCounts.Length; i += 7)
        {
            tetrominoCounts[i] /= 2;
        }
        //Z型重复计算了两遍
        for (int i = 6; i < tetrominoCounts.Length; i += 7)
        {
            tetrominoCounts[i] /= 2;
        }
    }

    private bool CheckTetromino(int[,] board, Tetromino tetromino, int startX, int startY)
    {
        int tetrominoWidth = tetromino.Shape[0].Length;
        int tetrominoHeight = tetromino.Shape.Length;
        if (startX + tetrominoWidth > boardSize || startY + tetrominoHeight > boardSize)
        {
            return false; // 方块超出棋盘范围，不匹配
        }

        int startColor = board[startX, startY];

        for (int i = 0; i < tetrominoHeight; i++)
        {
            for (int j = 0; j < tetrominoWidth; j++)
            {
                if (tetromino.Shape[i][j] == 1)
                {
                    int boardX = startX + j;
                    int boardY = startY + i;

                    if (board[boardX, boardY] != startColor)
                    {
                        return false; // 方块位置不匹配
                    }
                }
            }
        }
        //SWDebug.Log("x" + (startX + 1) + "y" + (startY + 1));
        return true; // 方块匹配成功
    }

    public void PrintTetrominoCounts()
    {
        for (int i = 0; i < colors.Length; i++)
        {
            for (int j = 0; j < tetrominoNames.Length; j++)
            {
                int tetrominoIndex = i * tetrominoNames.Length + j;
                Debug.Log("颜色 " + colorNames[i] + " 的方块图形 " + tetrominoNames[j] + " 出现 " + tetrominoCounts[tetrominoIndex] + " 次");
            }
        }
    }
}
