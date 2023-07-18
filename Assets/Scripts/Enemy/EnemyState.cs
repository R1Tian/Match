using QFramework;

public class EnemyState : ISingleton
{
    public static EnemyState instance
    {
        get { return SingletonProperty<EnemyState>.Instance; }
    }
    private EnemyState() { }

    #region Attribute
    private int EnemyMaxHP;
    private int EnemyHP;
    private int AttackBuffLayer;
    private int DefenceBuffLayer;
    private int WeakBuffLayer;
    private int FragileBuffLayer;
    private int Damage;

    #endregion
    public void OnSingletonInit()
    {
        EnemyMaxHP = 20;
        EnemyHP = 20;
        AttackBuffLayer = 0;
        DefenceBuffLayer = 0;
        WeakBuffLayer = 0;
        FragileBuffLayer = 0;
        
    }

    public void HealHealth(int hp) {
        EnemyHP += hp;
    }
    public void TakeDamge(int hp) {
        EnemyHP -= hp;
    }
    public int GetHP() {
        return EnemyHP;
    }
    public int GetMaxHP()
    {
        return EnemyMaxHP;
    }
    public void AddAttackBuff(int layer) {
        AttackBuffLayer += layer;
    }
    public void DropAttackBuff(int layer) {
        AttackBuffLayer -= layer;
    }
    public int GetAttackBuff() {
        return AttackBuffLayer;
    }
    public void AddDefenceBuffLayer(int layer) {
        DefenceBuffLayer += layer;
    }
    public void DropDefenceBuffLayer(int layer) {
        DefenceBuffLayer -= layer;
    }
    public int GetDefenceBuffLayer() {
        return DefenceBuffLayer;
    }
    public void AddWeakBuffLayer(int layer) {
        WeakBuffLayer += layer;
    }
    public void DropWeakBuffLayer(int layer) {
        WeakBuffLayer -= layer;
    }
    public int GetWeakBuffLayer() {
        return WeakBuffLayer;
    }
    public void AddFragileBuffLayer(int layer) {
        FragileBuffLayer += layer;
    }
    public void DropFragileBuffLayer(int layer) {
        FragileBuffLayer -= layer;
    }
    public int GetFragileBuffLayer() {
        return FragileBuffLayer;
    }
    public void AddDamage(int damage) {
        Damage += damage;
    }

    public int GetDamageBuff() {
        int res=AttackBuffLayer;
        AttackBuffLayer=0;
        return res;
    }
    public void Dispose(){SingletonProperty<EnemyState>.Dispose();}



}
