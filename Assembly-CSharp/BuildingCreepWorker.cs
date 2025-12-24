using System;

// Token: 0x02000245 RID: 581
public class BuildingCreepWorker : IDisposable
{
	// Token: 0x060015F3 RID: 5619 RVA: 0x000E1144 File Offset: 0x000DF344
	public BuildingCreepWorker(BuildingCreepHUB pParent)
	{
		this._parent = pParent;
		this._this_creep_biome = AssetManager.biome_library.get(this._parent.building.asset.grow_creep_type);
		this.steps_max = this._parent.building.asset.grow_creep_steps_max;
	}

	// Token: 0x060015F4 RID: 5620 RVA: 0x000E11A0 File Offset: 0x000DF3A0
	public void update()
	{
		if (this.cur_tile == null)
		{
			this._total_step_counter = 0;
			this.cur_tile = this._parent.building.current_tile;
			this.cur_direction = Randy.getRandom<ActorDirection>(Toolbox.directions);
		}
		this.checkRandomDirectionChange();
		this.updateMovement(this.cur_tile);
		if (this._total_step_counter > this.steps_max)
		{
			this.cur_tile = null;
		}
	}

	// Token: 0x060015F5 RID: 5621 RVA: 0x000E120C File Offset: 0x000DF40C
	private void checkRandomDirectionChange()
	{
		if (this._parent.building.asset.grow_creep_random_new_direction)
		{
			if (this._direction_step_amount >= this._parent.building.asset.grow_creep_steps_before_new_direction)
			{
				this.cur_direction = Randy.getRandom<ActorDirection>(Toolbox.directions);
				this._direction_step_amount = 0;
			}
			this._direction_step_amount++;
		}
	}

	// Token: 0x060015F6 RID: 5622 RVA: 0x000E1274 File Offset: 0x000DF474
	private void creepFlash(int pVal = 15)
	{
		if (this._parent.building.asset.grow_creep_flash)
		{
			World.world.flash_effects.flashPixel(this.cur_tile, pVal, ColorType.White);
		}
		if (this._parent.building.asset.grow_creep_redraw_tile)
		{
			World.world.redrawRenderedTile(this.cur_tile);
		}
	}

	// Token: 0x060015F7 RID: 5623 RVA: 0x000E12D8 File Offset: 0x000DF4D8
	private void updateMovement(WorldTile pNextTile)
	{
		this.cur_tile = pNextTile;
		if (this.canPlaceWorkerOn(this.cur_tile))
		{
			this.makeCreep(this.cur_tile);
			this.creepFlash(15);
			this._total_step_counter++;
			return;
		}
		if (this.cur_tile.Type.biome_asset == this._this_creep_biome)
		{
			this.creepFlash(15);
			pNextTile = this.getNextRandomTile(this.cur_tile);
			if (pNextTile == null)
			{
				this.cur_tile = null;
				return;
			}
			if (this.canPlaceWorkerOn(pNextTile))
			{
				this.cur_tile = pNextTile;
				return;
			}
			if (pNextTile.Type.biome_asset == this._this_creep_biome)
			{
				this.cur_tile = pNextTile;
				return;
			}
			if (pNextTile.Type.biome_asset != this._this_creep_biome)
			{
				this.creepFlash(30);
				this.cur_tile = pNextTile;
				return;
			}
			if (pNextTile.getCreepTileRank() == TileRank.Nothing)
			{
				pNextTile = this.cur_tile;
				this.cur_direction = Randy.getRandom<ActorDirection>(Toolbox.directions);
				return;
			}
		}
		else
		{
			this.cur_tile = null;
		}
	}

	// Token: 0x060015F8 RID: 5624 RVA: 0x000E13D0 File Offset: 0x000DF5D0
	private bool canPlaceWorkerOn(WorldTile pTile)
	{
		return pTile.getCreepTileRank() != TileRank.Nothing && (!pTile.Type.creep || pTile.Type.biome_asset != this._this_creep_biome);
	}

	// Token: 0x060015F9 RID: 5625 RVA: 0x000E1400 File Offset: 0x000DF600
	private void makeCreep(WorldTile pTile)
	{
		TopTileType tBiomeTileType = AssetManager.biome_library.get(this._parent.building.asset.grow_creep_type).getTile(pTile);
		if (tBiomeTileType == null)
		{
			return;
		}
		MapAction.terraformTop(pTile, tBiomeTileType, TerraformLibrary.flash, false);
	}

	// Token: 0x060015FA RID: 5626 RVA: 0x000E1444 File Offset: 0x000DF644
	private WorldTile getNextRandomTile(WorldTile pTile)
	{
		switch (this._parent.building.asset.grow_creep_movement_type)
		{
		case CreepWorkerMovementType.RandomNeighbourAll:
			return pTile.neighboursAll.GetRandom<WorldTile>();
		case CreepWorkerMovementType.RandomNeighbour:
			return pTile.neighbours.GetRandom<WorldTile>();
		case CreepWorkerMovementType.Direction:
			return this.getDirectionTile(pTile, this._parent.building.asset.grow_creep_direction_random_position);
		default:
			return pTile.neighboursAll.GetRandom<WorldTile>();
		}
	}

	// Token: 0x060015FB RID: 5627 RVA: 0x000E14BC File Offset: 0x000DF6BC
	private WorldTile getDirectionTile(WorldTile pTile, bool pAddRandom = true)
	{
		int tX = pTile.pos.x;
		int tY = pTile.pos.y;
		ActorDirection actorDirection = this.cur_direction;
		switch (actorDirection)
		{
		case ActorDirection.Up:
			if (pAddRandom)
			{
				tX += Randy.randomInt(-1, 2);
			}
			tY++;
			break;
		case ActorDirection.UpRight:
		case ActorDirection.UpLeft:
			break;
		case ActorDirection.Right:
			tX++;
			if (pAddRandom)
			{
				tY += Randy.randomInt(-1, 2);
			}
			break;
		case ActorDirection.Down:
			if (pAddRandom)
			{
				tX += Randy.randomInt(-1, 2);
			}
			tY--;
			break;
		default:
			if (actorDirection == ActorDirection.Left)
			{
				tX--;
				if (pAddRandom)
				{
					tY += Randy.randomInt(-1, 2);
				}
			}
			break;
		}
		return World.world.GetTile(tX, tY);
	}

	// Token: 0x060015FC RID: 5628 RVA: 0x000E1564 File Offset: 0x000DF764
	public void Dispose()
	{
		this._parent = null;
		this.cur_tile = null;
		this._this_creep_biome = null;
	}

	// Token: 0x0400126B RID: 4715
	private int steps_max;

	// Token: 0x0400126C RID: 4716
	private WorldTile cur_tile;

	// Token: 0x0400126D RID: 4717
	private ActorDirection cur_direction;

	// Token: 0x0400126E RID: 4718
	private BuildingCreepHUB _parent;

	// Token: 0x0400126F RID: 4719
	private int _total_step_counter;

	// Token: 0x04001270 RID: 4720
	private BiomeAsset _this_creep_biome;

	// Token: 0x04001271 RID: 4721
	private int _direction_step_amount;
}
