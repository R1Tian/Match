using QFramework;
using System.Collections.Generic;
using UnityEngine;

//public class Task
//{
//    public string Name { get; private set; }
//    public int RequiredCount { get; private set; }
//    public TaskType Type { get; private set; }
//    public Color? BlockColor { get; private set; }
//    public Tetromino? Tetromino { get; private set; }
//    public int TaskCount { get; set; }
//    public System.Action TaskEffect { get; private set; }

//    public Task(string name, int requiredCount, TaskType type, Color? blockColor = null, Tetromino? tetromino = null, System.Action taskEffect = null)
//    {
//        Name = name;
//        RequiredCount = requiredCount;
//        Type = type;
//        BlockColor = blockColor;
//        Tetromino = tetromino;
//        TaskEffect = taskEffect;
//        TaskCount = 0;
//    }
//}
//public enum TaskType
//{
//    Color,
//    Tetromino,
//    ColorAndTetromino,
//    Turn,
//    // Add more task types as needed
//}

public class EnemyState : ISingleton
{
    public static EnemyState instance
    {
        get { return SingletonProperty<EnemyState>.Instance; }
    }
    private EnemyState() { }

    

    #region Attribute
    private int EnemyMaxHP;
    private int EnemyHP;
    private int AttackBuffLayer;
    private int DefenceBuffLayer;
    private int WeakBuffLayer;
    private int FragileBuffLayer;
    private int Damage;
    //private List<Task> Tasks = new List<Task>();

    #endregion
    public void OnSingletonInit()
    {
        EnemyMaxHP = 100;
        EnemyHP = 100;
        AttackBuffLayer = 0;
        DefenceBuffLayer = 0;
        WeakBuffLayer = 0;
        FragileBuffLayer = 0;
        //InitTasks();

    }

    public void HealHealth(int hp) {
        EnemyHP += hp;
    }
    public void TakeDamge(int hp) {
        EnemyHP -= hp;
    }
    public int GetHP() {
        return EnemyHP;
    }
    public int GetMaxHP()
    {
        return EnemyMaxHP;
    }
    public void AddAttackBuff(int layer) {
        AttackBuffLayer += layer;
    }
    public void DropAttackBuff(int layer) {
        AttackBuffLayer -= layer;
    }
    public int GetAttackBuff() {
        return AttackBuffLayer;
    }
    public void AddDefenceBuffLayer(int layer) {
        DefenceBuffLayer += layer;
    }
    public void DropDefenceBuffLayer(int layer) {
        DefenceBuffLayer -= layer;
    }
    public int GetDefenceBuffLayer() {
        return DefenceBuffLayer;
    }
    public void AddWeakBuffLayer(int layer) {
        WeakBuffLayer += layer;
    }
    public void DropWeakBuffLayer(int layer) {
        WeakBuffLayer -= layer;
    }
    public int GetWeakBuffLayer() {
        return WeakBuffLayer;
    }
    public void AddFragileBuffLayer(int layer) {
        FragileBuffLayer += layer;
    }
    public void DropFragileBuffLayer(int layer) {
        FragileBuffLayer -= layer;
    }
    public int GetFragileBuffLayer() {
        return FragileBuffLayer;
    }
    public void AddDamage(int damage) {
        Damage += damage;
    }

    public int GetDamageBuff() {
        int res=AttackBuffLayer;
        AttackBuffLayer=0;
        return res;
    }

    //private void InitTasks()
    //{
    //    Task task1 = new Task("Task 1: Eliminate 5 red T-type", 5, TaskType.ColorAndTetromino, Color.red, Main.instance.GetTetShape("T型"), () =>
    //    {
    //        Debug.Log("Task 1 completed! Deal 5 damage to the player.");
    //        PlayerState.instance.TakeDamge(5);
    //    });

    //    Task task2 = new Task("Task 2: Eliminate 5 green T-type", 5, TaskType.ColorAndTetromino, Color.green, Main.instance.GetTetShape("T型"), () =>
    //    {
    //        Debug.Log("Task 2 completed! Heal 5 HP for the enemy.");
    //        HealHealth(5);
    //    });

    //    Tasks.Add(task1);
    //    Tasks.Add(task2);
    //}

    //public void IncrementTaskCount(Task task, Color? blockColor = null, Tetromino? tetromino = null)
    //{
    //    if (Tasks.Contains(task))
    //    {
    //        if (task.Type == TaskType.Color)
    //        {
    //            if (blockColor.HasValue && blockColor.Value == task.BlockColor)
    //            {
    //                task.TaskCount++;
    //                if (task.TaskCount >= task.RequiredCount)
    //                {
    //                    task.TaskCount = 0;
    //                    task.TaskEffect?.Invoke();
    //                }
    //            }
    //        }
    //        else if (task.Type == TaskType.Tetromino)
    //        {
    //            if (tetromino.HasValue && tetromino.Value == task.TetrominoType)
    //            {
    //                task.TaskCount++;
    //                if (task.TaskCount >= task.RequiredCount)
    //                {
    //                    task.TaskCount = 0;
    //                    task.TaskEffect?.Invoke();
    //                }
    //            }
    //        }
    //        else if (task.Type == TaskType.ColorAndTetromino)
    //        {
    //            if (blockColor.HasValue && tetrominoType.HasValue && blockColor.Value == task.BlockColor && tetrominoType.Value == task.TetrominoType)
    //            {
    //                task.TaskCount++;
    //                if (task.TaskCount >= task.RequiredCount)
    //                {
    //                    task.TaskCount = 0;
    //                    task.TaskEffect?.Invoke();
    //                }
    //            }
    //        }
    //        else if (task.Type == TaskType.Turn)
    //        {
    //            task.TaskCount++;
    //            if (task.TaskCount >= task.RequiredCount)
    //            {
    //                task.TaskCount = 0;
    //                task.TaskEffect?.Invoke();
    //            }
    //        }
    //    }
    //}


    public void Dispose(){SingletonProperty<EnemyState>.Dispose();}



}
