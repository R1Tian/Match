using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
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

    private void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        btn1.onClick.AddListener(() => Sparkle());
        btn2.onClick.AddListener(() => Shake());
    }

    private void Start()
    {
        Idle();
    }

    // Update is called once per frame
    void Update()
    {
        //Idle();
    }

    async void Sparkle()
    {
        for (int i = 0; i < 3; i++)
        {
            image.DOFade(0, sparkleStepTime);
            await UniTask.Delay(TimeSpan.FromSeconds(sparkleInterval));
            image.DOFade(1, sparkleStepTime);
            await UniTask.Delay(TimeSpan.FromSeconds(sparkleInterval));
        }
        
    }
    
    async void Shake()
    {
        float originalPos_x = rectTransform.anchoredPosition.x;
        for (int i = 0; i < 3; i++)
        {
            rectTransform.DOAnchorPosX(rectTransform.anchoredPosition.x - shakeRange, shakeStepTime).WaitForCompletion();
            await UniTask.Delay(TimeSpan.FromSeconds(shakeInterval));
            rectTransform.DOAnchorPosX(rectTransform.anchoredPosition.x + shakeRange, shakeStepTime).WaitForCompletion();
            await UniTask.Delay(TimeSpan.FromSeconds(shakeInterval));
            rectTransform.DOAnchorPosX(rectTransform.anchoredPosition.x + shakeRange, shakeStepTime).WaitForCompletion();
            await UniTask.Delay(TimeSpan.FromSeconds(shakeInterval));
            rectTransform.DOAnchorPosX(rectTransform.anchoredPosition.x - shakeRange, shakeStepTime).WaitForCompletion();
            await UniTask.Delay(TimeSpan.FromSeconds(shakeInterval));
        }
        rectTransform.DOAnchorPosX(originalPos_x, shakeStepTime).WaitForCompletion();
    }
    
    async void Idle()
    {
        float originalPos_y = rectTransform.anchoredPosition.y;
        Sequence sequence = DOTween.Sequence();
        sequence.SetLoops(-1);
        // for (int i = 0; i < 3; i++)
        // {
        //     rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y - idleRange, DT_Duration).WaitForCompletion();
        //     await UniTask.Delay(TimeSpan.FromSeconds(shakeInterval));
        //     rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + idleRange, DT_Duration).WaitForCompletion();
        //     await UniTask.Delay(TimeSpan.FromSeconds(shakeInterval));
        //     rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + idleRange, DT_Duration).WaitForCompletion();
        //     await UniTask.Delay(TimeSpan.FromSeconds(shakeInterval));
        //     rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y - idleRange, DT_Duration).WaitForCompletion();
        //     await UniTask.Delay(TimeSpan.FromSeconds(shakeInterval));
        //     rectTransform.DOAnchorPosY(originalPos_y, DT_Duration).WaitForCompletion();
        //     await UniTask.Delay(TimeSpan.FromSeconds(shakeInterval));
        // }

        sequence.Append(rectTransform.DOAnchorPosY(originalPos_y - idleRange, idleStepTime));
        //sequence.AppendInterval(idleInterval);
        sequence.Append(rectTransform.DOAnchorPosY(originalPos_y + idleRange,  2 * idleStepTime));
        //sequence.AppendInterval(idleInterval);
        sequence.Append(rectTransform.DOAnchorPosY(originalPos_y, idleStepTime));


    }
    
}