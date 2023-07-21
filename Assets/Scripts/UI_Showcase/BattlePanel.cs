using UnityEngine.UI;

public class BattlePanel : BasePanel
{
    public override void OnInit()
    {
        skinPath = "BattleField";
        layer = PanelManager.Layer.panel;
    }

    
}
