using UnityEngine.UI;

public class MapPanel : BasePanel
{
    public override void OnInit()
    {
        layer = PanelManager.Layer.panel;
        PanelManager.MapPanel = gameObject;
    }

    
}
