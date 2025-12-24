using System;
using System.ComponentModel;
using UnityEngine;

// Token: 0x02000127 RID: 295
[Serializable]
public class KnowledgeAsset : Asset, ILocalizedAsset
{
	// Token: 0x060008F8 RID: 2296 RVA: 0x000813AA File Offset: 0x0007F5AA
	public string getLocaleID()
	{
		return "knowledge_" + this.id;
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x000813BC File Offset: 0x0007F5BC
	public Sprite getIcon()
	{
		if (this._cache_icon == null)
		{
			this._cache_icon = SpriteTextureLoader.getSprite(this.path_icon);
		}
		return this._cache_icon;
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x000813DD File Offset: 0x0007F5DD
	public int countTotal()
	{
		return this.get_library().countTotalKnowledge();
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x000813EF File Offset: 0x0007F5EF
	public int countUnlockedByPlayer()
	{
		return this.get_library().countUnlockedByPlayer();
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x00081401 File Offset: 0x0007F601
	private static void showWindow(KnowledgeAsset pAsset)
	{
		ScrollWindow.showWindow(pAsset.window_id);
	}

	// Token: 0x0400092C RID: 2348
	[DefaultValue(true)]
	public bool show_in_knowledge_window = true;

	// Token: 0x0400092D RID: 2349
	public string path_icon;

	// Token: 0x0400092E RID: 2350
	public string path_icon_easter_egg;

	// Token: 0x0400092F RID: 2351
	public string button_prefab_path;

	// Token: 0x04000930 RID: 2352
	public string window_id;

	// Token: 0x04000931 RID: 2353
	public KnowledgeButtonLoader load_button;

	// Token: 0x04000932 RID: 2354
	public ButtonTipLoader tip_button_loader;

	// Token: 0x04000933 RID: 2355
	public ButtonTooltipLoader show_tooltip;

	// Token: 0x04000934 RID: 2356
	public LibraryGetter get_library;

	// Token: 0x04000935 RID: 2357
	public SpriteGetter get_asset_sprite;

	// Token: 0x04000936 RID: 2358
	public OnKnowledgeIconClick click_icon_action = new OnKnowledgeIconClick(KnowledgeAsset.showWindow);

	// Token: 0x04000937 RID: 2359
	private Sprite _cache_icon;

	// Token: 0x04000938 RID: 2360
	private ILibraryWithUnlockables _cache_library;
}
