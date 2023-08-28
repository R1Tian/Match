using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class FSMManager
{
    private static Dictionary<int, FSMGeneral> FindMachine;

    public static void OnInitMachine() {
        FindMachine = new Dictionary<int, FSMGeneral>() {
            { 1, new EasyMonster() },
            { 2, new PhantomWraithwolf() },
            { 3, new BloodfangBerserker() },
            { 7, new EvilBoneWarlock() },
            { 9, new DarkNightmares() },
        };
    }

    public static FSMGeneral FindStateMachine(int key) {
        FSMGeneral res = null;
        FindMachine.TryGetValue(key, out res);
        return res;
    }
}
