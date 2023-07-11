using UnityEngine;
using System;
using System.Diagnostics;

public class Test : MonoBehaviour
{
    void Start()
    {
        int[][] blocks = new int[][] {
            new int[] { 1, 1, 1, 1 },       // I型方块
            new int[] { 1, 1, 1, 0 },       // J型方块
            new int[] { 1, 1, 1, 0 },       // L型方块
            new int[] { 1, 1, 0, 0 },       // O型方块
            new int[] { 0, 1, 1, 1 },       // S型方块
            new int[] { 1, 1, 1, 0 },       // T型方块
            new int[] { 1, 1, 0, 1 }        // Z型方块
        };

        Stopwatch stopwatch = new Stopwatch();

        for (int i = 0; i < blocks.Length; i++)
        {
            int count = 0;

            stopwatch.Restart();

            int[] block = blocks[i];
            int rotations = (i == 0) ? 2 : 4;  // I型方块有两个旋转状态，其余方块有四个旋转状态

            for (int r = 0; r < rotations; r++)
            {
                count += CountBlock(block);
                block = RotateBlock(block);
            }

            stopwatch.Stop();

            UnityEngine.Debug.Log("Block " + (i + 1) + " Count: " + count + ", Time: " + stopwatch.ElapsedMilliseconds + "ms");
        }
    }

    static int CountBlock(int[] block)
    {
        int rows = 10;
        int cols = 10;
        int count = 0;

        ulong boardBitmap = GenerateRandomBoard(rows, cols);
        ulong blockBitmap = GetBlockBitmap(block);

        for (int i = 0; i <= rows - block.Length; i++)
        {
            for (int j = 0; j <= cols - block.Length; j++)
            {
                ulong windowBitmap = GetWindowBitmap(boardBitmap, i, j, block.Length);

                if ((windowBitmap & blockBitmap) == blockBitmap)
                {
                    count++;
                }
            }
        }

        return count;
    }

    static ulong GenerateRandomBoard(int rows, int cols)
    {
        ulong boardBitmap = 0;
        System.Random random = new System.Random();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int value = random.Next(2); // 随机生成0或1
                int index = i * cols + j;
                ulong bit = (ulong)value << index;
                boardBitmap |= bit;
            }
        }

        return boardBitmap;
    }

    static ulong GetBlockBitmap(int[] block)
    {
        int size = block.Length;
        ulong bitmap = 0;

        for (int i = 0; i < size; i++)
        {
            if (block[i] != 0)
            {
                ulong bit = 1UL << i;
                bitmap |= bit;
            }
        }
        return bitmap;
    }

    static ulong GetWindowBitmap(ulong bitmap, int row, int col, int size)
    {
        ulong windowBitmap = 0;
        int shift = col;

        for (int i = row; i < row + size; i++)
        {
            ulong rowBitmap = (bitmap >> (shift + (i * size))) & ((1UL << size) - 1);
            windowBitmap |= rowBitmap << ((i - row) * size);
        }

        return windowBitmap;
    }

    static int[] RotateBlock(int[] block)
    {
        int size = Mathf.RoundToInt(Mathf.Sqrt(block.Length));
        int[] rotatedBlock = new int[block.Length];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int rotatedRow = j;
                int rotatedCol = size - 1 - i;
                int rotatedIndex = rotatedRow * size + rotatedCol;
                rotatedBlock[rotatedIndex] = block[i * size + j];
            }
        }

        return rotatedBlock;
    }
}