using System;

// Token: 0x020004A9 RID: 1193
public static class InitAds
{
	// Token: 0x06002929 RID: 10537 RVA: 0x001479E6 File Offset: 0x00145BE6
	public static void initAdProviders()
	{
		if (Config.adsInitialized)
		{
			return;
		}
		if (InitAds.initiated)
		{
			return;
		}
		InitAds.initiated = true;
	}

	// Token: 0x04001ECF RID: 7887
	private static bool initiated;
}
