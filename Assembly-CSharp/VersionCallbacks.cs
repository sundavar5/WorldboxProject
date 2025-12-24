using System;
using Beebyte.Obfuscator;

// Token: 0x0200049B RID: 1179
[ObfuscateLiterals]
internal static class VersionCallbacks
{
	// Token: 0x0600289A RID: 10394 RVA: 0x00145A68 File Offset: 0x00143C68
	public static void init()
	{
		VersionCallbacks.versionCheck = VersionCheck._vsCheck;
		if (string.IsNullOrEmpty(VersionCallbacks.versionCheck))
		{
			return;
		}
		if (VersionCallbacks.versionCheck.Split('.', StringSplitOptions.None).Length == 3)
		{
			return;
		}
		if (VersionCallbacks.versionCallbacks != null && VersionCallbacks.versionCallbacks.GetInvocationList().Length != 0)
		{
			return;
		}
		TestingCB.init();
	}

	// Token: 0x0600289B RID: 10395 RVA: 0x00145ABC File Offset: 0x00143CBC
	internal static void updateVC(float pElapsed)
	{
		VersionCallbacks.timer -= pElapsed;
		if (VersionCallbacks.timer > 0f)
		{
			return;
		}
		VersionCallbacks.timer = 0f;
		try
		{
			VersionCallbacks.init();
			if (!string.IsNullOrEmpty(VersionCallbacks.versionCheck))
			{
				Action<string> action = VersionCallbacks.versionCallbacks;
				if (action != null)
				{
					action(VersionCallbacks.versionCheck);
				}
			}
			if (VersionCallbacks.versionCheck.Split('.', StringSplitOptions.None).Length != 3)
			{
				VersionCallbacks.timer = Randy.randomFloat(300f, 600f);
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x04001E80 RID: 7808
	internal static Action<string> versionCallbacks;

	// Token: 0x04001E81 RID: 7809
	internal static float timer = 0f;

	// Token: 0x04001E82 RID: 7810
	internal static string versionCheck = string.Empty;
}
