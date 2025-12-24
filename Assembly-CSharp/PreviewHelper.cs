using System;
using System.IO;
using UnityEngine;

// Token: 0x02000735 RID: 1845
public static class PreviewHelper
{
	// Token: 0x06003ABC RID: 15036 RVA: 0x0019EF10 File Offset: 0x0019D110
	public static Sprite loadWorkshopMapPreview()
	{
		string tPathPreviewImage = SaveManager.generatePngPreviewPath(SaveManager.currentWorkshopMapData.main_path);
		if (string.IsNullOrEmpty(tPathPreviewImage) || !File.Exists(tPathPreviewImage))
		{
			return null;
		}
		byte[] pngBytes = File.ReadAllBytes(tPathPreviewImage);
		Texture2D tTexture = new Texture2D(64, 64);
		if (tTexture.LoadImage(pngBytes))
		{
			return Sprite.Create(tTexture, new Rect(0f, 0f, (float)tTexture.width, (float)tTexture.height), new Vector2(0.5f, 0.5f));
		}
		return null;
	}

	// Token: 0x06003ABD RID: 15037 RVA: 0x0019EF90 File Offset: 0x0019D190
	public static Sprite getCurrentWorldPreview()
	{
		World.world.redrawMiniMap(true);
		Texture2D tTexture = Toolbox.ScaleTexture(World.world.world_layer.texture, 512, 512);
		return Sprite.Create(tTexture, new Rect(0f, 0f, (float)tTexture.width, (float)tTexture.height), new Vector2(0f, 0f));
	}

	// Token: 0x06003ABE RID: 15038 RVA: 0x0019EFFC File Offset: 0x0019D1FC
	public static Texture2D convertMapToTexture()
	{
		Texture2D tTextureOrigin = World.world.world_layer.texture;
		Texture2D texture2D = new Texture2D(tTextureOrigin.width, tTextureOrigin.height);
		Color32[] tPixels = tTextureOrigin.GetPixels32();
		texture2D.SetPixels32(tPixels);
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x06003ABF RID: 15039 RVA: 0x0019F040 File Offset: 0x0019D240
	public static int getMaxAdSlots()
	{
		int tRewardAdSlots = 1;
		if (World.world.game_stats.data.gameLaunches > 10L && World.world.game_stats.data.gameTime > 36000.0)
		{
			tRewardAdSlots = 3;
		}
		if (World.world.game_stats.data.gameLaunches > 30L && World.world.game_stats.data.gameTime > 72000.0)
		{
			tRewardAdSlots = 6;
		}
		for (int i = tRewardAdSlots + 1; i <= 6; i++)
		{
			if (SaveManager.slotExists(i))
			{
				return 6;
			}
		}
		return tRewardAdSlots;
	}
}
