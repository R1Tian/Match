using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TetrominoType
{
    LType,
    JType,
    TType,
    OType,
    IType,
    SType,
    ZType,
}

public class Tetromino
{
    public string Name { get; private set; }
    public int[][] Shape { get; private set; }
    public int Index { get; private set; }

    public TetrominoType TetrominoType { get; private set; }

    public Tetromino(string name, int[][] shape, int index, TetrominoType tetrominoType)
    {
        Name = name;
        Shape = shape;
        Index = index;
        TetrominoType = tetrominoType;
    }
}