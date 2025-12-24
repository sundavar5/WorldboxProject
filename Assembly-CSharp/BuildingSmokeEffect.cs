using System;
using UnityEngine;

// Token: 0x02000249 RID: 585
public class BuildingSmokeEffect : BaseBuildingComponent
{
	// Token: 0x06001606 RID: 5638 RVA: 0x000E186C File Offset: 0x000DFA6C
	internal override void create(Building pBuilding)
	{
		base.create(pBuilding);
		Sprite tDefaultSprite = this.building.asset.building_sprites.animation_data[0].main[0];
		this.centerTopVec = default(Vector3);
		this.centerTopVec.x = (float)this.building.current_tile.pos.x;
		this.centerTopVec.y = (float)this.building.current_tile.pos.y + tDefaultSprite.rect.height * this.building.asset.scale_base.y;
	}

	// Token: 0x06001607 RID: 5639 RVA: 0x000E191C File Offset: 0x000DFB1C
	public override void update(float pElapsed)
	{
		if (this.building.asset.smoke && !this.building.isUnderConstruction())
		{
			if (this.smokeTimer > 0f)
			{
				this.smokeTimer -= Time.deltaTime;
				return;
			}
			this.smokeTimer = this.building.asset.smoke_interval;
			World.world.particles_smoke.spawn(this.centerTopVec.x, this.centerTopVec.y, true);
		}
	}

	// Token: 0x04001276 RID: 4726
	private float smokeTimer;

	// Token: 0x04001277 RID: 4727
	private Vector3 centerTopVec;
}
