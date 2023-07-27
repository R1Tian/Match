using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Map {
    public class MapSetting : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public Setting setting;
        private Action OnClickAction;
        public GameObject Tip;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (OnClickAction != null) {
                OnClickAction();
                OnClickAction = null;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Tip.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Tip.SetActive(false);
        }

        private void Start()
        {
            OnClickAction += InitMapSetting.MapDic[setting];
        }
    }
}
