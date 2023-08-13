using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class FSMManager : MonoBehaviour
{
    public enum StateEnum
    {
        
    }
    
    private Dictionary<string, FSM<StateEnum>> fsmDictionary = new Dictionary<string, FSM<StateEnum>>();

    public static void OnInitFSMManager()
    {

    }
    
    
}
