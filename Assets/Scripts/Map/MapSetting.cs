using System;
using System.Collections.Generic;
using DG.Tweening;
using QFramework;
using UI_Showcase;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Map {
    public class MapSetting : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [FormerlySerializedAs("setting")] public MapPointType mapPointType;
        private Action OnClickAction;
        public GameObject Tip;
        public int index { get; set; }
        public bool Finished { get; set; }
        public MapSetting[] NextNode;

        public List<Sprite> sprites;

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
            Init();
        }

        public void Init()
        {
            OnClickAction += InitMapSetting.MapDic[mapPointType];
            switch (mapPointType)
            {
                case MapPointType.EasyEnemy:
                    OnClickAction += OpenChooseBattleCardPanel;
                    GetComponent<Image>().sprite = sprites[0];
                    break;
                case MapPointType.NormalEnemy:
                    OnClickAction += OpenChooseBattleCardPanel;
                    GetComponent<Image>().sprite = sprites[1];
                    break;
                case MapPointType.EliteEnemy:
                    OnClickAction += OpenChooseBattleCardPanel;
                    GetComponent<Image>().sprite = sprites[2];
                    break;
                case MapPointType.Support:
                    OnClickAction += OpenShopPanel;
                    GetComponent<Image>().sprite = sprites[3];
                    break;
            }

            OnClickAction += CloseMapPanel;
        }

        void OpenChooseBattleCardPanel()
        {
            PanelManager.Open<ChooseBattleCardPanel>("ChooseBattleCardPanel");
        }
        
        void OpenShopPanel()
        {
            PanelManager.Open<ShopPanel>("ShopPanel");
        }
        
        void CloseMapPanel()
        {
            PanelManager.MapPanel.SetActive(false);
        }

        private void Update()
        {
            
            
            
        }

       
    }
}
