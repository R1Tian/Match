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
//                int value = random.Next(2); // �������0��1
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

//        // �����̺ͷ���ת��Ϊλͼ
//        ulong boardBitmap = GetBoardBitmap(board);
//        ulong blockBitmap = GetBlockBitmap(block);

//        // �����̽��л�������ɨ��
//        for (int i = 0; i <= rows - block.Length; i++)
//        {
//            for (int j = 0; j <= cols - block.Length; j++)
//            {
//                // ��ȡ��ǰ���ڵ�λͼ
//                ulong windowBitmap = GetWindowBitmap(boardBitmap, i, j, block.Length);

//                // ��鴰��λͼ�뷽��λͼ�Ƿ�ƥ��
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
//        int[,] board = new int[10, 10]; // ��Ϸ���̣�����Ϊ10x10�Ĵ�С

//        int[][] blocks = new int[][] {
//            new int[] { 1, 1, 1, 1 },       // I�ͷ���
//            new int[] { 1, 1, 1, 0 },       // J�ͷ���
//            new int[] { 1, 1, 1, 0 },       // L�ͷ���
//            new int[] { 1, 1, 0, 0 },       // O�ͷ���
//            new int[] { 0, 1, 1, 1 },       // S�ͷ���
//            new int[] { 1, 1, 1, 0 },       // T�ͷ���
//            new int[] { 1, 1, 0, 1 }        // Z�ͷ���
//        };

//        Stopwatch stopwatch = new Stopwatch();

//        for (int i = 0; i < blocks.Length; i++)
//        {
//            int count = 0;

//            stopwatch.Restart();

//            int[] block = blocks[i];
//            int rotations = (i == 0) ? 2 : 4;  // I�ͷ�����������ת״̬�����෽�����ĸ���ת״̬

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
