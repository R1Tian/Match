using UnityEngine;

public class BlockShapesVisualizer : MonoBehaviour
{
    public GameObject cellPrefab; // ���鵥Ԫ���Ԥ����
    public Transform container; // ���������ã����ڽ���Ԫ����Ϊ���Ӷ���
    public float spacing = 1f; // ����֮��ļ��

    void Start()
    {
        VisualizeBlockShapes();
    }

    void VisualizeBlockShapes()
    {
        int[,] lBlockShapes = {
            {1, 0, 0, 0},   // L�ͷ���
            {1, 1, 1, 0},
            {0, 0, 0, 0},
            {0, 0, 0, 0}
        };

        int numRows = lBlockShapes.GetLength(0);
        int numCols = lBlockShapes.GetLength(1);

        float totalSpacing = spacing * (numCols - 1);
        float startY = (numRows - 1) * (1 + spacing) / 2f;

        for (int rotation = 0; rotation < 4; rotation++)
        {
            float offsetX = 0f;
            float currentY = startY;

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    if (lBlockShapes[i, j] == 1)
                    {
                        Vector3 position = new Vector3(offsetX, currentY, 0);
                        Instantiate(cellPrefab, position, Quaternion.identity, container);
                    }

                    offsetX += 1f + spacing;
                }

                offsetX = 0f;
                currentY -= 1f + spacing;
            }

            // ˳ʱ����ת L �ͷ���
            lBlockShapes = RotateBlock(lBlockShapes);
        }
    }

    int[,] RotateBlock(int[,] block)
    {
        int numRows = block.GetLength(0);
        int numCols = block.GetLength(1);
        int[,] rotatedBlock = new int[numCols, numRows];

        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                rotatedBlock[j, numRows - 1 - i] = block[i, j];
            }
        }

        return rotatedBlock;
    }
}