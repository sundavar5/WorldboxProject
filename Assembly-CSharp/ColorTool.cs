using System;
using System.IO;
using UnityEngine;

// Token: 0x02000526 RID: 1318
public class ColorTool : MonoBehaviour
{
	// Token: 0x06002B26 RID: 11046 RVA: 0x001561A3 File Offset: 0x001543A3
	private void resetCoords()
	{
	}

	// Token: 0x06002B27 RID: 11047 RVA: 0x001561A8 File Offset: 0x001543A8
	public void InitKingdoms()
	{
		this.cleanup();
		this.last_editor = "kingdoms";
		KingdomColorsLibrary kingdomColorsLibrary = new KingdomColorsLibrary();
		kingdomColorsLibrary.init();
		kingdomColorsLibrary.post_init();
		foreach (ColorAsset tColor in kingdomColorsLibrary.list)
		{
			this.createColorToolElement(tColor, this.prefabKingdom, this.last_editor);
		}
	}

	// Token: 0x06002B28 RID: 11048 RVA: 0x00156228 File Offset: 0x00154428
	public void InitCultures()
	{
		this.cleanup();
		this.last_editor = "cultures";
		CultureColorsLibrary cultureColorsLibrary = new CultureColorsLibrary();
		cultureColorsLibrary.init();
		cultureColorsLibrary.post_init();
		foreach (ColorAsset tColor in cultureColorsLibrary.list)
		{
			this.createColorToolElement(tColor, this.prefabCulture, this.last_editor);
		}
	}

	// Token: 0x06002B29 RID: 11049 RVA: 0x001562A8 File Offset: 0x001544A8
	public void InitClans()
	{
		this.cleanup();
		this.last_editor = "clans";
		ClanColorsLibrary clanColorsLibrary = new ClanColorsLibrary();
		clanColorsLibrary.init();
		clanColorsLibrary.post_init();
		foreach (ColorAsset tColor in clanColorsLibrary.list)
		{
			this.createColorToolElement(tColor, this.prefabClan, this.last_editor);
		}
	}

	// Token: 0x06002B2A RID: 11050 RVA: 0x00156328 File Offset: 0x00154528
	public void cleanup()
	{
		this.resetCoords();
		while (this.container.childCount > 0)
		{
			Object.DestroyImmediate(this.container.GetChild(0).gameObject);
		}
	}

	// Token: 0x06002B2B RID: 11051 RVA: 0x00156358 File Offset: 0x00154558
	private void createColorToolElement(ColorAsset pColor, GameObject pPrefab, string pWhat)
	{
		ColorToolElement tColorTool = Object.Instantiate<GameObject>(pPrefab, this.container).GetComponent<ColorToolElement>();
		if (this.last_editor == "kingdoms")
		{
			tColorTool.createKingdom(pColor);
		}
		else if (this.last_editor == "clans")
		{
			tColorTool.createClans(pColor);
		}
		else if (this.last_editor == "cultures")
		{
			tColorTool.createCulture(pColor);
		}
		tColorTool.transform.name = pColor.index_id.ToString() + "-" + pColor.id;
		tColorTool.transform.SetSiblingIndex(pColor.index_id);
	}

	// Token: 0x06002B2C RID: 11052 RVA: 0x00156400 File Offset: 0x00154600
	public void saveEditor()
	{
		if (this.last_editor == "kingdoms")
		{
			this.saveKingdoms();
			return;
		}
		if (this.last_editor == "clans")
		{
			this.saveClans();
			return;
		}
		if (this.last_editor == "cultures")
		{
			this.saveCultures();
		}
	}

	// Token: 0x06002B2D RID: 11053 RVA: 0x00156458 File Offset: 0x00154658
	private void convertToolIntoAsset(ColorToolElement pTool, ColorAsset pAsset)
	{
		pAsset.color_main = Toolbox.colorToHex(pTool.colorMain, false);
		pAsset.color_main_2 = Toolbox.colorToHex(pTool.colorMain2, false);
		pAsset.color_banner = Toolbox.colorToHex(pTool.colorBanner, false);
		pAsset.color_text = Toolbox.colorToHex(pTool.colorText, false);
		pAsset.id = pTool.id;
		pAsset.favorite = pTool.favorite;
	}

	// Token: 0x06002B2E RID: 11054 RVA: 0x001564DC File Offset: 0x001546DC
	private void saveKingdoms()
	{
		KingdomColorsLibrary tData = new KingdomColorsLibrary();
		this.saveLib(tData);
	}

	// Token: 0x06002B2F RID: 11055 RVA: 0x001564F8 File Offset: 0x001546F8
	private void saveCultures()
	{
		CultureColorsLibrary tData = new CultureColorsLibrary();
		this.saveLib(tData);
	}

	// Token: 0x06002B30 RID: 11056 RVA: 0x00156514 File Offset: 0x00154714
	private void saveClans()
	{
		ClanColorsLibrary tData = new ClanColorsLibrary();
		this.saveLib(tData);
	}

	// Token: 0x06002B31 RID: 11057 RVA: 0x00156530 File Offset: 0x00154730
	private void saveLib(ColorLibrary pLibrary)
	{
		for (int i = 0; i < this.container.childCount; i++)
		{
			ColorToolElement tColorTool = this.container.GetChild(i).GetComponent<ColorToolElement>();
			ColorAsset tColorAsset = new ColorAsset();
			this.convertToolIntoAsset(tColorTool, tColorAsset);
			tColorAsset.index_id = i;
			pLibrary.list.Add(tColorAsset);
		}
		string tJson = JsonUtility.ToJson(pLibrary, true);
		File.WriteAllText(pLibrary.getEditorPathForSave(), tJson);
	}

	// Token: 0x0400204C RID: 8268
	public string colorString;

	// Token: 0x0400204D RID: 8269
	public GameObject prefabKingdom;

	// Token: 0x0400204E RID: 8270
	public GameObject prefabClan;

	// Token: 0x0400204F RID: 8271
	public GameObject prefabCulture;

	// Token: 0x04002050 RID: 8272
	public GameObject prefabAlliance;

	// Token: 0x04002051 RID: 8273
	public Transform container;

	// Token: 0x04002052 RID: 8274
	public string last_editor = "";
}
