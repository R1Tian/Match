using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading;
using DG.Tweening;
using TMPro;
using Cysharp.Threading.Tasks;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;

public enum CurCountsType
{
    //本回合计数
    CurRedLJ,
    CurRedO,
    CurRedI,
    CurRedT,
    CurRedSZ,
    CurGreenLJ,
    CurGreenO,
    CurGreenI,
    CurGreenT,
    CurGreenSZ,
    CurBlueLJ,
    CurBlueO,
    CurBlueI,
    CurBlueT,
    CurBlueSZ,
    CurYellowLJ,
    CurYellowO,
    CurYellowI,
    CurYellowT,
    CurYellowSZ,

    Cur,


};

public enum AllCountsType
{
    //本场战斗计数
    AllRedLJ,
    AllRedO,
    AllRedI,
    AllRedT,
    AllRedSZ,
    AllGreenLJ,
    AllGreenO,
    AllGreenI,
    AllGreenT,
    AllGreenSZ,
    AllBlueLJ,
    AllBlueO,
    AllBlueI,
    AllBlueT,
    AllBlueSZ,
    AllYellowLJ,
    AllYellowO,
    AllYellowI,
    AllYellowT,
    AllYellowSZ,

    All,


        
};

public class TetrisStats : MonoBehaviour
{
    public List<GameObject> cubePrefab = new List<GameObject>();
    
    public GameObject blankPrefab;// 选中框预制体
    GameObject blank;

    public GameObject VanishEffectPrefab;//消除特效预制体
    private List<GameObject> VanishEffects = new List<GameObject>();
    
    public int boardSize = 8; // 自定义棋盘大小
    public float blockSize = 1f;// 格子大小


    private Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow };
    private string[] colorNames = { "红色", "绿色" ,"蓝色", "黄色" };
    private string[] tetrominoNames = { "L型", "J型", "O型", "I型", "T型", "S型", "Z型" };
    [ShowInInspector]
    public int[,] board;
    [ShowInInspector]
    public int[,] board1;//置为-1之后的棋盘数据

    [ShowInInspector]
    public List<int[,]> orginBoard;
    [ShowInInspector]
    public List<int[,]> allBoard;

    [ShowInInspector]
    public GameObject tipCube;

    [ShowInInspector]
    private GameObject[,] cubeMatrix;

    
    private int[] tetrominoCounts;

    public Transform boardContainer;

    public Button randomBtn;

    private float startTime;

    //是否换回来
    public bool isSwaped = true;

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
    private List<CardObject> bag;

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
    
    //当回合消除计数
    [ShowInInspector]
    public Dictionary<CurCountsType, int> curCounts;
    //本场战斗消除计数
    [ShowInInspector]
    public Dictionary<AllCountsType, int> allCounts;

    //是否正在生成规则棋盘
    [ShowInInspector]
    public bool isGeneratingRuledBoard = false;
    
    [ShowInInspector]
    public float fallTime = 0.05f;
    
    //是否生成过奖励弹窗
    private bool isRewarded = false;
    
    //取消令牌
    static CancellationTokenSource cts = new CancellationTokenSource();
    CancellationToken cancellationToken = cts.Token;

    private void ResetToken()
    {
        cts.Cancel();
        cts.Dispose();
        cts = new CancellationTokenSource();
        cancellationToken = cts.Token;
    }
    
    
    private void OnEnable()
    {
        ResetToken();
        BattleInitiate();
        boardContainer = GameObject.Find("BoardContainer").transform;

        InitCurCounts();
        InitAllCounts();
        
        InitBag();
        turn.text = Main.instance.GetTurn().ToString();

        board = new int[boardSize, boardSize];
        cubeMatrix = new GameObject[boardSize, boardSize];
        InitializeBoard();
        
        
        board1 = new int[boardSize, boardSize];

        matchedBlocks = new List<Vector2Int>();

        randomBtn.onClick.AddListener(GenerateRuledBoard);


        xOffset = 0.5f * (boardSize - 1);
        yOffset = 0.5f * (boardSize - 1);

        isRewarded = false;
    }

    private void OnDestroy()
    {
        //取消所有UniTask
        cts.Cancel();
        
        foreach (GameObject cube in cubeMatrix)
        {
            Destroy(cube);
        }

        if (blank != null)
        {
            Destroy(blank);
        }
        
        Destroy(GameObject.Find("BoardBG(Clone)"));
    }
    // public void Start()
    // {
    //     InitBag();
    //     turn.text = Main.instance.GetTurn().ToString();
    //
    //     board = new int[boardSize, boardSize];
    //     cubeMatrix = new GameObject[boardSize, boardSize];
    //     InitializeBoard();
    //     
    //     
    //     board1 = new int[boardSize, boardSize];
    //
    //     matchedBlocks = new List<Vector2Int>();
    //
    //     randomBtn.onClick.AddListener(GenerateRuledBoard);
    //
    //
    //     xOffset = 0.5f * (boardSize - 1);
    //     yOffset = 0.5f * (boardSize - 1);
    //     
    // }

    public void InitBag() {
        bag = PlayerState.instance.GetBattleCards();
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

        if (!isRewarded)
        {
            if (EnemyState.instance.GetHP() <= 0)
            {
                PlayerState.instance.AddBattleCount();
                PanelManager.Open<RewardPanel>("Reward");
                DOTween.KillAll();
                isRewarded = true;
            }
        }
    }

    public void OnButtonClick()
    {

        BattleInitiate();

        // 初始化棋盘和计数器
        //tetrominoCounts = new int[colors.Length * tetrominoNames.Length];

        // 生成棋盘颜色
        //GenerateBoardColors();

        GenerateRuledBoard();
        ChangeToCanSwap();;
        
        // await CountTetrominoesWithUsingCard();
        //
        // if (matchedBlocks.Count == 0)
        // {
        //     ChangeToCanSwap();;
        // }
        // else
        // {
        //     await LoopWithUsingCards();
        // }
    }



    public async UniTask LoopWithUsingCards(CancellationToken cancellationToken)
    {
        while (matchedBlocks.Count > 0)
        {
            //yield return new WaitForSeconds(0.1f);
            await UpdateBoardWithMatches(matchedBlocks,cancellationToken);
            matchedBlocks.Clear();
            await ChangeColor(cancellationToken);
            //yield return new WaitForSeconds(0.5f);
            await CountTetrominoesWithUsingCard(cancellationToken);
        }

        if (!await CheckCanEliminate(cancellationToken))
        {
            GenerateRuledBoard();
        }

        ChangeToCanSwap();;
    }

    public async UniTask LoopWithoutUsingCards(CancellationToken cancellationToken)
    {
        while (matchedBlocks.Count > 0)
        {
            await UpdateBoardWithMatches(matchedBlocks,cancellationToken);
            matchedBlocks.Clear();
            ChangeColorOnlyBoard();
            await CountTetrominoesWithoutUsingCard(cancellationToken);
        }

        ChangeToCanSwap();;
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
                    //Color randomColor = colors[randomColorIndex];
                    board[i, j] = randomColorIndex;
                    GameObject cube = Instantiate(cubePrefab[randomColorIndex], new Vector3(-boardSize + i + xOffset + 1, -j + yOffset, 0), Quaternion.identity);
                    //cube.GetComponent<SpriteRenderer>().color = randomColor;
                    cube.transform.SetParent(boardContainer);
                    cubeMatrix[i, j] = cube;
                }
            }
        }
    }


    private List<int[,]> GenerateRotatedBoard(int[,] originalBoard)
    {
        int size = originalBoard.GetLength(0);
        List<int[,]> rotatedBoard = new List<int[,]>();

        // Store the original board as the first rotation
        rotatedBoard.Add(originalBoard);

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
            rotatedBoard.Add(rotated);
        }

        return rotatedBoard;
    }

    public async UniTask CountTetrominoesWithUsingCard(CancellationToken cancellationToken)
    {
        List<int[,]> rotatedBoard = GenerateRotatedBoard(board);

        
        //Debug.Log(bag==null);
        //只检测背包
        //foreach (Card card in bag)
        //{
        //    for (int i = 0; i < boardSize; i++)
        //    {
        //        for (int j = 0; j < boardSize; j++)
        //        {
        //            for (int k = 0; k < 4; k++)
        //            {
        //                if (CheckTetromino(rotatedBoard[k], card.Tetromino, i, j))
        //                {
        //                    int colorIndex = rotatedBoard[k][i, j];
        //                    if (card.Color == colors[colorIndex])
        //                    {
        //                        //交换-1的格子会导致tetrominoCountIndex为负数
        //                        int tetrominoCountIndex = card.Tetromino.Index + colorIndex * Main.instance.GeTetLen();
        //                        Debug.Log(tetrominoCounts.Length);
        //                        Debug.Log(tetrominoCountIndex);
        //                        tetrominoCounts[tetrominoCountIndex]++;
        //                        card.UseCard();

        //                        // 将匹配成功的位置转换成原始未旋转的坐标
        //                        int originalX = i;
        //                        int originalY = j;
        //                        ConvertToOriginalCoordinates(originalX, originalY, k, out int originalXUnrotated, out int originalYUnrotated);


        //                        // 将匹配成功的格子位置和形状中的格子位置都记录下来
        //                        matchedBlocks.Add(new Vector2Int(originalXUnrotated, originalYUnrotated));
        //                        RecordShapeBlocks(originalX, originalY, card.Tetromino.Shape, matchedBlocks, k);
        //                    }

        //                }

        //            }
        //        }
        //    }
        //}
        ////检测所有类型
        foreach (Tetromino tetromino in Main.instance.GetTetrominoes())
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        string curStr = "Cur";
                        int colorIndex = rotatedBoard[k][i, j];
                        switch (colorIndex)
                        {
                            case 0:
                                curStr += "Red";
                                break;
                            case 1:
                                curStr += "Green";
                                break;
                            case 2:
                                curStr += "Blue";
                                break;
                            case 3:
                                curStr += "Yellow";
                                break;
                            default:
                                break;
                        }
                        curStr += Main.instance.GetTetAbbName(tetromino);
                        var curType = ToEnum<CurCountsType>(curStr);
                        if (CheckTetromino(rotatedBoard[k], tetromino, i, j))
                        {
                            //int colorIndex = rotatedBoard[k][i, j];
                            //int tetrominoCountIndex = tetromino.Index + colorIndex * Main.instance.GeTetLen();
                            //tetrominoCounts[tetrominoCountIndex]++;
                            //// 将匹配成功的位置转换成原始未旋转的坐标
                            //int originalX = i;
                            //int originalY = j;
                            //ConvertToOriginalCoordinates(originalX, originalY, k, out int originalXUnrotated, out int originalYUnrotated);
                            //// 将匹配成功的格子位置和形状中的格子位置都记录下来
                            //matchedBlocks.Add(new Vector2Int(originalXUnrotated, originalYUnrotated));
                            //RecordShapeBlocks(originalX, originalY, tetromino.Shape, matchedBlocks, k);

                            //交换-1的格子会导致tetrominoCountIndex为负数
                            //int tetrominoCountIndex = tetromino.Index + colorIndex * Main.instance.GeTetLen();
                            //Debug.Log(tetrominoCounts.Length);
                            //Debug.Log(tetrominoCountIndex);
                            //tetrominoCounts[tetrominoCountIndex]++;
                            curCounts[curType]++;


                            
                            // //todo 某些形状卡牌会多次触发
                            // foreach(Card card in bag)
                            // {
                            //     if (card.Color == colors[colorIndex])
                            //     {
                            //         if (card.Tetromino.Name == tetromino.Name)
                            //         {
                            //             card.UseCard();
                            //         }
                            //         else
                            //         {
                            //             switch (card.Tetromino.Name)
                            //             {
                            //                 case "L型":
                            //                     if(tetromino.Name == "J型")
                            //                     {
                            //                         card.UseCard();
                            //                     }
                            //                     break;
                            //                 case "J型":
                            //                     if (tetromino.Name == "L型")
                            //                     {
                            //                         card.UseCard();
                            //                     }
                            //                     break;
                            //                 case "Z型":
                            //                     if (tetromino.Name == "S型")
                            //                     {
                            //                         card.UseCard();
                            //                     }
                            //                     break;
                            //
                            //                 case "S型":
                            //                     if (tetromino.Name == "Z型")
                            //                     {
                            //                         card.UseCard();
                            //                     }
                            //                     break;
                            //                 default:
                            //                     break;
                            //             }
                            //         }
                            //     }
                            // }

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
        
        foreach (var pair in curCounts.ToList())
        {
            switch (pair.Key.ToString()[pair.Key.ToString().Length - 1].ToString())
             {
                 case "O":
                     curCounts[pair.Key] /= 4;
                     break;
                 case "I":
                     curCounts[pair.Key] /= 2;
                     break;
                 case "Z":
                     curCounts[pair.Key] /= 2;
                     break;
                 default:
                     break;
             }

        }
        foreach (var pair in allCounts.ToList())
        {
            CurCountsType curKey = ToEnum<CurCountsType>("Cur" + pair.Key.ToString().Substring(3));
            allCounts[pair.Key] += curCounts[curKey];
        }
        UseCard();
        // //O型重复计算了四遍
        // for (int i = 2; i < tetrominoCounts.Length; i += 7)
        // {
        //     tetrominoCounts[i] /= 4;
        // }
        // //I型重复计算了两遍
        // for (int i = 3; i < tetrominoCounts.Length; i += 7)
        // {
        //     tetrominoCounts[i] /= 2;
        // }
        // //S型重复计算了两遍
        // for (int i = 5; i < tetrominoCounts.Length; i += 7)
        // {
        //     tetrominoCounts[i] /= 2;
        // }
        // //Z型重复计算了两遍
        // for (int i = 6; i < tetrominoCounts.Length; i += 7)
        // {
        //     tetrominoCounts[i] /= 2;
        // }
    }

    public void UseCard()
    {
        foreach (var pair in curCounts.ToList())
        {
            ColorType colorType = default;
            TetrominoType tetrominoType = default;
            switch (pair.Key.ToString().Length >=4 ? pair.Key.ToString()[3].ToString() : "")
            { 
                case "R":
                    colorType = ColorType.Red;
                    break;
                case "B":
                    colorType = ColorType.Blue;
                    break;
                case "Y":
                    colorType = ColorType.Yellow;
                    break;
                case "G":
                    colorType = ColorType.Green;
                    break;
                default:
                    break;
            }
            switch (pair.Key.ToString()[pair.Key.ToString().Length - 1].ToString())
            {
                case "O":
                    tetrominoType = TetrominoType.OType;
                    break;
                case "I":
                    tetrominoType = TetrominoType.IType;
                    break;
                case "Z":
                    tetrominoType = TetrominoType.SZType;
                    break;
                case "T":
                    tetrominoType = TetrominoType.TType;
                    break;
                case "J":
                    tetrominoType = TetrominoType.LJType;
                    break;
                default:
                    break;
            }
            
            
            foreach (CardObject card in bag)
            {
                // if (tetrominoType != null)
                // {
                    if (colorType == card.ColorType && tetrominoType == card.TetrominoType)
                    {
                        for (; curCounts[pair.Key] > 0; curCounts[pair.Key]--)
                        {
                            //Debug.Log(card.id);
                            card.Do();
                        }
                    }
                // }
                
            }
            
        }
        InitCurCounts();
        
    }
    
    public async UniTask CountTetrominoesWithoutUsingCard(CancellationToken cancellationToken)
    {
        List<int[,]> rotatedBoard = GenerateRotatedBoard(board);

        
        //Debug.Log(bag==null);
        //只检测背包
        //foreach (Card card in bag)
        //{
        //    for (int i = 0; i < boardSize; i++)
        //    {
        //        for (int j = 0; j < boardSize; j++)
        //        {
        //            for (int k = 0; k < 4; k++)
        //            {
        //                if (CheckTetromino(rotatedBoard[k], card.Tetromino, i, j))
        //                {
        //                    int colorIndex = rotatedBoard[k][i, j];
        //                    if (card.Color == colors[colorIndex])
        //                    {
        //                        //交换-1的格子会导致tetrominoCountIndex为负数
        //                        int tetrominoCountIndex = card.Tetromino.Index + colorIndex * Main.instance.GeTetLen();
        //                        Debug.Log(tetrominoCounts.Length);
        //                        Debug.Log(tetrominoCountIndex);
        //                        tetrominoCounts[tetrominoCountIndex]++;
        //                        card.UseCard();

        //                        // 将匹配成功的位置转换成原始未旋转的坐标
        //                        int originalX = i;
        //                        int originalY = j;
        //                        ConvertToOriginalCoordinates(originalX, originalY, k, out int originalXUnrotated, out int originalYUnrotated);


        //                        // 将匹配成功的格子位置和形状中的格子位置都记录下来
        //                        matchedBlocks.Add(new Vector2Int(originalXUnrotated, originalYUnrotated));
        //                        RecordShapeBlocks(originalX, originalY, card.Tetromino.Shape, matchedBlocks, k);
        //                    }

        //                }

        //            }
        //        }
        //    }
        //}
        ////检测所有类型
        foreach (Tetromino tetromino in Main.instance.GetTetrominoes())
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        int colorIndex = rotatedBoard[k][i, j];
                        if (CheckTetromino(rotatedBoard[k], tetromino, i, j))
                        {
                            
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


    private async UniTask UpdateBoardWithMatches(List<Vector2Int> matchedBlocks,CancellationToken cancellationToken)
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
    }


    public  async UniTask ChangeColor(CancellationToken cancellationToken)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.SetAutoKill(false);
        List<UniTask> moveUniTasks = new List<UniTask>();

        List<GameObject> destroyCubes = new List<GameObject>();
        
        for (int i = 0; i < boardSize; i++) {
            int slow = boardSize - 1;

            for (int fast = boardSize - 1; fast >= 0; fast--) {
                if (board[i, fast] != -1)
                {
                    board[i, slow] = board[i, fast];

                    //动画效果
                    if (cubeMatrix[i, fast] != null && cubeMatrix[i, slow] != null)
                    {
                        sequence.Join(cubeMatrix[i, fast].transform
                            .DOMove(cubeMatrix[i, slow].transform.position, (slow - fast) * fallTime));
                        //moveUniTasks.Add(cubeMatrix[i, fast].transform.DOMove(cubeMatrix[i, slow].transform.position, (slow - fast) * fallTime).AsyncWaitForCompletion().AsUniTask());
                    }
                    
                    
                    //更新cubeMatrix数据
                    cubeMatrix[i, slow] = cubeMatrix[i, fast];
                    slow--;
                }
                else
                {
                    if (cubeMatrix[i, fast] != null)
                    {
                        VanishEffects.Add(GameObject.Instantiate(VanishEffectPrefab,cubeMatrix[i, fast].transform.position + Vector3.back,Quaternion.identity));
                        destroyCubes.Add(cubeMatrix[i, fast]);
                    }
                    
                }
            }

            foreach (GameObject cube in destroyCubes)
            {
                Destroy(cube);
            }

            for (int now = 1; slow >= 0; slow--) {
                //now是现在应该在棋盘上方几格处生成一个新的
                int index = ColorRandom();
                GameObject cube = Instantiate(cubePrefab[index], new Vector3(-boardSize + i + xOffset + 1, -slow + yOffset + now, 0), Quaternion.identity,boardContainer);
                cubeMatrix[i, slow] = cube;
                //cubeMatrix[i, slow].GetComponent<SpriteRenderer>().color = colors[index];
                board[i, slow] = index;

                //动画效果
                sequence.Join(cubeMatrix[i, slow].transform.DOMoveY(-slow + yOffset, (slow + now) * fallTime));
                //moveUniTasks.Add(cubeMatrix[i, slow].transform.DOMoveY(-slow + yOffset, (slow + now) * fallTime).AsyncWaitForCompletion().AsUniTask());
                
            }

            
        }
        moveUniTasks.Add(sequence.AsyncWaitForCompletion().AsUniTask());
        
        await UniTask.WhenAll(moveUniTasks).AttachExternalCancellation(cancellationToken).SuppressCancellationThrow();
        
        sequence.SetAutoKill(true);
    }
    
    public void ChangeColorOnlyBoard()
    {
        for (int i = 0; i < boardSize; i++) {
            int slow = boardSize - 1;

            for (int fast = boardSize - 1; fast >= 0; fast--) {
                if (board[i, fast] != -1) {
                    board[i, slow] = board[i, fast];
                }
            }

            for (int now = 1; slow >= 0; slow--) {
                //now是现在应该在棋盘上方几格处生成一个新的
                int index = ColorRandom();
                board[i, slow] = index;
            }
        }
    }

    public void RandomColor() {
        for (int i = 0; i < boardSize; i++) {
            for (int j = 0; j < boardSize; j++) {
                Vector2 pos = cubeMatrix[i, j].transform.position;
                Destroy(cubeMatrix[i, j]);
                int index = ColorRandom();
                GameObject cube = Instantiate(cubePrefab[index], pos, Quaternion.identity, boardContainer);
                cubeMatrix[i, j] = cube;
                //cubeMatrix[i,j].GetComponent<SpriteRenderer>().color = colors[index];
                board[i, j] = index;
            }
        }
    }
    
    public void RandomColorOnlyBoard() 
    {
        for (int i = 0; i < boardSize; i++) {
            for (int j = 0; j < boardSize; j++) {
                int index = ColorRandom();
                board[i, j] = index;
            }
        }
    }
    
    public void GenerateCubeWithBoard() 
    {
        for (int i = 0; i < boardSize; i++) {
            for (int j = 0; j < boardSize; j++) {
                //Vector2 pos = cubeMatrix[i, j].transform.position;
                if (cubeMatrix[i, j] != null)
                {
                    Destroy(cubeMatrix[i, j]);
                }
                GameObject cube = Instantiate(cubePrefab[board[i, j]], new Vector3(-boardSize + i + xOffset + 1, -j + yOffset, 0), Quaternion.identity, boardContainer);
                
                //GameObject cube = Instantiate(cubePrefab[board[i, j]], pos, Quaternion.identity, boardContainer);
                cubeMatrix[i, j] = cube;
            }
        }
    }

    public async void RandomColor1() {
        for (int i = 0; i < boardSize; i++) {
            for (int j = 0; j < boardSize; j++) {
                Vector2 pos = cubeMatrix[i, j].transform.position;
                Destroy(cubeMatrix[i, j]);
                int index = ColorRandom();
                GameObject cube = Instantiate(cubePrefab[index], pos, Quaternion.identity, boardContainer);
                cubeMatrix[i, j] = cube;
                //cubeMatrix[i,j].GetComponent<SpriteRenderer>().color = colors[index];
                board[i, j] = index;
            }
        }
        if(selectedBlock1 != null)
        {
            selectedBlock1 = null;
            Destroy(blank);
        }

        await CountTetrominoesWithUsingCard(cancellationToken);

        if (matchedBlocks.Count == 0)
        {
            ChangeToCanSwap();;
        }
        else
        {
            await LoopWithUsingCards(cancellationToken);
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


    private async void SwitchBlock() {
        // 获取鼠标点击位置的世界坐标
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 offset = boardContainer.position; // 棋盘的中心位置偏移
        Vector3 normalizedPosition = worldPosition - offset; // 将鼠标点击的世界坐标与棋盘中心位置进行偏移
        Vector3 scaledPosition = normalizedPosition /** blockSize*/; // 根据每个块的大小进行缩放

        //Debug.Log(scaledPosition);

        //奇数格棋盘变换一下
        if (boardSize % 2 == 1)
        {
            scaledPosition += new Vector3(0.5f, 0.5f, 0);
        }

        // 将世界坐标转换为棋盘格子坐标
        int row = Mathf.FloorToInt(scaledPosition.y); // 鼠标点击的行索引
        int column = Mathf.FloorToInt(scaledPosition.x); // 鼠标点击的列索引

        //Debug.Log(new Vector2Int(row, column));

        // 根据棋盘尺寸进行调整
        row += boardSize / 2;
        row = boardSize - row - 1;
        column += boardSize / 2;

        //Debug.Log(new Vector2Int(row, column));

        Vector2Int curPosition = new Vector2Int(row, column);


        // 检查索引是否在有效范围内
        if (IsValidBlock(curPosition))
        {
            if (selectedBlock1 == null)
            {
                selectedBlock1 = curPosition;
                blank = Instantiate(blankPrefab,cubeMatrix[curPosition.y, curPosition.x].transform.position, Quaternion.identity);
            }
            else if (selectedBlock1.Value == curPosition)
            {
                selectedBlock1 = null;
                //Debug.Log(selectedBlock1.HasValue);
                Destroy(blank);
            }
            else if (selectedBlock2 == null)
            {
                if(Vector2.Distance((Vector2)selectedBlock1,curPosition) <= 1f)
                {
                    Destroy(blank);
                    selectedBlock2 = curPosition;
                    canSwap = false;
                    //换回来的情况
                    if (isSwaped)
                    {
                        // 执行块交换逻辑
                        SwapBlocks(selectedBlock1.Value, selectedBlock2.Value);
                    }
                    //不换回来的情况
                    else
                    {
                        SwapBlocks1(selectedBlock1.Value, selectedBlock2.Value);

                        // 重置选定的块
                        selectedBlock1 = null;
                        selectedBlock2 = null;
                        // 统计方块图形个数
                        await CountTetrominoesWithUsingCard(cancellationToken);

                        if (matchedBlocks.Count == 0)
                        {
                            ChangeToCanSwap();;
                        }
                        else
                        {
                            await LoopWithUsingCards(cancellationToken);
                        }
                        Main.instance.AddOne();
                        turn.text = Main.instance.GetTurn().ToString();

                        if (Main.instance.GetTurn() % 3 == 0 && Main.instance.GetTurn() != 0)
                        {
                            Hurt();
                        }
                    }
                    
                    //Debug.Log(CheckCanEliminate());
                    
                    
                    // 输出统计结果
                    //PrintTetrominoCounts();


                    //Debug.Log(selectedBlock1);
                    //Debug.Log(selectedBlock2);
                }

            }
        }
    }

    private bool IsValidBlock(Vector2Int block)
    {
        int x = block.x;
        int y = block.y;

        return x >= 0 && x < boardSize && y >= 0 && y < boardSize;
    }
    /// <summary>
    /// 换回来的交换
    /// </summary>
    /// <param name="block1">格子1坐标</param>
    /// <param name="block2">格子2坐标</param>
    private async void SwapBlocks(Vector2Int block1, Vector2Int block2)
    {
        // 重置选定的块
        selectedBlock1 = null;
        selectedBlock2 = null;

        //数据交换
        int temp = board[block1.y, block1.x];
        board[block1.y, block1.x] = board[block2.y, block2.x];
        board[block2.y, block2.x] = temp;
        
        //动画效果
        Sequence sequence = DOTween.Sequence();
        sequence.Append(cubeMatrix[block1.y, block1.x].transform
            .DOMove(cubeMatrix[block2.y, block2.x].transform.position, 0.05f));
        sequence.Join(cubeMatrix[block2.y, block2.x].transform
            .DOMove(cubeMatrix[block1.y, block1.x].transform.position, 0.05f));
        List<UniTask> swapTaskList = new List<UniTask>();
        
        swapTaskList.Add(sequence.AsyncWaitForCompletion().AsUniTask());

        await UniTask.WhenAll(swapTaskList).AttachExternalCancellation(cancellationToken).SuppressCancellationThrow();
        sequence.SetAutoKill(true);
        
        //更新cubeMatrix数据
        GameObject temp1 = cubeMatrix[block1.y, block1.x];
        cubeMatrix[block1.y, block1.x] = cubeMatrix[block2.y, block2.x];
        cubeMatrix[block2.y, block2.x] = temp1;

        await CountTetrominoesWithUsingCard(cancellationToken);

        if (matchedBlocks.Count == 0)
        {
            // 重新执行动画效果，将块交换回来
            Sequence reverseSequence = DOTween.Sequence();
            reverseSequence.Append(cubeMatrix[block1.y, block1.x].transform.DOMove(cubeMatrix[block2.y, block2.x].transform.position, 0.2f));
            reverseSequence.Join(cubeMatrix[block2.y, block2.x].transform.DOMove(cubeMatrix[block1.y, block1.x].transform.position, 0.2f));
            List<UniTask> reverseTaskList = new List<UniTask>();
            reverseTaskList.Add(reverseSequence.AsyncWaitForCompletion().AsUniTask());

            await UniTask.WhenAll(reverseTaskList).AttachExternalCancellation(cancellationToken).SuppressCancellationThrow();

            reverseSequence.SetAutoKill(true);
            // 还原数据，交换回来
            int reverseTemp = board[block1.y, block1.x];
            board[block1.y, block1.x] = board[block2.y, block2.x];
            board[block2.y, block2.x] = reverseTemp;

            // 更新cubeMatrix数据
            GameObject reverseTemp1 = cubeMatrix[block1.y, block1.x];
            cubeMatrix[block1.y, block1.x] = cubeMatrix[block2.y, block2.x];
            cubeMatrix[block2.y, block2.x] = reverseTemp1;

            //允许再次交换
            ChangeToCanSwap();;
        }
        else
        {
            await LoopWithUsingCards(cancellationToken);
            Main.instance.AddOne();
            turn.text = Main.instance.GetTurn().ToString();
            
            BuffManager.instance.UpdateBuffs();

            if (Main.instance.GetTurn() % 3 == 0 && Main.instance.GetTurn() != 0)
            {
                Hurt();
            }
        }
            
        
        //isSwaped = false;
    }
    
    
    /// <summary>
    /// 换回来的交换
    /// </summary>
    /// <param name="block1">格子1坐标</param>
    /// <param name="block2">格子2坐标</param>
    // private async UniTask SwapBlocks(Vector2Int block1, Vector2Int block2)
    // {
    //     // 重置选定的块
    //     selectedBlock1 = null;
    //     selectedBlock2 = null;
    //
    //     int temp = board[block1.y, block1.x];
    //     board[block1.y, block1.x] = board[block2.y, block2.x];
    //     board[block2.y, block2.x] = temp;
    //
    //     List<UniTask> swapTaskList = new List<UniTask>();
    //     //动画效果
    //     Sequence sequence = DOTween.Sequence();
    //     cubeMatrix[block1.y, block1.x].transform.DOMove(cubeMatrix[block2.y, block2.x].transform.position, 0.05f);
    //     //sequence.Append(cubeMatrix[block1.y, block1.x].transform.DOMove(cubeMatrix[block2.y, block2.x].transform.position, 0.2f));
    //     cubeMatrix[block2.y, block2.x].transform.DOMove(cubeMatrix[block1.y, block1.x].transform.position, 0.05f);
    //     //sequence.Join(cubeMatrix[block2.y, block2.x].transform.DOMove(cubeMatrix[block1.y, block1.x].transform.position, 0.2f));
    //     // sequence.AppendCallback(() =>
    //     // {
    //     //     //更新cubeMatrix数据
    //     //     GameObject temp1 = cubeMatrix[block1.y, block1.x];
    //     //     cubeMatrix[block1.y, block1.x] = cubeMatrix[block2.y, block2.x];
    //     //     cubeMatrix[block2.y, block2.x] = temp1;
    //     //
    //     //     CountTetrominoes();
    //     //
    //     //     if (matchedBlocks.Count == 0)
    //     //     {
    //     //         // 重新执行动画效果，将块交换回来
    //     //         Sequence reverseSequence = DOTween.Sequence();
    //     //         reverseSequence.Append(cubeMatrix[block1.y, block1.x].transform.DOMove(cubeMatrix[block2.y, block2.x].transform.position, 0.2f));
    //     //         reverseSequence.Join(cubeMatrix[block2.y, block2.x].transform.DOMove(cubeMatrix[block1.y, block1.x].transform.position, 0.2f));
    //     //         reverseSequence.AppendCallback(() =>
    //     //         {
    //     //             // 还原数据，交换回来
    //     //             int reverseTemp = board[block1.y, block1.x];
    //     //             board[block1.y, block1.x] = board[block2.y, block2.x];
    //     //             board[block2.y, block2.x] = reverseTemp;
    //     //
    //     //             // 更新cubeMatrix数据
    //     //             GameObject reverseTemp1 = cubeMatrix[block1.y, block1.x];
    //     //             cubeMatrix[block1.y, block1.x] = cubeMatrix[block2.y, block2.x];
    //     //             cubeMatrix[block2.y, block2.x] = reverseTemp1;
    //     //
    //     //             ChangeToCanSwap();;
    //     //         });
    //     //     }
    //     //     else
    //     //     {
    //     //         StartCoroutine(Loop());
    //     //         Main.instance.AddOne();
    //     //         turn.text = Main.instance.GetTurn().ToString();
    //     //
    //     //         if (Main.instance.GetTurn() % 3 == 0 && Main.instance.GetTurn() != 0)
    //     //         {
    //     //             Hurt();
    //     //         }
    //     //     }
    //     //     
    //     // });
    //     
    //     await UniTask.Delay(TimeSpan.FromSeconds(1f));
    //         //更新cubeMatrix数据
    //         GameObject temp1 = cubeMatrix[block1.y, block1.x];
    //         cubeMatrix[block1.y, block1.x] = cubeMatrix[block2.y, block2.x];
    //         cubeMatrix[block2.y, block2.x] = temp1;
    //
    //         CountTetrominoes();
    //
    //         if (matchedBlocks.Count == 0)
    //         {
    //             // 重新执行动画效果，将块交换回来
    //             Sequence reverseSequence = DOTween.Sequence();
    //             reverseSequence.Append(cubeMatrix[block1.y, block1.x].transform.DOMove(cubeMatrix[block2.y, block2.x].transform.position, 0.2f));
    //             reverseSequence.Join(cubeMatrix[block2.y, block2.x].transform.DOMove(cubeMatrix[block1.y, block1.x].transform.position, 0.2f));
    //             reverseSequence.AppendCallback(() =>
    //             {
    //                 // 还原数据，交换回来
    //                 int reverseTemp = board[block1.y, block1.x];
    //                 board[block1.y, block1.x] = board[block2.y, block2.x];
    //                 board[block2.y, block2.x] = reverseTemp;
    //
    //                 // 更新cubeMatrix数据
    //                 GameObject reverseTemp1 = cubeMatrix[block1.y, block1.x];
    //                 cubeMatrix[block1.y, block1.x] = cubeMatrix[block2.y, block2.x];
    //                 cubeMatrix[block2.y, block2.x] = reverseTemp1;
    //
    //                 ChangeToCanSwap();;
    //             });
    //         }
    //         else
    //         {
    //             StartCoroutine(Loop());
    //             Main.instance.AddOne();
    //             turn.text = Main.instance.GetTurn().ToString();
    //
    //             if (Main.instance.GetTurn() % 3 == 0 && Main.instance.GetTurn() != 0)
    //             {
    //                 Hurt();
    //             }
    //         }
    //         
    //     
    //     //isSwaped = false;
    // }
    
    /// <summary>
    /// 不换回来的交换
    /// </summary>
    /// <param name="block1">格子1坐标</param>
    /// <param name="block2">格子2坐标</param>
    private void SwapBlocks1(Vector2Int block1, Vector2Int block2)
    {
        int temp = board[block1.y, block1.x];
        board[block1.y, block1.x] = board[block2.y, block2.x];
        board[block2.y, block2.x] = temp;

        //动画效果
        Sequence sequence = DOTween.Sequence();
        sequence.Append(cubeMatrix[block1.y, block1.x].transform.DOMove(cubeMatrix[block2.y, block2.x].transform.position, 0.2f));
        sequence.Join(cubeMatrix[block2.y, block2.x].transform.DOMove(cubeMatrix[block1.y, block1.x].transform.position, 0.2f));
        sequence.AppendCallback(() =>
        {
            //更新cubeMatrix数据
            GameObject temp1 = cubeMatrix[block1.y, block1.x];
            cubeMatrix[block1.y, block1.x] = cubeMatrix[block2.y, block2.x];
            cubeMatrix[block2.y, block2.x] = temp1;
        });
    }

    public void Hurt()
    {
        PlayerState.instance.TakeDamge(EnemyState.instance.GetDamage() + PlayerState.instance.GetBattleCount() * 10);

    }

    public void UpdatePlayerUI()
    {
        playerHP.value = PlayerState.instance.GetHP() / PlayerState.instance.GetMaxHP();
    }

    private void BattleInitiate()
    {
        Main.instance.OnSingletonInit();
        //PlayerState.instance.OnSingletonInit();
        playerHP.value = 1;
        PlayerState.instance.DeleteDamageBuff();
        PlayerState.instance.DeleteDefenceBuffLayer();
        
        EnemyState.instance.OnSingletonInit();
        //todo 临时强化

        #region MyRegion

        EnemyState.instance.AddMaxHP(100 * PlayerState.instance.GetBattleCount());
        EnemyState.instance.AddHPToMax();

        #endregion
       
        
        enemyHP.value = 1;
        turn.text = Main.instance.GetTurn().ToString();
        
        BuffManager.instance.OnSingletonInit();
        
        InitCurCounts();
        InitAllCounts();
    }

    public IEnumerator<WaitForSeconds> Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public async UniTask<bool> CheckCanEliminate(CancellationToken cancellationToken)
    {
        Array.Copy(board, board1, board.Length);
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {

                if (i - 1 >= 0)
                {
                    int temp = board1[i, j];
                    board1[i, j] = board1[i - 1, j];
                    board1[i - 1, j] = temp;
                    if (SearchFourConnected(board1, i - 1, j) >= 4 || SearchFourConnected(board1, i, j) >= 4)
                    {
                        tipCube = cubeMatrix[i, j];
                        return true;
                    }
                    temp = board1[i, j];
                    board1[i, j] = board1[i - 1, j];
                    board1[i - 1, j] = temp;
                }

                if (i + 1 < boardSize)
                {
                    int temp = board1[i, j];
                    board1[i, j] = board1[i + 1, j];
                    board1[i + 1, j] = temp;
                    if (SearchFourConnected(board1, i + 1, j) >= 4 || SearchFourConnected(board1, i, j) >= 4)
                    {
                        tipCube = cubeMatrix[i, j];
                        return true;
                    }
                    temp = board1[i, j];
                    board1[i, j] = board1[i + 1, j];
                    board1[i + 1, j] = temp;
                }

                if (j - 1 >= 0)
                {
                    int temp = board1[i, j];
                    board1[i, j] = board1[i, j - 1];
                    board1[i, j - 1] = temp;
                    if (SearchFourConnected(board1, i, j - 1) >= 4 || SearchFourConnected(board1, i, j) >= 4)
                    {
                        tipCube = cubeMatrix[i, j];
                        return true;
                    }
                    temp = board1[i, j];
                    board1[i, j] = board1[i, j - 1];
                    board1[i, j - 1] = temp;
                }

                if (j + 1 < boardSize)
                {
                    int temp = board1[i, j];
                    board1[i, j] = board1[i, j + 1];
                    board1[i, j + 1] = temp;
                    if (SearchFourConnected(board1, i, j + 1) >= 4 || SearchFourConnected(board1, i, j) >= 4)
                    {
                        tipCube = cubeMatrix[i, j];
                        return true;
                    }
                    temp = board1[i, j];
                    board1[i, j] = board1[i, j + 1];
                    board1[i, j + 1] = temp;
                }
            }

        }


        return false; 
    }

    int SearchFourConnected(int[,] board, int startX, int startY)
    {
        int connectedCount = 0;
        bool[,] visited = new bool[boardSize, boardSize];
        return DFS(board, startX, startY, visited, connectedCount);
    }

    int DFS(int[,] board, int x, int y, bool[,] visited, int connectedCount)
    {
        if (x < 0 || y < 0 || x >= boardSize || y >= boardSize || visited[x, y])
        {
            return connectedCount;
        }

        visited[x, y] = true;
        int startColor = board[x, y];

        int[] dx = { 1, -1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };

        connectedCount++;

        for (int i = 0; i < 4; i++)
        {
            int nextX = x + dx[i];
            int nextY = y + dy[i];

            if (nextX >= 0 && nextX < boardSize && nextY >= 0 && nextY < boardSize && board[nextX, nextY] == startColor)
            {
                connectedCount = DFS(board, nextX, nextY, visited, connectedCount);
            }
        }

        return connectedCount;
    }

    /// <summary>
    /// 生成一个不会产生消除效果和动画，且是可以通过交换消除的棋盘，
    /// </summary>
    public async void GenerateRuledBoard()
    {
        if (isGeneratingRuledBoard)
        {
            return;
        }

        isGeneratingRuledBoard = true;
        
        if(selectedBlock1 != null)
        {
            selectedBlock1 = null;
            Destroy(blank);
        }

        do
        {
            RandomColorOnlyBoard();

            await CountTetrominoesWithoutUsingCard(cancellationToken);

            await LoopWithoutUsingCards(cancellationToken);

            ChangeToCanSwap();
        } while (!await CheckCanEliminate(cancellationToken));
        
        GenerateCubeWithBoard();
        //
        // while (!await CheckCanEliminate())
        // {
        //     await CountTetrominoesWithoutUsingCard();
        //     if (matchedBlocks.Count == 0)
        //     {
        //         ChangeToCanSwap();;
        //     }
        //     else
        //     {
        //         await LoopWithoutUsingCards();
        //     }
        // }

        isGeneratingRuledBoard = false;
    }

    private void ChangeToCanSwap()
    {
        canSwap = true;

        selectedBlock1 = null;
        selectedBlock2 = null;
    }

    void InitCurCounts()
    {
        curCounts = new Dictionary<CurCountsType, int>();
        foreach (CurCountsType type in Enum.GetValues(typeof(CurCountsType)))
        {
            curCounts.Add(type,0);
        }
    }
    
    void InitAllCounts()
    {
        allCounts = new Dictionary<AllCountsType, int>();
        foreach (AllCountsType type in Enum.GetValues(typeof(AllCountsType)))
        {
            allCounts.Add(type,0);
        }
    }
    
    /// <summary>
    /// 字符串转Enum
    /// </summary>
    /// <typeparam name="T">枚举</typeparam>
    /// <param name="str">字符串</param>
    /// <returns>转换的枚举</returns>
    public static T ToEnum<T>(string str)
    {
        return (T)Enum.Parse(typeof(T), str);
    }
}
