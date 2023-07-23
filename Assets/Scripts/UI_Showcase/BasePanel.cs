using Sirenix.OdinInspector;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    //public string skinPath;
    [Title("BasePanel 通用字段")]
    public GameObject skin;
    
    [PropertySpace(0,60)]
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
