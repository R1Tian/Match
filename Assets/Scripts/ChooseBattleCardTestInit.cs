using System;
using UI_Showcase;
using UnityEngine;

namespace DefaultNamespace
{
    public class ChooseBattleCardTestInit : MonoBehaviour
    {
        private void Awake()
        {
            PanelManager.Init();
            PanelManager.Open<ChooseBattleCardPanel>();
        }
    }
}