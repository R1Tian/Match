using UnityEngine;





public class TetrisStats2 : MonoBehaviour
{
    public GameObject cubePrefab;
    public int boardSize = 8; // �Զ������̴�С
    private Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow };
    private string[] colorNames = { "��ɫ", "��ɫ", "��ɫ", "��ɫ" };
    private Tetromino[] tetrominoes;
    private string[] tetrominoNames = { "L��", "J��", "O��", "I��", "T��", "S��", "Z��" };
    private int[,] board;
    private int[] tetrominoCounts;

    public Transform boardContainer;

    //private void Start()
    //{
    //    float startTime = Time.realtimeSinceStartup;

    //    // ��ʼ�����̺ͼ�����
    //    board = new int[boardSize, boardSize];
    //    tetrominoCounts = new int[colors.Length * tetrominoNames.Length];

    //    // ��ʼ������ͼ������
    //    tetrominoes = new Tetromino[]
    //    {
    //        new Tetromino("L��",new int[][] // L�ͷ���
    //        {
    //            new int[] { 1, 0 },
    //            new int[] { 1, 0 },
    //            new int[] { 1, 1 }
    //        },0),

    //        new Tetromino("J��",new int[][] // J�ͷ���
    //        {
    //            new int[] { 1, 0, 0 },
    //            new int[] { 1, 1, 1 }
    //            },1),

    //        new Tetromino("O��",new int[][] // O�ͷ���
    //        {
    //            new int[] { 1, 1 },
    //            new int[] { 1, 1 }
    //        },2),

    //        new Tetromino("I��",new int[][] // I�ͷ���
    //        {
    //            new int[] { 1, 1, 1, 1 }
    //        },3),

    //        new Tetromino("T��",new int[][] // T�ͷ���
    //        {
    //            new int[] { 1, 0 },
    //            new int[] { 1, 1 },
    //            new int[] { 1, 0 }
    //        },4),

    //        new Tetromino("S��",new int[][] // S�ͷ���
    //        {
    //            new int[] { 1, 0 },
    //            new int[] { 1, 1 },
    //            new int[] { 0, 1 }
    //        },5),

    //        new Tetromino("Z��",new int[][] // Z�ͷ���
    //        {
    //            new int[] { 1, 1, 0 },
    //            new int[] { 0, 1, 1 }
    //        },6),
    //    };




    //    // ����������ɫ
    //    GenerateBoardColors();

    //    // ͳ�Ʒ���ͼ�θ���
    //    CountTetrominoes();

    //    // ���ͳ�ƽ��
    //    PrintTetrominoCounts();

    //    // �������ִ��ʱ��


    //    float endTime = Time.realtimeSinceStartup;
    //    float elapsedTime = (endTime - startTime) * 1000f;
    //    Debug.Log("��ʱ��" + elapsedTime.ToString("F2") + " ms");
    //}

    public void OnButtonClick()
    {
        float startTime = Time.realtimeSinceStartup;

        // ��ʼ�����̺ͼ�����
        board = new int[boardSize, boardSize];
        tetrominoCounts = new int[colors.Length * tetrominoNames.Length];

        
        // ��ʼ������ͼ������,��Ҫ��֤��Ԫ�ز�Ϊ0
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
        //���ɵ����̻����һ�����������ת��������Ϊ��ʼ�����������ݵ�״̬
        boardContainer.transform.eulerAngles = new Vector3(0, 0, 90);
        //boardContainer.transform.Rotate(new Vector3(0, 0, 90));

    }
    /// <summary>
    /// �����������ݣ����ظ����̼���������ת��
    /// </summary>
    /// <param name="originalBoard">��������</param>
    /// <returns></returns>
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

    /// <summary>
    /// ����ÿһ��Tetromino���������̵�ÿһ�����ӣ������ĸ�����ת�����̣�����Ƿ����Tetromino��Shape��������ʹ����������ɫ��������״������1
    /// </summary>
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
    /// <summary>
    /// ����Ƿ���Tetromino��Shape������ȼ������ʼ��ΪTetromino��Ԫ�ص�Tetromino��Shape�Ƿ񳬳����̴�С�����������¼�ø��ӵ����ݣ���ɫ��������Ȼ��������Tetromino��Shape�����ݣ����Ϊ1���ͱȶ������϶�Ӧλ�õ����ݣ���ɫ�������Ƿ�����ʼ����ͬ�����ȫ��Ϊ1�����ݶ��ܱȶԳɹ����򷵻�true
    /// </summary>
    /// <param name="board">��������</param>
    /// <param name="tetromino">Ҫ����Tetromino</param>
    /// <param name="startX">�������x����-1</param>
    /// <param name="startY">�������y����-1</param>
    /// <returns></returns>
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
    /// <summary>
    /// �������
    /// </summary>
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
}
