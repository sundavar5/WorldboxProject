using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009D RID: 157
[Serializable]
public class Achievement : Asset, IDescriptionAsset, ILocalizedAsset
{
	// Token: 0x060004D9 RID: 1241 RVA: 0x00033524 File Offset: 0x00031724
	public void checkBySignal(object pCheckData = null)
	{
		SignalManager.add(this._signal, pCheckData);
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x00033534 File Offset: 0x00031734
	public bool check(object pCheckData = null)
	{
		if (AchievementLibrary.isUnlocked(this))
		{
			return true;
		}
		bool tCheck = true;
		if (this.action != null)
		{
			tCheck = this.action(pCheckData);
		}
		if (tCheck)
		{
			AchievementLibrary.unlock(this);
			this.checkUnlockables();
			return true;
		}
		return false;
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x00033574 File Offset: 0x00031774
	private void checkUnlockables()
	{
		if (!this.isUnlocked())
		{
			return;
		}
		if (this.unlocks_something)
		{
			foreach (BaseUnlockableAsset baseUnlockableAsset in this.unlock_assets)
			{
				baseUnlockableAsset.unlock(true);
			}
		}
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x000335D8 File Offset: 0x000317D8
	public bool isUnlocked()
	{
		return AchievementLibrary.isUnlocked(this);
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x000335E0 File Offset: 0x000317E0
	public string getLocaleID()
	{
		return this.locale_key;
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x000335E8 File Offset: 0x000317E8
	public string getDescriptionID()
	{
		return this.getLocaleID() + "_description";
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x000335FC File Offset: 0x000317FC
	public Sprite getIcon()
	{
		if (this.cached_sprite == null)
		{
			this.cached_sprite = SpriteTextureLoader.getSprite(this.icon);
		}
		if (this.cached_sprite == null)
		{
			Debug.LogError("Error: Sprite not found : " + this.icon);
		}
		return this.cached_sprite;
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x0003364B File Offset: 0x0003184B
	public void setSignal(SignalAsset pSignal)
	{
		this._signal = pSignal;
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x00033654 File Offset: 0x00031854
	public SignalAsset getSignal()
	{
		return this._signal;
	}

	// Token: 0x04000510 RID: 1296
	public string play_store_id;

	// Token: 0x04000511 RID: 1297
	public string steam_id;

	// Token: 0x04000512 RID: 1298
	public bool hidden;

	// Token: 0x04000513 RID: 1299
	public string group = "miscellaneous";

	// Token: 0x04000514 RID: 1300
	public string icon;

	// Token: 0x04000515 RID: 1301
	public string locale_key;

	// Token: 0x04000516 RID: 1302
	public AchievementCheck action;

	// Token: 0x04000517 RID: 1303
	public bool unlocks_something;

	// Token: 0x04000518 RID: 1304
	public List<BaseUnlockableAsset> unlock_assets;

	// Token: 0x04000519 RID: 1305
	[NonSerialized]
	protected Sprite cached_sprite;

	// Token: 0x0400051A RID: 1306
	[NonSerialized]
	private SignalAsset _signal;

	// Token: 0x0400051B RID: 1307
	public bool has_signal;
}
