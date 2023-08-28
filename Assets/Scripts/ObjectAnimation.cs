using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using QFramework;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ObjectAnimation : MonoBehaviour
{
    private Image image;
    private RectTransform rectTransform;
    
    //临时
    public Button btn1;
    public Button btn2;

    public float shakeStepTime = 0.1f;
    public float sparkleStepTime = 0.1f;
    public float idleStepTime = 0.5f;
    public float sparkleInterval = 0.1f;
    public float shakeInterval = 0.1f;
    public float idleInterval = 0.1f;

    public float shakeRange = 5f;
    public float idleRange = 10f;

    private static bool idleComplete = false;
    public static bool sparkleComplete = false;
    public static bool shakeComplete = false;
    
    private void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        // btn1.onClick.AddListener(() => Sparkle());
        // btn2.onClick.AddListener(() => Shake());
    }

    private void Start()
    {
        //Idle();
    }

    // Update is called once per frame
    public async void Sparkle()
    {
        sparkleComplete = false;
        image.DOKill();
        for (int i = 0; i < 3; i++)
        {
            if(image != null) image.DOFade(0, sparkleStepTime);
            await UniTask.Delay(TimeSpan.FromSeconds(sparkleInterval));
            if(image != null) image.DOFade(1, sparkleStepTime);
            await UniTask.Delay(TimeSpan.FromSeconds(sparkleInterval));
        }
        sparkleComplete = true;
    }
    
    public async void Shake()
    {
        shakeComplete = false;
        float originalPos_x = rectTransform.anchoredPosition.x;
        rectTransform.DOKill();
        for (int i = 0; i < 3; i++)
        {
            if (rectTransform != null) rectTransform.DOAnchorPosX(rectTransform.anchoredPosition.x - shakeRange, shakeStepTime).WaitForCompletion();
            await UniTask.Delay(TimeSpan.FromSeconds(shakeInterval));
            if (rectTransform != null) rectTransform.DOAnchorPosX(rectTransform.anchoredPosition.x + shakeRange, shakeStepTime).WaitForCompletion();
            await UniTask.Delay(TimeSpan.FromSeconds(shakeInterval));
            if (rectTransform != null) rectTransform.DOAnchorPosX(rectTransform.anchoredPosition.x + shakeRange, shakeStepTime).WaitForCompletion();
            await UniTask.Delay(TimeSpan.FromSeconds(shakeInterval));
            if (rectTransform != null) rectTransform.DOAnchorPosX(rectTransform.anchoredPosition.x - shakeRange, shakeStepTime).WaitForCompletion();
            await UniTask.Delay(TimeSpan.FromSeconds(shakeInterval));
        }
        rectTransform.DOAnchorPosX(originalPos_x, shakeStepTime).WaitForCompletion();

        shakeComplete = true;
    }
    
    public void Idle(FSM<BattleFSM.OrganismsState> fsm)
    {
        idleComplete = false;
        float originalPos_y = rectTransform.anchoredPosition.y;
        Sequence sequence = DOTween.Sequence();
        sequence.SetLoops(-1);
        sequence.Append(rectTransform.DOAnchorPosY(originalPos_y - idleRange, idleStepTime));
        //sequence.AppendInterval(idleInterval);
        sequence.Append(rectTransform.DOAnchorPosY(originalPos_y + idleRange,  2 * idleStepTime));
        //sequence.AppendInterval(idleInterval);
        sequence.Append(rectTransform.DOAnchorPosY(originalPos_y, idleStepTime));
        sequence.AppendCallback(() =>
        {
            if (fsm.CurrentStateId != BattleFSM.OrganismsState.Idle)
            {
                idleComplete = true;
                sequence.Pause();
            }
        });
        sequence.Play();
    }

    public Func<bool> GetIdleComplete = () => idleComplete;
    public Func<bool> GetSparkleComplete = () => sparkleComplete;
    public Func<bool> GetShakeComplete = () => shakeComplete;

    private void OnDestroy()
    {
        //DOTween.KillAll();
    }
}
