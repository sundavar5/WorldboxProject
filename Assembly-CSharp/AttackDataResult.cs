using System;

// Token: 0x0200025A RID: 602
public struct AttackDataResult
{
	// Token: 0x17000147 RID: 327
	// (get) Token: 0x06001682 RID: 5762 RVA: 0x000E3447 File Offset: 0x000E1647
	public static AttackDataResult Continue
	{
		get
		{
			return new AttackDataResult(ApplyAttackState.Continue, -1L);
		}
	}

	// Token: 0x17000148 RID: 328
	// (get) Token: 0x06001683 RID: 5763 RVA: 0x000E3451 File Offset: 0x000E1651
	public static AttackDataResult Miss
	{
		get
		{
			return new AttackDataResult(ApplyAttackState.Miss, -1L);
		}
	}

	// Token: 0x17000149 RID: 329
	// (get) Token: 0x06001684 RID: 5764 RVA: 0x000E345B File Offset: 0x000E165B
	public static AttackDataResult Hit
	{
		get
		{
			return new AttackDataResult(ApplyAttackState.Hit, -1L);
		}
	}

	// Token: 0x1700014A RID: 330
	// (get) Token: 0x06001685 RID: 5765 RVA: 0x000E3465 File Offset: 0x000E1665
	public static AttackDataResult Block
	{
		get
		{
			return new AttackDataResult(ApplyAttackState.Block, -1L);
		}
	}

	// Token: 0x06001686 RID: 5766 RVA: 0x000E346F File Offset: 0x000E166F
	public AttackDataResult(ApplyAttackState pState, long pDeflectedByWhoId = -1L)
	{
		this.state = pState;
		this.deflected_by_who_id = pDeflectedByWhoId;
	}

	// Token: 0x040012A7 RID: 4775
	public long deflected_by_who_id;

	// Token: 0x040012A8 RID: 4776
	public ApplyAttackState state;
}
