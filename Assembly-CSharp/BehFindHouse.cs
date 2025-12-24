using System;
using ai.behaviours;

// Token: 0x02000397 RID: 919
public class BehFindHouse : BehCityActor
{
	// Token: 0x060021B8 RID: 8632 RVA: 0x0011D404 File Offset: 0x0011B604
	public override BehResult execute(Actor pActor)
	{
		if (pActor.hasHouse())
		{
			return BehResult.Stop;
		}
		Building tBuilding = null;
		foreach (Building tCityBuilding in pActor.city.buildings)
		{
			if (!tCityBuilding.isUnderConstruction() && tCityBuilding.hasResidentSlots())
			{
				tBuilding = tCityBuilding;
				break;
			}
		}
		if (tBuilding == null)
		{
			tBuilding = BehFindHouse.tryToFindFamilyHouse(pActor);
		}
		if (tBuilding == null)
		{
			return BehResult.Stop;
		}
		pActor.setHomeBuilding(tBuilding);
		pActor.changeHappiness("just_found_house", tBuilding.asset.housing_happiness);
		return BehResult.Continue;
	}

	// Token: 0x060021B9 RID: 8633 RVA: 0x0011D4A4 File Offset: 0x0011B6A4
	private static Building tryToFindFamilyHouse(Actor pActor)
	{
		if (!pActor.hasFamily())
		{
			return null;
		}
		int tCheckCount = 0;
		Family tFamily = pActor.family;
		foreach (Actor tFamilyMember in pActor.family.units.LoopRandom<Actor>())
		{
			if (tFamilyMember != pActor)
			{
				if (++tCheckCount > 5)
				{
					break;
				}
				if (tFamilyMember.hasHouse() && tFamilyMember.city == pActor.city)
				{
					Building tBuilding = BehFindHouse.checkBuilding(tFamilyMember.home_building, tFamily);
					if (tBuilding != null)
					{
						return tBuilding;
					}
				}
			}
		}
		return null;
	}

	// Token: 0x060021BA RID: 8634 RVA: 0x0011D548 File Offset: 0x0011B748
	private static Building checkBuilding(Building pGetHomeBuilding, Family pFamily)
	{
		foreach (long tID in pGetHomeBuilding.residents)
		{
			Actor tActor = BehaviourActionBase<Actor>.world.units.get(tID);
			if (tActor != null && tActor.isAlive() && tActor.family == pFamily)
			{
				tActor.clearHomeBuilding();
				return pGetHomeBuilding;
			}
		}
		return null;
	}
}
