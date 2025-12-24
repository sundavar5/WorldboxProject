using System;
using UnityEngine;

// Token: 0x02000807 RID: 2055
public class WorkshopEmptyListWindow : MonoBehaviour
{
	// Token: 0x0600407B RID: 16507 RVA: 0x001B9E45 File Offset: 0x001B8045
	private void OnEnable()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		if (WindowHistory.hasHistory())
		{
			WindowHistory.list.RemoveAt(WindowHistory.list.Count - 1);
		}
	}
}
