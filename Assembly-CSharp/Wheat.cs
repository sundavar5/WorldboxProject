using System;

// Token: 0x02000250 RID: 592
public class Wheat : BaseBuildingComponent
{
	// Token: 0x06001635 RID: 5685 RVA: 0x000E2532 File Offset: 0x000E0732
	internal override void create(Building pBuilding)
	{
		base.create(pBuilding);
		this._current_level = 0;
		this._max_level = this.building.asset.building_sprites.animation_data.Count - 1;
		this.checkSprite();
	}

	// Token: 0x06001636 RID: 5686 RVA: 0x000E256C File Offset: 0x000E076C
	public override void update(float pElapsed)
	{
		this.building.data.grow_time += pElapsed;
		if (this.isMaxLevel())
		{
			return;
		}
		float tTimeToGrow = this.building.asset.growth_time * (float)(this._current_level + 1);
		if (this.building.data.grow_time < tTimeToGrow)
		{
			return;
		}
		this.grow();
	}

	// Token: 0x06001637 RID: 5687 RVA: 0x000E25D0 File Offset: 0x000E07D0
	internal void grow()
	{
		if (this._current_level < this._max_level)
		{
			this._current_level++;
			this.checkSprite();
		}
		MusicBox.playSound("event:/SFX/DROPS/DropSeedGrass", this.building.current_tile, true, true);
		if (!World.world_era.flag_crops_grow && Randy.randomBool())
		{
			this.building.startDestroyBuilding();
		}
	}

	// Token: 0x06001638 RID: 5688 RVA: 0x000E2634 File Offset: 0x000E0834
	public void growFull()
	{
		this._current_level = this._max_level;
		this.checkSprite();
		MusicBox.playSound("event:/SFX/DROPS/DropSeedGrass", this.building.current_tile, true, true);
	}

	// Token: 0x06001639 RID: 5689 RVA: 0x000E2660 File Offset: 0x000E0860
	private void checkSprite()
	{
		this.building.setAnimData(this._current_level);
		if (this.building.asset.random_flip && !this.building.asset.shadow)
		{
			this.building.flip_x = Randy.randomBool();
		}
		this.building.setScaleTween(0f, 0.2f, 1f, null, null, 0);
		World.world.setTileDirty(this.building.current_tile);
	}

	// Token: 0x0600163A RID: 5690 RVA: 0x000E26E4 File Offset: 0x000E08E4
	public bool isMaxLevel()
	{
		return this._current_level == this._max_level;
	}

	// Token: 0x0600163B RID: 5691 RVA: 0x000E26F4 File Offset: 0x000E08F4
	public int getCurrentLevel()
	{
		return this._current_level;
	}

	// Token: 0x04001288 RID: 4744
	private int _current_level;

	// Token: 0x04001289 RID: 4745
	private int _max_level;
}
