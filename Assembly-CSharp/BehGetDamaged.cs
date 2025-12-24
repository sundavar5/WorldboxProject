using System;
using ai.behaviours;

// Token: 0x0200039E RID: 926
public class BehGetDamaged : BehaviourActionActor
{
	// Token: 0x060021D9 RID: 8665 RVA: 0x0011DE5E File Offset: 0x0011C05E
	public BehGetDamaged(int pDamage, AttackType pAttackType)
	{
		this._damage = pDamage;
		this._attackType = pAttackType;
	}

	// Token: 0x060021DA RID: 8666 RVA: 0x0011DE74 File Offset: 0x0011C074
	public override BehResult execute(Actor pActor)
	{
		pActor.getHit((float)this._damage, true, this._attackType, null, true, false, true);
		if (pActor.hasHealth())
		{
			return BehResult.Continue;
		}
		return BehResult.Stop;
	}

	// Token: 0x040018E4 RID: 6372
	private int _damage;

	// Token: 0x040018E5 RID: 6373
	private AttackType _attackType;
}
