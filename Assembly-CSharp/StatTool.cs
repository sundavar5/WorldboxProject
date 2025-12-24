using System;

// Token: 0x020005AC RID: 1452
public static class StatTool
{
	// Token: 0x06003016 RID: 12310 RVA: 0x00174274 File Offset: 0x00172474
	public static float getDPS(Actor pActor)
	{
		ActorAsset tAsset = pActor.asset;
		float num = tAsset.base_stats["damage"];
		float tAttackSpeed = 1f / tAsset.base_stats["attack_speed"];
		return num * tAttackSpeed;
	}

	// Token: 0x06003017 RID: 12311 RVA: 0x001742B1 File Offset: 0x001724B1
	public static float getSecondsLife(Actor pActor)
	{
		return pActor.asset.base_stats["lifespan"] * 60f;
	}

	// Token: 0x06003018 RID: 12312 RVA: 0x001742D0 File Offset: 0x001724D0
	public static string getStringSecondsLife(Actor pActor)
	{
		float tSecondsLife = pActor.asset.base_stats["lifespan"] * 60f;
		return tSecondsLife.ToString("0") + StatTool.toMinutes(tSecondsLife);
	}

	// Token: 0x06003019 RID: 12313 RVA: 0x00174310 File Offset: 0x00172510
	public static string getAmountFood(Actor pActor)
	{
		float tNutritionMax = (float)pActor.asset.nutrition_max;
		float tNutritionDecayInterval = SimGlobals.m.interval_nutrition_decay;
		return (StatTool.getSecondsLife(pActor) / (tNutritionDecayInterval * tNutritionMax)).ToString("0.0");
	}

	// Token: 0x0600301A RID: 12314 RVA: 0x0017434C File Offset: 0x0017254C
	public static string getStringAmountBreeding(Actor pActor)
	{
		ActorAsset tAsset = pActor.asset;
		if (!pActor.hasSubspecies())
		{
			return "0.0";
		}
		float tBreedingTimeout = (float)tAsset.months_breeding_timeout * 5f;
		float tSecondsAvailableFertility = StatTool.getSecondsLife(pActor) - pActor.subspecies.age_breeding * 60f;
		return (tSecondsAvailableFertility / tBreedingTimeout).ToString("0.0") + StatTool.toMinutes(tSecondsAvailableFertility);
	}

	// Token: 0x0600301B RID: 12315 RVA: 0x001743B0 File Offset: 0x001725B0
	private static string toMinutes(float pValue)
	{
		float tMin = pValue / 60f;
		float tGameYears = pValue / 60f;
		return string.Concat(new string[]
		{
			" (",
			tMin.ToString("0.0"),
			"m) ",
			tGameYears.ToString("0.0"),
			"y"
		});
	}
}
