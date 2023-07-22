using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager
{
    public enum Layer
    {
        panel,
        TIP
    }

    private static Dictionary<Layer, Transform> layers = new Dictionary<Layer, Transform>();
    private static Dictionary<string, string> NameToPath;
    public static Dictionary<string, BasePanel> panels = new Dictionary<string, BasePanel>();
    public static Transform root;
    public static Transform canvas;

    public static void Init()
    {
        root = GameObject.Find("UI").transform;
        canvas = root.Find("UICanvas");
        Transform panel = canvas.Find("Main");
        Transform tip = canvas.Find("Tip");
        layers.Add(Layer.panel, panel);
        layers.Add(Layer.TIP, tip);
        EnrollPath();
    }

    public static void EnrollPath() {
        NameToPath = new Dictionary<string, string> {
            { "StartPanel", "Start"},
            { "BattlePanel", "BattleField"},
            { "RewardPanel", "Reward"}
        };
    }

    public static void Open<T>(params object[] objects) where T : BasePanel
    {
        string name = typeof(T).ToString();
        if (panels.ContainsKey(name)) return;

        GameObject skin = Object.Instantiate(ResourcesManager.LoadPrefeb(NameToPath[name]));
        

        BasePanel panel = skin.GetComponent<BasePanel>();
        panel.skin = skin;
        panel.OnInit();
        //panel.Init();

        Transform layer = layers[panel.layer];
        skin.transform.SetParent(layer, false);

        panels.Add(name, panel);
        panel.OnShow(objects);
    }

    public static void Close(string name)
    {
        if (!panels.ContainsKey(name))
        {
            return;
        }

        BasePanel panel = panels[name];
        panel.OnClose();
        panels.Remove(name);
        Object.Destroy(panel.skin);
        Object.Destroy(panel);
    }
}
