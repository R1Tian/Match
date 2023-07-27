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
        public MapSetting[] NextNode;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (OnClickAction != null && CanBeClick()) {
                InitMapSetting.LastNodeIndex = index;
                OnClickAction();
                OnClickAction = null;
                gameObject.GetComponent<Image>().color = Color.red;
            }
        }

        private bool CanBeClick() {
            if (InitMapSetting.LastNodeIndex == -1)
            {
                return InitMapSetting.PointList[0].GetHashCode() == GetHashCode();
            }
            else {
                MapSetting LastNode = InitMapSetting.PointList[InitMapSetting.LastNodeIndex];

                for (int i = 0; i < LastNode.NextNode.Length; i++)
                {
                    if (LastNode.NextNode[i].GetHashCode() == GetHashCode()) return true;
                }

                return false;
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
