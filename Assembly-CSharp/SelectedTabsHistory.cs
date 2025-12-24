using System;
using System.Collections.Generic;

// Token: 0x0200075D RID: 1885
public class SelectedTabsHistory
{
	// Token: 0x06003BB6 RID: 15286 RVA: 0x001A13F8 File Offset: 0x0019F5F8
	public static void addToHistory(NanoObject pObject)
	{
		TabHistoryData tPrevData;
		if (SelectedTabsHistory._stack.TryPeek(out tPrevData) && tPrevData.id == pObject.id && tPrevData.meta_type == pObject.getMetaType())
		{
			return;
		}
		TabHistoryData tData = new TabHistoryData(pObject);
		SelectedTabsHistory._stack.Push(tData);
	}

	// Token: 0x06003BB7 RID: 15287 RVA: 0x001A1444 File Offset: 0x0019F644
	public static bool showPreviousTab()
	{
		TabHistoryData tabHistoryData;
		if (!SelectedTabsHistory._stack.TryPop(out tabHistoryData))
		{
			return false;
		}
		TabHistoryData tData;
		while (SelectedTabsHistory._stack.TryPop(out tData))
		{
			MetaTypeAsset tAsset = AssetManager.meta_type_library.getAsset(tData.meta_type);
			NanoObject tObject = tAsset.get(tData.id);
			if (!tObject.isRekt())
			{
				if (tData.meta_type == MetaType.Unit)
				{
					SelectedUnit.select(tObject as Actor, true);
					SelectedObjects.setNanoObject(SelectedUnit.unit);
					PowerTabController.showTabSelectedUnit();
				}
				else
				{
					tAsset.selectAndInspect(tObject, false, false, false);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003BB8 RID: 15288 RVA: 0x001A14CF File Offset: 0x0019F6CF
	public static bool hasHistory()
	{
		return SelectedTabsHistory._stack.Count > 0;
	}

	// Token: 0x06003BB9 RID: 15289 RVA: 0x001A14E0 File Offset: 0x0019F6E0
	public static int count()
	{
		int tAmount = 0;
		foreach (TabHistoryData tData in SelectedTabsHistory._stack)
		{
			if (!tData.getNanoObject().isRekt())
			{
				tAmount++;
			}
		}
		return tAmount;
	}

	// Token: 0x06003BBA RID: 15290 RVA: 0x001A1540 File Offset: 0x0019F740
	public static TabHistoryData? getPrevData()
	{
		int tNeededIndex = 1;
		TabHistoryData? result;
		for (;;)
		{
			int tIndex = 0;
			using (Stack<TabHistoryData>.Enumerator enumerator = SelectedTabsHistory._stack.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TabHistoryData tData = enumerator.Current;
					if (tNeededIndex != tIndex)
					{
						tIndex++;
					}
					else
					{
						if (!tData.getNanoObject().isRekt())
						{
							return new TabHistoryData?(tData);
						}
						tNeededIndex++;
						if (tNeededIndex > SelectedTabsHistory._stack.Count - 1)
						{
							result = null;
							return result;
						}
						break;
					}
				}
				continue;
			}
			break;
		}
		return result;
	}

	// Token: 0x06003BBB RID: 15291 RVA: 0x001A15D4 File Offset: 0x0019F7D4
	public static void clear()
	{
		SelectedTabsHistory._stack.Clear();
	}

	// Token: 0x04002BBE RID: 11198
	private static Stack<TabHistoryData> _stack = new Stack<TabHistoryData>();
}
