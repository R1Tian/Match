using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.UI;

namespace Map {
    public enum MapPointType
    {
        EasyEnemy,
        NormalEnemy,
        EliteEnemy,
        Support
    }

    public class InitMapSetting : MonoBehaviour
    {
        public static Dictionary<MapPointType, Action> MapDic;
        public static List<MapSetting> PointList;
        public static int LastNodeIndex = -1;
        public EnemyList Easy;
        public EnemyList Normal;
        public EnemyList Elite;

        private bool needReset = false;
        public float duration = 0.1f;
        public float interval = 0.02f;

        public delegate void ResetAnimationDelegate(); 
        public static event ResetAnimationDelegate ResetAnimationEvent;
        private void Awake()
        {
            EnrollDic();
            EnrollList();
            ResetAnimationEvent += ResetAnimation;
        }

        private void EnrollList() {
            PointList = new List<MapSetting>();
            Transform parent = GameObject.Find("MapPointList").transform;

            for (int i = 0; i < parent.childCount; i++) {
                MapSetting child = parent.GetChild(i).GetComponent<MapSetting>();
                child.index = i;
                PointList.Add(child);
            }
        }

        private void EnrollDic()
        {
            MapDic = new Dictionary<MapPointType, Action>
        {
            { MapPointType.EasyEnemy, RandomEasyEnemy },
            { MapPointType.NormalEnemy, RandomNormalEnemy},
            { MapPointType.EliteEnemy, RandomEliteEnemy},
            { MapPointType.Support, RandomSupport}
        };

        }

        private void RandomEasyEnemy() {
            string[] list = Easy.MonsterList;
            int index = UnityEngine.Random.Range(0, list.Length);
            EnemyState.instance.ReadEnemyData(ResourcesManager.LoadEnemy(list[index]));
        }
        private void RandomNormalEnemy() {
            string[] list = Normal.MonsterList;
            int index = UnityEngine.Random.Range(0, list.Length);
            EnemyState.instance.ReadEnemyData(ResourcesManager.LoadEnemy(list[index]));
        }
        private void RandomEliteEnemy() {
            string[] list = Elite.MonsterList;
            int index = UnityEngine.Random.Range(0, list.Length);
            EnemyState.instance.ReadEnemyData(ResourcesManager.LoadEnemy(list[index]));
        }
        private void RandomSupport() {
            string[] Support = new string[] { "商店", "升级卡片", "回血" };
            int index = UnityEngine.Random.Range(0, Support.Length);
            Debug.Log(Support[index]);
        }

        public static void Init()
        {
            LastNodeIndex = -1;
            foreach (MapSetting mapSetting in PointList)
            {
                mapSetting.Init();
                mapSetting.Finished = false;
                mapSetting.GetComponent<Image>().color = Color.white;
            }

            ResetAnimationEvent?.Invoke();

        }
        
        /// <summary>
        /// 动画
        /// </summary>
        void Reset()
        {
            if (needReset)
            {
                needReset = false;
                if (LastNodeIndex == -1)
                {
                    Sequence sequence = DOTween.Sequence();
                    if (!PointList[0].Finished)
                    {
                        sequence.SetLoops(-1).SetAutoKill(false);
                        sequence.Append(PointList[0].GetComponent<Transform>().DOScale(new Vector3(1.5f, 1.5f, 1), duration)
                            .SetEase(Ease.InSine));
                        sequence.Append(PointList[0].GetComponent<Transform>().DOScale(new Vector3(1f, 1f, 1), duration)
                            .SetEase(Ease.InSine));
                        //sequence.Append(GetComponent<Transform>().DOScale(new Vector3(1.5f, 1.5f, 1), duration));
                    }
                    else
                    {
                        sequence.Kill();
                        PointList[0].GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                    }
                }
                else
                {
                    foreach (MapSetting mapSetting in PointList[LastNodeIndex].NextNode)
                    {
                        Sequence sequence = DOTween.Sequence();
                        if (!mapSetting.Finished)
                        {
                            sequence.SetLoops(-1).SetAutoKill(false);
                            sequence.Append(mapSetting.GetComponent<Transform>().DOScale(new Vector3(1.5f, 1.5f, 1), duration)
                                .SetEase(Ease.InSine));
                            sequence.Append(mapSetting.GetComponent<Transform>().DOScale(new Vector3(1f, 1f, 1), duration)
                                .SetEase(Ease.InSine));
                            //sequence.Append(GetComponent<Transform>().DOScale(new Vector3(1.5f, 1.5f, 1), duration));
                        }
                        else
                        {
                            sequence.Kill();
                            mapSetting.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                        }
                    }
                }
                
            }
        }

        private void Update()
        {
            
        }

        private void OnEnable()
        {
            ResetAnimation();
        }

        private void ResetAnimation()
        {
            needReset = true;
            DOTween.KillAll();
            Reset();

        }
    }
}
