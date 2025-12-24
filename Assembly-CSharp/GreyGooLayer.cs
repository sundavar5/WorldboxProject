using System;
using System.Collections.Generic;

// Token: 0x02000405 RID: 1029
public class GreyGooLayer : BaseModule
{
	// Token: 0x06002386 RID: 9094 RVA: 0x00127C78 File Offset: 0x00125E78
	internal override void create()
	{
		base.create();
		this.hashset = new HashSet<WorldTile>();
	}

	// Token: 0x06002387 RID: 9095 RVA: 0x00127C8B File Offset: 0x00125E8B
	internal override void clear()
	{
		base.clear();
		this.hashset.Clear();
		this._to_remove.Clear();
		this._to_add.Clear();
		this._initiated = false;
	}

	// Token: 0x06002388 RID: 9096 RVA: 0x00127CBB File Offset: 0x00125EBB
	public bool isActive()
	{
		return this.hashset.Count > 0;
	}

	// Token: 0x06002389 RID: 9097 RVA: 0x00127CCC File Offset: 0x00125ECC
	private void init()
	{
		this._initiated = true;
		foreach (WorldTile tTile in World.world.tiles_list)
		{
			if (tTile.Type.grey_goo)
			{
				this.add(tTile);
			}
		}
	}

	// Token: 0x0600238A RID: 9098 RVA: 0x00127D14 File Offset: 0x00125F14
	public override void update(float pElapsed)
	{
		if (!this._initiated)
		{
			this.init();
		}
		base.update(pElapsed);
		if (!this.isActive())
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		if (this.timer > 0f)
		{
			this.timer -= pElapsed;
			return;
		}
		this.timer = 0.08f;
		this._to_remove.Clear();
		this._to_add.Clear();
		this.updateGooTiles();
		this.removeFromHashset();
		this.addToHashset();
	}

	// Token: 0x0600238B RID: 9099 RVA: 0x00127D9C File Offset: 0x00125F9C
	private void updateGooTiles()
	{
		if (this.hashset.Count == 0)
		{
			return;
		}
		foreach (WorldTile tTile in this.hashset)
		{
			if (!tTile.Type.grey_goo)
			{
				this._to_remove.Add(tTile);
			}
			else
			{
				if (tTile.hasBuilding())
				{
					tTile.building.startDestroyBuilding();
				}
				if (Randy.randomChance(0.05f))
				{
					this.terraform(tTile);
					this.checkAroundTiles(tTile);
					this._to_remove.Add(tTile);
				}
				else if (Randy.randomChance(0.05f))
				{
					this.checkAroundTiles(tTile);
				}
				else if (Randy.randomChance(0.09f) && this.areAroundTilesEmpty(tTile))
				{
					this._to_remove.Add(tTile);
					if (tTile.Type.grey_goo)
					{
						this.terraform(tTile);
					}
				}
			}
		}
	}

	// Token: 0x0600238C RID: 9100 RVA: 0x00127EA0 File Offset: 0x001260A0
	private void removeFromHashset()
	{
		if (this._to_remove.Count == 0)
		{
			return;
		}
		for (int i = 0; i < this._to_remove.Count; i++)
		{
			WorldTile tTile = this._to_remove[i];
			this.remove(tTile);
		}
	}

	// Token: 0x0600238D RID: 9101 RVA: 0x00127EE8 File Offset: 0x001260E8
	private void addToHashset()
	{
		if (this._to_add.Count == 0)
		{
			return;
		}
		for (int i = 0; i < this._to_add.Count; i++)
		{
			WorldTile tTile = this._to_add[i];
			this.add(tTile);
		}
	}

	// Token: 0x0600238E RID: 9102 RVA: 0x00127F30 File Offset: 0x00126130
	private void checkAroundTiles(WorldTile pTile)
	{
		if (WorldLawLibrary.world_law_gaias_covenant.isEnabled())
		{
			return;
		}
		foreach (WorldTile tTile in pTile.neighbours)
		{
			TileTypeBase tType = tTile.Type;
			if (!tType.grey_goo && !tType.IsType("pit_deep_ocean") && (!tType.IsType("deep_ocean") || tTile.hasBuilding()))
			{
				this._to_add.Add(tTile);
			}
		}
	}

	// Token: 0x0600238F RID: 9103 RVA: 0x00127FA0 File Offset: 0x001261A0
	private bool areAroundTilesEmpty(WorldTile pTile)
	{
		foreach (WorldTile worldTile in pTile.neighbours)
		{
			TileTypeBase tType = worldTile.Type;
			if (worldTile.hasBuilding())
			{
				return false;
			}
			if (!tType.grey_goo && !tType.considered_empty_tile)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06002390 RID: 9104 RVA: 0x00127FE8 File Offset: 0x001261E8
	private void makeGoo(WorldTile pTile)
	{
		pTile.unfreeze(99);
		MapAction.terraformMain(pTile, TileLibrary.grey_goo, false);
	}

	// Token: 0x06002391 RID: 9105 RVA: 0x00127FFE File Offset: 0x001261FE
	private void terraform(WorldTile pTile)
	{
		MapAction.terraformMain(pTile, TileLibrary.pit_deep_ocean, TerraformLibrary.grey_goo, false);
		MusicBox.playSound("event:/SFX/DESTRUCTION/GreyGooEat", pTile, false, true);
	}

	// Token: 0x06002392 RID: 9106 RVA: 0x0012801E File Offset: 0x0012621E
	public void remove(WorldTile pTile)
	{
		this.hashset.Remove(pTile);
	}

	// Token: 0x06002393 RID: 9107 RVA: 0x0012802D File Offset: 0x0012622D
	public void add(WorldTile pTile)
	{
		if (pTile.Type.considered_empty_tile)
		{
			return;
		}
		if (this.hashset.Add(pTile))
		{
			this.makeGoo(pTile);
		}
	}

	// Token: 0x040019AF RID: 6575
	private const float SPREAD_CHANCE = 0.05f;

	// Token: 0x040019B0 RID: 6576
	private const float REMOVE_CHANCE = 0.09f;

	// Token: 0x040019B1 RID: 6577
	private const float INTERVAL = 0.08f;

	// Token: 0x040019B2 RID: 6578
	private List<WorldTile> _to_remove = new List<WorldTile>();

	// Token: 0x040019B3 RID: 6579
	private List<WorldTile> _to_add = new List<WorldTile>();

	// Token: 0x040019B4 RID: 6580
	private bool _initiated;
}
