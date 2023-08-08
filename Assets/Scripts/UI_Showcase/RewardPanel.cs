using UnityEngine.UI;
using UnityEngine;

public class RewardPanel : BasePanel
{
    private Button BackBtn;
    private Text CoinCount;

    public override void OnInit()
    {
        //skinPath = "Reward";
        layer = PanelManager.Layer.TIP;
    }

    public override void OnShow(params object[] objects)
    {
        BackBtn = skin.transform.Find("Back").GetComponent<Button>();
        CoinCount = skin.transform.Find("Coin").GetComponent<Text>();

        CoinCount.text = Random.Range(1, 10).ToString();

        BackBtn.onClick.AddListener(() =>
        {
            PanelManager.Close("BattlePanel");
            PanelManager.MapPanel.SetActive(true);
            Close();
        });
    }
}
