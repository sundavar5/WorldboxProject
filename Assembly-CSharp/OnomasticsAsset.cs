using System;
using System.ComponentModel;
using UnityEngine;

// Token: 0x02000142 RID: 322
[Serializable]
public class OnomasticsAsset : Asset, IDescription2Asset, IDescriptionAsset, ILocalizedAsset
{
	// Token: 0x060009BF RID: 2495 RVA: 0x0009013B File Offset: 0x0008E33B
	public bool isGroupType()
	{
		return this.type == OnomasticsAssetType.Group;
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x00090146 File Offset: 0x0008E346
	public Sprite getSprite()
	{
		if (this._cached_sprite == null)
		{
			this._cached_sprite = SpriteTextureLoader.getSprite(this.path_icon);
		}
		return this._cached_sprite;
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x00090167 File Offset: 0x0008E367
	public string getLocaleID()
	{
		return "onomastics_" + this.id;
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x00090179 File Offset: 0x0008E379
	public string getIDSubname()
	{
		if (this.forced_locale_subname_id != string.Empty)
		{
			return this.forced_locale_subname_id;
		}
		return "onomastics_" + this.id + "_subname";
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x000901A9 File Offset: 0x0008E3A9
	public string getDescriptionID()
	{
		if (this.forced_locale_description_id != string.Empty)
		{
			return this.forced_locale_description_id;
		}
		return this.getLocaleID() + "_info";
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x000901D4 File Offset: 0x0008E3D4
	public string getDescriptionID2()
	{
		if (this.forced_locale_description_id_2 != string.Empty)
		{
			return this.forced_locale_description_id_2;
		}
		return this.getLocaleID() + "_info_2";
	}

	// Token: 0x040009AF RID: 2479
	public OnomasticsAssetType type;

	// Token: 0x040009B0 RID: 2480
	public string path_icon;

	// Token: 0x040009B1 RID: 2481
	public string color_text = "#FF0000";

	// Token: 0x040009B2 RID: 2482
	[NonSerialized]
	private Sprite _cached_sprite;

	// Token: 0x040009B3 RID: 2483
	public bool affects_left;

	// Token: 0x040009B4 RID: 2484
	public bool affects_left_word;

	// Token: 0x040009B5 RID: 2485
	public bool affects_left_group_only;

	// Token: 0x040009B6 RID: 2486
	public bool affects_everything;

	// Token: 0x040009B7 RID: 2487
	public bool is_divider;

	// Token: 0x040009B8 RID: 2488
	public bool is_upper;

	// Token: 0x040009B9 RID: 2489
	public bool is_word_divider;

	// Token: 0x040009BA RID: 2490
	public bool is_immune;

	// Token: 0x040009BB RID: 2491
	[DefaultValue(-1)]
	public int group_id = -1;

	// Token: 0x040009BC RID: 2492
	[DefaultValue('?')]
	public char short_id = '?';

	// Token: 0x040009BD RID: 2493
	[DefaultValue("")]
	public string forced_locale_subname_id = string.Empty;

	// Token: 0x040009BE RID: 2494
	[DefaultValue("")]
	public string forced_locale_description_id = string.Empty;

	// Token: 0x040009BF RID: 2495
	[DefaultValue("")]
	public string forced_locale_description_id_2 = string.Empty;

	// Token: 0x040009C0 RID: 2496
	public OnomasticsNameMakerDelegate namemaker_delegate;

	// Token: 0x040009C1 RID: 2497
	public OnomasticsCheckDelegate check_delegate;
}
