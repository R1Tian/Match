using UnityEngine;

public class Init : MonoBehaviour
{
    private void Awake()
    {
        PanelManager.Init();
        EnemyState.instance.OnSingletonInit();
        CardManager.OnInitCardDatabase();
        FSMManager.OnInitMachine();
        PanelManager.Open<StartPanel>("Start");
        //PanelManager.Open<ShopPanel>("ShopPanel");
    }
}
