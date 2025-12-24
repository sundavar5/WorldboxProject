using System;

// Token: 0x0200024A RID: 586
public class BuildingSpreadBiome : BaseBuildingComponent
{
	// Token: 0x06001609 RID: 5641 RVA: 0x000E19AC File Offset: 0x000DFBAC
	internal override void create(Building pBuilding)
	{
		base.create(pBuilding);
		this._biome_asset = AssetManager.biome_library.get(pBuilding.asset.spread_biome_id);
	}

	// Token: 0x0600160A RID: 5642 RVA: 0x000E19D0 File Offset: 0x000DFBD0
	public override void update(float pElapsed)
	{
		if (!WorldLawLibrary.world_law_terramorphing.isEnabled())
		{
			return;
		}
		base.update(pElapsed);
		if (this._spread_timer > 0f)
		{
			this._spread_timer -= pElapsed;
			return;
		}
		this._spread_timer = Randy.randomFloat(8f, 16f);
		this.spreadBiome();
	}

	// Token: 0x0600160B RID: 5643 RVA: 0x000E1A28 File Offset: 0x000DFC28
	private void spreadBiome()
	{
		TileTypeBase tSpreadType = this._biome_asset.getTileHigh();
		WorldTile tTile;
		if (this.building.current_tile.Type.biome_asset != tSpreadType.biome_asset)
		{
			tTile = this.building.current_tile;
		}
		else
		{
			tTile = Toolbox.getRandomTileWithinDistance(this.building.current_tile, 2);
		}
		WorldBehaviourActionBiomes.trySpreadBiomeAround(tTile, tSpreadType, false, false, false, true);
	}

	// Token: 0x0600160C RID: 5644 RVA: 0x000E1A89 File Offset: 0x000DFC89
	public override void Dispose()
	{
		this._biome_asset = null;
		base.Dispose();
	}

	// Token: 0x04001278 RID: 4728
	private const float SPREAD_INTERVAL_MIN = 8f;

	// Token: 0x04001279 RID: 4729
	private const float SPREAD_INTERVAL_MAX = 16f;

	// Token: 0x0400127A RID: 4730
	private const int SPREAD_RANGE = 2;

	// Token: 0x0400127B RID: 4731
	private BiomeAsset _biome_asset;

	// Token: 0x0400127C RID: 4732
	private float _spread_timer = 1f;
}
