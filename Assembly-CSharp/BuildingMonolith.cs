using System;
using UnityEngine;

// Token: 0x02000248 RID: 584
public class BuildingMonolith : BaseBuildingComponent
{
	// Token: 0x06001602 RID: 5634 RVA: 0x000E1690 File Offset: 0x000DF890
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (Date.isMonolithMonth() && this.building.is_visible && this.building.isNormal() && Time.frameCount % 30 == 0)
		{
			EffectsLibrary.spawnAt("fx_monolith_glow_2", this.building.current_tile.posV3, this.building.current_scale.y);
		}
		if (this._action_timer > 0f)
		{
			this._action_timer -= pElapsed;
			return;
		}
		if (Date.isMonolithMonth())
		{
			this._action_timer = 10f;
			this.doMonolithAction(this.building.current_tile, false);
		}
	}

	// Token: 0x06001603 RID: 5635 RVA: 0x000E173C File Offset: 0x000DF93C
	internal void doMonolithAction(WorldTile pFromTile, bool pForce = false)
	{
		if (!WorldLawLibrary.world_law_evolution_events.isEnabled())
		{
			return;
		}
		this.spawnMainEffect();
		World.world.applyForceOnTile(this.building.current_tile, 10, 3f, true, 0, null, null, null, false);
		int tMax = 3;
		int tCount = 0;
		foreach (Actor tActor in Finder.getUnitsFromChunk(pFromTile, 1, 0f, true))
		{
			if (!tActor.hasStatus("confused") && tActor.hasSubspecies() && (Date.isMonolithMonth() || pForce))
			{
				if (ActionLibrary.tryToEvolveUnitViaMonolith(tActor))
				{
					tCount++;
				}
				if (tCount >= tMax)
				{
					break;
				}
			}
		}
	}

	// Token: 0x06001604 RID: 5636 RVA: 0x000E17F4 File Offset: 0x000DF9F4
	public void spawnMainEffect()
	{
		EffectsLibrary.spawnAt("fx_monolith_launch_bottom", this.building.current_tile.posV3, this.building.current_scale.y);
		EffectsLibrary.spawnAt("fx_monolith_launch", this.building.current_tile.posV3, this.building.current_scale.y);
	}

	// Token: 0x04001274 RID: 4724
	private const float ACTION_INTERVAL = 10f;

	// Token: 0x04001275 RID: 4725
	private float _action_timer = 10f;
}
