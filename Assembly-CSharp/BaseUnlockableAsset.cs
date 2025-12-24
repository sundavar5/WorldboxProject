using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// Token: 0x020000A5 RID: 165
[Serializable]
public class BaseUnlockableAsset : Asset, ILocalizedAsset
{
	// Token: 0x1700001A RID: 26
	// (get) Token: 0x0600056E RID: 1390 RVA: 0x0005307C File Offset: 0x0005127C
	protected GameProgressData _progress_data
	{
		get
		{
			GameProgress instance = GameProgress.instance;
			if (instance == null)
			{
				return null;
			}
			return instance.data;
		}
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x0600056F RID: 1391 RVA: 0x0005308E File Offset: 0x0005128E
	protected virtual HashSet<string> progress_elements
	{
		get
		{
			throw new NotImplementedException(base.GetType().Name);
		}
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x000530A0 File Offset: 0x000512A0
	public void setUnlockedWithAchievement(string pAchievementID)
	{
		this.unlocked_with_achievement = true;
		this.achievement_id = pAchievementID;
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x000530B0 File Offset: 0x000512B0
	public virtual bool unlock(bool pSaveData = true)
	{
		if (this.progress_elements == null)
		{
			return false;
		}
		if (this.isAvailable())
		{
			return false;
		}
		bool tAvailableAlready = false;
		ActorAsset tActorAsset = this as ActorAsset;
		if (tActorAsset != null)
		{
			string tAssetId = string.IsNullOrEmpty(tActorAsset.base_asset_id) ? this.id : tActorAsset.base_asset_id;
			tAvailableAlready = AssetManager.actor_library.get(tAssetId).isAvailable();
		}
		this.progress_elements.Add(this.id);
		if (pSaveData)
		{
			GameProgress.saveData();
		}
		if (!this.unlocked_with_achievement && this.has_locales && !tAvailableAlready)
		{
			WorldTip.showNowTop("new_knowledge_gain".Localize().Replace("$knowledge$", this.getLocaleID().Localize()), false);
		}
		return true;
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x0005315D File Offset: 0x0005135D
	public bool isUnlocked()
	{
		return this.isDebugUnlockedAll() || this.isCheatEnabled() || this.progress_elements.Contains(this.id);
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x00053184 File Offset: 0x00051384
	public bool isAvailable()
	{
		if (this.isDebugUnlockedAll())
		{
			return true;
		}
		if (this.isCheatEnabled())
		{
			return true;
		}
		if (this.unlocked_with_achievement)
		{
			return GameProgress.isAchievementUnlocked(this.achievement_id);
		}
		return !this.needs_to_be_explored || this.isUnlocked();
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x000531BE File Offset: 0x000513BE
	public bool isUnlockedByPlayer()
	{
		if (this.unlocked_with_achievement)
		{
			return GameProgress.isAchievementUnlocked(this.achievement_id);
		}
		return !this.needs_to_be_explored || this.progress_elements.Contains(this.id);
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x000531EF File Offset: 0x000513EF
	protected virtual bool isDebugUnlockedAll()
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x00053201 File Offset: 0x00051401
	public bool isCheatEnabled()
	{
		return WorldLawLibrary.world_law_cursed_world.isEnabled();
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x0005320D File Offset: 0x0005140D
	public bool ShouldSerializebase_stats()
	{
		return !this.base_stats.isEmpty();
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x0005321D File Offset: 0x0005141D
	public virtual string getLocaleID()
	{
		if (!this.has_locales)
		{
			return null;
		}
		return this.id;
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x0005322F File Offset: 0x0005142F
	public string getAchievementLocaleID()
	{
		Achievement achievement = this.getAchievement();
		if (achievement == null)
		{
			return null;
		}
		return achievement.getLocaleID();
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x00053242 File Offset: 0x00051442
	public Achievement getAchievement()
	{
		if (!this.unlocked_with_achievement)
		{
			return null;
		}
		return AssetManager.achievements.get(this.achievement_id);
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x0005325E File Offset: 0x0005145E
	public virtual Sprite getSprite()
	{
		if (this.cached_sprite == null)
		{
			this.cached_sprite = SpriteTextureLoader.getSprite(this.path_icon);
		}
		return this.cached_sprite;
	}

	// Token: 0x040005C3 RID: 1475
	public string path_icon;

	// Token: 0x040005C4 RID: 1476
	[DefaultValue(true)]
	public bool show_for_unlockables_ui = true;

	// Token: 0x040005C5 RID: 1477
	[DefaultValue(null)]
	public BaseStats base_stats;

	// Token: 0x040005C6 RID: 1478
	public bool unlocked_with_achievement;

	// Token: 0x040005C7 RID: 1479
	[DefaultValue(true)]
	public bool needs_to_be_explored = true;

	// Token: 0x040005C8 RID: 1480
	public string achievement_id;

	// Token: 0x040005C9 RID: 1481
	[DefaultValue(true)]
	public bool show_in_knowledge_window = true;

	// Token: 0x040005CA RID: 1482
	[NonSerialized]
	protected Sprite cached_sprite;

	// Token: 0x040005CB RID: 1483
	[DefaultValue(true)]
	public bool has_locales = true;
}
