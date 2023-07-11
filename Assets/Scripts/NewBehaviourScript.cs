using UnityEngine;

public class BlockShapesVisualizer : MonoBehaviour
{
    public GameObject cellPrefab; // 方块单元格的预制体
    public Transform container; // 容器的引用，用于将单元格作为其子对象
    public float spacing = 1f; // 方块之间的间距

    void Start()
    {
        VisualizeBlockShapes();
    }

    void VisualizeBlockShapes()
    {
        int[,] lBlockShapes = {
            {1, 0, 0, 0},   // L型方块
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

            // 顺时针旋转 L 型方块
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