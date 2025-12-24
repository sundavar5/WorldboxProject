using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x0200011B RID: 283
[Serializable]
public class KingdomAsset : Asset
{
	// Token: 0x17000035 RID: 53
	// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0007D806 File Offset: 0x0007BA06
	// (set) Token: 0x060008B6 RID: 2230 RVA: 0x0007D839 File Offset: 0x0007BA39
	[JsonIgnore]
	public ColorAsset debug_color_asset
	{
		get
		{
			if (this._debug_color_asset == null)
			{
				KingdomColorsLibrary kingdom_colors_library = AssetManager.kingdom_colors_library;
				ColorAsset debug_color_asset;
				if (kingdom_colors_library == null)
				{
					debug_color_asset = null;
				}
				else
				{
					List<ColorAsset> list = kingdom_colors_library.list;
					debug_color_asset = ((list != null) ? list.GetRandom<ColorAsset>() : null);
				}
				this._debug_color_asset = debug_color_asset;
			}
			return this._debug_color_asset;
		}
		set
		{
			this._debug_color_asset = value;
		}
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x0007D842 File Offset: 0x0007BA42
	public Sprite getSprite()
	{
		if (this._cached_sprite == null)
		{
			this._cached_sprite = SpriteTextureLoader.getSprite(this.path_icon);
		}
		return this._cached_sprite;
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x0007D869 File Offset: 0x0007BA69
	public void setIcon(string pPath)
	{
		this.path_icon = pPath;
		this.show_icon = true;
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x0007D879 File Offset: 0x0007BA79
	public void addTag(string pTag)
	{
		this.list_tags.Add(pTag);
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x0007D888 File Offset: 0x0007BA88
	public void addFriendlyTag(string pTag)
	{
		this.friendly_tags.Add(pTag);
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x0007D897 File Offset: 0x0007BA97
	public void addEnemyTag(string pTag)
	{
		this.enemy_tags.Add(pTag);
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x0007D8A8 File Offset: 0x0007BAA8
	public bool isFoe(KingdomAsset pTarget)
	{
		int tCachedResult = 0;
		int tHashCode = pTarget.GetHashCode();
		this._cached_enemies.TryGetValue(tHashCode, out tCachedResult);
		if (tCachedResult != 0)
		{
			return tCachedResult == 1;
		}
		if (this.nature || pTarget.nature)
		{
			this._cached_enemies.Add(tHashCode, -1);
			return false;
		}
		if (this == pTarget)
		{
			this._cached_enemies.Add(tHashCode, this.always_attack_each_other ? 1 : -1);
			return this.always_attack_each_other;
		}
		if (this.enemy_tags.Count > 0 && this.enemy_tags.Overlaps(pTarget.list_tags))
		{
			this._cached_enemies.Add(tHashCode, 1);
			return true;
		}
		pTarget.list_tags.Add(pTarget.id);
		this.list_tags.Add(this.id);
		if (this.friendly_tags.Count > 0 && this.friendly_tags.Overlaps(pTarget.list_tags))
		{
			this._cached_enemies.Add(tHashCode, -1);
			return false;
		}
		this._cached_enemies.Add(tHashCode, 1);
		return true;
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x0007D9A9 File Offset: 0x0007BBA9
	public void clearKingdomColor()
	{
		this.default_kingdom_color = null;
	}

	// Token: 0x040008FD RID: 2301
	public bool civ;

	// Token: 0x040008FE RID: 2302
	[DefaultValue(-1)]
	public int default_civ_color_index = -1;

	// Token: 0x040008FF RID: 2303
	public bool nomads;

	// Token: 0x04000900 RID: 2304
	public bool nature;

	// Token: 0x04000901 RID: 2305
	public bool abandoned;

	// Token: 0x04000902 RID: 2306
	public bool concept;

	// Token: 0x04000903 RID: 2307
	public bool always_attack_each_other;

	// Token: 0x04000904 RID: 2308
	public bool units_always_looking_for_enemies;

	// Token: 0x04000905 RID: 2309
	[NonSerialized]
	public HashSet<string> assets_discrepancies;

	// Token: 0x04000906 RID: 2310
	[NonSerialized]
	public HashSet<string> assets_discrepancies_bad;

	// Token: 0x04000907 RID: 2311
	public bool force_look_all_chunks;

	// Token: 0x04000908 RID: 2312
	public bool mobs;

	// Token: 0x04000909 RID: 2313
	public bool neutral;

	// Token: 0x0400090A RID: 2314
	public bool brain;

	// Token: 0x0400090B RID: 2315
	public bool group_miniciv;

	// Token: 0x0400090C RID: 2316
	public bool group_minicivs_cool;

	// Token: 0x0400090D RID: 2317
	public bool group_creeps;

	// Token: 0x0400090E RID: 2318
	public bool group_main;

	// Token: 0x0400090F RID: 2319
	public bool is_forced_by_trait;

	// Token: 0x04000910 RID: 2320
	[DefaultValue("")]
	public string forced_by_trait_kingdom_id = string.Empty;

	// Token: 0x04000911 RID: 2321
	[DefaultValue("")]
	public string building_attractor_id = string.Empty;

	// Token: 0x04000912 RID: 2322
	public bool count_as_danger = true;

	// Token: 0x04000913 RID: 2323
	public HashSet<string> friendly_tags = new HashSet<string>();

	// Token: 0x04000914 RID: 2324
	public HashSet<string> enemy_tags = new HashSet<string>();

	// Token: 0x04000915 RID: 2325
	public HashSet<string> list_tags = new HashSet<string>();

	// Token: 0x04000916 RID: 2326
	private Dictionary<int, int> _cached_enemies = new Dictionary<int, int>();

	// Token: 0x04000917 RID: 2327
	public ColorAsset default_kingdom_color;

	// Token: 0x04000918 RID: 2328
	public Color color_building = Color.white;

	// Token: 0x04000919 RID: 2329
	private Sprite _cached_sprite;

	// Token: 0x0400091A RID: 2330
	public string path_icon = "ui/Icons/iconWarning";

	// Token: 0x0400091B RID: 2331
	public bool show_icon;

	// Token: 0x0400091C RID: 2332
	public bool friendship_for_everyone;

	// Token: 0x0400091D RID: 2333
	private ColorAsset _debug_color_asset;
}
