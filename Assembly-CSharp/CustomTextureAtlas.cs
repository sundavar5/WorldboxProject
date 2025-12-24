using System;

// Token: 0x0200044D RID: 1101
public class CustomTextureAtlas
{
	// Token: 0x06002615 RID: 9749 RVA: 0x00137FAF File Offset: 0x001361AF
	public static bool filesExists()
	{
		return true;
	}

	// Token: 0x06002616 RID: 9750 RVA: 0x00137FB2 File Offset: 0x001361B2
	public static void createUnityBin()
	{
	}

	// Token: 0x06002617 RID: 9751 RVA: 0x00137FB4 File Offset: 0x001361B4
	private static void save(string pData)
	{
	}

	// Token: 0x06002618 RID: 9752 RVA: 0x00137FB6 File Offset: 0x001361B6
	internal static void delete(string pTexture)
	{
	}

	// Token: 0x06002619 RID: 9753 RVA: 0x00137FB8 File Offset: 0x001361B8
	public static string createTextureID(string pString)
	{
		string tTexturePreID = CustomTextureAtlas.width.ToString() + CustomTextureAtlas.height.ToString();
		return Toolbox.textureID(pString, tTexturePreID);
	}

	// Token: 0x04001CD2 RID: 7378
	private static int width = 1202;

	// Token: 0x04001CD3 RID: 7379
	private static int height = 2021;
}
