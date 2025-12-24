using System;
using UnityEngine;

// Token: 0x020006B1 RID: 1713
public class ButtonGraphListCompare : MonoBehaviour
{
	// Token: 0x060036D3 RID: 14035 RVA: 0x0018D0F0 File Offset: 0x0018B2F0
	public void compareListItems()
	{
		ScrollWindow tCurrentWindow = ScrollWindow.getCurrentWindow();
		IComponentList tComponent = tCurrentWindow.GetComponentInChildren<IComponentList>(true);
		if (tComponent == null)
		{
			Debug.LogError("IComponentList missing in " + tCurrentWindow.gameObject.name, tCurrentWindow.gameObject);
			return;
		}
		using (ListPool<NanoObject> tElements = tComponent.getElements())
		{
			if (tElements.Count > 0)
			{
				Config.selected_objects_graph.Clear();
				int i = 0;
				while (i < tElements.Count && i < 3)
				{
					NanoObject tElement = tElements[i];
					Config.selected_objects_graph.Add(tElement);
					i++;
				}
			}
			ScrollWindow.showWindow("chart_comparer");
		}
	}
}
