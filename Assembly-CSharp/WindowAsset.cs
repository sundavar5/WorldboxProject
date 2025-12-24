using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// Token: 0x02000805 RID: 2053
[Serializable]
public class WindowAsset : Asset
{
	// Token: 0x0600406E RID: 16494 RVA: 0x001B82BF File Offset: 0x001B64BF
	public Sprite getSprite()
	{
		if (this._cached_icon == null)
		{
			this._cached_icon = SpriteTextureLoader.getSprite("ui/Icons/" + this.icon_path);
		}
		return this._cached_icon;
	}

	// Token: 0x0600406F RID: 16495 RVA: 0x001B82EA File Offset: 0x001B64EA
	private static IEnumerable<string> getDefaultHoveringIconPath(WindowAsset pAsset)
	{
		yield return pAsset.icon_path;
		yield break;
	}

	// Token: 0x04002EA3 RID: 11939
	public bool preload;

	// Token: 0x04002EA4 RID: 11940
	public string related_parent_window;

	// Token: 0x04002EA5 RID: 11941
	[NonSerialized]
	public MetaTypeAsset meta_type_asset;

	// Token: 0x04002EA6 RID: 11942
	[DefaultValue(true)]
	public bool window_toolbar_enabled = true;

	// Token: 0x04002EA7 RID: 11943
	public string icon_path = "iconAye";

	// Token: 0x04002EA8 RID: 11944
	public HoveringBGIconsGetter get_hovering_icons = new HoveringBGIconsGetter(WindowAsset.getDefaultHoveringIconPath);

	// Token: 0x04002EA9 RID: 11945
	[DefaultValue(true)]
	public bool is_testable = true;

	// Token: 0x04002EAA RID: 11946
	[NonSerialized]
	private Sprite _cached_icon;
}
