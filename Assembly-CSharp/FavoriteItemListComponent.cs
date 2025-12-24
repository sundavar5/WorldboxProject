using System;
using System.Collections.Generic;

// Token: 0x02000697 RID: 1687
public class FavoriteItemListComponent : ComponentListBase<FavoriteItemListElement, Item, ItemData, FavoriteItemListComponent>
{
	// Token: 0x17000306 RID: 774
	// (get) Token: 0x060035F3 RID: 13811 RVA: 0x0018A0F0 File Offset: 0x001882F0
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Item;
		}
	}

	// Token: 0x060035F4 RID: 13812 RVA: 0x0018A0F4 File Offset: 0x001882F4
	protected override void setupSortingTabs()
	{
		this.sorting_tab.tryAddButton("ui/Icons/iconAge", "sort_by_age", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Item>(FavoriteItemListComponent.sortByAge);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconKills", "sort_by_kills", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Item>(FavoriteItemListComponent.sortByKills);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconDamage", "sort_by_damage", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Item>(FavoriteItemListComponent.sortByDamage);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconArmor", "sort_by_armor", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Item>(FavoriteItemListComponent.sortByArmor);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconItemType", "sort_by_type", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Item>(FavoriteItemListComponent.sortByType);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconItemQuality", "sort_by_quality", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Item>(FavoriteItemListComponent.sortByQuality);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconCity", "sort_by_city", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Item>(FavoriteItemListComponent.sortByCity);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconHumans", "sort_by_owner", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Item>(FavoriteItemListComponent.sortByOwner);
		});
	}

	// Token: 0x060035F5 RID: 13813 RVA: 0x0018A279 File Offset: 0x00188479
	protected override IEnumerable<Item> getObjectsList()
	{
		this._meta_objects.Clear();
		foreach (Item tItem in World.world.items)
		{
			if (!tItem.isRekt() && tItem.isFavorite())
			{
				this._meta_objects.Add(tItem);
				if (tItem.hasCity())
				{
					this._meta_objects.Add(tItem.getCity());
				}
				if (tItem.hasActor())
				{
					this._meta_objects.Add(tItem.getActor());
				}
				yield return tItem;
			}
		}
		IEnumerator<Item> enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x060035F6 RID: 13814 RVA: 0x0018A28C File Offset: 0x0018848C
	public static int sortByAge(Item pItem1, Item pItem2)
	{
		return -pItem2.data.created_time.CompareTo(pItem1.data.created_time);
	}

	// Token: 0x060035F7 RID: 13815 RVA: 0x0018A2B8 File Offset: 0x001884B8
	public static int sortByKills(Item pItem1, Item pItem2)
	{
		return pItem2.data.kills.CompareTo(pItem1.data.kills);
	}

	// Token: 0x060035F8 RID: 13816 RVA: 0x0018A2D5 File Offset: 0x001884D5
	public static int sortByType(Item pItem1, Item pItem2)
	{
		return pItem2.getAsset().equipment_type.CompareTo(pItem1.getAsset().equipment_type);
	}

	// Token: 0x060035F9 RID: 13817 RVA: 0x0018A300 File Offset: 0x00188500
	public static int sortByQuality(Item pItem1, Item pItem2)
	{
		return pItem2.getQuality().CompareTo(pItem1.getQuality());
	}

	// Token: 0x060035FA RID: 13818 RVA: 0x0018A32C File Offset: 0x0018852C
	public static int sortByCity(Item pItem1, Item pItem2)
	{
		int tCityCompare = pItem1.hasCity().CompareTo(pItem2.hasCity());
		if (tCityCompare != 0)
		{
			return tCityCompare;
		}
		if (!pItem1.hasCity() || !pItem2.hasCity())
		{
			return pItem2.name.CompareTo(pItem1.name);
		}
		int tKingdomCompare = pItem2.getCity().kingdom.CompareTo(pItem1.getCity().kingdom);
		if (tKingdomCompare != 0)
		{
			return tKingdomCompare;
		}
		return pItem2.getCity().name.CompareTo(pItem1.getCity().name);
	}

	// Token: 0x060035FB RID: 13819 RVA: 0x0018A3B4 File Offset: 0x001885B4
	public static int sortByOwner(Item pItem1, Item pItem2)
	{
		int tActorCompare = pItem1.hasActor().CompareTo(pItem2.hasActor());
		if (tActorCompare != 0)
		{
			return tActorCompare;
		}
		if (!pItem1.hasActor() || !pItem2.hasActor())
		{
			return pItem2.name.CompareTo(pItem1.name);
		}
		Actor tActor = pItem1.getActor();
		Actor tActor2 = pItem2.getActor();
		int tKingdomCompare = tActor.kingdom.CompareTo(tActor2.kingdom);
		if (tKingdomCompare != 0)
		{
			return tKingdomCompare;
		}
		int tCityCompare = tActor.hasCity().CompareTo(tActor2.hasCity());
		if (tCityCompare != 0)
		{
			return tCityCompare;
		}
		if (tActor.hasCity() && tActor2.hasCity())
		{
			int tCityCompare2 = tActor.getCity().name.CompareTo(tActor2.getCity().name);
			if (tCityCompare2 != 0)
			{
				return tCityCompare2;
			}
		}
		return pItem2.getActor().name.CompareTo(pItem1.getActor().name);
	}

	// Token: 0x060035FC RID: 13820 RVA: 0x0018A498 File Offset: 0x00188698
	public static int sortByDamage(Item pItem1, Item pItem2)
	{
		return pItem2.getFullStats()["damage"].CompareTo(pItem1.getFullStats()["damage"]);
	}

	// Token: 0x060035FD RID: 13821 RVA: 0x0018A4D0 File Offset: 0x001886D0
	public static int sortByArmor(Item pItem1, Item pItem2)
	{
		return pItem2.getFullStats()["armor"].CompareTo(pItem1.getFullStats()["armor"]);
	}

	// Token: 0x060035FE RID: 13822 RVA: 0x0018A505 File Offset: 0x00188705
	public override void clear()
	{
		base.clear();
		this._meta_objects.Clear();
	}

	// Token: 0x060035FF RID: 13823 RVA: 0x0018A518 File Offset: 0x00188718
	public override bool checkRefreshWindow()
	{
		using (List<NanoObject>.Enumerator enumerator = this._meta_objects.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isRekt())
				{
					return true;
				}
			}
		}
		return base.checkRefreshWindow();
	}

	// Token: 0x0400280F RID: 10255
	private List<NanoObject> _meta_objects = new List<NanoObject>();
}
