using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000226 RID: 550
public class MetaObjectWithTraits<TData, TBaseTrait> : MetaObject<TData>, ITraitsOwner<TBaseTrait> where TData : MetaObjectData where TBaseTrait : BaseTrait<TBaseTrait>
{
	// Token: 0x06001470 RID: 5232 RVA: 0x000DB038 File Offset: 0x000D9238
	public override void loadData(TData pData)
	{
		base.loadData(pData);
		this.loadTraits();
	}

	// Token: 0x06001471 RID: 5233 RVA: 0x000DB048 File Offset: 0x000D9248
	private void resetStatsAndCallbacks()
	{
		this.all_actions_actor_death = null;
		this.all_actions_actor_growth = null;
		this.all_actions_actor_birth = null;
		this.all_actions_actor_attack_target = null;
		this.all_actions_actor_get_hit = null;
		this.all_actions_actor_special_effect.Clear();
		this.base_stats.clear();
		this.base_stats_meta.clear();
		this.decisions_assets.Clear();
		this.combat_actions.reset();
		this.spells.reset();
	}

	// Token: 0x06001472 RID: 5234 RVA: 0x000DB0BA File Offset: 0x000D92BA
	public void forceRecalcBaseStats()
	{
		this.recalcBaseStats();
	}

	// Token: 0x06001473 RID: 5235 RVA: 0x000DB0C4 File Offset: 0x000D92C4
	protected virtual void recalcBaseStats()
	{
		this.resetStatsAndCallbacks();
		foreach (TBaseTrait tTrait in this._traits)
		{
			this.base_stats.mergeStats(tTrait.base_stats, 1f);
			this.base_stats_meta.mergeStats(tTrait.base_stats_meta, 1f);
			this.all_actions_actor_death = (WorldAction)Delegate.Combine(this.all_actions_actor_death, tTrait.action_death);
			this.all_actions_actor_growth = (WorldAction)Delegate.Combine(this.all_actions_actor_growth, tTrait.action_growth);
			this.all_actions_actor_birth = (WorldAction)Delegate.Combine(this.all_actions_actor_birth, tTrait.action_birth);
			this.all_actions_actor_attack_target = (AttackAction)Delegate.Combine(this.all_actions_actor_attack_target, tTrait.action_attack_target);
			this.all_actions_actor_get_hit = (GetHitAction)Delegate.Combine(this.all_actions_actor_get_hit, tTrait.action_get_hit);
			if (tTrait.action_special_effect != null)
			{
				this.all_actions_actor_special_effect.Add(tTrait);
			}
			if (tTrait.hasDecisions())
			{
				this.decisions_assets.AddRange(tTrait.decisions_assets);
			}
			if (tTrait.hasCombatActions())
			{
				this.combat_actions.mergeWith(tTrait.combat_actions);
			}
			if (tTrait.hasSpells())
			{
				this.spells.mergeWith(tTrait.spells);
				foreach (SpellAsset tSpell in tTrait.spells)
				{
					if (tSpell.hasDecisions())
					{
						this.decisions_assets.AddRange(tSpell.decisions_assets);
					}
				}
			}
		}
		this.setUnitStatsDirty();
	}

	// Token: 0x06001474 RID: 5236 RVA: 0x000DB2F8 File Offset: 0x000D94F8
	private void setUnitStatsDirty()
	{
		List<Actor> tUnits = base.units;
		int tLength = tUnits.Count;
		for (int i = 0; i < tLength; i++)
		{
			tUnits[i].setStatsDirty();
		}
	}

	// Token: 0x06001475 RID: 5237 RVA: 0x000DB32C File Offset: 0x000D952C
	private void loadTraits()
	{
		if (this.saved_traits == null)
		{
			return;
		}
		this.fillTraitAssetsFromStringList(this.saved_traits);
		foreach (TBaseTrait tTrait in this._traits)
		{
			WorldActionTrait action_on_augmentation_load = tTrait.action_on_augmentation_load;
			if (action_on_augmentation_load != null)
			{
				action_on_augmentation_load(this, tTrait);
			}
		}
	}

	// Token: 0x06001476 RID: 5238 RVA: 0x000DB3AC File Offset: 0x000D95AC
	protected void fillTraitAssetsFromStringList(List<string> pList)
	{
		foreach (string tID in pList.LoopRandom<string>())
		{
			TBaseTrait tTrait = this.trait_library.get(tID);
			if (tTrait != null && !this.hasOppositeTrait(tTrait))
			{
				this._traits.Add(tTrait);
			}
		}
		this.recalcBaseStats();
	}

	// Token: 0x06001477 RID: 5239 RVA: 0x000DB424 File Offset: 0x000D9624
	protected override void generateNewMetaObject()
	{
		base.generateNewMetaObject();
		if (this.default_traits != null)
		{
			this.fillTraitAssetsFromStringList(this.default_traits);
		}
	}

	// Token: 0x06001478 RID: 5240 RVA: 0x000DB440 File Offset: 0x000D9640
	protected override void generateNewMetaObject(bool pAddDefaultTraits)
	{
		base.generateNewMetaObject();
		if (this.default_traits != null && pAddDefaultTraits)
		{
			this.fillTraitAssetsFromStringList(this.default_traits);
		}
	}

	// Token: 0x06001479 RID: 5241 RVA: 0x000DB461 File Offset: 0x000D9661
	public List<string> getTraitsAsStrings()
	{
		return Toolbox.getListForSave<TBaseTrait>(this._traits);
	}

	// Token: 0x0600147A RID: 5242 RVA: 0x000DB470 File Offset: 0x000D9670
	public string getTraitsAsLocalizedString()
	{
		string tResult = "";
		foreach (TBaseTrait tBaseTrait in this._traits)
		{
			tResult = tResult + tBaseTrait.getTranslatedName() + ", ";
		}
		return tResult;
	}

	// Token: 0x0600147B RID: 5243 RVA: 0x000DB4DC File Offset: 0x000D96DC
	public void copyTraits(IReadOnlyCollection<TBaseTrait> pTraitsToCopy)
	{
		foreach (TBaseTrait tTrait in pTraitsToCopy)
		{
			if (!this.hasOppositeTrait(tTrait))
			{
				this._traits.Add(tTrait);
			}
		}
		this.recalcBaseStats();
	}

	// Token: 0x0600147C RID: 5244 RVA: 0x000DB53C File Offset: 0x000D973C
	protected void clearTraits()
	{
		if (this._traits.Count == 0)
		{
			return;
		}
		this._traits.Clear();
	}

	// Token: 0x0600147D RID: 5245 RVA: 0x000DB557 File Offset: 0x000D9757
	public IReadOnlyCollection<TBaseTrait> getTraits()
	{
		return this._traits;
	}

	// Token: 0x0600147E RID: 5246 RVA: 0x000DB560 File Offset: 0x000D9760
	public void sortTraits(IReadOnlyCollection<TBaseTrait> pTraits)
	{
		if (!this._traits.SetEquals(pTraits))
		{
			return;
		}
		this._traits.Clear();
		foreach (TBaseTrait tTrait in pTraits)
		{
			this._traits.Add(tTrait);
		}
	}

	// Token: 0x0600147F RID: 5247 RVA: 0x000DB5C8 File Offset: 0x000D97C8
	public virtual void traitModifiedEvent()
	{
	}

	// Token: 0x06001480 RID: 5248 RVA: 0x000DB5CC File Offset: 0x000D97CC
	public override void triggerOnRemoveObject()
	{
		base.triggerOnRemoveObject();
		foreach (TBaseTrait tTrait in this._traits)
		{
			WorldActionTrait action_on_object_remove = tTrait.action_on_object_remove;
			if (action_on_object_remove != null)
			{
				action_on_object_remove(this, tTrait);
			}
		}
	}

	// Token: 0x06001481 RID: 5249 RVA: 0x000DB63C File Offset: 0x000D983C
	public void removeTrait(string pTraitID)
	{
		TBaseTrait tTrait = this.trait_library.get(pTraitID);
		this.removeTrait(tTrait);
	}

	// Token: 0x06001482 RID: 5250 RVA: 0x000DB660 File Offset: 0x000D9860
	public bool hasTrait(string pTrait)
	{
		TBaseTrait tTrait = this.trait_library.get(pTrait);
		return this.hasTrait(tTrait);
	}

	// Token: 0x06001483 RID: 5251 RVA: 0x000DB681 File Offset: 0x000D9881
	public bool hasMetaTag(string pTag)
	{
		return this.base_stats_meta.hasTag(pTag);
	}

	// Token: 0x06001484 RID: 5252 RVA: 0x000DB68F File Offset: 0x000D988F
	public bool hasTraits()
	{
		return this._traits.Count > 0;
	}

	// Token: 0x06001485 RID: 5253 RVA: 0x000DB69F File Offset: 0x000D989F
	public bool hasTrait(TBaseTrait pTrait)
	{
		return this._traits.Contains(pTrait);
	}

	// Token: 0x06001486 RID: 5254 RVA: 0x000DB6B4 File Offset: 0x000D98B4
	public void removeTraits(ListPool<string> pTraits)
	{
		bool tAnyRemoved = false;
		foreach (string ptr in pTraits)
		{
			string tTraitID = ptr;
			TBaseTrait tTrait = this.trait_library.get(tTraitID);
			if (this._traits.Remove(tTrait))
			{
				WorldActionTrait action_on_augmentation_remove = tTrait.action_on_augmentation_remove;
				if (action_on_augmentation_remove != null)
				{
					action_on_augmentation_remove(this, tTrait);
				}
				tAnyRemoved = true;
			}
		}
		if (tAnyRemoved)
		{
			this.recalcBaseStats();
		}
	}

	// Token: 0x06001487 RID: 5255 RVA: 0x000DB744 File Offset: 0x000D9944
	public virtual bool removeTrait(TBaseTrait pTrait)
	{
		bool flag = this._traits.Remove(pTrait);
		if (flag)
		{
			WorldActionTrait action_on_augmentation_remove = pTrait.action_on_augmentation_remove;
			if (action_on_augmentation_remove != null)
			{
				action_on_augmentation_remove(this, pTrait);
			}
			this.recalcBaseStats();
		}
		return flag;
	}

	// Token: 0x06001488 RID: 5256 RVA: 0x000DB77C File Offset: 0x000D997C
	private void removeOppositeTraits(TBaseTrait pTrait)
	{
		if (!pTrait.hasOppositeTraits<TBaseTrait>())
		{
			return;
		}
		foreach (TBaseTrait tTrait in pTrait.opposite_traits)
		{
			this.removeTrait(tTrait);
		}
	}

	// Token: 0x06001489 RID: 5257 RVA: 0x000DB7E0 File Offset: 0x000D99E0
	public virtual bool addTrait(string pTraitID, bool pRemoveOpposites = false)
	{
		TBaseTrait tTrait = this.trait_library.get(pTraitID);
		return tTrait != null && this.addTrait(tTrait, pRemoveOpposites);
	}

	// Token: 0x0600148A RID: 5258 RVA: 0x000DB80C File Offset: 0x000D9A0C
	public virtual bool addTrait(TBaseTrait pTrait, bool pRemoveOpposites = false)
	{
		if (this.hasTrait(pTrait))
		{
			return false;
		}
		if (pRemoveOpposites)
		{
			this.removeOppositeTraits(pTrait);
		}
		if (this.hasOppositeTrait(pTrait))
		{
			return false;
		}
		this._traits.Add(pTrait);
		WorldActionTrait action_on_augmentation_add = pTrait.action_on_augmentation_add;
		if (action_on_augmentation_add != null)
		{
			action_on_augmentation_add(this, pTrait);
		}
		this.recalcBaseStats();
		return true;
	}

	// Token: 0x0600148B RID: 5259 RVA: 0x000DB86B File Offset: 0x000D9A6B
	public override Sprite getTopicSprite()
	{
		if (this._traits.Count == 0)
		{
			return null;
		}
		return this._traits.GetRandom<TBaseTrait>().getSprite();
	}

	// Token: 0x0600148C RID: 5260 RVA: 0x000DB891 File Offset: 0x000D9A91
	internal bool hasOppositeTrait(TBaseTrait pTrait)
	{
		return pTrait.hasOppositeTrait(this._traits);
	}

	// Token: 0x1700011F RID: 287
	// (get) Token: 0x0600148D RID: 5261 RVA: 0x000DB89F File Offset: 0x000D9A9F
	protected virtual AssetLibrary<TBaseTrait> trait_library
	{
		get
		{
			throw new NotImplementedException(base.GetType().Name);
		}
	}

	// Token: 0x17000120 RID: 288
	// (get) Token: 0x0600148E RID: 5262 RVA: 0x000DB8B1 File Offset: 0x000D9AB1
	protected virtual List<string> default_traits
	{
		get
		{
			return null;
		}
	}

	// Token: 0x17000121 RID: 289
	// (get) Token: 0x0600148F RID: 5263 RVA: 0x000DB8B4 File Offset: 0x000D9AB4
	protected virtual List<string> saved_traits
	{
		get
		{
			return null;
		}
	}

	// Token: 0x17000122 RID: 290
	// (get) Token: 0x06001490 RID: 5264 RVA: 0x000DB8B7 File Offset: 0x000D9AB7
	protected virtual string species_id
	{
		get
		{
			return "human";
		}
	}

	// Token: 0x06001491 RID: 5265 RVA: 0x000DB8C0 File Offset: 0x000D9AC0
	public override ActorAsset getActorAsset()
	{
		if (this._species_asset == null)
		{
			string tSpeciesAsset = this.species_id;
			this._species_asset = AssetManager.actor_library.get(tSpeciesAsset);
		}
		return this._species_asset;
	}

	// Token: 0x06001492 RID: 5266 RVA: 0x000DB8F3 File Offset: 0x000D9AF3
	public bool isSameActorAsset(ActorAsset pAsset)
	{
		return this.getActorAsset() == pAsset;
	}

	// Token: 0x06001493 RID: 5267 RVA: 0x000DB900 File Offset: 0x000D9B00
	public void addRandomTraitFromBiome<T>(WorldTile pTile, List<string> pTraitList, AssetLibrary<T> pTraitLibrary) where T : BaseTrait<TBaseTrait>
	{
		if (!pTile.Type.is_biome)
		{
			return;
		}
		if (pTraitList == null || pTraitList.Count == 0)
		{
			return;
		}
		int tTries = pTraitList.Count;
		for (int i = 0; i < tTries; i++)
		{
			if (!Randy.randomBool())
			{
				string tRandomTraitID = pTraitList.GetRandom<string>();
				Asset tTrait = pTraitLibrary.get(tRandomTraitID);
				this.addTrait((TBaseTrait)((object)tTrait), true);
			}
		}
	}

	// Token: 0x06001494 RID: 5268 RVA: 0x000DB964 File Offset: 0x000D9B64
	public void addTraitFromBiome<T>(WorldTile pTile, List<string> pTraitList, AssetLibrary<T> pTraitLibrary) where T : BaseTrait<TBaseTrait>
	{
		if (!pTile.Type.is_biome)
		{
			return;
		}
		if (pTraitList == null || pTraitList.Count == 0)
		{
			return;
		}
		for (int i = 0; i < pTraitList.Count; i++)
		{
			Asset tTrait = pTraitLibrary.get(pTraitList[i]);
			this.addTrait((TBaseTrait)((object)tTrait), true);
		}
	}

	// Token: 0x06001495 RID: 5269 RVA: 0x000DB9C0 File Offset: 0x000D9BC0
	public TBaseTrait getTraitForBook()
	{
		IReadOnlyCollection<TBaseTrait> tTraits = this.getTraits();
		TBaseTrait tbaseTrait;
		using (ListPool<TBaseTrait> tList = new ListPool<TBaseTrait>(tTraits.Count))
		{
			foreach (TBaseTrait tTrait in tTraits)
			{
				if (tTrait.can_be_in_book)
				{
					tList.Add(tTrait);
				}
			}
			if (tList.Count == 0)
			{
				tbaseTrait = default(TBaseTrait);
				tbaseTrait = tbaseTrait;
			}
			else
			{
				tbaseTrait = tList.GetRandom<TBaseTrait>();
			}
		}
		return tbaseTrait;
	}

	// Token: 0x06001496 RID: 5270 RVA: 0x000DBA60 File Offset: 0x000D9C60
	public override void Dispose()
	{
		this._species_asset = null;
		this.clearTraits();
		this.resetStatsAndCallbacks();
		base.Dispose();
	}

	// Token: 0x040011B8 RID: 4536
	private readonly HashSet<TBaseTrait> _traits = new HashSet<TBaseTrait>();

	// Token: 0x040011B9 RID: 4537
	public readonly BaseStats base_stats = new BaseStats();

	// Token: 0x040011BA RID: 4538
	public readonly BaseStats base_stats_meta = new BaseStats();

	// Token: 0x040011BB RID: 4539
	private ActorAsset _species_asset;

	// Token: 0x040011BC RID: 4540
	public readonly List<BaseAugmentationAsset> all_actions_actor_special_effect = new List<BaseAugmentationAsset>();

	// Token: 0x040011BD RID: 4541
	public AttackAction all_actions_actor_attack_target;

	// Token: 0x040011BE RID: 4542
	public GetHitAction all_actions_actor_get_hit;

	// Token: 0x040011BF RID: 4543
	public WorldAction all_actions_actor_death;

	// Token: 0x040011C0 RID: 4544
	public WorldAction all_actions_actor_growth;

	// Token: 0x040011C1 RID: 4545
	public WorldAction all_actions_actor_birth;

	// Token: 0x040011C2 RID: 4546
	public readonly List<DecisionAsset> decisions_assets = new List<DecisionAsset>();

	// Token: 0x040011C3 RID: 4547
	public readonly CombatActionHolder combat_actions = new CombatActionHolder();

	// Token: 0x040011C4 RID: 4548
	public readonly SpellHolder spells = new SpellHolder();
}
