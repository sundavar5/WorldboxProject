using System;
using System.Collections.Generic;

// Token: 0x0200069B RID: 1691
public class WindowFavorites : WindowListBaseActor
{
	// Token: 0x06003626 RID: 13862 RVA: 0x0018AD54 File Offset: 0x00188F54
	protected override void setupSortingTabs()
	{
		this.sorting_tab.tryAddButton("ui/Icons/iconAge", "sort_by_age", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Actor>(WindowFavorites.sortByAge);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconRenown", "sort_by_renown", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Actor>(WindowFavorites.sortByRenown);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconLevels", "sort_by_level", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Actor>(WindowFavorites.sortByLevel);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconKills", "sort_by_kills", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Actor>(WindowFavorites.sortByKills);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconKingdom", "sort_by_kingdom", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Actor>(WindowFavorites.sortByKingdom);
		});
	}

	// Token: 0x06003627 RID: 13863 RVA: 0x0018AE4C File Offset: 0x0018904C
	protected override void show()
	{
		base.show();
		if (this._title_counter != null)
		{
			this._title_counter.text = this._temp_list_actor.Count.ToString();
		}
	}

	// Token: 0x06003628 RID: 13864 RVA: 0x0018AE8C File Offset: 0x0018908C
	protected override List<Actor> getObjects()
	{
		this._temp_list_actor.Clear();
		foreach (Actor tActor in World.world.units)
		{
			if (tActor.isAlive() && tActor.isFavorite())
			{
				this._temp_list_actor.Add(tActor);
			}
		}
		return this._temp_list_actor;
	}

	// Token: 0x06003629 RID: 13865 RVA: 0x0018AF04 File Offset: 0x00189104
	public static int sortByRenown(Actor pObject1, Actor pObject2)
	{
		return pObject2.data.renown.CompareTo(pObject1.data.renown);
	}

	// Token: 0x0600362A RID: 13866 RVA: 0x0018AF2F File Offset: 0x0018912F
	public static int sortByKingdom(Actor pActor1, Actor pActor2)
	{
		return pActor2.kingdom.CompareTo(pActor1.kingdom);
	}

	// Token: 0x0600362B RID: 13867 RVA: 0x0018AF44 File Offset: 0x00189144
	public static int sortByAge(Actor pActor1, Actor pActor2)
	{
		return pActor2.getAge().CompareTo(pActor1.getAge());
	}

	// Token: 0x0600362C RID: 13868 RVA: 0x0018AF68 File Offset: 0x00189168
	public static int sortByLevel(Actor pActor1, Actor pActor2)
	{
		return pActor2.data.level.CompareTo(pActor1.data.level);
	}

	// Token: 0x0600362D RID: 13869 RVA: 0x0018AF94 File Offset: 0x00189194
	public static int sortByKills(Actor pActor1, Actor pActor2)
	{
		return pActor2.data.kills.CompareTo(pActor1.data.kills);
	}

	// Token: 0x0400282E RID: 10286
	private List<Actor> _temp_list_actor = new List<Actor>();
}
