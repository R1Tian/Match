using System.Collections;
using System.Collections.Generic;
using System;
using QFramework;
using UI_Showcase;
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
        public MapSetting[] NextNode;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (OnClickAction != null && CanBeClick()) {
                InitMapSetting.LastNodeIndex = index;
                OnClickAction();
                OnClickAction = null;
                InitMapSetting.PointList[InitMapSetting.LastNodeIndex].Finished = true;
                gameObject.GetComponent<Image>().color = Color.red;
                Tip.SetActive(false);
            }
        }

        private bool CanBeClick() {
            if (InitMapSetting.LastNodeIndex == -1)
            {
                if (index == 0) return true;
            }
            else {
                foreach (var item in InitMapSetting.PointList[InitMapSetting.LastNodeIndex].NextNode) {
                    if (item.GetHashCode() == GetHashCode()) return true;
                }   

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
            OnClickAction += OpenChooseBattleCardPanel;
            OnClickAction += CloseMapPanel;
        }

        void OpenChooseBattleCardPanel()
        {
            PanelManager.Open<ChooseBattleCardPanel>("ChooseBattleCardPanel");
        }
        
        void CloseMapPanel()
        {
            PanelManager.MapPanel.SetActive(false);
        }
    }
}
