using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using DG.Tweening;
using TMPro;

public class TetrisStats : MonoBehaviour
{
    public GameObject cubePrefab;
    public int boardSize = 8; // 自定义棋盘大小
    public float blockSize = 1f;//格子大小

    private Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow };
    private string[] colorNames = { "红色", "绿色" ,"蓝色", "黄色" };
    private string[] tetrominoNames = { "L型", "J型", "O型", "I型", "T型", "S型", "Z型" };
    [ShowInInspector]
    public int[,] board;
    [ShowInInspector]
    public int[,] board1;//置为-1之后的棋盘数据
    [ShowInInspector]
    private GameObject[,] cubeMatrix;

    
    private int[] tetrominoCounts;

    public Transform boardContainer;

    public Button randomBtn;

    private float startTime;

    //要消除的格子（有重复计算的格子）
    List<Vector2Int> matchedBlocks;
    //排好序后要消除的格子（按y坐标倒叙排列，无重复）
    List<Vector2Int> sortedBlocks;

    //在Inspector中显示的要消除的格子
    [ShowInInspector]
    List<Vector2Int> matchedBlocksInInspector;

    [ShowInInspector]
    private Vector2Int? selectedBlock1;
    [ShowInInspector]
    private Vector2Int? selectedBlock2;

    float xOffset;
    float yOffset;

    //玩家当局战斗的背包（卡组）
    [ShowInInspector]
    private Card[] bag;

    //是否可以点击交换
    [ShowInInspector]
    private bool canSwap = false;

    //当前回合数
    [ShowInInspector]
    public TextMeshProUGUI turn;

    //当前AttackBuff
    public TextMeshProUGUI attackBuff;

    //当前DefendBuff
    public TextMeshProUGUI defendBuff;

    //玩家血条
    public Slider playerHP;

    //怪物血条
    public Slider enemyHP;

    private void Awake()
    {
<<<<<<< Updated upstream
        Main.instance.OnSingletonInit();
        PlayerState.instance.OnSingletonInit();
        playerHP.value = 1;
        EnemyState.instance.OnSingletonInit();
        enemyHP.value = 1;
        
=======
        BattleInitiate();
        boardContainer = GameObject.Find("BoardContainer").transform;
>>>>>>> Stashed changes
    }

    public void Start()
    {
        InitBag();
        turn.text = Main.instance.GetTurn().ToString();

        board = new int[boardSize, boardSize];
        cubeMatrix = new GameObject[boardSize, boardSize];
        InitializeBoard();
        
        
        board1 = new int[boardSize, boardSize];

        matchedBlocks = new List<Vector2Int>();

        randomBtn.onClick.AddListener(RandomColor);


        xOffset = 0.5f * (boardSize - 1);
        yOffset = 0.5f * (boardSize - 1);
    }

    public void InitBag() {
        bag = PlayerState.instance.GetCards();
    }

    void Update()
    {
        if (canSwap)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SwitchBlock();
            }
        }


        attackBuff.text = PlayerState.instance.GetAttackBuff().ToString();
        defendBuff.text = PlayerState.instance.GetDefenceBuffLayer().ToString();

        playerHP.value = (float)PlayerState.instance.GetHP() / PlayerState.instance.GetMaxHP();
        enemyHP.value = (float)EnemyState.instance.GetHP() / EnemyState.instance.GetMaxHP();

        if (EnemyState.instance.GetHP() <= 0) PanelManager.Open<RewardPanel>();
    }

    public void OnButtonClick()
    {
        startTime = Time.realtimeSinceStartup;

        // 初始化棋盘和计数器
        tetrominoCounts = new int[colors.Length * tetrominoNames.Length];

        // 生成棋盘颜色
        GenerateBoardColors();

        CountTetrominoes();

        if (matchedBlocks.Count == 0)
        {
            canSwap = true;
        }
        else
        {
            StartCoroutine(Loop());
        }
    }



    public IEnumerator<WaitForSeconds> Loop()
    {
        while (matchedBlocks.Count > 0)
        {
            yield return new WaitForSeconds(0.25f);
            UpdateBoardWithMatches(matchedBlocks);
            matchedBlocks.Clear();
            ChangeColor();
            yield return new WaitForSeconds(1);
            CountTetrominoes();
        }

        canSwap = true;
    }

    public void DestroyMatchedBlocks()
    {
        foreach(Vector2Int vector in matchedBlocks)
        {
            Destroy(cubeMatrix[vector.x, vector.y]);
        }
    }

    public void GenerateBoardColors()
    {

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                if (board[i, j] == -1) // 已消除的格子
                {
                    int randomColorIndex = ColorRandom();
                    Color randomColor = colors[randomColorIndex];
                    board[i, j] = randomColorIndex;
                    GameObject cube = Instantiate(cubePrefab, new Vector3(-boardSize + i + xOffset + 1, -j + yOffset, 0), Quaternion.identity);
                    cube.GetComponent<SpriteRenderer>().color = randomColor;
                    cube.transform.SetParent(boardContainer);
                    cubeMatrix[i, j] = cube;
                }
            }
        }
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
        //Debug.Log(bag==null);
        //只检测背包
        foreach (Card card in bag)
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        if (CheckTetromino(rotatedBoard[k], card.Tetromino, i, j))
                        {
                            int colorIndex = rotatedBoard[k][i, j];
                            if (card.Color == colors[colorIndex])
                            {
                                //交换-1的格子会导致tetrominoCountIndex为负数
                                int tetrominoCountIndex = card.Tetromino.Index + colorIndex * Main.instance.GeTetLen();
                                Debug.Log(tetrominoCounts.Length);
                                Debug.Log(tetrominoCountIndex);
                                tetrominoCounts[tetrominoCountIndex]++;
                                card.UseCard();

                                // 将匹配成功的位置转换成原始未旋转的坐标
                                int originalX = i;
                                int originalY = j;
                                ConvertToOriginalCoordinates(originalX, originalY, k, out int originalXUnrotated, out int originalYUnrotated);


                                // 将匹配成功的格子位置和形状中的格子位置都记录下来
                                matchedBlocks.Add(new Vector2Int(originalXUnrotated, originalYUnrotated));
                                RecordShapeBlocks(originalX, originalY, card.Tetromino.Shape, matchedBlocks, k);
                            }

                        }

                    }
                }
            }
        }
        ////检测所有类型
        //foreach (Tetromino tetromino in Main.instance.GetTetrominoes())
        //{
        //    for (int i = 0; i < boardSize; i++)
        //    {
        //        for (int j = 0; j < boardSize; j++)
        //        {
        //            for (int k = 0; k < 4; k++)
        //            {
        //                if (CheckTetromino(rotatedBoard[k], tetromino, i, j))
        //                {
        //                    int colorIndex = rotatedBoard[k][i, j];
        //                    int tetrominoCountIndex = tetromino.Index + colorIndex * Main.instance.GeTetLen();
        //                    tetrominoCounts[tetrominoCountIndex]++;
        //                    // 将匹配成功的位置转换成原始未旋转的坐标
        //                    int originalX = i;
        //                    int originalY = j;
        //                    ConvertToOriginalCoordinates(originalX, originalY, k, out int originalXUnrotated, out int originalYUnrotated);
        //                    // 将匹配成功的格子位置和形状中的格子位置都记录下来
        //                    matchedBlocks.Add(new Vector2Int(originalXUnrotated, originalYUnrotated));
        //                    RecordShapeBlocks(originalX, originalY, tetromino.Shape, matchedBlocks, k);
        //                }

        //            }
        //        }
        //    }
        //}
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
                if (board[i, fast] != -1) {
                    board[i, slow] = board[i, fast];

                    //动画效果
                    cubeMatrix[i, fast].transform.DOMove(cubeMatrix[i, slow].transform.position, (slow - fast) * 0.2f);
                    //更新cubeMatrix数据
                    cubeMatrix[i, slow] = cubeMatrix[i, fast];
                    slow--;
                }
                else
                {
                    Destroy(cubeMatrix[i, fast]);
                }
            }

            for (int now = 1; slow >= 0; slow--) {
                //now是现在应该在棋盘上方几格处生成一个新的
                int index = ColorRandom();
                GameObject cube = Instantiate(cubePrefab, new Vector3(-boardSize + i + xOffset + 1, -slow + yOffset + now, 0), Quaternion.identity,boardContainer);
                cubeMatrix[i, slow] = cube;
                cubeMatrix[i, slow].GetComponent<SpriteRenderer>().color = colors[index];
                board[i, slow] = index;

                //动画效果
                cubeMatrix[i, slow].transform.DOMoveY(-slow + yOffset, (slow + now) * 0.2f);
            }
        }
    }

    public void RandomColor() {
        for (int i = 0; i < boardSize; i++) {
            for (int j = 0; j < boardSize; j++) {
                int index = ColorRandom();
                cubeMatrix[i,j].GetComponent<SpriteRenderer>().color = colors[index];
                board[i, j] = index;
            }
        }
    }

    public int ColorRandom() {
        return UnityEngine.Random.Range(0, colors.Length);
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


    private void SwitchBlock() {
        // 获取鼠标点击位置的世界坐标
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 offset = boardContainer.position; // 棋盘的中心位置偏移
        Vector3 normalizedPosition = worldPosition - offset; // 将鼠标点击的世界坐标与棋盘中心位置进行偏移
        Vector3 scaledPosition = normalizedPosition / blockSize; // 根据每个块的大小进行缩放
                                                                 // 将世界坐标转换为棋盘格子坐标
        int row = Mathf.FloorToInt(scaledPosition.y); // 鼠标点击的行索引
        int column = Mathf.FloorToInt(scaledPosition.x); // 鼠标点击的列索引

        // 根据棋盘尺寸进行调整
        row += boardSize / 2;
        row = boardSize - row - 1;
        column += boardSize / 2;

        Vector2Int curPosition = new Vector2Int(row, column);
        // 检查索引是否在有效范围内
        if (IsValidBlock(curPosition))
        {
            if (selectedBlock1 == null)
            {
                selectedBlock1 = curPosition;
            }
            else if (selectedBlock1.Value == curPosition)
            {
                selectedBlock1 = null;
            }
            else if (selectedBlock2 == null)
            {
                selectedBlock2 = curPosition;
                // 执行块交换逻辑
                SwapBlocks(selectedBlock1.Value, selectedBlock2.Value);
                // 重置选定的块
                selectedBlock1 = null;
                selectedBlock2 = null;
                canSwap = false;

                // 统计方块图形个数
                CountTetrominoes();

                // 输出统计结果
                //PrintTetrominoCounts();

                if (matchedBlocks.Count == 0)
                {
                    canSwap = true;
                }
                else
                {
                    StartCoroutine(Loop());
                }
                Main.instance.AddOne();
                turn.text = Main.instance.GetTurn().ToString();

                if (Main.instance.GetTurn() % 3 == 0 && Main.instance.GetTurn() != 0)
                {
                    Hurt();
                }
                //Debug.Log(selectedBlock1);
                //Debug.Log(selectedBlock2);
            }
        }
    }

    private bool IsValidBlock(Vector2Int block)
    {
        int x = block.x;
        int y = block.y;

        return x >= 0 && x < boardSize && y >= 0 && y < boardSize;
    }

    private void SwapBlocks(Vector2Int block1, Vector2Int block2)
    {
        int temp = board[block1.y, block1.x];
        board[block1.y, block1.x] = board[block2.y, block2.x];
        board[block2.y, block2.x] = temp;

        //动画效果
        cubeMatrix[block1.y, block1.x].transform.DOMove(cubeMatrix[block2.y, block2.x].transform.position, 0.2f);
        cubeMatrix[block2.y, block2.x].transform.DOMove(cubeMatrix[block1.y, block1.x].transform.position, 0.2f);

        //更新cubeMatrix数据
        GameObject temp1 = cubeMatrix[block1.y, block1.x];
        cubeMatrix[block1.y, block1.x] = cubeMatrix[block2.y, block2.x];
        cubeMatrix[block2.y, block2.x] = temp1;

        

        
        //// 输出程序执行时间


        //float endTime = Time.realtimeSinceStartup;
        //float elapsedTime = (endTime - startTime) * 1000f;
        ////Debug.Log("用时：" + elapsedTime.ToString("F2") + " ms");

        //UpdateBoardWithMatches(matchedBlocks);
        ////ShowMatchedBlocksInInspector();
        //matchedBlocks.Clear();
    }

    public void Hurt()
    {
        PlayerState.instance.TakeDamge(1);

    }

    public void UpdatePlayerUI()
    {
        playerHP.value = PlayerState.instance.GetHP() / PlayerState.instance.GetMaxHP();
    }



}
