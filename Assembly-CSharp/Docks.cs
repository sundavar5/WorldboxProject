using System;
using System.Collections.Generic;
using UnityPools;

// Token: 0x0200024C RID: 588
public class Docks : BaseBuildingComponent
{
	// Token: 0x06001619 RID: 5657 RVA: 0x000E1E0E File Offset: 0x000E000E
	internal override void create(Building pBuilding)
	{
		base.create(pBuilding);
		this.tiles_ocean = new ListPool<WorldTile>();
		this._boat_types = UnsafeCollectionPool<Dictionary<string, int>, KeyValuePair<string, int>>.Get();
	}

	// Token: 0x0600161A RID: 5658 RVA: 0x000E1E2D File Offset: 0x000E002D
	public TileIsland getIsland()
	{
		if (!this.building.hasCity())
		{
			return null;
		}
		WorldTile tile = this.building.city.getTile(false);
		if (tile == null)
		{
			return null;
		}
		return tile.region.island;
	}

	// Token: 0x0600161B RID: 5659 RVA: 0x000E1E60 File Offset: 0x000E0060
	public WorldTile getOceanTileInSameOcean(WorldTile pTile)
	{
		foreach (WorldTile tOceanTile in this.tiles_ocean.LoopRandom<WorldTile>())
		{
			if (tOceanTile.isSameIsland(pTile))
			{
				return tOceanTile;
			}
		}
		return null;
	}

	// Token: 0x0600161C RID: 5660 RVA: 0x000E1EBC File Offset: 0x000E00BC
	public bool hasOceanTiles()
	{
		this.recalculateOceanTiles();
		return this.tiles_ocean.Count > 0;
	}

	// Token: 0x0600161D RID: 5661 RVA: 0x000E1ED4 File Offset: 0x000E00D4
	public void recalculateOceanTiles()
	{
		this.tiles_ocean.Clear();
		WorldTile tTile = World.world.GetTile(this.building.current_tile.x - 4, this.building.current_tile.y);
		if (tTile != null && tTile.isGoodForBoat())
		{
			this.tiles_ocean.Add(tTile);
		}
		tTile = World.world.GetTile(this.building.current_tile.x + 5, this.building.current_tile.y);
		if (tTile != null && tTile.isGoodForBoat())
		{
			this.tiles_ocean.Add(tTile);
		}
		tTile = World.world.GetTile(this.building.current_tile.x, this.building.current_tile.y - 4);
		if (tTile != null && tTile.isGoodForBoat())
		{
			this.tiles_ocean.Add(tTile);
		}
		tTile = World.world.GetTile(this.building.current_tile.x, this.building.current_tile.y + 7);
		if (tTile != null && tTile.isGoodForBoat())
		{
			this.tiles_ocean.Add(tTile);
		}
		if (this.tiles_ocean.Count == 0)
		{
			this.building.startDestroyBuilding();
		}
	}

	// Token: 0x0600161E RID: 5662 RVA: 0x000E2014 File Offset: 0x000E0214
	public bool isDockGood()
	{
		if (this.tiles_ocean.Count == 0)
		{
			return false;
		}
		for (int i = 0; i < this.tiles_ocean.Count; i++)
		{
			if (!this.tiles_ocean[i].Type.ocean)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600161F RID: 5663 RVA: 0x000E2064 File Offset: 0x000E0264
	private bool ifStayingOnGround()
	{
		for (int i = 0; i < this.building.tiles.Count; i++)
		{
			if (this.building.tiles[i].Type.ground)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001620 RID: 5664 RVA: 0x000E20AC File Offset: 0x000E02AC
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this.ifStayingOnGround())
		{
			this.building.getHit(1000f, true, AttackType.Other, null, true, false, true);
		}
		if (this.building.hasCity() && this.building.city.buildings.Count == 1)
		{
			this.building.getHit(1000f, true, AttackType.Other, null, true, false, true);
		}
	}

	// Token: 0x06001621 RID: 5665 RVA: 0x000E211C File Offset: 0x000E031C
	public int countBoatTypes(string pType)
	{
		int tValue;
		this._boat_types.TryGetValue(pType, out tValue);
		return tValue;
	}

	// Token: 0x06001622 RID: 5666 RVA: 0x000E2139 File Offset: 0x000E0339
	public bool isFull(string pType)
	{
		return this.countBoatTypes(pType) >= 1;
	}

	// Token: 0x06001623 RID: 5667 RVA: 0x000E2148 File Offset: 0x000E0348
	public bool isOverfilled(string pType)
	{
		return this.countBoatTypes(pType) > 1;
	}

	// Token: 0x06001624 RID: 5668 RVA: 0x000E2158 File Offset: 0x000E0358
	public Actor buildBoatFromHere(City pCity)
	{
		ActorAsset tNewBoatAsset = this.building.asset.getRandomBoatAssetToBuild(pCity);
		if (tNewBoatAsset == null)
		{
			return null;
		}
		if (this.countBoatTypes(tNewBoatAsset.boat_type) >= 1)
		{
			return null;
		}
		if (!pCity.hasEnoughResourcesFor(tNewBoatAsset.cost))
		{
			return null;
		}
		if (this.tiles_ocean.Count == 0)
		{
			this.recalculateOceanTiles();
			return null;
		}
		WorldTile tTile = this.tiles_ocean.GetRandom<WorldTile>();
		if (!tTile.region.island.goodForDocks())
		{
			return null;
		}
		Actor tNewBoat = World.world.units.createNewUnit(tNewBoatAsset.id, tTile, false, 0f, null, null, true, false, false, false);
		if (tNewBoat == null)
		{
			return null;
		}
		this.addBoatToDock(tNewBoat);
		pCity.spendResourcesForBuildingAsset(tNewBoatAsset.cost);
		return tNewBoat;
	}

	// Token: 0x06001625 RID: 5669 RVA: 0x000E220E File Offset: 0x000E040E
	public void clearBoatCounter()
	{
		this._boat_types.Clear();
	}

	// Token: 0x06001626 RID: 5670 RVA: 0x000E221C File Offset: 0x000E041C
	public void increaseBoatCounter(Actor pActor)
	{
		int tCount = this.countBoatTypes(pActor.asset.boat_type);
		this._boat_types[pActor.asset.boat_type] = tCount + 1;
	}

	// Token: 0x06001627 RID: 5671 RVA: 0x000E2256 File Offset: 0x000E0456
	public void addBoatToDock(Actor pBoat)
	{
		pBoat.setHomeBuilding(this.building);
		pBoat.joinCity(this.building.city);
		this.increaseBoatCounter(pBoat);
	}

	// Token: 0x06001628 RID: 5672 RVA: 0x000E227C File Offset: 0x000E047C
	public override void Dispose()
	{
		base.Dispose();
		this.tiles_ocean.Dispose();
		this.tiles_ocean = null;
		this._boat_types.Clear();
		UnsafeCollectionPool<Dictionary<string, int>, KeyValuePair<string, int>>.Release(this._boat_types);
		this._boat_types = null;
	}

	// Token: 0x04001282 RID: 4738
	public ListPool<WorldTile> tiles_ocean;

	// Token: 0x04001283 RID: 4739
	private Dictionary<string, int> _boat_types;
}
