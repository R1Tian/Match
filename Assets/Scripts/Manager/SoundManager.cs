using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    private static string SE_Path = "resources://Audio/SE/";
    private static string BGM_Path = "resources://Audio/BGM/";

    public static string GetSE_Path()
    {
        return SE_Path;
    } 
    
    public static string GetBGM_Path()
    {
        return BGM_Path;
    }
}
