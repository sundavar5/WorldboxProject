using System;
using System.IO;
using RSG;

// Token: 0x02000590 RID: 1424
public static class PreviewUploader
{
	// Token: 0x06002F58 RID: 12120 RVA: 0x0016D89C File Offset: 0x0016BA9C
	public static Promise<string> uploadImagePreview()
	{
		string tDateNow = DateTime.UtcNow.ToString("yyyyMMdd");
		return S3Manager.instance.uploadFileToAWS3(string.Concat(new string[]
		{
			"png/",
			tDateNow.ToString(),
			"/",
			Auth.userId,
			"_",
			Guid.NewGuid().ToString(),
			".png"
		}), PreviewUploader.getImagePreview());
	}

	// Token: 0x06002F59 RID: 12121 RVA: 0x0016D91D File Offset: 0x0016BB1D
	private static byte[] getImagePreview()
	{
		return File.ReadAllBytes(SaveManager.getPngSlotPath(-1));
	}
}
