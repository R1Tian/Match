using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.UI;
public class TetrisStats : MonoBehaviour
{
    public GameObject cubePrefab;
    public int boardSize = 8; // 自定义棋盘大小
    private Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow };
    private string[] colorNames = { "红色", "绿色" ,"蓝色", "黄色" };
    private Tetromino[] tetrominoes;
    private string[] tetrominoNames = { "L型", "J型", "O型", "I型", "T型", "S型", "Z型" };
    [ShowInInspector]
    public int[,] board;
    [ShowInInspector]
    public int[,] board1;//置为-1之后的棋盘数据
    private GameObject[,] cubeMatrix;

    
    private int[] tetrominoCounts;

    public Transform boardContainer;

    public Button deleteBtn;
    public Button randomBtn;

    //要消除的格子（有重复计算的格子）
    List<Vector2Int> matchedBlocks;
    //排好序后要消除的格子（按y坐标倒叙排列，无重复）
    List<Vector2Int> sortedBlocks;

    //在Inspector中显示的要消除的格子
    [ShowInInspector]
    List<Vector2Int> matchedBlocksInInspector;

    [ShowInInspector]
    private Vector2Int selectedBlock1;
    [ShowInInspector]
    private Vector2Int selectedBlock2;

    public void Start()
    {
        board = new int[boardSize, boardSize];
        cubeMatrix = new GameObject[boardSize, boardSize];
        InitializeBoard();

        board1 = new int[boardSize, boardSize];

        matchedBlocks = new List<Vector2Int>();

        deleteBtn.onClick.AddListener(ChangeColor);
        randomBtn.onClick.AddListener(RandomColor);

    }


    public void OnButtonClick()
    {
        float startTime = Time.realtimeSinceStartup;

        // 初始化棋盘和计数器
        //board = new int[boardSize, boardSize];
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

        UpdateBoardWithMatches(matchedBlocks);
        ShowMatchedBlocksInInspector();
        matchedBlocks.Clear();
    }

    public void GenerateBoardColors()
    {
        float xOffset = 0.5f * (boardSize - 1);
        float yOffset = 0.5f * (boardSize - 1);

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                if (board[i, j] == -1) // 已消除的格子
                {
                    int randomColorIndex = Random.Range(0, colors.Length);
                    Color randomColor = colors[randomColorIndex];
                    board[i, j] = randomColorIndex;
                    GameObject cube = Instantiate(cubePrefab, new Vector3(-j + xOffset, -i + yOffset, 0), Quaternion.identity);
                    cube.GetComponent<SpriteRenderer>().color = randomColor;
                    cube.transform.SetParent(boardContainer);
                    cubeMatrix[i, j] = cube;
                }
                //else // 未消除的格子
                //{
                //    Color color = colors[board[i, j]];
                //    GameObject cube = Instantiate(cubePrefab, new Vector3(-j + xOffset, -i + yOffset, 0), Quaternion.identity);
                //    cube.GetComponent<SpriteRenderer>().color = color;
                //    cube.transform.SetParent(boardContainer);
                //}
            }
        }

        boardContainer.transform.eulerAngles = new Vector3(0, 0, 90);
        //boardContainer.transform.Rotate(new Vector3(0, 0, 90));
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
        int[][,] rotatedBoard = GenerateRotatedBoard(board);
        foreach (Tetromino tetromino in tetrominoes)
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {

                        //List<Vector2Int> matchedBlocks = new List<Vector2Int>(); // 使用 List 替代 HashSet

                        if (CheckTetromino(rotatedBoard[k], tetromino, i, j))
                        {
                            int colorIndex = rotatedBoard[k][i, j];
                            int tetrominoCountIndex = tetromino.Index + colorIndex * tetrominoes.Length;
                            tetrominoCounts[tetrominoCountIndex]++;

                            // 将匹配成功的位置转换成原始未旋转的坐标
                            int originalX = i;
                            int originalY = j;
                            ConvertToOriginalCoordinates(originalX, originalY, k, out int originalXUnrotated, out int originalYUnrotated);


                            // 将匹配成功的格子位置和形状中的格子位置都记录下来
                            matchedBlocks.Add(new Vector2Int(originalXUnrotated, originalYUnrotated));
                            RecordShapeBlocks(originalX, originalY, tetromino.Shape, matchedBlocks, k);
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

    private void RecordShapeBlocks(int startX, int startY, int[][] shape, List<Vector2Int> blocks, int rotationIndex)
    {
        int shapeWidth = shape[0].Length;
        int shapeHeight = shape.Length;

        for (int i = 0; i < shapeHeight; i++)
        {
            for (int j = 0; j < shapeWidth; j++)
            {
                if (startX + shapeWidth > boardSize || startY + shapeHeight > boardSize)
                {
                    continue; // 方块超出棋盘范围，不匹配
                }
                if (shape[i][j] == 1)
                {
                    int rotatedX = startX + j;
                    int rotatedY = startY + i;

                    int unrotatedX, unrotatedY;
                    ConvertToOriginalCoordinates(rotatedX, rotatedY, rotationIndex, out unrotatedX, out unrotatedY);

                    blocks.Add(new Vector2Int(unrotatedX, unrotatedY));
                }
            }
        }
    }

    private void ConvertToOriginalCoordinates(int rotatedX, int rotatedY, int rotationIndex, out int unrotatedX, out int unrotatedY)
    {
        switch (rotationIndex)
        {
            case 0:
                unrotatedX = rotatedX;
                unrotatedY = rotatedY;
                break;
            case 1:
                unrotatedX = rotatedY;
                unrotatedY = boardSize - rotatedX - 1;
                break;
            case 2:
                unrotatedX = boardSize - rotatedX - 1;
                unrotatedY = boardSize - rotatedY - 1;
                break;
            case 3:
                unrotatedX = boardSize - rotatedY - 1;
                unrotatedY = rotatedX;
                break;
            default:
                unrotatedX = 0;
                unrotatedY = 0;
                break;
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


    private void UpdateBoardWithMatches(List<Vector2Int> matchedBlocks)
    {

        // 使用 HashSet 存储唯一的格子位置
        HashSet<Vector2Int> uniqueBlocks = new HashSet<Vector2Int>(matchedBlocks);

        // 将匹配成功的格子位置按照纵坐标进行排序，从上到下遍历
        sortedBlocks = new List<Vector2Int>(uniqueBlocks);
        sortedBlocks.Sort((a, b) => {
            return b.y - a.y;
        });

        foreach (var item in sortedBlocks)
        {
            board[item.x, item.y] = -1;
        }

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                board1[i, j] = board[i, j];
            }
        }
    }


    public void ChangeColor()
    {
        for (int i = 0; i < boardSize; i++) {
            int slow = boardSize - 1;

            for (int fast = boardSize - 1; fast >= 0; fast--) {
                if (board1[i, fast] != -1) {
                    board1[i, slow] = board1[i, fast];
                    slow--;
                }
            }

            for (; slow >= 0; slow--) {
                int index = Random.Range(0, colors.Length);
                cubeMatrix[i, slow].GetComponent<SpriteRenderer>().color = colors[index];
                board1[i, slow] = index;
            }
        }
    }

    public void RandomColor() {
        foreach (var item in cubeMatrix)
        {
            item.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
        }
    }

    //初始化棋盘
    void InitializeBoard()
    {
        // 初始化棋盘颜色索引为无效值
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                board[i, j] = -1;
            }
        }
    }

    /// <summary>
    /// 在Inspector中显示要消除的格子
    /// </summary>
    void ShowMatchedBlocksInInspector()
    {
        //matchedBlocksInInspector = new List<Vector2Int>();

        //foreach (Vector2Int block in sortedBlocks)
        //{
        //    int transformedX = block.x;
        //    int transformedY = block.y;
        //    matchedBlocksInInspector.Add(new Vector2Int(transformedX, transformedY));
        //}
    }

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        // 获取鼠标点击位置的世界坐标
    //        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        // 将世界坐标转换为棋盘格子坐标
    //        Vector2Int clickedBlock = GetClickedBlock(mousePosition);

    //        // 检查点击的格子是否在有效范围内
    //        if (IsValidBlock(clickedBlock))
    //        {
    //            // 如果还没有选中第一个格子，则选中它
    //            if (selectedBlock1 == null)
    //            {
    //                selectedBlock1 = clickedBlock;
    //            }
    //            // 如果已经选中了一个格子，则选中第二个格子，并进行交换
    //            else
    //            {
    //                selectedBlock2 = clickedBlock;
    //                SwapBlocks(selectedBlock1, selectedBlock2);

    //                // 重置选中的格子
    //                selectedBlock1 = null;
    //                selectedBlock2 = null;
    //            }
    //        }
    //    }
    //}

    ///// <summary>
    ///// 将世界坐标转换为棋盘格子坐标：
    ///// </summary>
    ///// <param name="position">世界坐标</param>
    ///// <returns></returns>
    //private Vector2Int GetClickedBlock(Vector3 position)
    //{
    //    int x = Mathf.RoundToInt(position.x);
    //    int y = Mathf.RoundToInt(position.y);

    //    return new Vector2Int(x, y);
    //}

    //private bool IsValidBlock(Vector2Int block)
    //{
    //    int x = block.x;
    //    int y = block.y;

    //    return x >= 0 && x < boardSize && y >= 0 && y < boardSize;
    //}

    //private void SwapBlocks(Vector2Int block1, Vector2Int block2)
    //{
    //    int temp = board[block1.x, block1.y];
    //    board[block1.x, block1.y] = board[block2.x, block2.y];
    //    board[block2.x, block2.y] = temp;
    //}


}
