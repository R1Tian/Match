using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class FSMManager
{
    private static Dictionary<int, FSMGeneral> FindMachine;

    public void OnInitMachine() {
        FindMachine = new Dictionary<int, FSMGeneral>() {
            { 1, new EasyMonster()}
        };
    }

    public static FSMGeneral FindStateMachine(int key) {
        FSMGeneral res = null;
        FindMachine.TryGetValue(key, out res);
        return res;
    }
    
    
}
