using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Showcase
{
    public class ChooseBattleCardPanel : BasePanel
    {
        public GameObject boardBG;
        
        public GameObject fullCardPrefab;

        public ScrollRect battleCardScrollRect; // 上方选中的卡牌
        public ScrollRect repositoryCardScrollRect; // 下方仓库的卡牌

        [Header("卡牌移动过渡时间")]
        public float doTweenDuration;
        
        /// <summary>
        /// 所有仓库中的卡牌
        /// </summary>
        private List<CardObject> m_AllRepositoryCardList = new ();

        private List<int> m_curBattleCardIdList = new (); // 现在选中的卡牌
        private List<int> m_curRepositoryCardIdList = new (); // 现在还未选中的卡牌

        /// <summary>
        /// 正在操作卡片，同一时间只允许一张卡片在操作，防止脏数据
        /// </summary>
        private bool isOperateCard = false;

        public override void OnInit()
        {
            GetAllRepositoryCard();
            
            m_curBattleCardIdList.Clear();
            m_curRepositoryCardIdList.AddRange(PlayerState.instance.GetBattleCards().ConvertAll(c => c.id));
            
            m_curRepositoryCardIdList.Clear();
            m_curRepositoryCardIdList.AddRange(m_AllRepositoryCardList.ConvertAll(c => c.id));
            foreach (CardObject cardObject in PlayerState.instance.GetBattleCards())
            {
                m_curRepositoryCardIdList.Remove(cardObject.id);
            }

            
        }

        public override void OnShow(params object[] objects)
        {
            ReFillCurRepositoryCard();
            ReFillBattleCard();
        }

        public override void OnClose()
        {
            
        }

        /// <summary>
        /// 获取仓库中所有卡牌
        /// </summary>
        private void GetAllRepositoryCard()
        {
            m_AllRepositoryCardList = new List<CardObject>();

            // Tetromino lShape = Main.instance.GetTetShape("L型");
            // Tetromino jShape = Main.instance.GetTetShape("J型");
            // Tetromino oShape = Main.instance.GetTetShape("O型");
            // Tetromino iShape = Main.instance.GetTetShape("I型");
            // Tetromino tShape = Main.instance.GetTetShape("T型");
            // Tetromino sShape = Main.instance.GetTetShape("S型");
            // Tetromino zShape = Main.instance.GetTetShape("Z型");
            
            // m_AllRepositoryCardList.Add(new Card(0, "0号牌",Color.red, tShape, Skill.Damage, "T型", "消除时，造成2/3/5的数值伤害"));
            // m_AllRepositoryCardList.Add(new Card(1,"1号牌", Color.yellow, iShape, Skill.Power, "I型", "消除时，生成1/2/3层力量buff（每增加1层力量buff攻击牌造成的伤害+1）"));
            // m_AllRepositoryCardList.Add(new Card(2,"2号牌", Color.blue, oShape, Skill.Defend, "O型", "消除时，生成2/3/4点防御值"));
            // m_AllRepositoryCardList.Add(new Card(3,"3号牌", Color.green, iShape, Skill.Heal, "I型", "消除时，恢复2/3/4点生命值"));
            // m_AllRepositoryCardList.Add(new Card(4, "4号牌",Color.red, lShape, Skill.Damage, "L型", "消除时，造成2/3/5的数值伤害"));
            // m_AllRepositoryCardList.Add(new Card(5,"5号牌", Color.yellow, jShape, Skill.Power, "J型", "消除时，生成1/2/3层力量buff（每增加1层力量buff攻击牌造成的伤害+1）"));
            // m_AllRepositoryCardList.Add(new Card(6,"6号牌", Color.blue, oShape, Skill.Defend, "O型", "消除时，生成2/3/4点防御值"));
            // m_AllRepositoryCardList.Add(new Card(7,"7号牌", Color.green, iShape, Skill.Heal, "I型", "消除时，恢复2/3/4点生命值"));
            // m_AllRepositoryCardList.Add(new Card(8,"8号牌", Color.blue, iShape, Skill.Heal, "L型", "c"));
            // m_AllRepositoryCardList.Add(new Card(9,"9号牌", Color.red, iShape, Skill.Heal, "O型", "消除时，恢复2/3/4点生命值"));
            // m_AllRepositoryCardList.Add(new Card(10,"10号牌", Color.yellow, iShape, Skill.Heal, "J型", "消除时，恢复2/3/4点生命值"));
            // foreach (CardObject cardObject in PlayerState.instance.GetAllCards())
            // {
            //     m_AllRepositoryCardList.Add(cardObject);
            // }

            if (PlayerState.instance.GetAllCards().Count <= 0)
            {
                PlayerState.instance.AddBattleCards(CardManager.GetCardById(0));
                PlayerState.instance.AddBattleCards(CardManager.GetCardById(1));
                PlayerState.instance.AddBattleCards(CardManager.GetCardById(2));
                
                m_AllRepositoryCardList.Add(CardManager.GetCardById(0));
                m_AllRepositoryCardList.Add(CardManager.GetCardById(1));
                m_AllRepositoryCardList.Add(CardManager.GetCardById(2));
            }
        }

        /// <summary>
        /// 生成初始时仓库中所有卡牌
        /// </summary>
        private void ReFillCurRepositoryCard()
        {
            for (int i = 0; i < m_curRepositoryCardIdList.Count; i++)
            {
                CreateRepositoryFullCard(GetCardById(m_curRepositoryCardIdList[i]), repositoryCardScrollRect.content);
            }
        }
        /// <summary>
        /// 生成初始时战斗中所有卡牌
        /// </summary>
        private void ReFillBattleCard()
        {
            for (int i = 0; i < m_curBattleCardIdList.Count; i++)
            {
                CreateBattleFullCard(GetCardById(m_curBattleCardIdList[i]), battleCardScrollRect.content);
            }
        }

        private MonoFullCard CreateRepositoryFullCard(CardObject card, Transform parent)
        {
            MonoFullCard monoFullCard = Instantiate(fullCardPrefab, parent).GetComponent<MonoFullCard>();
            monoFullCard.RepositoryInit(card);
            monoFullCard.clickButton.onClick.RemoveAllListeners();
            monoFullCard.clickButton.onClick.AddListener(async () => SelectOneCard(monoFullCard.card, monoFullCard));

            return monoFullCard;
        }

        private MonoFullCard CreateBattleFullCard(CardObject card, Transform parent)
        {
            MonoFullCard monoFullCard = Instantiate(fullCardPrefab, parent).GetComponent<MonoFullCard>();
            monoFullCard.RepositoryInit(card);
            monoFullCard.clickButton.onClick.RemoveAllListeners();
            monoFullCard.clickButton.onClick.AddListener(async () => UnSelectOneCard(monoFullCard.card, monoFullCard));

            return monoFullCard;
        }

        private MonoFullCard CreateDoTweenFullCard(CardObject card, Transform parent)
        {
            MonoFullCard monoFullCard = Instantiate(fullCardPrefab, parent).GetComponent<MonoFullCard>();
            monoFullCard.RepositoryInit(card);

            return monoFullCard;
        }
        
        /// <summary>
        /// 选中一张卡牌（下到上）
        /// </summary>
        /// <param name="selectedCard">选中的卡牌的 Card 信息</param>
        /// <param name="disappearCardInRepository">原先在下面的消失的 MonoFullCard</param>
        private async void SelectOneCard(CardObject selectedCard, MonoFullCard disappearCardInRepository)
        {
            if (isOperateCard) return;

            isOperateCard = true;
            
            MonoFullCard appearCardInBattle = CreateBattleFullCard(selectedCard, battleCardScrollRect.content);
            appearCardInBattle.SetDisplayable(false); // 新生成的 fullCard 关闭显示
            disappearCardInRepository.SetDisplayable(false); // 原来下方的的 fullCard 关闭显示
            
            await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);
            LayoutRebuilder.ForceRebuildLayoutImmediate(battleCardScrollRect.content);

            battleCardScrollRect.horizontal = false;
            repositoryCardScrollRect.vertical = false;
            
            if (battleCardScrollRect.content.sizeDelta.x > battleCardScrollRect.viewport.sizeDelta.x)
            {
                await battleCardScrollRect.DONormalizedPos(new Vector2(1f, battleCardScrollRect.normalizedPosition.y),
                                doTweenDuration).AsyncWaitForCompletion();
            }
            
            // 生成一张临时做 DOTween 的样子 FullCard
            MonoFullCard doTweenFullCard = CreateDoTweenFullCard(selectedCard, gameObject.transform);

            Vector3 disappearCardInRepositoryWorldPos =
                disappearCardInRepository.transform.parent.TransformPoint(disappearCardInRepository.transform.localPosition);
            Vector3 appearCardInBattleWorldPos =
                appearCardInBattle.transform.parent.TransformPoint(appearCardInBattle.transform.localPosition);

            doTweenFullCard.transform.position = disappearCardInRepositoryWorldPos;
            
            // DoTween
            await doTweenFullCard.transform.DOMove(appearCardInBattleWorldPos, doTweenDuration)
                .SetEase(Ease.Linear)
                .AsyncWaitForCompletion();
            
            Destroy(doTweenFullCard.gameObject); // 销毁过渡卡牌
            Destroy(disappearCardInRepository.gameObject); // 销毁消失卡牌
            appearCardInBattle.SetDisplayable(true); // 打开新加上去的显示卡牌
            battleCardScrollRect.horizontal = true; // 恢复滑动
            repositoryCardScrollRect.vertical = true;

            // List 数据操作
            if (m_curRepositoryCardIdList.Contains(selectedCard.id))
            {
                m_curRepositoryCardIdList.Remove(selectedCard.id);
                Debug.Log($"从下方移除 id {selectedCard.id}");
            }
            else
            {
                Debug.LogError($"m_curRepositoryCardIdList 没有 id {selectedCard.id} 无法移除改卡片");
                return;
            }

            if (!m_curBattleCardIdList.Contains(selectedCard.id))
            {
                m_curBattleCardIdList.Add(selectedCard.id);
                Debug.Log($"上方添加 id {selectedCard.id}");
            }
            else
            {
                Debug.LogError($"m_curBattleCardIdList 已经有 id {selectedCard.id} 无法加入改卡片");
                return;
            }
            
            isOperateCard = false;
        }

        /// <summary>
        /// 取消选中一张卡牌（上到下）
        /// </summary>
        /// <param name="unSelectedCard">取消选中的卡牌的 Card 信息</param>
        /// <param name="disappearCardInBattle">原先在上面的消失的 MonoFullCard</param>
        private async void UnSelectOneCard(CardObject unSelectedCard, MonoFullCard disappearCardInBattle)
        {
            if (isOperateCard) return;

            isOperateCard = true;
            
            MonoFullCard appearCardInRepository = CreateRepositoryFullCard(unSelectedCard, repositoryCardScrollRect.content);
            appearCardInRepository.SetDisplayable(false); // 新生成的 fullCard 关闭显示
            disappearCardInBattle.SetDisplayable(false); // 原来上方的的 fullCard 关闭显示
            
            await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);
            LayoutRebuilder.ForceRebuildLayoutImmediate(repositoryCardScrollRect.content);

            battleCardScrollRect.horizontal = false;
            repositoryCardScrollRect.vertical = false;
            
            if (repositoryCardScrollRect.content.sizeDelta.y > repositoryCardScrollRect.viewport.sizeDelta.y)
            {
                await battleCardScrollRect.DONormalizedPos(new Vector2(battleCardScrollRect.normalizedPosition.x, 1f),
                    doTweenDuration).AsyncWaitForCompletion();
            }
            
            // 生成一张临时做 DOTween 的样子 FullCard
            MonoFullCard doTweenFullCard = CreateDoTweenFullCard(unSelectedCard, gameObject.transform);

            Vector3 disappearCardInBattleWorldPos =
                disappearCardInBattle.transform.parent.TransformPoint(disappearCardInBattle.transform.localPosition);
            Vector3 appearCardInRepositoryWorldPos =
                appearCardInRepository.transform.parent.TransformPoint(appearCardInRepository.transform.localPosition);
            
            doTweenFullCard.transform.position = disappearCardInBattleWorldPos;
            
            // DoTween
            await doTweenFullCard.transform.DOMove(appearCardInRepositoryWorldPos, doTweenDuration)
                .SetEase(Ease.Linear)
                .AsyncWaitForCompletion();
            
            Destroy(doTweenFullCard.gameObject); // 销毁过渡卡牌
            Destroy(disappearCardInBattle.gameObject); // 销毁消失卡牌
            appearCardInRepository.SetDisplayable(true); // 打开新加上去的显示卡牌
            battleCardScrollRect.horizontal = true; // 恢复滑动
            repositoryCardScrollRect.vertical = true;
            
            // List 数据操作
            if (m_curBattleCardIdList.Contains(unSelectedCard.id))
            {
                m_curBattleCardIdList.Remove(unSelectedCard.id);
                Debug.Log($"从上方移除 id {unSelectedCard.id}");
            }
            else
            {
                Debug.LogError($"m_curBattleCardIdList 没有 id {unSelectedCard.id} 无法移除改卡片");
                return;
            }

            if (!m_curRepositoryCardIdList.Contains(unSelectedCard.id))
            {
                m_curRepositoryCardIdList.Add(unSelectedCard.id);
                Debug.Log($"下方添加 id {unSelectedCard.id}");
            }
            else
            {
                Debug.LogError($"m_curRepositoryCardIdList 已经有 id {unSelectedCard.id} 无法加入改卡片");
                return;
            }
            
            
            isOperateCard = false;
        }

        public void SelectOver()
        {
            foreach (var id in m_curBattleCardIdList)
            {
                Debug.Log($"选择的战斗卡牌 Id {id}");
                PlayerState.instance.AddBattleCards(GetCardById(id));
            }
            PanelManager.Open<BattlePanel>("BattleField");
            Instantiate(boardBG);
            Close();
        }

        #region Tool

        private CardObject GetCardById(int id)
        {
            return m_AllRepositoryCardList.Find(card => card.id == id);
        }
        

        #endregion
    }
}