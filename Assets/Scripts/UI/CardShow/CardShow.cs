using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

public class CardShow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image BG;
    public Text Name;
    public Text Shape;
    public Text Description;
    public GameObject EffectDes;
    public Animator Animator;

    [HideInInspector] public ColorType colorType;
    
    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EffectDes.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EffectDes.SetActive(false);
    }
    public void Update()
    {
        if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("Default") && Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            // 将动画播放头设置回初始位置
            Animator.Play("Default");
            Animator.SetBool("NeedToUse",false);
        }
    }

}
