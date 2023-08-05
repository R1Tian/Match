using UnityEngine;

public class Init : MonoBehaviour
{
    private void Awake()
    {
        PanelManager.Init();
        CardManager.OnInitCardDatabase();
        PanelManager.Open<StartPanel>("Start");
    }
}
