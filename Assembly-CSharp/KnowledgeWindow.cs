using System;
using UnityEngine;

// Token: 0x02000705 RID: 1797
public class KnowledgeWindow : TabbedWindow
{
	// Token: 0x06003974 RID: 14708 RVA: 0x001991F0 File Offset: 0x001973F0
	protected override void create()
	{
		base.create();
		foreach (KnowledgeAsset tAsset in AssetManager.knowledge_library.list)
		{
			if (tAsset.show_in_knowledge_window)
			{
				KnowledgeElement knowledgeElement = Object.Instantiate<KnowledgeElement>(this._element_prefab, this._elements_parent);
				knowledgeElement.setAsset(tAsset);
				knowledgeElement.setCube(this._cube_overview_big, this._cube_tab);
			}
		}
	}

	// Token: 0x06003975 RID: 14709 RVA: 0x00199278 File Offset: 0x00197478
	private void OnEnable()
	{
		int tValue = 0;
		int tMax = 0;
		foreach (KnowledgeAsset tAsset in AssetManager.knowledge_library.list)
		{
			if (tAsset.show_in_knowledge_window)
			{
				tValue += tAsset.countUnlockedByPlayer();
				tMax += tAsset.countTotal();
			}
		}
		this._progress_bar.setBar((float)tValue, (float)tMax, "/" + tMax.ToText(), true, false, true, 0.3f);
	}

	// Token: 0x04002A5B RID: 10843
	[SerializeField]
	private Transform _elements_parent;

	// Token: 0x04002A5C RID: 10844
	[SerializeField]
	private KnowledgeElement _element_prefab;

	// Token: 0x04002A5D RID: 10845
	[SerializeField]
	private StatBar _progress_bar;

	// Token: 0x04002A5E RID: 10846
	[SerializeField]
	private CubeOverview _cube_overview_big;

	// Token: 0x04002A5F RID: 10847
	[SerializeField]
	private WindowMetaTab _cube_tab;
}
