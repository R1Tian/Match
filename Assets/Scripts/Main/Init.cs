using UnityEngine;

public class Init : MonoBehaviour
{
    private void Awake()
    {
        PanelManager.Init();
        CardManager.OnInitCardDatabase();
        FSMManager.OnInitMachine();
        PanelManager.Open<StartPanel>("Start");

    }
}
