using System;
using UnityEngine.Device;

// Token: 0x020000EF RID: 239
public static class DynamicSpritesConfig
{
	// Token: 0x17000025 RID: 37
	// (get) Token: 0x0600071B RID: 1819 RVA: 0x0006970D File Offset: 0x0006790D
	public static int texture_size
	{
		get
		{
			if (DynamicSpritesConfig._cached_texture_size == 0)
			{
				DynamicSpritesConfig._cached_texture_size = DynamicSpritesConfig.calculateTargetTextureSize();
			}
			return DynamicSpritesConfig._cached_texture_size;
		}
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x00069728 File Offset: 0x00067928
	private static int calculateTargetTextureSize()
	{
		int tDefaultTextureSize;
		if (Config.isMobile)
		{
			tDefaultTextureSize = 512;
		}
		else
		{
			tDefaultTextureSize = 1024;
		}
		int tMaxTextureSize = SystemInfo.maxTextureSize;
		if (tMaxTextureSize < tDefaultTextureSize)
		{
			tDefaultTextureSize = tMaxTextureSize;
		}
		return tDefaultTextureSize;
	}

	// Token: 0x04000797 RID: 1943
	public const int EDGE_PIXEL = 1;

	// Token: 0x04000798 RID: 1944
	public const int TEXTURE_SIZE_512 = 512;

	// Token: 0x04000799 RID: 1945
	public const int TEXTURE_SIZE_1024 = 1024;

	// Token: 0x0400079A RID: 1946
	public const int TEXTURE_SIZE_2048 = 2048;

	// Token: 0x0400079B RID: 1947
	private static int _cached_texture_size;
}
