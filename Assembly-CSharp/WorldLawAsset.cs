using System;
using System.Runtime.CompilerServices;

// Token: 0x020001B2 RID: 434
[Serializable]
public class WorldLawAsset : BaseAugmentationAsset, IDescription2Asset, IDescriptionAsset, ILocalizedAsset
{
	// Token: 0x17000063 RID: 99
	// (get) Token: 0x06000C9E RID: 3230 RVA: 0x000B7F7E File Offset: 0x000B617E
	private static WorldLaws _world_laws
	{
		get
		{
			return World.world.world_laws;
		}
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x000B7F8A File Offset: 0x000B618A
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isEnabled()
	{
		return this._cached_enabled;
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x000B7F92 File Offset: 0x000B6192
	public bool isEnabledRaw()
	{
		return this.getOption().boolVal;
	}

	// Token: 0x06000CA1 RID: 3233 RVA: 0x000B7F9F File Offset: 0x000B619F
	public PlayerOptionData getOption()
	{
		return WorldLawAsset._world_laws.dict[this.id];
	}

	// Token: 0x06000CA2 RID: 3234 RVA: 0x000B7FB6 File Offset: 0x000B61B6
	public void updateCachedEnabled(WorldLaws pWorldLaws)
	{
		this._cached_enabled = pWorldLaws.isEnabled(this.id);
	}

	// Token: 0x06000CA3 RID: 3235 RVA: 0x000B7FCA File Offset: 0x000B61CA
	public void toggle(bool pState)
	{
		this.getOption().boolVal = pState;
		this._cached_enabled = pState;
	}

	// Token: 0x06000CA4 RID: 3236 RVA: 0x000B7FDF File Offset: 0x000B61DF
	public override BaseCategoryAsset getGroup()
	{
		return AssetManager.world_law_groups.get(this.group_id);
	}

	// Token: 0x06000CA5 RID: 3237 RVA: 0x000B7FF1 File Offset: 0x000B61F1
	public override string getLocaleID()
	{
		return this.id + "_title";
	}

	// Token: 0x06000CA6 RID: 3238 RVA: 0x000B8003 File Offset: 0x000B6203
	public string getDescriptionID()
	{
		return this.id + "_description";
	}

	// Token: 0x06000CA7 RID: 3239 RVA: 0x000B8015 File Offset: 0x000B6215
	public string getDescriptionID2()
	{
		return this.id + "_description_2";
	}

	// Token: 0x06000CA8 RID: 3240 RVA: 0x000B8027 File Offset: 0x000B6227
	public string getTranslatedName()
	{
		return LocalizedTextManager.getText(this.getLocaleID(), null, false);
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x000B8036 File Offset: 0x000B6236
	public string getTranslatedDescription()
	{
		return LocalizedTextManager.getText(this.getDescriptionID(), null, false);
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x000B8045 File Offset: 0x000B6245
	public string getTranslatedDescription2()
	{
		return LocalizedTextManager.getText(this.getDescriptionID2(), null, false);
	}

	// Token: 0x04000C20 RID: 3104
	public bool default_state;

	// Token: 0x04000C21 RID: 3105
	public PlayerOptionAction on_state_change;

	// Token: 0x04000C22 RID: 3106
	public PlayerOptionAction on_state_enabled;

	// Token: 0x04000C23 RID: 3107
	public OnWorldLoadAction on_world_load;

	// Token: 0x04000C24 RID: 3108
	public string icon_path;

	// Token: 0x04000C25 RID: 3109
	public bool can_turn_off = true;

	// Token: 0x04000C26 RID: 3110
	public bool requires_premium;

	// Token: 0x04000C27 RID: 3111
	private bool _cached_enabled;
}
