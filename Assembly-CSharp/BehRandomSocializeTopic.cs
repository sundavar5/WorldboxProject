using System;
using ai.behaviours;

// Token: 0x020003DB RID: 987
public class BehRandomSocializeTopic : BehaviourActionActor
{
	// Token: 0x0600229E RID: 8862 RVA: 0x00122541 File Offset: 0x00120741
	public BehRandomSocializeTopic(float pMinTimer, float pMaxTimer, float pChance)
	{
		this.socialize = true;
		this._timer_min = pMinTimer;
		this._timer_max = pMaxTimer;
		this._chance = pChance;
	}

	// Token: 0x0600229F RID: 8863 RVA: 0x00122568 File Offset: 0x00120768
	public override BehResult execute(Actor pActor)
	{
		pActor.clearLastTopicSprite();
		if (!pActor.hasSubspecies())
		{
			return BehResult.Stop;
		}
		if (!pActor.subspecies.has_advanced_communication)
		{
			return BehResult.Stop;
		}
		if (!Randy.randomChance(this._chance))
		{
			return BehResult.Stop;
		}
		float tWait = Randy.randomFloat(this._timer_min, this._timer_max);
		pActor.makeWait(tWait);
		return BehResult.Continue;
	}

	// Token: 0x040018F6 RID: 6390
	private float _timer_min;

	// Token: 0x040018F7 RID: 6391
	private float _timer_max;

	// Token: 0x040018F8 RID: 6392
	private float _chance;
}
