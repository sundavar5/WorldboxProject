using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000298 RID: 664
public class SapientListWindow : ListWindow
{
	// Token: 0x0600193C RID: 6460 RVA: 0x000EEFB1 File Offset: 0x000ED1B1
	protected override void initComponent(IComponentList pComponent)
	{
		base.initComponent(pComponent);
		ISapientListComponent sapientListComponent = (ISapientListComponent)pComponent;
		sapientListComponent.setSapientCounter(this._sapient_counter);
		sapientListComponent.setNonSapientCounter(this._non_sapient_counter);
	}

	// Token: 0x0600193D RID: 6461 RVA: 0x000EEFD8 File Offset: 0x000ED1D8
	protected override void initTabsCallbacks(IComponentList pComponent)
	{
		base.initTabsCallbacks(pComponent);
		ISapientListComponent tComponent = (ISapientListComponent)pComponent;
		LocalizedText tNoItems = base.getNoItems();
		base.setTabCallbacks(this._tab_sapients, new Action(tComponent.setShowSapientOnly), null);
		this._tab_sapients.tab_action.AddListener(delegate(WindowMetaTab _)
		{
			tNoItems.setKeyAndUpdate("empty_sapient_list");
		});
		base.setTabCallbacks(this._tab_non_sapients, new Action(tComponent.setShowNonSapientOnly), null);
		this._tab_non_sapients.tab_action.AddListener(delegate(WindowMetaTab _)
		{
			tNoItems.setKeyAndUpdate("empty_non_sapient_list");
		});
	}

	// Token: 0x040013D5 RID: 5077
	[SerializeField]
	private WindowMetaTab _tab_sapients;

	// Token: 0x040013D6 RID: 5078
	[SerializeField]
	private WindowMetaTab _tab_non_sapients;

	// Token: 0x040013D7 RID: 5079
	[SerializeField]
	private Text _sapient_counter;

	// Token: 0x040013D8 RID: 5080
	[SerializeField]
	private Text _non_sapient_counter;
}
