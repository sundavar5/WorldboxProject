using System;
using RSG;

// Token: 0x0200058F RID: 1423
public static class MapUploader
{
	// Token: 0x06002F56 RID: 12118 RVA: 0x0016D808 File Offset: 0x0016BA08
	public static Promise<string> uploadMap()
	{
		string tDateNow = DateTime.UtcNow.ToString("yyyyMMdd");
		return S3Manager.instance.uploadFileToAWS3(string.Concat(new string[]
		{
			"wbox/",
			tDateNow.ToString(),
			"/",
			Auth.userId,
			"_",
			Guid.NewGuid().ToString(),
			".wbox"
		}), MapUploader.getMapData());
	}

	// Token: 0x06002F57 RID: 12119 RVA: 0x0016D889 File Offset: 0x0016BA89
	private static byte[] getMapData()
	{
		return SaveManager.getMapFromPath(SaveManager.currentSavePath, false).toZip();
	}
}
