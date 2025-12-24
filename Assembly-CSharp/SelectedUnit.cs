using System;
using System.Collections.Generic;

// Token: 0x0200032C RID: 812
public static class SelectedUnit
{
	// Token: 0x06001F4A RID: 8010 RVA: 0x0010F8A7 File Offset: 0x0010DAA7
	public static HashSet<Actor> getAllSelected()
	{
		return SelectedUnit._units_hashset;
	}

	// Token: 0x06001F4B RID: 8011 RVA: 0x0010F8AE File Offset: 0x0010DAAE
	public static List<Actor> getAllSelectedList()
	{
		return SelectedUnit._units_list;
	}

	// Token: 0x06001F4C RID: 8012 RVA: 0x0010F8B5 File Offset: 0x0010DAB5
	public static bool multipleSelected()
	{
		return SelectedUnit._units_hashset.Count > 1;
	}

	// Token: 0x06001F4D RID: 8013 RVA: 0x0010F8C4 File Offset: 0x0010DAC4
	public static int countSelected()
	{
		return SelectedUnit._units_hashset.Count;
	}

	// Token: 0x06001F4E RID: 8014 RVA: 0x0010F8D0 File Offset: 0x0010DAD0
	public static bool isSet()
	{
		return SelectedUnit._units_hashset.Count != 0;
	}

	// Token: 0x06001F4F RID: 8015 RVA: 0x0010F8E4 File Offset: 0x0010DAE4
	public static void selectMultiple(ListPool<Actor> pActors)
	{
		Actor tOldest = null;
		foreach (Actor ptr in pActors)
		{
			Actor tActor = ptr;
			SelectedUnit.select(tActor, false);
			if (tOldest == null || tActor.data.created_time < tOldest.data.created_time)
			{
				tOldest = tActor;
			}
		}
		if (tOldest != null)
		{
			SelectedUnit.makeMainSelected(tOldest);
		}
	}

	// Token: 0x06001F50 RID: 8016 RVA: 0x0010F95C File Offset: 0x0010DB5C
	public static bool select(Actor pActor, bool pSetMainUnit = true)
	{
		if (pSetMainUnit)
		{
			SelectedUnit.makeMainSelected(pActor);
		}
		if (SelectedUnit._units_hashset.Add(pActor))
		{
			SelectedUnit.hashsetChanged();
		}
		return SelectedUnit.isSet();
	}

	// Token: 0x06001F51 RID: 8017 RVA: 0x0010F97E File Offset: 0x0010DB7E
	public static void unselect(Actor pActor)
	{
		if (SelectedUnit._units_hashset.Remove(pActor))
		{
			SelectedUnit.hashsetChanged();
		}
	}

	// Token: 0x06001F52 RID: 8018 RVA: 0x0010F992 File Offset: 0x0010DB92
	public static void clear()
	{
		SelectedUnit._units_hashset.Clear();
		SelectedUnit.hashsetChanged();
		SelectedUnit.clearMain();
		SelectedUnitClearEvent on_clear_events = SelectedUnit._on_clear_events;
		if (on_clear_events == null)
		{
			return;
		}
		on_clear_events();
	}

	// Token: 0x06001F53 RID: 8019 RVA: 0x0010F9B7 File Offset: 0x0010DBB7
	private static void hashsetChanged()
	{
		SelectedUnit._units_list.Clear();
		SelectedUnit._units_list.AddRange(SelectedUnit._units_hashset);
		SelectedUnit._units_list.Shuffle<Actor>();
		SelectedUnit._selection_version++;
	}

	// Token: 0x06001F54 RID: 8020 RVA: 0x0010F9E8 File Offset: 0x0010DBE8
	public static bool isMainSelected(Actor pActor)
	{
		return SelectedUnit.isSet() && SelectedUnit._unit_main == pActor;
	}

	// Token: 0x170001E4 RID: 484
	// (get) Token: 0x06001F55 RID: 8021 RVA: 0x0010F9FB File Offset: 0x0010DBFB
	public static Actor unit
	{
		get
		{
			return SelectedUnit._unit_main;
		}
	}

	// Token: 0x06001F56 RID: 8022 RVA: 0x0010FA02 File Offset: 0x0010DC02
	public static bool isSelected(Actor pActor)
	{
		return SelectedUnit.isSet() && SelectedUnit._units_hashset.Contains(pActor);
	}

	// Token: 0x06001F57 RID: 8023 RVA: 0x0010FA18 File Offset: 0x0010DC18
	public static void subscribeClearEvent(SelectedUnitClearEvent pEvent)
	{
		SelectedUnit._on_clear_events = (SelectedUnitClearEvent)Delegate.Combine(SelectedUnit._on_clear_events, pEvent);
	}

	// Token: 0x06001F58 RID: 8024 RVA: 0x0010FA2F File Offset: 0x0010DC2F
	public static void removeSelected(Actor pActor)
	{
		if (SelectedUnit._units_hashset.Remove(pActor))
		{
			SelectedUnit.hashsetChanged();
		}
		if (SelectedUnit._unit_main == pActor)
		{
			SelectedUnit.clearMain();
			SelectedUnit.trySelectNewMain();
		}
	}

	// Token: 0x06001F59 RID: 8025 RVA: 0x0010FA55 File Offset: 0x0010DC55
	private static void clearMain()
	{
		SelectedUnit._unit_main = null;
	}

	// Token: 0x06001F5A RID: 8026 RVA: 0x0010FA5D File Offset: 0x0010DC5D
	private static void trySelectNewMain()
	{
		if (SelectedUnit._units_hashset.Count == 0)
		{
			SelectedUnit.clear();
			return;
		}
		SelectedUnit.makeMainSelected(SelectedUnit._units_hashset.GetRandom<Actor>());
	}

	// Token: 0x06001F5B RID: 8027 RVA: 0x0010FA80 File Offset: 0x0010DC80
	public static void nextMainUnit()
	{
		if (!SelectedUnit.isSet())
		{
			return;
		}
		SelectedUnit.makeMainSelected(SelectedUnit._units_list.LoopNext(SelectedUnit._unit_main));
	}

	// Token: 0x06001F5C RID: 8028 RVA: 0x0010FAA0 File Offset: 0x0010DCA0
	public static void killSelected()
	{
		if (!SelectedUnit.isSet())
		{
			return;
		}
		using (ListPool<Actor> tList = new ListPool<Actor>(SelectedUnit._units_hashset))
		{
			foreach (Actor ptr in tList)
			{
				Actor actor = ptr;
				actor.getHit((float)(actor.getMaxHealth() * 2), true, AttackType.Divine, null, true, false, true);
			}
			SelectedUnit.clear();
		}
	}

	// Token: 0x06001F5D RID: 8029 RVA: 0x0010FB2C File Offset: 0x0010DD2C
	public static void makeMainSelected(Actor pActor)
	{
		if (SelectedUnit._unit_main != pActor)
		{
			pActor.makeSpawnSound(true);
		}
		SelectedUnit._unit_main = pActor;
	}

	// Token: 0x06001F5E RID: 8030 RVA: 0x0010FB43 File Offset: 0x0010DD43
	public static int getSelectionVersion()
	{
		return SelectedUnit._selection_version;
	}

	// Token: 0x040016D6 RID: 5846
	private static Actor _unit_main;

	// Token: 0x040016D7 RID: 5847
	private static HashSet<Actor> _units_hashset = new HashSet<Actor>();

	// Token: 0x040016D8 RID: 5848
	private static List<Actor> _units_list = new List<Actor>();

	// Token: 0x040016D9 RID: 5849
	private static SelectedUnitClearEvent _on_clear_events;

	// Token: 0x040016DA RID: 5850
	private static int _selection_version;
}
