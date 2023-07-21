using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager
{
    public static GameObject LoadPrefeb(string path)
    {
        return Resources.Load<GameObject>(path);
    }
}
