using System;
using System.Collections.Generic;

// Token: 0x02000215 RID: 533
public class ArmyManager : MetaSystemManager<Army, ArmyData>
{
	// Token: 0x06001339 RID: 4921 RVA: 0x000D70B4 File Offset: 0x000D52B4
	public ArmyManager()
	{
		this.type_id = "army";
	}

	// Token: 0x0600133A RID: 4922 RVA: 0x000D70C8 File Offset: 0x000D52C8
	public Army newArmy(Actor pActor, City pCity)
	{
		World.world.game_stats.data.armiesCreated += 1L;
		World.world.map_stats.armiesCreated += 1L;
		Army tNewArmy = base.newObject();
		tNewArmy.createArmy(pActor, pCity);
		pActor.setArmy(tNewArmy);
		pCity.setArmy(tNewArmy);
		return tNewArmy;
	}

	// Token: 0x0600133B RID: 4923 RVA: 0x000D7128 File Offset: 0x000D5328
	public override void removeObject(Army pObject)
	{
		World.world.game_stats.data.armiesDestroyed += 1L;
		World.world.map_stats.armiesDestroyed += 1L;
		base.removeObject(pObject);
	}

	// Token: 0x0600133C RID: 4924 RVA: 0x000D7168 File Offset: 0x000D5368
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		foreach (Army army in this)
		{
			army.checkCaptainExistence();
		}
	}

	// Token: 0x0600133D RID: 4925 RVA: 0x000D71B4 File Offset: 0x000D53B4
	protected override void updateDirtyUnits()
	{
		List<Actor> tActorList = World.world.units.units_only_alive;
		for (int i = 0; i < tActorList.Count; i++)
		{
			Actor tUnit = tActorList[i];
			Army tArmy = tUnit.army;
			if (tArmy != null && tArmy.isDirtyUnits())
			{
				tArmy.listUnit(tUnit);
			}
		}
	}
}
