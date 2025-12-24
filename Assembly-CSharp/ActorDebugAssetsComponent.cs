using System;
using System.Collections.Generic;

// Token: 0x0200054A RID: 1354
public class ActorDebugAssetsComponent : BaseDebugAssetsComponent<ActorAsset, ActorDebugAssetElement, ActorAssetElementPlace>
{
	// Token: 0x06002C25 RID: 11301 RVA: 0x0015BEF9 File Offset: 0x0015A0F9
	protected override List<ActorAsset> getAssetsList()
	{
		return AssetManager.actor_library.list;
	}

	// Token: 0x06002C26 RID: 11302 RVA: 0x0015BF08 File Offset: 0x0015A108
	protected override void init()
	{
		this.sorting_tab.addButton("ui/Icons/iconHealth", "sort_by_health", new SortButtonAction(base.setDataResorted), delegate
		{
			this.list_assets_sorted = this.list_assets_sorting;
			this.list_assets_sorted.Sort(new Comparison<ActorAsset>(this.sortByHealth));
			base.checkReverseSort();
		});
		this.sorting_tab.addButton("ui/Icons/iconDamage", "sort_by_damage", new SortButtonAction(base.setDataResorted), delegate
		{
			this.list_assets_sorted = this.list_assets_sorting;
			this.list_assets_sorted.Sort(new Comparison<ActorAsset>(this.sortByDamage));
			base.checkReverseSort();
		});
		this.sorting_tab.addButton("ui/Icons/iconSpeed", "sort_by_speed", new SortButtonAction(base.setDataResorted), delegate
		{
			this.list_assets_sorted = this.list_assets_sorting;
			this.list_assets_sorted.Sort(new Comparison<ActorAsset>(this.sortBySpeed));
			base.checkReverseSort();
		});
		this.sorting_tab.addButton("ui/Icons/iconAge", "sort_by_lifespan", new SortButtonAction(base.setDataResorted), delegate
		{
			this.list_assets_sorted = this.list_assets_sorting;
			this.list_assets_sorted.Sort(new Comparison<ActorAsset>(this.sortByLifespan));
			base.checkReverseSort();
		});
		base.init();
	}

	// Token: 0x06002C27 RID: 11303 RVA: 0x0015BFD4 File Offset: 0x0015A1D4
	private int sortByHealth(ActorAsset pObject1, ActorAsset pObject2)
	{
		return -pObject1.getStatsForOverview()["health"].CompareTo(pObject2.getStatsForOverview()["health"]);
	}

	// Token: 0x06002C28 RID: 11304 RVA: 0x0015C00C File Offset: 0x0015A20C
	private int sortByDamage(ActorAsset pObject1, ActorAsset pObject2)
	{
		return -pObject1.getStatsForOverview()["damage"].CompareTo(pObject2.getStatsForOverview()["damage"]);
	}

	// Token: 0x06002C29 RID: 11305 RVA: 0x0015C044 File Offset: 0x0015A244
	private int sortBySpeed(ActorAsset pObject1, ActorAsset pObject2)
	{
		return -pObject1.getStatsForOverview()["speed"].CompareTo(pObject2.getStatsForOverview()["speed"]);
	}

	// Token: 0x06002C2A RID: 11306 RVA: 0x0015C07C File Offset: 0x0015A27C
	private int sortByLifespan(ActorAsset pObject1, ActorAsset pObject2)
	{
		return -pObject1.getStatsForOverview()["lifespan"].CompareTo(pObject2.getStatsForOverview()["lifespan"]);
	}

	// Token: 0x06002C2B RID: 11307 RVA: 0x0015C0B4 File Offset: 0x0015A2B4
	protected override List<ActorAsset> getListCivsSort()
	{
		bool tShow = this.sorting_tab.getCurrentButton().getState() == SortButtonState.Up;
		List<ActorAsset> tResult = new List<ActorAsset>();
		foreach (ActorAsset tAsset in this.getAssetsList())
		{
			if (tAsset.civ == tShow)
			{
				tResult.Add(tAsset);
			}
		}
		return tResult;
	}
}
