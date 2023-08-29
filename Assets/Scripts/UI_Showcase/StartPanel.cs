using Map;
using UI_Showcase;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    public Button nextBtn;
    public GameObject boardBG;
    GameObject gameObject;
    public override void OnInit()
    {
        //skinPath = "Start";
        layer = PanelManager.Layer.panel;
        gameObject = Instantiate(boardBG);
    }

    public override void OnShow(params object[] objects)
    {

        nextBtn.onClick.AddListener(() =>
        {
            //PanelManager.Open<BattlePanel>("BattleField");
            //PanelManager.Open<ChooseBattleCardPanel>("ChooseBattleCardPanel");
            PlayerState.instance.OnSingletonInit();
            CardManager.OnInitCardDatabase();
            PanelManager.Open<MapPanel>("Map");
            InitMapSetting.Init();
            //PanelManager.MapPanel.SetActive(true);
            //GameObject.Find("BoardBG").SetActive(false);
            Destroy(gameObject);
            Close();
        });

        
    }

    public override void OnClose()
    {
        base.OnClose();
    }
}
