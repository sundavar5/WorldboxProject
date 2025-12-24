using System;

// Token: 0x02000251 RID: 593
public abstract class BuildingWaypoint : BaseBuildingComponent
{
	// Token: 0x17000134 RID: 308
	// (get) Token: 0x0600163D RID: 5693
	protected abstract string effect_id { get; }

	// Token: 0x17000135 RID: 309
	// (get) Token: 0x0600163E RID: 5694
	protected abstract string trait_id { get; }

	// Token: 0x0600163F RID: 5695 RVA: 0x000E2704 File Offset: 0x000E0904
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this._action_timer > 0f)
		{
			this._action_timer -= pElapsed;
			return;
		}
		this._action_timer = 20f;
		this.doAction(this.building.current_tile);
	}

	// Token: 0x06001640 RID: 5696 RVA: 0x000E2750 File Offset: 0x000E0950
	internal void doAction(WorldTile pFromTile)
	{
		this.spawnMainEffect();
		World.world.applyForceOnTile(this.building.current_tile, 10, 3f, true, 0, null, null, null, false);
		int tCount = 0;
		foreach (Actor tActor in Finder.getUnitsFromChunk(pFromTile, 1, 0f, true))
		{
			if (!tActor.hasTrait(this.trait_id))
			{
				if (tActor.addTrait(this.trait_id, false))
				{
					tCount++;
				}
				if (tCount >= 5)
				{
					break;
				}
			}
		}
	}

	// Token: 0x06001641 RID: 5697 RVA: 0x000E27EC File Offset: 0x000E09EC
	public void spawnMainEffect()
	{
		EffectsLibrary.spawnAt(this.effect_id, this.building.current_tile.posV3, this.building.current_scale.y);
	}

	// Token: 0x0400128A RID: 4746
	private const int UNITS_AFFECTED_PER_ACTION = 5;

	// Token: 0x0400128B RID: 4747
	private const float ACTION_INTERVAL = 20f;

	// Token: 0x0400128C RID: 4748
	private float _action_timer = 20f;
}
