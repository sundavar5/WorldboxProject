using System;
using System.Collections.Generic;

// Token: 0x020007F8 RID: 2040
public class WindowHistory
{
	// Token: 0x06003FFE RID: 16382 RVA: 0x001B6BB6 File Offset: 0x001B4DB6
	public static void clear()
	{
		if (WindowHistory.historyClearCallback != null)
		{
			WindowHistory.historyClearCallback();
			WindowHistory.historyClearCallback = null;
		}
		WindowHistory.list.Clear();
	}

	// Token: 0x06003FFF RID: 16383 RVA: 0x001B6BDC File Offset: 0x001B4DDC
	public static void addIntoHistory(ScrollWindow pWindow)
	{
		WindowHistoryData tHistory = new WindowHistoryData
		{
			index = WindowHistory.list.Count + 1,
			window = pWindow
		};
		foreach (MetaTypeAsset metaTypeAsset in AssetManager.meta_type_library.list)
		{
			MetaTypeHistoryAction window_history_action_update = metaTypeAsset.window_history_action_update;
			if (window_history_action_update != null)
			{
				window_history_action_update(ref tHistory);
			}
		}
		WindowHistory.list.Add(tHistory);
	}

	// Token: 0x06004000 RID: 16384 RVA: 0x001B6C70 File Offset: 0x001B4E70
	public static void popHistory()
	{
		List<WindowHistoryData> list = WindowHistory.list;
		if (list == null)
		{
			return;
		}
		list.Pop<WindowHistoryData>();
	}

	// Token: 0x06004001 RID: 16385 RVA: 0x001B6C82 File Offset: 0x001B4E82
	public static void clickBack()
	{
		if (ScrollWindow.getCurrentWindow().historyActionEnabled && WindowHistory.returnWindowBack())
		{
			return;
		}
		ScrollWindow.hideAllEvent(true);
	}

	// Token: 0x06004002 RID: 16386 RVA: 0x001B6CA0 File Offset: 0x001B4EA0
	private static bool returnWindowBack()
	{
		if (!WindowHistory.canReturnWindowBack())
		{
			return false;
		}
		WindowHistoryData tHistory = WindowHistory.list.Pop<WindowHistoryData>();
		while (WindowHistory.list.Count > 0)
		{
			tHistory = WindowHistory.list.Pop<WindowHistoryData>();
			MetaTypeAsset meta_type_asset = AssetManager.window_library.get(tHistory.window.screen_id).meta_type_asset;
			if (meta_type_asset != null)
			{
				MetaTypeHistoryAction window_history_action_restore = meta_type_asset.window_history_action_restore;
				if (window_history_action_restore != null)
				{
					window_history_action_restore(ref tHistory);
				}
			}
			if (!tHistory.window.shouldClose())
			{
				break;
			}
		}
		if (tHistory.window.shouldClose())
		{
			return false;
		}
		tHistory.window.clickShowLeft();
		return true;
	}

	// Token: 0x06004003 RID: 16387 RVA: 0x001B6D35 File Offset: 0x001B4F35
	public static bool canReturnWindowBack()
	{
		return !WorkshopUploadingWorldWindow.uploading && WindowHistory.list.Count >= 2;
	}

	// Token: 0x06004004 RID: 16388 RVA: 0x001B6D50 File Offset: 0x001B4F50
	public static bool hasHistory()
	{
		return WindowHistory.list.Count > 0;
	}

	// Token: 0x06004005 RID: 16389 RVA: 0x001B6D60 File Offset: 0x001B4F60
	public static void debug(DebugTool pTool)
	{
		pTool.setText("hasHistory:", WindowHistory.hasHistory(), 0f, false, 0L, false, false, "");
		pTool.setText("canReturnWindowBack:", WindowHistory.canReturnWindowBack(), 0f, false, 0L, false, false, "");
		pTool.setText("list.Count:", WindowHistory.list.Count, 0f, false, 0L, false, false, "");
		foreach (WindowHistoryData tHistory in WindowHistory.list)
		{
			using (ListPool<string> tText = new ListPool<string>())
			{
				Kingdom kingdom = tHistory.kingdom;
				if (kingdom != null && kingdom.isAlive())
				{
					tText.Add(tHistory.kingdom.getTypeID());
				}
				Culture culture = tHistory.culture;
				if (culture != null && culture.isAlive())
				{
					tText.Add(tHistory.culture.getTypeID());
				}
				Actor unit = tHistory.unit;
				if (unit != null && unit.isAlive())
				{
					tText.Add(tHistory.unit.getTypeID());
				}
				City city = tHistory.city;
				if (city != null && city.isAlive())
				{
					tText.Add(tHistory.city.getTypeID());
				}
				Clan clan = tHistory.clan;
				if (clan != null && clan.isAlive())
				{
					tText.Add(tHistory.clan.getTypeID());
				}
				Plot plot = tHistory.plot;
				if (plot != null && plot.isAlive())
				{
					tText.Add(tHistory.plot.getTypeID());
				}
				War war = tHistory.war;
				if (war != null && war.isAlive())
				{
					tText.Add(tHistory.war.getTypeID());
				}
				Alliance alliance = tHistory.alliance;
				if (alliance != null && alliance.isAlive())
				{
					tText.Add(tHistory.alliance.getTypeID());
				}
				Language language = tHistory.language;
				if (language != null && language.isAlive())
				{
					tText.Add(tHistory.language.getTypeID());
				}
				Subspecies subspecies = tHistory.subspecies;
				if (subspecies != null && subspecies.isAlive())
				{
					tText.Add(tHistory.subspecies.getTypeID());
				}
				Religion religion = tHistory.religion;
				if (religion != null && religion.isAlive())
				{
					tText.Add(tHistory.religion.getTypeID());
				}
				Family family = tHistory.family;
				if (family != null && family.isAlive())
				{
					tText.Add(tHistory.family.getTypeID());
				}
				Army army = tHistory.army;
				if (army != null && army.isAlive())
				{
					tText.Add(tHistory.army.getTypeID());
				}
				Item item = tHistory.item;
				if (item != null && item.isAlive())
				{
					tText.Add(tHistory.item.getTypeID());
				}
				pTool.setText(string.Format("history {0} {1}:", tHistory.index, tHistory.window.screen_id), string.Join(",", tText), 0f, false, 0L, false, false, "");
			}
		}
	}

	// Token: 0x04002E65 RID: 11877
	public static readonly List<WindowHistoryData> list = new List<WindowHistoryData>();

	// Token: 0x04002E66 RID: 11878
	public static Action historyClearCallback;
}
