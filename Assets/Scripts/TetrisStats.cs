using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class TetrisStats : MonoBehaviour
{
    public GameObject cubePrefab;
    public int boardSize = 8; // �Զ������̴�С
    private Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow };
    private string[] colorNames = { "��ɫ", "��ɫ" ,"��ɫ", "��ɫ" };
    private Tetromino[] tetrominoes;
    private string[] tetrominoNames = { "L��", "J��", "O��", "I��", "T��", "S��", "Z��" };
    private int[,] board;
    private int[] tetrominoCounts;

    public Transform boardContainer;

    
    List<Vector2Int> matchedBlocks;

    [ShowInInspector]
    List<Vector2Int> matchedBlocksInInspector;

    public void Start()
    {
        board = new int[boardSize, boardSize];
        InitializeBoard();

        matchedBlocks = new List<Vector2Int>();
    }


    public void OnButtonClick()
    {
        float startTime = Time.realtimeSinceStartup;

        // ��ʼ�����̺ͼ�����
        //board = new int[boardSize, boardSize];
        tetrominoCounts = new int[colors.Length * tetrominoNames.Length];

        // ��ʼ������ͼ������
        tetrominoes = new Tetromino[]
        {
            new Tetromino("L��",new int[][] // L�ͷ���
            {
                new int[] { 1, 0 },
                new int[] { 1, 0 },
                new int[] { 1, 1 }
            },0),

            new Tetromino("J��",new int[][] // J�ͷ���
            {
                new int[] { 1, 0, 0 },
                new int[] { 1, 1, 1 }
                },1),

            new Tetromino("O��",new int[][] // O�ͷ���
            {
                new int[] { 1, 1 },
                new int[] { 1, 1 }
            },2),

            new Tetromino("I��",new int[][] // I�ͷ���
            {
                new int[] { 1, 1, 1, 1 }
            },3),

            new Tetromino("T��",new int[][] // T�ͷ���
            {
                new int[] { 1, 0 },
                new int[] { 1, 1 },
                new int[] { 1, 0 }
            },4),

            new Tetromino("S��",new int[][] // S�ͷ���
            {
                new int[] { 1, 0 },
                new int[] { 1, 1 },
                new int[] { 0, 1 }
            },5),

            new Tetromino("Z��",new int[][] // Z�ͷ���
            {
                new int[] { 1, 1, 0 },
                new int[] { 0, 1, 1 }
            },6),
        };




        // ����������ɫ
        GenerateBoardColors();

        // ͳ�Ʒ���ͼ�θ���
        CountTetrominoes();

        // ���ͳ�ƽ��
        PrintTetrominoCounts();

        // �������ִ��ʱ��


        float endTime = Time.realtimeSinceStartup;
        float elapsedTime = (endTime - startTime) * 1000f;
        Debug.Log("��ʱ��" + elapsedTime.ToString("F2") + " ms");

        UpdateBoardWithMatches(matchedBlocks);
        ShowMatchedBlocksInInspector();
        //matchedBlocks.Clear();
    }

    public void GenerateBoardColors()
    {
        float xOffset = 0.5f * (boardSize - 1);
        float yOffset = 0.5f * (boardSize - 1);

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                if (board[i, j] == -1) // �������ĸ���
                {
                    int randomColorIndex = Random.Range(0, colors.Length);
                    Color randomColor = colors[randomColorIndex];
                    board[i, j] = randomColorIndex;
                    GameObject cube = Instantiate(cubePrefab, new Vector3(-j + xOffset, -i + yOffset, 0), Quaternion.identity);
                    cube.GetComponent<SpriteRenderer>().color = randomColor;
                    cube.transform.SetParent(boardContainer);
                }
                //else // δ�����ĸ���
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

                        //List<Vector2Int> matchedBlocks = new List<Vector2Int>(); // ʹ�� List ��� HashSet

                        if (CheckTetromino(rotatedBoard[k], tetromino, i, j))
                        {
                            int colorIndex = rotatedBoard[k][i, j];
                            int tetrominoCountIndex = tetromino.Index + colorIndex * tetrominoes.Length;
                            tetrominoCounts[tetrominoCountIndex]++;

                            // ��ƥ��ɹ���λ��ת����ԭʼδ��ת������
                            int originalX = i;
                            int originalY = j;
                            ConvertToOriginalCoordinates(originalX, originalY, k, out int originalXUnrotated, out int originalYUnrotated);


                            // ��ƥ��ɹ��ĸ���λ�ú���״�еĸ���λ�ö���¼����
                            matchedBlocks.Add(new Vector2Int(originalXUnrotated, originalYUnrotated));
                            RecordShapeBlocks(originalX, originalY, tetromino.Shape, matchedBlocks, k);
                        }

                    }
                }
            }
        }
        //O���ظ��������ı�
        for (int i = 2; i < tetrominoCounts.Length; i += 7)
        {
            tetrominoCounts[i] /= 4;
        }
        //I���ظ�����������
        for (int i = 3; i < tetrominoCounts.Length; i += 7)
        {
            tetrominoCounts[i] /= 2;
        }
        //S���ظ�����������
        for (int i = 5; i < tetrominoCounts.Length; i += 7)
        {
            tetrominoCounts[i] /= 2;
        }
        //Z���ظ�����������
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
                    continue; // ���鳬�����̷�Χ����ƥ��
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
                unrotatedX = boardSize - rotatedY - 1;
                unrotatedY = rotatedX;
                break;
            case 2:
                unrotatedX = boardSize - rotatedX - 1;
                unrotatedY = boardSize - rotatedY - 1;
                break;
            case 3:
                unrotatedX = rotatedY;
                unrotatedY = boardSize - rotatedX - 1;
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
            return false; // ���鳬�����̷�Χ����ƥ��
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
                        return false; // ����λ�ò�ƥ��
                    }
                }
            }
        }
        //SWDebug.Log("x" + (startX + 1) + "y" + (startY + 1));
        return true; // ����ƥ��ɹ�
    }

    public void PrintTetrominoCounts()
    {
        for (int i = 0; i < colors.Length; i++)
        {
            for (int j = 0; j < tetrominoNames.Length; j++)
            {
                int tetrominoIndex = i * tetrominoNames.Length + j;
                Debug.Log("��ɫ " + colorNames[i] + " �ķ���ͼ�� " + tetrominoNames[j] + " ���� " + tetrominoCounts[tetrominoIndex] + " ��");
            }
        }
    }

    private void UpdateBoardWithMatches(List<Vector2Int> matchedBlocks)
    {
        foreach (Vector2Int block in matchedBlocks)
        {
            int x = block.x;
            int y = block.y;
            board[x, y] = -1;
        }
    }

    //��ʼ������
    void InitializeBoard()
    {
        // ��ʼ��������ɫ����Ϊ��Чֵ
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                board[i, j] = -1;
            }
        }
    }

    void ShowMatchedBlocksInInspector()
    {
        matchedBlocksInInspector = new List<Vector2Int>();

        foreach (Vector2Int block in matchedBlocks)
        {
            int transformedX = block.x + 1;
            int transformedY = block.y + 1;
            matchedBlocksInInspector.Add(new Vector2Int(transformedX, transformedY));
        }
    }

}
