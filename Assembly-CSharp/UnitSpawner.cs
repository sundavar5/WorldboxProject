using System;

// Token: 0x0200024F RID: 591
public class UnitSpawner : BaseBuildingComponent
{
	// Token: 0x06001630 RID: 5680 RVA: 0x000E23C8 File Offset: 0x000E05C8
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this._spawn_timer > 0f)
		{
			this._spawn_timer -= pElapsed;
			return;
		}
		this._spawn_timer = 10f;
		this.trySpawnUnit();
	}

	// Token: 0x06001631 RID: 5681 RVA: 0x000E2400 File Offset: 0x000E0600
	private void trySpawnUnit()
	{
		int tCurrentResidents = this.building.countResidents();
		if (tCurrentResidents >= this.building.asset.housing_slots)
		{
			return;
		}
		Subspecies tSubspecies = null;
		if (tCurrentResidents > 0)
		{
			foreach (long tActorID in this.building.residents)
			{
				Actor tActor = World.world.units.get(tActorID);
				if (!tActor.isRekt())
				{
					tSubspecies = tActor.subspecies;
					break;
				}
			}
		}
		if (!tSubspecies.isRekt() && tSubspecies.hasReachedPopulationLimit())
		{
			return;
		}
		this.spawnUnit(tSubspecies);
	}

	// Token: 0x06001632 RID: 5682 RVA: 0x000E24B4 File Offset: 0x000E06B4
	private void spawnUnit(Subspecies pSubspecies)
	{
		string tStatId = this.building.asset.spawn_units_asset;
		Actor tNewActor = World.world.units.createNewUnit(tStatId, this.building.current_tile, false, 0f, pSubspecies, null, true, true, false, false);
		tNewActor.applyRandomForce(1.5f, 2f);
		this.setUnitFromHere(tNewActor);
	}

	// Token: 0x06001633 RID: 5683 RVA: 0x000E2511 File Offset: 0x000E0711
	public void setUnitFromHere(Actor pActor)
	{
		pActor.setHomeBuilding(this.building);
	}

	// Token: 0x04001286 RID: 4742
	private const float SPAWN_INTERVAL = 10f;

	// Token: 0x04001287 RID: 4743
	private float _spawn_timer = 1f;
}
