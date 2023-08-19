using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

        private void Awake()
        {
            EnrollDic();
            EnrollList();
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
            string[] NormalEnemy = new string[] { "剧毒蜘蛛", "噬魂幽灵", "堕落收割者", "邪恶骨巫" };
            int index = UnityEngine.Random.Range(0, NormalEnemy.Length);
            Debug.Log(NormalEnemy[index]);
        }
        private void RandomEliteEnemy() {
            string[] EliteEnemy = new string[] { "腐败瘟疫使者", "暗夜梦魇", "深渊虚灵", "五彩狂怒者" };
            int index = UnityEngine.Random.Range(0, EliteEnemy.Length);
            Debug.Log(EliteEnemy[index]);
        }
        private void RandomSupport() {
            string[] Support = new string[] { "商店", "升级卡片", "回血" };
            int index = UnityEngine.Random.Range(0, Support.Length);
            Debug.Log(Support[index]);
        }
    }
}
