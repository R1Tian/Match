using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Showcase
{
    public class ChooseBattleCardPanel : BasePanel
    {
        public GameObject fullCardPrefab;

        public ScrollRect selectedCardScrollRect; // 上方选中的卡牌
        public ScrollRect repositoryCardScrollRect; // 下方仓库的卡牌
        
        /// <summary>
        /// 所有仓库中的卡牌
        /// </summary>
        private List<Card> m_AllRepositoryCardList;

        public override void OnInit()
        {
            GetAllRepositoryCard(m_AllRepositoryCardList);
        }

        public override void OnShow(params object[] objects)
        {
            
        }

        public override void OnClose()
        {
            
        }

        private void GetAllRepositoryCard(List<Card> allRepositoryCardList)
        {
            
        }
    }
}