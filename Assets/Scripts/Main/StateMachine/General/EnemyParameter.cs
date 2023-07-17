using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParameter
{
    [Tooltip("攻击伤害")]
    public float attackDamge;
    [Tooltip("当前生命值")]
    public float currentHealth;
    [Tooltip("最大生命值")]
    public float maxHealth;
    [Tooltip("治愈值")]
    public float treatMent;
    [Tooltip("是否为活动状态")]
    public bool isActived;
    [Tooltip("当前阶段")]
    [Range(0,3)]
    public int currentStep;
    [Tooltip("伤害系数")]
    public int attackCoefficien=1;
    [Tooltip("动画状态机")]
    public Animator enemyAnimator;




}
