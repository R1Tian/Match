//////using UnityEngine;
//////using System;
//////using System.Diagnostics;

//////public class BlockCount : MonoBehaviour
//////{
//////    public int boardSize = 10; // 棋盘的大小

//////    static private Transform boardContainer; // 棋盘容器

//////    public GameObject[] cellPrefabs; // 在 Inspector 中拖拽四种颜色的预制体到这个数组中

//////    private static GameObject[] s_CellPrefabs; // 静态的预制体引用

//////    void Awake()
//////    {
//////        // 将静态的预制体引用设置为实例字段的值
//////        s_CellPrefabs = cellPrefabs;
//////    }
//////    void Start()
//////    {
//////        boardContainer = new GameObject("BoardContainer").transform;
//////        boardContainer.position = Vector3.zero;

//////        int[][] blocks = new int[][] {
//////            new int[] { 1, 1, 1, 1 },       // I型方块
//////            new int[] { 1, 1, 1, 0 },       // J型方块
//////            new int[] { 1, 1, 1, 0 },       // L型方块
//////            new int[] { 1, 1, 0, 0 },       // O型方块
//////            new int[] { 0, 1, 1, 1 },       // S型方块
//////            new int[] { 1, 1, 1, 0 },       // T型方块
//////            new int[] { 1, 1, 0, 1 }        // Z型方块
//////        };

//////        Stopwatch stopwatch = new Stopwatch();

//////        for (int i = 0; i < blocks.Length; i++)
//////        {
//////            int count = 0;

//////            stopwatch.Restart();

//////            int[] block = blocks[i];
//////            int rotations = (i == 0) ? 2 : 4;  // I型方块有两个旋转状态，其余方块有四个旋转状态

//////            for (int r = 0; r < rotations; r++)
//////            {
//////                count += CountBlock(block);
//////                block = RotateBlock(block);
//////            }

//////            stopwatch.Stop();

//////            UnityEngine.Debug.Log("Block " + (i + 1) + " Count: " + count + ", Time: " + stopwatch.ElapsedMilliseconds + "ms");
//////        }
//////    }

//////    static int CountBlock(int[] block)
//////    {
//////        int rows = 10;
//////        int cols = 10;
//////        int count = 0;

//////        ulong boardBitmap = GenerateRandomBoard(rows, cols);
//////        ulong blockBitmap = GetBlockBitmap(block);

//////        for (int i = 0; i <= rows - block.Length; i++)
//////        {
//////            for (int j = 0; j <= cols - block.Length; j++)
//////            {
//////                ulong windowBitmap = GetWindowBitmap(boardBitmap, i, j, block.Length);

//////                if ((windowBitmap & blockBitmap) == blockBitmap)
//////                {
//////                    count++;
//////                }
//////            }
//////        }

//////        return count;
//////    }

//////    static ulong GenerateRandomBoard(int rows, int cols)
//////    {
//////        ulong boardBitmap = 0;
//////        System.Random random = new System.Random();

//////        Camera mainCamera = Camera.main;
//////        Vector3 centerPosition = mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth / 2f, mainCamera.pixelHeight / 2f, 0f));

//////        float startX = centerPosition.x - (cols - 1) / 2f;
//////        float startY = centerPosition.y - (rows - 1) / 2f;

//////        for (int i = 0; i < rows; i++)
//////        {
//////            for (int j = 0; j < cols; j++)
//////            {
//////                int value = random.Next(s_CellPrefabs.Length); // 随机选择预制体的索引
//////                int index = i * cols + j;
//////                ulong bit = (ulong)value << index;
//////                boardBitmap |= bit;

//////                // 创建单元格游戏对象并进行位置设置
//////                GameObject cell = Instantiate(s_CellPrefabs[value], new Vector3(startX + j, startY + i, 0), Quaternion.identity);
//////                cell.transform.SetParent(boardContainer);
//////            }
//////        }

//////        return boardBitmap;
//////    }

//////    static ulong GetBlockBitmap(int[] block)
//////    {
//////        int size = block.Length;
//////        ulong bitmap = 0;

//////        for (int i = 0; i < size; i++)
//////        {
//////            if (block[i] != 0)
//////            {
//////                ulong bit = 1UL << i;
//////                bitmap |= bit;
//////            }
//////        }

//////        return bitmap;
//////    }

//////    static ulong GetWindowBitmap(ulong bitmap, int row, int col, int size)
//////    {
//////        ulong windowBitmap = 0;
//////        int shift = col;

//////        for (int i = row; i < row + size; i++)
//////        {
//////            ulong rowBitmap = (bitmap >> (shift + (i * size))) & ((1UL << size) - 1);
//////            windowBitmap |= rowBitmap << ((i - row) * size);
//////        }

//////        return windowBitmap;
//////    }

//////    static int[] RotateBlock(int[] block)
//////    {
//////        int size = Mathf.RoundToInt(Mathf.Sqrt(block.Length));
//////        int[] rotatedBlock = new int[block.Length];

//////        for (int i = 0; i < size; i++)
//////        {
//////            for (int j = 0; j < size; j++)
//////            {
//////                int rotatedRow = j;
//////                int rotatedCol = size - 1 - i;
//////                int rotatedIndex = rotatedRow * size + rotatedCol;
//////                rotatedBlock[rotatedIndex] = block[i * size + j];
//////            }
//////        }

//////        return rotatedBlock;
//////    }
//////}

////using UnityEngine;
////using System;
////using System.Diagnostics;
////using System.Collections.Generic;

////public class BlockCount : MonoBehaviour
////{
////    public GameObject[] cellPrefabs; // 在 Inspector 中拖拽四种颜色的预制体到这个数组中
////    public Transform boardContainer; // 棋盘容器的引用，将单元格作为其子对象

////    private static GameObject[] s_CellPrefabs; // 静态的预制体引用

////    void Awake()
////    {
////        // 将静态的预制体引用设置为实例字段的值
////        s_CellPrefabs = cellPrefabs;
////    }

////    void Start()
////    {
////        Stopwatch stopwatch = new Stopwatch();
////        stopwatch.Start();

////        // 生成棋盘并统计不同颜色不同形状的四格方块的个数
////        ulong boardBitmap = GenerateRandomBoard(8, 8);
////        Dictionary<string, int> blockCounts = CountBlocks(boardBitmap);

////        stopwatch.Stop();
////        TimeSpan elapsed = stopwatch.Elapsed;

////        // 输出统计结果和计时
////        foreach (KeyValuePair<string, int> count in blockCounts)
////        {
////            UnityEngine.Debug.Log(count.Key + "：" + count.Value + "个，用时：" + GetElapsedTimeMilliseconds(elapsed));
////        }
////    }

////    ulong GenerateRandomBoard(int rows, int cols)
////    {
////        ulong boardBitmap = 0;
////        System.Random random = new System.Random();

////        for (int i = 0; i < rows; i++)
////        {
////            for (int j = 0; j < cols; j++)
////            {
////                int value = random.Next(s_CellPrefabs.Length); // 随机选择预制体的索引
////                int index = i * cols + j;
////                ulong bit = (ulong)value << index;
////                boardBitmap |= bit;

////                // 创建单元格游戏对象并进行位置设置
////                GameObject cell = Instantiate(s_CellPrefabs[value], new Vector3(j - (cols - 1) / 2f, (rows - 1) / 2f - i, 0), Quaternion.identity);
////                cell.transform.SetParent(boardContainer);
////            }
////        }

////        return boardBitmap;
////    }

////    Dictionary<string, int> CountBlocks(ulong boardBitmap)
////    {
////        Dictionary<string, int> blockCounts = new Dictionary<string, int>();

////        // 遍历所有预制体组合
////        for (int i = 0; i < s_CellPrefabs.Length; i++)
////        {
////            GameObject prefab = s_CellPrefabs[i];
////            Color color = prefab.GetComponent<SpriteRenderer>().color;
////            string colorName = GetColorName(color);

////            // 统计不同形状的四格方块的个数
////            for (int j = 0; j < 2; j++)
////            {
////                int[] block = GetBlock(j, i);
////                ulong rotatedBitmap = GetBlockBitmap(block);

////                int count = CountOccurrences(boardBitmap, rotatedBitmap);

////                if (count > 0)
////                {
////                    string blockType = GetBlockType(block);
////                    string key = colorName + " " + blockType;

////                    if (blockCounts.ContainsKey(key))
////                    {
////                        blockCounts[key] += count;
////                    }
////                    else
////                    {
////                        blockCounts[key] =count;
////                    }
////                }
////            }
////        }

////        return blockCounts;
////    }

////    string GetBlockType(int[] block)
////    {
////        string blockType = "";

////        // I型方块
////        if (block.Length == 4 && block[0] == 1 && block[1] == 1 && block[2] == 1 && block[3] == 1)
////        {
////            blockType = "L型";
////        }
////        // J型方块
////        else if (block.Length == 4 && block[0] == 1 && block[1] == 1 && block[2] == 1 && block[3] == 0)
////        {
////            blockType = "J型";
////        }
////        // L型方块
////        else if (block.Length == 4 && block[0] == 1 && block[1] == 1 && block[2] == 0 && block[3] == 0)
////        {
////            blockType = "L型";
////        }
////        // O型方块
////        else if (block.Length == 4 && block[0] == 1 && block[1] == 1 && block[2] == 0 && block[3] == 0)
////        {
////            blockType = "O型";
////        }
////        // S型方块
////        else if (block.Length == 4 && block[0] == 0 && block[1] == 1 && block[2] == 1 && block[3] == 1)
////        {
////            blockType = "S型";
////        }
////        // T型方块
////        else if (block.Length == 4 && block[0] == 1 && block[1] == 1 && block[2] == 1 && block[3] == 0)
////        {
////            blockType = "T型";
////        }
////        // Z型方块
////        else if (block.Length == 4 && block[0] == 1 && block[1] == 1 && block[2] == 0 && block[3] == 1)
////        {
////            blockType = "Z型";
////        }

////        return blockType;
////    }

////    string GetColorName(Color color)
////    {
////        if (color == Color.red)
////        {
////            return "红色";
////        }
////        else if (color == Color.green)
////        {
////            return "绿色";
////        }
////        else if (color == Color.blue)
////        {
////            return "蓝色";
////        }
////        else if (color == Color.yellow)
////        {
////            return "黄色";
////        }
////        else
////        {
////            return "未知颜色";
////        }
////    }

////    string GetElapsedTimeMilliseconds(TimeSpan time)
////    {
////        return time.TotalMilliseconds.ToString("F2") + "毫秒";
////    }


////    int CountOccurrences(ulong boardBitmap, ulong targetBitmap)
////    {
////        int count = 0;
////        ulong mask = 0xF;

////        while (boardBitmap != 0)
////        {
////            if ((boardBitmap & mask) == targetBitmap)
////                count++;

////            boardBitmap >>= 1;
////        }

////        return count;
////    }

////    static ulong GetBlockBitmap(int[] block)
////    {
////        int size = block.Length;
////        ulong bitmap = 0;

////        for (int i = 0; i < size; i++)
////        {
////            if (block[i] != 0)
////            {
////                ulong bit = 1UL << i;
////                bitmap |= bit;
////            }
////        }

////        return bitmap;
////    }

////    int[] GetBlock(int rotation, int colorIndex)
////    {
////        // I型方块
////        if (colorIndex == 0)
////        {
////            if (rotation == 0)
////                return new int[] { 1, 1, 1, 1 };
////            else
////                return new int[] { 1, 1, 1, 1 };
////        }
////        // J型方块
////        else if (colorIndex == 1)
////        {
////            if (rotation == 0)
////                return new int[] { 1, 1, 1, 0 };
////            else if (rotation == 1)
////                return new int[] { 1, 0, 0, 0 };
////            else if (rotation == 2)
////                return new int[] { 0, 0, 1, 0 };
////            else
////                return new int[] { 0, 0, 0, 1 };
////        }
////        // L型方块
////        else if (colorIndex == 2)
////        {
////            if (rotation == 0)
////                return new int[] { 1, 1, 1, 0 };
////            else if (rotation == 1)
////                return new int[] { 0, 1, 1, 0 };
////            else if (rotation == 2)
////                return new int[] { 0, 1, 0, 0 };
////            else
////                return new int[] { 0, 0, 0, 1 };
////        }
////        // O型方块
////        else if (colorIndex == 3)
////        {
////            return new int[] { 1, 1, 0, 0 };
////        }
////        // S型方块
////        else if (colorIndex == 4)
////        {
////            if (rotation == 0 || rotation == 2)
////                return new int[] { 0, 1, 1, 1 };
////            else
////                return new int[] { 1, 1, 0, 0 };
////        }
////        // T型方块
////        else if (colorIndex == 5)
////        {
////            if (rotation == 0)
////                return new int[] { 1, 1, 1, 0 };
////            else if (rotation == 1)
////                return new int[] { 0, 1, 0, 0 };
////            else if (rotation == 2)
////                return new int[] { 0, 1, 0, 0 };
////            else
////                return new int[] { 0, 0, 0, 1 };
////        }
////        // Z型方块
////        else if (colorIndex == 6)
////        {
////            if (rotation == 0 || rotation == 2)
////                return new int[] { 1, 1, 0, 1 };
////            else
////                return new int[] { 0, 1, 1, 0 };
////        }

////        return new int[] { };
////    }

////    string GetBlockShape(int[] block)
////    {
////        int size = Mathf.RoundToInt(Mathf.Sqrt(block.Length));
////        string shape = "";

////        for (int i = 0; i < block.Length; i++)
////        {
////            if (block[i] == 1)
////            {
////                if (shape != "")
////                    shape += ", ";

////                int row = i / size;
////                int col = i % size;
////                shape += "(" + row + ", " + col + ")";
////            }
////        }

////        return shape;
////    }

////}

//using UnityEngine;
//using System;
//using System.Diagnostics;
//using System.Collections.Generic;

//public class BlockCount : MonoBehaviour
//{
//    public GameObject[] cellPrefabs; // 在 Inspector 中拖拽四种颜色的预制体到这个数组中
//    public Transform boardContainer; // 棋盘容器的引用，将单元格作为其子对象

//    private static GameObject[] s_CellPrefabs; // 静态的预制体引用

//    int[][,] blockShapes = {
//        // I-shaped block
//        new int[,] {
//            {1, 1, 1, 1}
//        },
//        // J-shaped block
//        new int[,] {
//            {1, 0, 0},
//            {1, 1, 1}
//        },
//        // L-shaped block
//        new int[,] {
//            {0, 0, 1},
//            {1, 1, 1}
//        },
//        // O-shaped block
//        new int[,] {
//            {1, 1},
//            {1, 1}
//        },
//        // S-shaped block
//        new int[,] {
//            {0, 1, 1},
//            {1, 1, 0}
//        },
//        // T-shaped block
//        new int[,] {
//            {0, 1, 0},
//            {1, 1, 1}
//        },
//        // Z-shaped block
//        new int[,] {
//            {1, 1, 0},
//            {0, 1, 1}
//        }
//    };



//    void Awake()
//    {
//        // 将静态的预制体引用设置为实例字段的值
//        s_CellPrefabs = cellPrefabs;
//    }

//    void Start()
//    {
//        Stopwatch stopwatch = new Stopwatch();
//        stopwatch.Start();

//        // 生成棋盘并统计不同颜色不同形状的四格方块的个数
//        ulong boardBitmap = GenerateRandomBoard(8, 8);
//        Dictionary<string, int> blockCounts = CountBlocks(boardBitmap);

//        stopwatch.Stop();
//        TimeSpan elapsed = stopwatch.Elapsed;

//        // 输出统计结果和计时
//        foreach (KeyValuePair<string, int> count in blockCounts)
//        {
//            UnityEngine.Debug.Log(count.Key + "：" + count.Value + "个，用时：" + GetElapsedTimeMilliseconds(elapsed));
//        }
//    }

//    ulong GenerateRandomBoard(int rows, int cols)
//    {
//        ulong boardBitmap = 0;
//        System.Random random = new System.Random();

//        for (int i = 0; i < rows; i++)
//        {
//            for (int j = 0; j < cols; j++)
//            {
//                int value = random.Next(s_CellPrefabs.Length); // 随机选择预制体的索引
//                int index = i * cols + j;
//                ulong bit = (ulong)value << index;
//                boardBitmap |= bit;

//                // 创建单元格游戏对象并进行位置设置
//                GameObject cell = Instantiate(s_CellPrefabs[value], new Vector3(j - (cols - 1) / 2f, (rows - 1) / 2f - i, 0), Quaternion.identity);
//                cell.transform.SetParent(boardContainer);
//            }
//        }

//        return boardBitmap;
//    }

//    Dictionary<string, int> CountBlocks(ulong boardBitmap)
//    {
//        Dictionary<string, int> blockCounts = new Dictionary<string, int>();

//        foreach (GameObject shapePrefab in s_CellPrefabs)
//        {
//            int[,] block = GetBlock(shapePrefab);

//            ulong rotatedBitmap = GetBlockBitmap(block);
//            int count = CountOccurrences(boardBitmap, rotatedBitmap);

//            string blockType = GetBlockType(block);
//            string key = blockType;

//            if (blockCounts.ContainsKey(key))
//            {
//                blockCounts[key] += count;
//            }
//            else
//            {
//                blockCounts[key] = count;
//            }
//        }

//        return blockCounts;
//    }

//    int[,] GetBlock(GameObject shapePrefab)
//    {
//        int[,] shape = null;

//        if (shapePrefab == cellPrefabs[0])
//        {
//            // I-shaped block
//            shape = new int[,] {
//            {1, 1, 1, 1}
//        };
//        }
//        else if (shapePrefab == cellPrefabs[1])
//        {
//            // J-shaped block
//            shape = new int[,] {
//            {1, 0, 0},
//            {1, 1, 1}
//        };
//        }
//        else if (shapePrefab == cellPrefabs[2])
//        {
//            // L-shaped block
//            shape = new int[,] {
//            {0, 0, 1},
//            {1, 1, 1}
//        };
//        }
//        else if (shapePrefab == cellPrefabs[3])
//        {
//            // O-shaped block
//            shape = new int[,] {
//            {1, 1},
//            {1, 1}
//        };
//        }
//        else if (shapePrefab == cellPrefabs[4])
//        {
//            // S-shaped block
//            shape = new int[,] {
//            {0, 1, 1},
//            {1, 1, 0}
//        };
//        }
//        else if (shapePrefab == cellPrefabs[5])
//        {
//            // T-shaped block
//            shape = new int[,] {
//            {0, 1, 0},
//            {1, 1, 1}
//        };
//        }
//        else if (shapePrefab == cellPrefabs[6])
//        {
//            // Z-shaped block
//            shape = new int[,] {
//            {1, 1, 0},
//            {0, 1, 1}
//        };
//        }

//        return shape;
//    }

//    ulong GetBlockBitmap(int[] block)
//    {
//        ulong bitmap = 0;

//        for (int i = 0; i < block.Length; i++)
//        {
//            ulong bit = (ulong)block[i] << i;
//            bitmap |= bit;
//        }

//        return bitmap;
//    }

//    ulong GetColorBitmap(Color color)
//    {
//        ulong colorBitmap = 0;

//        foreach (GameObject prefab in s_CellPrefabs)
//        {
//            if (prefab.GetComponent<SpriteRenderer>().color == color)
//            {
//                int index = Array.IndexOf(s_CellPrefabs, prefab);
//                colorBitmap = (ulong)index << 60;
//                break;
//            }
//        }

//        return colorBitmap;
//    }

//    int CountOccurrences(ulong boardBitmap, ulong targetBitmap, ulong colorBitmap)
//    {
//        int count = 0;

//        while (boardBitmap != 0)
//        {
//            if ((boardBitmap & targetBitmap) == targetBitmap && (boardBitmap & colorBitmap) == colorBitmap)
//            {
//                count++;
//            }

//            boardBitmap >>= 1;
//        }

//        return count;
//    }

//    string GetBlockType(int[,] block)
//    {
//        string blockType = "";

//        // I型方块
//        if (Array2DEquals(block, BlockShapes.Shapes[0]))
//        {
//            blockType = "I型";
//        }
//        // J型方块
//        else if (Array2DEquals(block, BlockShapes.Shapes[1]))
//        {
//            blockType = "J型";
//        }
//        // L型方块
//        else if (Array2DEquals(block, BlockShapes.Shapes[2]))
//        {
//            blockType = "L型";
//        }
//        // O型方块
//        else if (Array2DEquals(block, BlockShapes.Shapes[3]))
//        {
//            blockType = "O型";
//        }
//        // S型方块
//        else if (Array2DEquals(block, BlockShapes.Shapes[4]))
//        {
//            blockType = "S型";
//        }
//        // T型方块
//        else if (Array2DEquals(block, BlockShapes.Shapes[5]))
//        {
//            blockType = "T型";
//        }
//        // Z型方块
//        else if (Array2DEquals(block, BlockShapes.Shapes[6]))
//        {
//            blockType = "Z型";
//        }

//        return blockType;
//    }

//    bool Array2DEquals(int[,] array1, int[,] array2)
//    {
//        int rows1 = array1.GetLength(0);
//        int cols1 = array1.GetLength(1);
//        int rows2 = array2.GetLength(0);
//        int cols2 = array2.GetLength(1);

//        if (rows1 != rows2 || cols1 != cols2)
//            return false;

//        for (int i = 0; i < rows1; i++)
//        {
//            for (int j = 0; j < cols1; j++)
//            {
//                if (array1[i, j] != array2[i, j])
//                    return false;
//            }
//        }

//        return true;
//    }

//    string GetColorName(Color color)
//    {
//        if (color == Color.red)
//        {
//            return "红色";
//        }
//        else if (color == Color.green)
//        {
//            return "绿色";
//        }
//        else if (color == Color.blue)
//        {
//            return "蓝色";
//        }
//        else if (color == Color.yellow)
//        {
//            return "黄色";
//        }
//        else
//        {
//            return "未知颜色";
//        }
//    }

//    string GetElapsedTimeMilliseconds(TimeSpan time)
//    {
//        return time.TotalMilliseconds.ToString("F2") + "毫秒";
//    }
//}


using UnityEngine;
using System;
using System.Diagnostics;
using System.Collections.Generic;

public class BlockCount : MonoBehaviour
{
    public GameObject[] cellPrefabs; // 在 Inspector 中拖拽四种颜色的预制体到这个数组中
    public Transform boardContainer; // 棋盘容器的引用，将单元格作为其子对象

    private static GameObject[] s_CellPrefabs; // 静态的预制体引用

    void Awake()
    {
        // 将静态的预制体引用设置为实例字段的值
        s_CellPrefabs = cellPrefabs;
    }

    void Start()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // 生成棋盘并统计不同颜色不同形状的四格方块的个数
        ulong boardBitmap = GenerateRandomBoard(8, 8);
        Dictionary<string, int> blockCounts = CountBlocks(boardBitmap);

        stopwatch.Stop();
        TimeSpan elapsed = stopwatch.Elapsed;

        // 输出统计结果和计时
        foreach (KeyValuePair<string, int> count in blockCounts)
        {
            UnityEngine.Debug.Log(count.Key + "：" + count.Value + "个，用时：" + GetElapsedTimeMilliseconds(elapsed));
        }
    }

    ulong GenerateRandomBoard(int rows, int cols)
    {
        ulong boardBitmap = 0;
        System.Random random = new System.Random();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int value = random.Next(s_CellPrefabs.Length); // 随机选择预制体的索引
                int index = i * cols + j;
                ulong bit = (ulong)value << index;
                boardBitmap |= bit;

                // 创建单元格游戏对象并进行位置设置
                GameObject cell = Instantiate(s_CellPrefabs[value], new Vector3(j - (cols - 1) / 2f, (rows - 1) / 2f - i, 0), Quaternion.identity);
                cell.transform.SetParent(boardContainer);
            }
        }

        return boardBitmap;
    }

    Dictionary<string, int> CountBlocks(ulong boardBitmap)
    {
        Dictionary<string, int> blockCounts = new Dictionary<string, int>();

        // 遍历所有预制体组合
        for (int i = 0; i < s_CellPrefabs.Length; i++)
        {
            GameObject prefab = s_CellPrefabs[i];
            Color color = prefab.GetComponent<SpriteRenderer>().color;
            string colorName = GetColorName(color);

            // 统计不同形状的四格方块的个数
            int[][] blockShapes = GetBlockShapes(i);
            for (int j = 0; j < blockShapes.Length; j++)
            {
                int[] block = blockShapes[j];

                // 统计当前颜色和形状的四格方块的个数
                ulong rotatedBitmap = GetBlockBitmap(block);
                ulong colorBitmap = GetColorBitmap(color);

                int count = CountOccurrences(boardBitmap, rotatedBitmap, colorBitmap);

                string blockType = GetBlockType(block);
                string key = colorName + " " + blockType;

                if (blockCounts.ContainsKey(key))
                {
                    blockCounts[key] += count;
                }
                else
                {
                    blockCounts[key] = count;
                }
            }
        }

        return blockCounts;
    }

    int[][] GetBlockShapes(int blockIndex)
    {
        int[][] blockShapes = new int[][]
        {
        new int[] {1, 1, 1, 1},   // I型方块
        new int[] {1, 1, 1, 0},   // J型方块
        new int[] {1, 1, 0, 0},   // L型方块
        new int[] {1, 1, 0, 0},   // O型方块
        new int[] {0, 1, 1, 1},   // S型方块
        new int[] {1, 1, 1, 0},   // T型方块
        new int[] {1, 1, 0, 1}    // Z型方块
        };

        int[][] shapes = new int[blockShapes.Length][];

        for (int i = 0; i < blockShapes.Length; i++)
        {
            shapes[i] = RotateBlock(blockShapes[i], blockIndex);
        }

        return shapes;
    }

    int[] RotateBlock(int[] block, int blockIndex)
    {
        int rotations = (blockIndex == 0) ? 2 : 4; // I型方块有两个旋转状态，其余方块有四个旋转状态
        int[] rotatedBlock = new int[block.Length];

        Array.Copy(block, rotatedBlock, block.Length);

        for (int r = 0; r < rotations; r++)
        {
            rotatedBlock = RotateClockwise(rotatedBlock);
        }

        return rotatedBlock;
    }

    int[] RotateClockwise(int[] block)
    {
        int size = (int)Mathf.Sqrt(block.Length);
        int[] rotatedBlock = new int[block.Length];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                rotatedBlock[j * size + size - 1 - i] = block[i * size + j];
            }
        }

        return rotatedBlock;
    }

    ulong GetBlockBitmap(int[] block)
    {
        ulong bitmap = 0;

        for (int i = 0; i < block.Length; i++)
        {
            if (block[i] != 0)
            {
                ulong bit = 1UL << i;
                bitmap |= bit;
            }
        }

        return bitmap;
    }

    ulong GetColorBitmap(Color color)
    {
        ulong colorBitmap = 0;

        for (int i = 0; i < s_CellPrefabs.Length; i++)
        {
            if (s_CellPrefabs[i].GetComponent<SpriteRenderer>().color == color)
            {
                colorBitmap = (ulong)i << 60;
                break;
            }
        }

        return colorBitmap;
    }

    int CountOccurrences(ulong boardBitmap, ulong targetBitmap, ulong colorBitmap)
    {
        int count = 0;

        while (boardBitmap != 0)
        {
            if ((boardBitmap & targetBitmap) == targetBitmap && (boardBitmap & colorBitmap) == colorBitmap)
            {
                count++;
            }

            boardBitmap >>= 1;
        }

        return count;
    }

    string GetBlockType(int[] block)
    {
        int[][] blockShapes = new int[][]
        {
        new int[] {1, 1, 1, 1},   // I型方块
        new int[] {1, 1, 1, 0},   // J型方块
        new int[] {1, 1, 0, 0},   // L型方块
        new int[] {1, 1, 0, 0},   // O型方块
        new int[] {0, 1, 1, 1},   // S型方块
        new int[] {1, 1, 1, 0},   // T型方块
        new int[] {1, 1, 0, 1}    // Z型方块
        };

        string[] blockTypes = new string[]
        {
        "I", "J", "L", "O", "S", "T", "Z"
        };

        for (int i = 0; i < blockShapes.Length; i++)
        {
            if (ArraysAreEqual(blockShapes[i], block))
            {
                return blockTypes[i];
            }
        }

        return "Unknown";
    }

    bool ArraysAreEqual(int[] arr1, int[] arr2)
    {
        if (arr1.Length != arr2.Length)
        {
            return false;
        }

        for (int i = 0; i < arr1.Length; i++)
        {
            if (arr1[i] != arr2[i])
            {
                return false;
            }
        }

        return true;
    }

    string GetColorName(Color color)
    {
        for (int i = 0; i < s_CellPrefabs.Length; i++)
        {
            if (s_CellPrefabs[i].GetComponent<SpriteRenderer>().color == color)
            {
                return s_CellPrefabs[i].name;
            }
        }

        return "Unknown";
    }

    string GetElapsedTimeMilliseconds(TimeSpan elapsed)
    {
        return Mathf.Round((float)elapsed.TotalMilliseconds) + "ms";
    }
}