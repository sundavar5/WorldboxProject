using System;

// Token: 0x02000242 RID: 578
public class BuildingBiomeFoodProducer : BaseBuildingComponent
{
	// Token: 0x060015ED RID: 5613 RVA: 0x000E0F4C File Offset: 0x000DF14C
	public override void update(float pElapsed)
	{
		if (this.building.city == null)
		{
			return;
		}
		if (!this.building.isUsable())
		{
			return;
		}
		if (this.timer > 0f)
		{
			this.timer -= pElapsed;
			return;
		}
		this.timer = 90f;
		WorldTile tTile = this.building.tiles.GetRandom<WorldTile>();
		string tFoodRes = tTile.Type.food_resource;
		if (string.IsNullOrEmpty(tFoodRes))
		{
			tFoodRes = tTile.main_type.food_resource;
		}
		if (string.IsNullOrEmpty(tFoodRes))
		{
			return;
		}
		if (this.building.city.getResourcesAmount(tFoodRes) >= 10)
		{
			return;
		}
		this.building.city.addResourcesToRandomStockpile(tFoodRes, 1);
	}

	// Token: 0x04001262 RID: 4706
	private const float FOOD_INTERVAL = 90f;

	// Token: 0x04001263 RID: 4707
	private float timer = 90f;
}
