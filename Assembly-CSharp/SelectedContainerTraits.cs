using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200074E RID: 1870
public class SelectedContainerTraits<TTrait, TTraitButton, TTraitContainer, TTraitEditor> : SelectedElementBase<TTraitButton>, ISelectedContainerTrait where TTrait : BaseTrait<TTrait> where TTraitButton : TraitButton<TTrait> where TTraitContainer : ITraitsContainer<TTrait, TTraitButton> where TTraitEditor : ITraitsEditor<TTrait>
{
	// Token: 0x17000362 RID: 866
	// (get) Token: 0x06003B29 RID: 15145 RVA: 0x001A00BF File Offset: 0x0019E2BF
	protected virtual MetaType meta_type { get; }

	// Token: 0x17000363 RID: 867
	// (get) Token: 0x06003B2A RID: 15146 RVA: 0x001A00C7 File Offset: 0x0019E2C7
	protected string window_id
	{
		get
		{
			return AssetManager.meta_type_library.getAsset(this.meta_type).window_name;
		}
	}

	// Token: 0x06003B2B RID: 15147 RVA: 0x001A00DE File Offset: 0x0019E2DE
	private void Awake()
	{
		this._pool = new ObjectPoolGenericMono<TTraitButton>(this._prefab_trait, this._grid);
		this._grid.gameObject.AddOrGetComponent<TraitsGrid>();
	}

	// Token: 0x06003B2C RID: 15148 RVA: 0x001A0108 File Offset: 0x0019E308
	public void update(NanoObject pNano)
	{
		this.refresh(pNano);
	}

	// Token: 0x06003B2D RID: 15149 RVA: 0x001A0114 File Offset: 0x0019E314
	protected override void refresh(NanoObject pNano)
	{
		base.clear();
		foreach (TTrait tAsset in this.getTraits())
		{
			this.addButton(tAsset);
		}
	}

	// Token: 0x06003B2E RID: 15150 RVA: 0x001A0168 File Offset: 0x0019E368
	private void addButton(TTrait pObject)
	{
		TTraitButton next = this._pool.getNext();
		next.load(pObject);
		next.removeClickAction(new AugmentationButtonClickAction(this.showTraitsTabAndScroll));
		next.addClickAction(new AugmentationButtonClickAction(this.showTraitsTabAndScroll));
	}

	// Token: 0x06003B2F RID: 15151 RVA: 0x001A01BC File Offset: 0x0019E3BC
	private void showTraitsTabAndScroll(GameObject pButton)
	{
		if (!this.canEditTraits())
		{
			return;
		}
		TTraitButton tTraitButton = pButton.GetComponent<TTraitButton>();
		if (!InputHelpers.mouseSupported && !Tooltip.isShowingFor(tTraitButton))
		{
			return;
		}
		ScrollWindow.showWindow(this.window_id);
		World.world.StartCoroutine(this.showTraitsTabAndScrollRoutine(tTraitButton));
	}

	// Token: 0x06003B30 RID: 15152 RVA: 0x001A020B File Offset: 0x0019E40B
	private IEnumerator showTraitsTabAndScrollRoutine(TTraitButton pTraitButton)
	{
		ScrollWindow tWindow = ScrollWindow.getCurrentWindow();
		TTraitEditor tEditor = tWindow.GetComponentInChildren<TTraitEditor>(true);
		WindowMetaTab tTab = tEditor.getEditorTab();
		if (tTab.container.getActiveTab() != tTab)
		{
			tTab.container.showTab(tTab);
			yield return new WaitForSeconds(Config.getScrollToGroupDelay());
		}
		TTraitContainer tContainer = tWindow.GetComponentInChildren<TTraitContainer>();
		using (IEnumerator<TTraitButton> enumerator = tContainer.getTraitButtons().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				TTraitButton tButton = enumerator.Current;
				if (tButton.getElementAsset() == pTraitButton.getElementAsset())
				{
					tEditor.scrollToGroupStarter(tButton.gameObject, true);
					yield break;
				}
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x06003B31 RID: 15153 RVA: 0x001A021A File Offset: 0x0019E41A
	protected virtual IReadOnlyCollection<TTrait> getTraits()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003B32 RID: 15154 RVA: 0x001A0221 File Offset: 0x0019E421
	protected virtual bool canEditTraits()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003B34 RID: 15156 RVA: 0x001A0230 File Offset: 0x0019E430
	Transform ISelectedContainerTrait.get_transform()
	{
		return base.transform;
	}

	// Token: 0x04002B93 RID: 11155
	[SerializeField]
	private TTraitButton _prefab_trait;
}
