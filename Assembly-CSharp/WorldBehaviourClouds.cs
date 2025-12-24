using System;

// Token: 0x0200030E RID: 782
public class WorldBehaviourClouds
{
	// Token: 0x06001D72 RID: 7538 RVA: 0x001070F8 File Offset: 0x001052F8
	public static void spawnRandomCloud()
	{
		if (World.world_era.clouds == null || World.world_era.clouds.Count == 0)
		{
			return;
		}
		string tRandomCloudID = World.world_era.clouds.GetRandom<string>();
		CloudAsset tAsset = AssetManager.clouds.get(tRandomCloudID);
		if (tAsset == null)
		{
			return;
		}
		if (tAsset.normal_cloud && !WorldLawLibrary.world_law_clouds.isEnabled())
		{
			return;
		}
		if (tAsset.considered_disaster && !WorldLawLibrary.world_law_disasters_nature.isEnabled())
		{
			return;
		}
		EffectsLibrary.spawn("fx_cloud", null, tAsset.id, null, 0f, -1f, -1f, null);
	}

	// Token: 0x06001D73 RID: 7539 RVA: 0x00107190 File Offset: 0x00105390
	public static void setEra(WorldAgeAsset pAsset)
	{
		WorldBehaviourAsset worldBehaviourAsset = AssetManager.world_behaviours.get("clouds");
		worldBehaviourAsset.interval = pAsset.cloud_interval;
		worldBehaviourAsset.interval_random = pAsset.cloud_interval;
	}
}
