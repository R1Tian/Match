using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardShow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image BG;
    public Text Name;
    public Text Shape;
    public Text Description;
    public GameObject EffectDes;

    public void OnPointerEnter(PointerEventData eventData)
    {
        EffectDes.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EffectDes.SetActive(false);
    }
}
