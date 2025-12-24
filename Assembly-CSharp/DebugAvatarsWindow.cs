using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200055D RID: 1373
public class DebugAvatarsWindow : MonoBehaviour
{
	// Token: 0x06002CA1 RID: 11425 RVA: 0x0015E0FC File Offset: 0x0015C2FC
	private void Awake()
	{
		this.init();
	}

	// Token: 0x06002CA2 RID: 11426 RVA: 0x0015E104 File Offset: 0x0015C304
	private void init()
	{
		this._avatars = new ObjectPoolGenericMono<UnitAvatarLoader>(this._avatar_prefab, this._avatars_parent);
		this.preparePools();
	}

	// Token: 0x06002CA3 RID: 11427 RVA: 0x0015E123 File Offset: 0x0015C323
	private void OnEnable()
	{
		this.showAvatars();
	}

	// Token: 0x06002CA4 RID: 11428 RVA: 0x0015E12B File Offset: 0x0015C32B
	private void OnDisable()
	{
		this.clear();
	}

	// Token: 0x06002CA5 RID: 11429 RVA: 0x0015E133 File Offset: 0x0015C333
	private void clear()
	{
		this._avatars.clear(true);
	}

	// Token: 0x06002CA6 RID: 11430 RVA: 0x0015E144 File Offset: 0x0015C344
	private void showAvatars()
	{
		foreach (ActorAsset tAsset in AssetManager.actor_library.list)
		{
			if (!tAsset.has_override_sprite && tAsset.has_sprite_renderer)
			{
				SubspeciesTrait tMutation = this.getRandomMutation();
				bool tIsAdult = this.getRandomIsAdult();
				ActorSex tSex = this.getRandomSex();
				ColorAsset tKingdomColor = AssetManager.kingdom_colors_library.list.GetRandom<ColorAsset>();
				bool tIsUnconscious = this.getRandomIsUnconscious();
				bool tIsLying = tIsUnconscious || this.getRandomIsLying();
				bool tIsHovering = this.getRandomIsHovering();
				bool tIsTouchingLiquid = this.getRandomIsTouchingLiquid() && !tIsHovering;
				bool tIsImmovable = this.getRandomIsImmovable();
				AvatarCombineHandItem tHandWeapon = this.getRandomItemPath();
				bool tStopIdleAnimation;
				List<string> tStatuses = this.getRandomStatuses(out tStopIdleAnimation);
				PhenotypeAsset tPhenotype = this.getRandomPhenotype();
				int tPhenotypeIndex = Actor.getRandomPhenotypeShade();
				SubspeciesTrait tEgg = this.getRandomEgg();
				bool tIsEgg = !tIsAdult && tEgg != null;
				ActorTextureSubAsset tTextureAsset;
				if (tMutation != null)
				{
					tTextureAsset = tMutation.texture_asset;
					BaseStats tMetaStats = tMutation.base_stats_meta;
					if (!tMetaStats.isEmpty() && tMetaStats.hasTag("always_idle_animation"))
					{
						tStopIdleAnimation = false;
					}
				}
				else
				{
					tTextureAsset = tAsset.texture_asset;
				}
				DynamicActorSpriteCreatorUI.getContainerForUI(tAsset, tIsAdult, tTextureAsset, tMutation, tIsEgg, tEgg, null);
				ActorAvatarData tData = new ActorAvatarData();
				tData.setData(tAsset, tMutation, tSex, (long)Randy.randomInt(0, int.MaxValue), -1, null, tPhenotype.phenotype_index, tPhenotypeIndex, tKingdomColor, tIsEgg, false, false, false, tEgg, tIsAdult, tIsLying, tIsTouchingLiquid, false, tIsHovering, tIsImmovable, tIsUnconscious, tStopIdleAnimation, (tHandWeapon != null) ? tHandWeapon.hand_renderer : null, 1, tStatuses, null);
				this._avatars.getNext().load(tData, false);
			}
		}
	}

	// Token: 0x06002CA7 RID: 11431 RVA: 0x0015E300 File Offset: 0x0015C500
	private void preparePools()
	{
		foreach (SubspeciesTrait tTrait in AssetManager.subspecies_traits.list)
		{
			if (tTrait.is_mutation_skin)
			{
				this._pool_mutations.Add(tTrait);
			}
			if (tTrait.phenotype_egg)
			{
				this._pool_eggs.Add(tTrait);
			}
			if (tTrait.phenotype_skin)
			{
				PhenotypeAsset tPhenotype = AssetManager.phenotype_library.get(tTrait.id_phenotype);
				this._pool_phenotype.Add(tPhenotype);
			}
		}
		foreach (EquipmentAsset tItem in AssetManager.items.pot_weapon_assets_all)
		{
			this._pool_hand_renderers.Add(new AvatarCombineHandItem(tItem));
		}
		foreach (ResourceAsset tResource in AssetManager.resources.list)
		{
			this._pool_hand_renderers.Add(new AvatarCombineHandItem(tResource));
		}
		foreach (UnitHandToolAsset tTool in AssetManager.unit_hand_tools.list)
		{
			this._pool_hand_renderers.Add(new AvatarCombineHandItem(tTool));
		}
		foreach (StatusAsset tStatus in AssetManager.status.list)
		{
			if (tStatus.need_visual_render)
			{
				this._pool_statuses.Add(tStatus);
			}
		}
	}

	// Token: 0x06002CA8 RID: 11432 RVA: 0x0015E4EC File Offset: 0x0015C6EC
	private SubspeciesTrait getRandomMutation()
	{
		if (Randy.randomChance(0.75f))
		{
			return null;
		}
		return this._pool_mutations.GetRandom<SubspeciesTrait>();
	}

	// Token: 0x06002CA9 RID: 11433 RVA: 0x0015E507 File Offset: 0x0015C707
	private SubspeciesTrait getRandomEgg()
	{
		if (Randy.randomChance(0.9f))
		{
			return null;
		}
		return this._pool_eggs.GetRandom<SubspeciesTrait>();
	}

	// Token: 0x06002CAA RID: 11434 RVA: 0x0015E522 File Offset: 0x0015C722
	private PhenotypeAsset getRandomPhenotype()
	{
		return this._pool_phenotype.GetRandom<PhenotypeAsset>();
	}

	// Token: 0x06002CAB RID: 11435 RVA: 0x0015E52F File Offset: 0x0015C72F
	private ActorSex getRandomSex()
	{
		if (Randy.randomChance(0.5f))
		{
			return ActorSex.Male;
		}
		return ActorSex.Female;
	}

	// Token: 0x06002CAC RID: 11436 RVA: 0x0015E540 File Offset: 0x0015C740
	private bool getRandomIsAdult()
	{
		return Randy.randomBool();
	}

	// Token: 0x06002CAD RID: 11437 RVA: 0x0015E547 File Offset: 0x0015C747
	private bool getRandomIsLying()
	{
		return Randy.randomChance(0.2f);
	}

	// Token: 0x06002CAE RID: 11438 RVA: 0x0015E553 File Offset: 0x0015C753
	private bool getRandomIsTouchingLiquid()
	{
		return Randy.randomBool();
	}

	// Token: 0x06002CAF RID: 11439 RVA: 0x0015E55A File Offset: 0x0015C75A
	private bool getRandomIsHovering()
	{
		return Randy.randomChance(0.2f);
	}

	// Token: 0x06002CB0 RID: 11440 RVA: 0x0015E566 File Offset: 0x0015C766
	private bool getRandomIsImmovable()
	{
		return Randy.randomChance(0.2f);
	}

	// Token: 0x06002CB1 RID: 11441 RVA: 0x0015E572 File Offset: 0x0015C772
	private bool getRandomIsUnconscious()
	{
		return Randy.randomChance(0.2f);
	}

	// Token: 0x06002CB2 RID: 11442 RVA: 0x0015E57E File Offset: 0x0015C77E
	private AvatarCombineHandItem getRandomItemPath()
	{
		if (Randy.randomChance(0.4f))
		{
			return null;
		}
		return this._pool_hand_renderers.GetRandom<AvatarCombineHandItem>();
	}

	// Token: 0x06002CB3 RID: 11443 RVA: 0x0015E59C File Offset: 0x0015C79C
	private List<string> getRandomStatuses(out bool pStopIdleAnimation)
	{
		pStopIdleAnimation = false;
		List<string> tStatuses = new List<string>();
		foreach (StatusAsset tStatus in AssetManager.status.list)
		{
			if (tStatus.need_visual_render && !Randy.randomChance(0.95f))
			{
				if (tStatus.base_stats.hasTag("stop_idle_animation"))
				{
					pStopIdleAnimation = true;
				}
				tStatuses.Add(tStatus.id);
			}
		}
		return tStatuses;
	}

	// Token: 0x06002CB4 RID: 11444 RVA: 0x0015E62C File Offset: 0x0015C82C
	public void toggleAutotest()
	{
		this._autotest_state = !this._autotest_state;
		if (this._autotest_state)
		{
			this._autotest_button_icon.sprite = this._sprite_pause;
			this._autotest_routine = base.StartCoroutine(this.autotestRoutine());
			return;
		}
		this._autotest_button_icon.sprite = this._sprite_play;
		base.StopCoroutine(this._autotest_routine);
	}

	// Token: 0x06002CB5 RID: 11445 RVA: 0x0015E694 File Offset: 0x0015C894
	private T getFromPool<T>(List<T> pPool, int pGlobalIndex, string pId) where T : class
	{
		int tIndex = this._combine_data.getListIndex(pGlobalIndex, pId);
		if (pPool.Count - 1 < tIndex)
		{
			return default(T);
		}
		return pPool[tIndex];
	}

	// Token: 0x06002CB6 RID: 11446 RVA: 0x0015E6CB File Offset: 0x0015C8CB
	private bool getBool(int pGlobalIndex, string pId)
	{
		return this._combine_data.getListIndex(pGlobalIndex, pId) == 1;
	}

	// Token: 0x06002CB7 RID: 11447 RVA: 0x0015E6DD File Offset: 0x0015C8DD
	private IEnumerator autotestRoutine()
	{
		this._combine_data.clear();
		this._statuses.Clear();
		this._check_collisions.Clear();
		ActorSex tSex = ActorSex.Male;
		bool tAdult = false;
		bool tTouchingLiquid = false;
		bool tLying = false;
		bool tImmovable = false;
		bool tUnconscious = false;
		this._combine_data.add("tAdult", 2);
		this._combine_data.add("tTouchingLiquid", 2);
		this._combine_data.add("tLying", 2);
		this._combine_data.add("tImmovable", 2);
		this._combine_data.add("tUnconscious", 2);
		this._combine_data.add("tSex", 2);
		if (DebugAvatarsWindow._test_mutations)
		{
			this._combine_data.add("_pool_mutations", this._pool_mutations.Count);
		}
		if (DebugAvatarsWindow._test_eggs)
		{
			this._combine_data.add("_pool_eggs", this._pool_eggs.Count);
		}
		if (DebugAvatarsWindow._test_hand_items)
		{
			this._combine_data.add("_pool_hand_renderers", this._pool_hand_renderers.Count);
		}
		if (DebugAvatarsWindow._test_statuses)
		{
			this._combine_data.add("_pool_statuses", this._pool_statuses.Count);
		}
		int tTotal = this._combine_data.totalCombinations();
		int num;
		for (int i = 0; i < tTotal; i = num + 1)
		{
			tAdult = this.getBool(i, "tAdult");
			tTouchingLiquid = this.getBool(i, "tTouchingLiquid");
			tLying = this.getBool(i, "tLying");
			tImmovable = this.getBool(i, "tImmovable");
			tUnconscious = this.getBool(i, "tUnconscious");
			tSex = (this.getBool(i, "tSex") ? ActorSex.Male : ActorSex.Female);
			bool tStopIdleAnimation = false;
			bool tAlwaysIdleAnimation = false;
			long tHashCode = (long)((tAdult ? 1 : 2) + (tTouchingLiquid ? 1 : 2) * 10 + (tLying ? 1 : 2) * 100 + (tImmovable ? 1 : 2) * 1000 + (tUnconscious ? 1 : 2) * 10000 + ((tSex == ActorSex.Male) ? 1 : 2) * 100000 + (tStopIdleAnimation ? 1 : 2) * 1000000);
			SubspeciesTrait tMutation = null;
			if (DebugAvatarsWindow._test_mutations)
			{
				tMutation = this.getFromPool<SubspeciesTrait>(this._pool_mutations, i, "_pool_mutations");
				tHashCode += (long)(this._pool_mutations.IndexOf(tMutation) * 100000000);
				BaseStats tBaseStatsMeta = tMutation.base_stats_meta;
				if (!tBaseStatsMeta.isEmpty() && tBaseStatsMeta.hasTag("always_idle_animation"))
				{
					tAlwaysIdleAnimation = true;
				}
			}
			SubspeciesTrait tEgg = null;
			if (tMutation == null && DebugAvatarsWindow._test_eggs)
			{
				tEgg = this.getFromPool<SubspeciesTrait>(this._pool_eggs, i, "_pool_eggs");
				tHashCode += (long)this._pool_eggs.IndexOf(tEgg) * 10000000000L;
			}
			bool tIsEgg = tEgg != null;
			IHandRenderer tItemRenderer;
			if (!tIsEgg && DebugAvatarsWindow._test_hand_items)
			{
				AvatarCombineHandItem tItem = this.getFromPool<AvatarCombineHandItem>(this._pool_hand_renderers, i, "_pool_hand_renderers");
				tHashCode += (long)this._pool_hand_renderers.IndexOf(tItem) * 10000000000000L;
				tItemRenderer = tItem.hand_renderer;
			}
			else
			{
				tItemRenderer = null;
				tHashCode += (long)this._pool_hand_renderers.Count * 10000000000000L;
			}
			StatusAsset tStatus = null;
			if (DebugAvatarsWindow._test_statuses)
			{
				tStatus = this.getFromPool<StatusAsset>(this._pool_statuses, i, "_pool_statuses");
				tHashCode += (long)this._pool_statuses.IndexOf(tStatus) * 10000000000000000L;
			}
			int tHash = 1;
			foreach (UnitAvatarLoader unitAvatarLoader in this._avatars.getListTotal())
			{
				this._statuses.Clear();
				StatusAsset tRandomStatus = (DebugAvatarsWindow._test_statuses && Randy.randomBool()) ? this._pool_statuses.GetRandom<StatusAsset>() : null;
				StatusAsset tRandomStatus2 = (DebugAvatarsWindow._test_statuses && Randy.randomBool()) ? this._pool_statuses.GetRandom<StatusAsset>() : null;
				if (tStatus != null)
				{
					this._statuses.Add(tStatus.id);
					if (tStatus.base_stats.hasTag("stop_idle_animation"))
					{
						tStopIdleAnimation = true;
					}
				}
				if (tRandomStatus != null)
				{
					this._statuses.Add(tRandomStatus.id);
					if (tRandomStatus.base_stats.hasTag("stop_idle_animation"))
					{
						tStopIdleAnimation = true;
					}
				}
				if (tRandomStatus2 != null)
				{
					this._statuses.Add(tRandomStatus2.id);
					if (tRandomStatus2.base_stats.hasTag("stop_idle_animation"))
					{
						tStopIdleAnimation = true;
					}
				}
				tHash++;
				ActorAvatarData tOldData = unitAvatarLoader.getData();
				ActorAsset tAsset = tOldData.asset;
				ActorTextureSubAsset tTextureAsset;
				if (tMutation != null)
				{
					tTextureAsset = tMutation.texture_asset;
				}
				else
				{
					tTextureAsset = tAsset.texture_asset;
				}
				DynamicActorSpriteCreatorUI.getContainerForUI(tAsset, tAdult, tTextureAsset, tMutation, tIsEgg, tEgg, null);
				if (tAlwaysIdleAnimation)
				{
					tStopIdleAnimation = false;
				}
				ActorAvatarData tNewData = new ActorAvatarData();
				tNewData.setData(tOldData.asset, tMutation, tSex, (long)Randy.randomInt(0, int.MaxValue), -1, null, tOldData.phenotype_index, tOldData.phenotype_skin_shade, tOldData.kingdom_color, tIsEgg, false, false, false, tEgg, tAdult, tLying, tTouchingLiquid, false, tOldData.is_hovering, tImmovable, tUnconscious, tStopIdleAnimation, tItemRenderer, tHash, this._statuses, null);
				unitAvatarLoader.load(tNewData, false);
			}
			this._check_collisions.Add(tHashCode);
			Debug.Log(string.Format("tested: {0}/{1}, hashset: {2}/{3} adult: {4}, liquid: {5}, lying: {6}, immovable: {7}, uncon: {8}, sex: {9}, mut: {10}, egg: {11}, item: {12}, status: {13}", new object[]
			{
				i + 1,
				tTotal,
				this._check_collisions.Count,
				tTotal,
				tAdult,
				tTouchingLiquid,
				tLying,
				tImmovable,
				tUnconscious,
				tSex,
				((tMutation != null) ? tMutation.id : null) ?? "null",
				((tEgg != null) ? tEgg.id : null) ?? "null",
				tItemRenderer,
				((tStatus != null) ? tStatus.id : null) ?? "null"
			}));
			yield return null;
			num = i;
		}
		yield break;
	}

	// Token: 0x04002231 RID: 8753
	private static readonly bool _test_mutations = false;

	// Token: 0x04002232 RID: 8754
	private static readonly bool _test_eggs = true;

	// Token: 0x04002233 RID: 8755
	private static readonly bool _test_hand_items = false;

	// Token: 0x04002234 RID: 8756
	private static readonly bool _test_statuses = false;

	// Token: 0x04002235 RID: 8757
	[SerializeField]
	private Transform _avatars_parent;

	// Token: 0x04002236 RID: 8758
	[SerializeField]
	private UnitAvatarLoader _avatar_prefab;

	// Token: 0x04002237 RID: 8759
	[SerializeField]
	private Image _autotest_button_icon;

	// Token: 0x04002238 RID: 8760
	[SerializeField]
	private Sprite _sprite_play;

	// Token: 0x04002239 RID: 8761
	[SerializeField]
	private Sprite _sprite_pause;

	// Token: 0x0400223A RID: 8762
	private ObjectPoolGenericMono<UnitAvatarLoader> _avatars;

	// Token: 0x0400223B RID: 8763
	private List<SubspeciesTrait> _pool_mutations = new List<SubspeciesTrait>();

	// Token: 0x0400223C RID: 8764
	private List<SubspeciesTrait> _pool_eggs = new List<SubspeciesTrait>();

	// Token: 0x0400223D RID: 8765
	private List<PhenotypeAsset> _pool_phenotype = new List<PhenotypeAsset>();

	// Token: 0x0400223E RID: 8766
	private List<AvatarCombineHandItem> _pool_hand_renderers = new List<AvatarCombineHandItem>();

	// Token: 0x0400223F RID: 8767
	private List<StatusAsset> _pool_statuses = new List<StatusAsset>();

	// Token: 0x04002240 RID: 8768
	private AvatarsCombineDataContainer _combine_data = new AvatarsCombineDataContainer();

	// Token: 0x04002241 RID: 8769
	private HashSet<string> _statuses = new HashSet<string>();

	// Token: 0x04002242 RID: 8770
	private HashSet<long> _check_collisions = new HashSet<long>();

	// Token: 0x04002243 RID: 8771
	private bool _autotest_state;

	// Token: 0x04002244 RID: 8772
	private Coroutine _autotest_routine;
}
