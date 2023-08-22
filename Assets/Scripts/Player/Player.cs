using UnityEngine;

public class Player : MonoBehaviour
{
    private void Awake()
    {
        PlayerState.instance.OnSingletonInit();
    }
}
