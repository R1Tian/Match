using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObjectOnAnimatonComplete : MonoBehaviour
{
    // Start is called before the first frame update
    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
