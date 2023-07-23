using System.Collections.Generic;
using System.Linq;
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
        private List<Card> m_AllRepositoryCardList = new ();

        private List<Card> m_curBattleCardList = new (); // 现在选中的卡牌
        private List<Card> m_curRepositoryCardList = new (); // 现在还未选中的卡牌

        public override void OnInit()
        {
            GetAllRepositoryCard();
            
            m_curRepositoryCardList.Clear();
            m_curRepositoryCardList.AddRange(m_AllRepositoryCardList.ToList());
            
            m_curBattleCardList.Clear();
        }

        public override void OnShow(params object[] objects)
        {
            ReFillAllRepositoryCard();
        }

        public override void OnClose()
        {
            
        }

        /// <summary>
        /// 获取仓库中所有卡牌
        /// </summary>
        private void GetAllRepositoryCard()
        {
            m_AllRepositoryCardList = new List<Card>();

            Tetromino lShape = Main.instance.GetTetShape("L型");
            Tetromino jShape = Main.instance.GetTetShape("J型");
            Tetromino oShape = Main.instance.GetTetShape("O型");
            Tetromino iShape = Main.instance.GetTetShape("I型");
            Tetromino tShape = Main.instance.GetTetShape("T型");
            Tetromino sShape = Main.instance.GetTetShape("S型");
            Tetromino zShape = Main.instance.GetTetShape("Z型");
            
            m_AllRepositoryCardList.Add(new Card(0, "0号牌",Color.red, lShape, Skill.Damage, "L型", "消除时，造成2/3/5的数值伤害"));
            m_AllRepositoryCardList.Add(new Card(1,"1号牌", Color.yellow, jShape, Skill.Power, "J型", "消除时，生成1/2/3层力量buff（每增加1层力量buff攻击牌造成的伤害+1）"));
            m_AllRepositoryCardList.Add(new Card(2,"2号牌", Color.blue, oShape, Skill.Defend, "O型", "消除时，生成2/3/4点防御值"));
            m_AllRepositoryCardList.Add(new Card(3,"3号牌", Color.green, iShape, Skill.Heal, "I型", "消除时，恢复2/3/4点生命值"));
            m_AllRepositoryCardList.Add(new Card(4, "4号牌",Color.red, lShape, Skill.Damage, "L型", "消除时，造成2/3/5的数值伤害"));
            m_AllRepositoryCardList.Add(new Card(5,"5号牌", Color.yellow, jShape, Skill.Power, "J型", "消除时，生成1/2/3层力量buff（每增加1层力量buff攻击牌造成的伤害+1）"));
            m_AllRepositoryCardList.Add(new Card(6,"6号牌", Color.blue, oShape, Skill.Defend, "O型", "消除时，生成2/3/4点防御值"));
            m_AllRepositoryCardList.Add(new Card(7,"7号牌", Color.green, iShape, Skill.Heal, "I型", "消除时，恢复2/3/4点生命值"));
            
        }

        /// <summary>
        /// 生成初始时仓库中所有卡牌
        /// </summary>
        private void ReFillAllRepositoryCard()
        {
            for (int i = 0; i < m_curRepositoryCardList.Count; i++)
            {
                CreateUnSelectedFullCard(m_curRepositoryCardList[i], repositoryCardScrollRect.content);
            }
        }

        private MonoFullCard CreateUnSelectedFullCard(Card card, Transform parent)
        {
            MonoFullCard monoFullCard = Instantiate(fullCardPrefab, parent).GetComponent<MonoFullCard>();
            monoFullCard.RepositoryInit(card);
            monoFullCard.clickButton.onClick.RemoveAllListeners();
            monoFullCard.clickButton.onClick.AddListener(() => SelectOneCard(monoFullCard.card, monoFullCard));

            return monoFullCard;
        }

        private MonoFullCard CreateSelectedFullCard(Card card, Transform parent)
        {
            MonoFullCard monoFullCard = Instantiate(fullCardPrefab, parent).GetComponent<MonoFullCard>();
            monoFullCard.RepositoryInit(card);
            monoFullCard.clickButton.onClick.RemoveAllListeners();
            monoFullCard.clickButton.onClick.AddListener(() => UnSelectOneCard(monoFullCard.card));

            return monoFullCard;
        }

        private MonoFullCard CreateDoTweenFullCard(Card card, Transform parent)
        {
            MonoFullCard monoFullCard = Instantiate(fullCardPrefab, parent).GetComponent<MonoFullCard>();
            monoFullCard.RepositoryInit(card);

            return monoFullCard;
        }

        // 选中一张卡牌
        private void SelectOneCard(Card card, MonoFullCard disappearCardInRepository)
        {
            MonoFullCard appearCardInBattle = CreateSelectedFullCard(card, selectedCardScrollRect.content);
            appearCardInBattle.SetDisplayable(false); // 新生成的 fullCard 关闭显示
            disappearCardInRepository.SetDisplayable(false); // 原来下方的的 fullCard 关闭显示
            
            // 生成一张临时做 DOTween 的样子 FullCard
            MonoFullCard doTweenFullCard = CreateDoTweenFullCard(card, gameObject.transform);

            Vector3 disappearCardInRepositoryWorldPos =
                disappearCardInRepository.transform.TransformPoint(disappearCardInRepository.transform.localPosition);
            Vector3 appearCardInBattleWorldPos =
                appearCardInBattle.transform.TransformPoint(appearCardInBattle.transform.localPosition);
            
            
            doTweenFullCard.transform.position = disappearCardInRepositoryWorldPos;
        }

        private void UnSelectOneCard(Card card)
        {
            
        }
    }
}