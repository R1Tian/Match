using QFramework;

public class PlayerState : ISingleton
{
    public static PlayerState instance
    {
        get { return SingletonProperty<PlayerState>.Instance; }
    }
    private PlayerState() { }

    #region Attribute
    private int PlayerHP;
    private int AttackBuffLayer;
    private int DefenceBuffLayer;
    private int Damage;
    #endregion

    public void OnSingletonInit()
    {
        PlayerHP = 10;
        AttackBuffLayer = 0;
        DefenceBuffLayer = 0;
    }

    public void HealHealth(int hp) {
        PlayerHP += hp;
    }

    public void TakeDamge(int hp) {
        PlayerHP -= hp;
    }

    public int GetHP() {
        return PlayerHP;
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

    public void AddDamage(int damage) {
        Damage += damage;
    }

    public int GetDamageBuff() {
        return  AttackBuffLayer;
    }

    public void DeleteDamageBuff() {
        AttackBuffLayer = 0;
    }

    public void Dispose() { SingletonProperty<PlayerState>.Instance.Dispose(); }
}
