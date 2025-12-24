using System;
using ai.behaviours;

// Token: 0x020003D7 RID: 983
public class BehTrySleep : BehaviourActionActor
{
	// Token: 0x06002285 RID: 8837 RVA: 0x0012194F File Offset: 0x0011FB4F
	public BehTrySleep(bool pSleepOutside = false)
	{
		this._sleep_outside = pSleepOutside;
	}

	// Token: 0x06002286 RID: 8838 RVA: 0x00121960 File Offset: 0x0011FB60
	public override BehResult execute(Actor pActor)
	{
		float tWaitTimer = this.getWaitTimer(pActor);
		pActor.makeSleep(tWaitTimer);
		if (pActor.hasCity() && !pActor.hasHouse() && pActor.isSapient())
		{
			pActor.changeHappiness("slept_outside", 0);
		}
		return BehResult.Continue;
	}

	// Token: 0x06002287 RID: 8839 RVA: 0x001219A4 File Offset: 0x0011FBA4
	private float getWaitTimer(Actor pActor)
	{
		if (!pActor.hasSubspecies())
		{
			return 20f;
		}
		WorldAgeAsset tWorldAge = BehaviourActionBase<Actor>.world.era_manager.getCurrentAge();
		Subspecies tSubspecies = pActor.subspecies;
		bool tShouldHibernate = false;
		if (tWorldAge.flag_winter && tSubspecies.hasTrait("winter_slumberers"))
		{
			tShouldHibernate = true;
		}
		else if (tWorldAge.flag_night && tSubspecies.hasTrait("nocturnal_dormancy"))
		{
			tShouldHibernate = true;
		}
		else if (!tWorldAge.flag_chaos && tSubspecies.hasTrait("chaos_driven"))
		{
			tShouldHibernate = true;
		}
		else if (tWorldAge.flag_light_age && tSubspecies.hasTrait("circadian_drift"))
		{
			tShouldHibernate = true;
		}
		float tSleepTimer;
		if (tShouldHibernate)
		{
			tSleepTimer = 100f;
		}
		else
		{
			float tMin = 20f;
			float tMax = 60f;
			if (tSubspecies.hasTrait("monophasic_sleep"))
			{
				tMin = 40f;
				tMax = 90f;
			}
			tSleepTimer = Randy.randomFloat(tMin, tMax);
			if (tSubspecies.hasTrait("prolonged_rest"))
			{
				tSleepTimer += Randy.randomFloat(tMin, tMax);
			}
		}
		return tSleepTimer;
	}

	// Token: 0x040018F5 RID: 6389
	private bool _sleep_outside;
}
