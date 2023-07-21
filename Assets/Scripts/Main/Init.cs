using UnityEngine;

public class Init : MonoBehaviour
{
    private void Awake()
    {
        PanelManager.Init();
        PanelManager.Open<StartPanel>();
    }
}
