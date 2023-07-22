using UnityEngine;

public class BasePanel : MonoBehaviour
{
    //public string skinPath;
    public GameObject skin;
    public PanelManager.Layer layer = PanelManager.Layer.panel;

    //public void Init()
    //{
    //    skin = Instantiate(ResourcesManager.LoadPrefeb(skinPath));
    //}

    public void Close()
    {
        string name = GetType().ToString();
        PanelManager.Close(name);
    }

    public virtual void OnInit() { }
    public virtual void OnShow(params object[] objects) { }
    public virtual void OnClose() { }
}
