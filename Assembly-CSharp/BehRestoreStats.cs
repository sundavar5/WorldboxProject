using System;
using ai.behaviours;

// Token: 0x020003A0 RID: 928
public class BehRestoreStats : BehaviourActionActor
{
	// Token: 0x060021DD RID: 8669 RVA: 0x0011DEBF File Offset: 0x0011C0BF
	public BehRestoreStats(float pHealth = 0f, float pMana = 0f)
	{
		this._health = pHealth;
		this._mana = pMana;
	}

	// Token: 0x060021DE RID: 8670 RVA: 0x0011DED5 File Offset: 0x0011C0D5
	public override BehResult execute(Actor pActor)
	{
		if (this._health != 0f)
		{
			pActor.restoreHealthPercent(this._health);
		}
		if (this._mana != 0f)
		{
			pActor.restoreManaPercent(this._mana);
		}
		return BehResult.Continue;
	}

	// Token: 0x040018E6 RID: 6374
	private readonly float _health;

	// Token: 0x040018E7 RID: 6375
	private readonly float _mana;
}
