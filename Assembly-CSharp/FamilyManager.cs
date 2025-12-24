using System;
using System.Collections.Generic;

// Token: 0x02000275 RID: 629
public class FamilyManager : MetaSystemManager<Family, FamilyData>
{
	// Token: 0x0600176C RID: 5996 RVA: 0x000E718F File Offset: 0x000E538F
	public FamilyManager()
	{
		this.type_id = "family";
	}

	// Token: 0x0600176D RID: 5997 RVA: 0x000E71A4 File Offset: 0x000E53A4
	public Family newFamily(Actor pActor, WorldTile pTile, Actor pActor2)
	{
		World.world.game_stats.data.familiesCreated += 1L;
		World.world.map_stats.familiesCreated += 1L;
		Family tNewFamily = base.newObject();
		tNewFamily.newFamily(pActor, pActor2, pTile);
		if (pActor.hasFamily())
		{
			tNewFamily.saveOriginFamily1(pActor.family.id);
		}
		pActor.setFamily(tNewFamily);
		if (pActor2 != null)
		{
			if (pActor2.hasFamily())
			{
				tNewFamily.saveOriginFamily2(pActor2.family.id);
			}
			pActor2.setFamily(tNewFamily);
		}
		return tNewFamily;
	}

	// Token: 0x0600176E RID: 5998 RVA: 0x000E723A File Offset: 0x000E543A
	public override void removeObject(Family pObject)
	{
		World.world.game_stats.data.familiesDestroyed += 1L;
		World.world.map_stats.familiesDestroyed += 1L;
		base.removeObject(pObject);
	}

	// Token: 0x0600176F RID: 5999 RVA: 0x000E7278 File Offset: 0x000E5478
	public Family getNearbyFamily(ActorAsset pUnitAsset, WorldTile pTile)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 4, (float)pUnitAsset.family_spawn_radius, true))
		{
			if (tActor.isAlive() && tActor.hasFamily() && !tActor.family.isFull() && tActor.family.isSameSpecies(pUnitAsset.id) && tActor.current_tile.isSameIsland(pTile))
			{
				return tActor.family;
			}
		}
		return null;
	}

	// Token: 0x06001770 RID: 6000 RVA: 0x000E7314 File Offset: 0x000E5514
	protected override void updateDirtyUnits()
	{
		List<Actor> tActorList = World.world.units.units_only_alive;
		for (int i = 0; i < tActorList.Count; i++)
		{
			Actor tUnit = tActorList[i];
			Family tFamily = tUnit.family;
			if (tFamily != null && tFamily.isDirtyUnits())
			{
				tFamily.listUnit(tUnit);
			}
		}
	}
}
