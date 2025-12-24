using System;
using ai.behaviours;

// Token: 0x020003C0 RID: 960
public class BehLaunchFireworks : BehaviourActionActor
{
	// Token: 0x0600223E RID: 8766 RVA: 0x0012039E File Offset: 0x0011E59E
	public override BehResult execute(Actor pActor)
	{
		if (!pActor.hasEnoughMoney(SimGlobals.m.festive_fireworks_cost))
		{
			return BehResult.Stop;
		}
		pActor.finishStatusEffect("festive_spirit");
		this.spawnFireworksByUnit(pActor);
		pActor.spendMoney(SimGlobals.m.festive_fireworks_cost);
		return BehResult.Continue;
	}

	// Token: 0x0600223F RID: 8767 RVA: 0x001203D7 File Offset: 0x0011E5D7
	internal void spawnFireworksByUnit(Actor pActor)
	{
		EffectsLibrary.spawn("fx_fireworks", pActor.current_tile, null, null, 0f, -1f, -1f, null);
	}
}
