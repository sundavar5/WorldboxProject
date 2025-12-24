using System;

// Token: 0x02000589 RID: 1417
public static class FavoriteWorld
{
	// Token: 0x06002F09 RID: 12041 RVA: 0x0016C2EE File Offset: 0x0016A4EE
	public static void checkFavoriteWorld()
	{
		if (!FavoriteWorld.hasFavoriteWorldSet(false))
		{
			return;
		}
		if (SaveManager.slotExists(PlayerConfig.instance.data.favorite_world))
		{
			return;
		}
		FavoriteWorld.clearFavoriteWorld();
	}

	// Token: 0x06002F0A RID: 12042 RVA: 0x0016C315 File Offset: 0x0016A515
	public static void clearFavoriteWorld()
	{
		PlayerConfig.instance.data.favorite_world = -1;
		PlayerConfig.saveData();
	}

	// Token: 0x06002F0B RID: 12043 RVA: 0x0016C32C File Offset: 0x0016A52C
	public static bool hasFavoriteWorldSet(bool pCheck = true)
	{
		if (pCheck)
		{
			FavoriteWorld.checkFavoriteWorld();
		}
		return PlayerConfig.instance.data.favorite_world != -1;
	}

	// Token: 0x06002F0C RID: 12044 RVA: 0x0016C34B File Offset: 0x0016A54B
	public static void restoreCachedFavoriteWorldOnSuccess()
	{
		if (FavoriteWorld._cache_favorite_world_id == -1)
		{
			return;
		}
		PlayerConfig.instance.data.favorite_world = FavoriteWorld._cache_favorite_world_id;
		PlayerConfig.saveData();
		FavoriteWorld._cache_favorite_world_id = -1;
	}

	// Token: 0x06002F0D RID: 12045 RVA: 0x0016C375 File Offset: 0x0016A575
	public static void cacheSaveSlotID(int pID)
	{
		FavoriteWorld._cache_favorite_world_id = pID;
	}

	// Token: 0x04002300 RID: 8960
	private const int NO_WORLD_SET = -1;

	// Token: 0x04002301 RID: 8961
	private static int _cache_favorite_world_id = -1;
}
