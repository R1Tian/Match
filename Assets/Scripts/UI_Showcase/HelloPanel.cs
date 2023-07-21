using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelloPanel : BasePanel
{
    private Button nextBtn;
    private Button closeBtn;

    public override void OnInit()
    {
        skinPath = "test01";
        layer = PanelManager.Layer.panel;
    }

    public override void OnShow(params object[] objects)
    {
        nextBtn = skin.transform.Find("Hello").GetComponent<Button>();
        closeBtn = skin.transform.Find("Close").GetComponent<Button>();

        nextBtn.onClick.AddListener(OnClickTip);
        closeBtn.onClick.AddListener(Close);
    }

    public void OnClickTip() {
        PanelManager.Open<TipPanel>();
    }
}
