using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Help : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject HelpDetailPrefab;
    private GameObject helpDetail;
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        helpDetail = Instantiate(HelpDetailPrefab);
        helpDetail.transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(helpDetail);
    }

    private void OnDestroy()
    {
        Destroy(helpDetail);
    }
}
