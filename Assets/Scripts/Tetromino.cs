using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino
{
    public string Name { get; private set; }
    public int[][] Shape { get; private set; }
    public int Index { get; private set; }

    public Tetromino(string name, int[][] shape, int index = 0)
    {
        Name = name;
        Shape = shape;
        Index = index;
    }
}