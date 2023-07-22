using UnityEngine.UI;

public class StartPanel : BasePanel
{
    public Button nextBtn;

    public override void OnInit()
    {
        //skinPath = "Start";
        layer = PanelManager.Layer.panel;
    }

    public override void OnShow(params object[] objects)
    {

        nextBtn.onClick.AddListener(() =>
        {
            PanelManager.Open<BattlePanel>("BattleField");
            Close();
        });
    }

    public override void OnClose()
    {
        base.OnClose();
    }
}
