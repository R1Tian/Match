//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Diagnostics;

//public class Count : MonoBehaviour
//{
//    static void Main(/*string[] args*/)
//    {
        
//        }
    

//    static ulong GenerateRandomBoard(int rows, int cols)
//    {
//        ulong boardBitmap = 0;
//        System.Random random = new System.Random();

//        for (int i = 0; i < rows; i++)
//        {
//            for (int j = 0; j < cols; j++)
//            {
//                int value = random.Next(2); // 随机生成0或1
//                int index = i * cols + j;
//                ulong bit = (ulong)value << index;
//                boardBitmap |= bit;
//            }
//        }

//        return boardBitmap;
//    }

//    static int CountBlock(int[,] board, int[] block)
//    {
//        int rows = board.GetLength(0);
//        int cols = board.GetLength(1);
//        int count = 0;

//        // 将棋盘和方块转换为位图
//        ulong boardBitmap = GetBoardBitmap(board);
//        ulong blockBitmap = GetBlockBitmap(block);

//        // 对棋盘进行滑动窗口扫描
//        for (int i = 0; i <= rows - block.Length; i++)
//        {
//            for (int j = 0; j <= cols - block.Length; j++)
//            {
//                // 获取当前窗口的位图
//                ulong windowBitmap = GetWindowBitmap(boardBitmap, i, j, block.Length);

//                // 检查窗口位图与方块位图是否匹配
//                if ((windowBitmap & blockBitmap) == blockBitmap)
//                {
//                    count++;
//                }
//            }
//        }

//        return count;
//    }

//    static ulong GetBoardBitmap(int[,] board)
//    {
//        int rows = board.GetLength(0);
//        int cols = board.GetLength(1);
//        ulong bitmap = 0;

//        for (int i = 0; i < rows; i++)
//        {
//            for (int j = 0; j < cols; j++)
//            {
//                if (board[i, j] != 0)
//                {
//                    int index = i * cols + j;
//                    ulong bit = 1UL << index;
//                    bitmap |= bit;
//                }
//            }
//        }

//        return bitmap;
//    }

//    static ulong GetBlockBitmap(int[] block)
//    {
//        int size = block.Length;
//        ulong bitmap = 0;

//        for (int i = 0; i < size; i++)
//        {
//            if (block[i] != 0)
//            {
//                ulong bit = 1UL << i;
//                bitmap |= bit;
//            }
//        }

//        return bitmap;
//    }

//    static ulong GetWindowBitmap(ulong bitmap, int row, int col, int size)
//    {
//        ulong windowBitmap = 0;
//        int shift = col;

//        for (int i = row; i < row + size; i++)
//        {
//            ulong rowBitmap = (bitmap >> (shift + (i * size))) & ((1UL << size) - 1);
//            windowBitmap |= rowBitmap << (i * size);
//        }

//        return windowBitmap;
//    }

//    static int[] RotateBlock(int[] block)
//    {
//        int size = block.Length;
//        int[] rotatedBlock = new int[size];

//        for (int i = 0; i < size; i++)
//        {
//            int row = i / size;
//            int col = i % size;
//            int rotatedRow = col;
//            int rotatedCol = size - 1 - row;
//            int rotatedIndex = rotatedRow * size + rotatedCol;
//            rotatedBlock[rotatedIndex] = block[i];
//        }

//        return rotatedBlock;
//    }



//// Start is called before the first frame update
//void Start()
//    {
//        int[,] board = new int[10, 10]; // 游戏棋盘，假设为10x10的大小

//        int[][] blocks = new int[][] {
//            new int[] { 1, 1, 1, 1 },       // I型方块
//            new int[] { 1, 1, 1, 0 },       // J型方块
//            new int[] { 1, 1, 1, 0 },       // L型方块
//            new int[] { 1, 1, 0, 0 },       // O型方块
//            new int[] { 0, 1, 1, 1 },       // S型方块
//            new int[] { 1, 1, 1, 0 },       // T型方块
//            new int[] { 1, 1, 0, 1 }        // Z型方块
//        };

//        Stopwatch stopwatch = new Stopwatch();

//        for (int i = 0; i < blocks.Length; i++)
//        {
//            int count = 0;

//            stopwatch.Restart();

//            int[] block = blocks[i];
//            int rotations = (i == 0) ? 2 : 4;  // I型方块有两个旋转状态，其余方块有四个旋转状态

//            for (int r = 0; r < rotations; r++)
//            {
//                count += CountBlock(board, block);
//                block = RotateBlock(block);
//            }

//            stopwatch.Stop();
//            UnityEngine.Debug.Log("Block " + (i + 1) + " Count: " + count + ", Time: " + stopwatch.ElapsedMilliseconds + "ms");

//            //var board = GenerateRandomBoard(10, 10);
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
