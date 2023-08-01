using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Map {
    public class MapSetting : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public Setting setting;
        private Action OnClickAction;
        public GameObject Tip;
        public int index { get; set; }
        public bool Finished { get; set; }
        public MapSetting[] requirement;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (OnClickAction != null && CanBeClick()) {
                InitMapSetting.AcivateIndex = index;
                OnClickAction();
                OnClickAction = null;
                InitMapSetting.PointList[InitMapSetting.AcivateIndex].Finished = true;
                gameObject.GetComponent<Image>().color = Color.red;
            }
        }

        private bool CanBeClick() {
            if (requirement.Length == 0) {
                return true;
            }

            for (int i = 0; i < requirement.Length; i++) {
                if (requirement[i].Finished) return true;
            }
            return false;
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
