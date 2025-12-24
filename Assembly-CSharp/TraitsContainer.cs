using System;
using System.Collections;
using System.Collections.Generic;
using LayoutGroupExt;
using UnityEngine;

// Token: 0x02000796 RID: 1942
public class TraitsContainer<TTrait, TTraitButton> : MonoBehaviour, ITraitsContainer<TTrait, TTraitButton> where TTrait : BaseTrait<TTrait> where TTraitButton : TraitButton<TTrait>
{
	// Token: 0x06003D8B RID: 15755 RVA: 0x001AE568 File Offset: 0x001AC768
	private void Awake()
	{
		this._trait_window = base.GetComponentInParent<ITraitWindow<TTrait, TTraitButton>>();
		this._pool_traits = new ObjectPoolGenericMono<TTraitButton>(this._prefab_trait, this._grid);
		this._layout_grid = this._grid.GetComponent<LayoutGroupExtended>();
		this._grid.gameObject.AddOrGetComponent<TraitsGrid>().on_change = new OnChange(this.sortTraits);
	}

	// Token: 0x06003D8C RID: 15756 RVA: 0x001AE5CC File Offset: 0x001AC7CC
	private void OnEnable()
	{
		if (this._regular_title != null)
		{
			if (this._unlocked_title.gameObject.activeSelf)
			{
				this._regular_title.gameObject.SetActive(false);
			}
			else
			{
				this._regular_title.gameObject.SetActive(true);
			}
		}
		base.StartCoroutine(this.loadActiveTraits(true));
	}

	// Token: 0x06003D8D RID: 15757 RVA: 0x001AE62B File Offset: 0x001AC82B
	private void OnDisable()
	{
		this._traits.Clear();
		this._pool_traits.clear(true);
	}

	// Token: 0x06003D8E RID: 15758 RVA: 0x001AE644 File Offset: 0x001AC844
	public void reloadTraits(bool pAnimated)
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.loadActiveTraits(pAnimated));
	}

	// Token: 0x06003D8F RID: 15759 RVA: 0x001AE65A File Offset: 0x001AC85A
	protected IEnumerator loadActiveTraits(bool pAnimated = true)
	{
		using (ListPool<TTrait> tListPool = new ListPool<TTrait>(this._trait_window.getTraits()))
		{
			this._traits.Clear();
			this._pool_traits.clear(true);
			foreach (TTrait ptr in tListPool)
			{
				TTrait tTrait = ptr;
				this.loadActiveTrait(tTrait);
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x06003D90 RID: 15760 RVA: 0x001AE66C File Offset: 0x001AC86C
	private void loadActiveTrait(TTrait pTraitAsset)
	{
		TTraitButton tButton = this._pool_traits.getNext();
		tButton.load(pTraitAsset);
		this._traits[pTraitAsset] = tButton;
		AugmentationUnlockedAction tAction = new AugmentationUnlockedAction(this._trait_window.getEditor().reloadButtons);
		tButton.removeElementUnlockedAction(tAction);
		tButton.addElementUnlockedAction(tAction);
		ITraitsEditor<TTrait> tEditor = this._trait_window.getEditor();
		tButton.removeClickAction(new AugmentationButtonClickAction(tEditor.scrollToGroupStarter));
		tButton.addClickAction(new AugmentationButtonClickAction(tEditor.scrollToGroupStarter));
	}

	// Token: 0x06003D91 RID: 15761 RVA: 0x001AE70C File Offset: 0x001AC90C
	public void sortTraits()
	{
		using (ListPool<TTrait> tListPool = new ListPool<TTrait>(this._traits.Keys))
		{
			tListPool.Sort((TTrait a, TTrait b) => this._traits[a].transform.GetSiblingIndex().CompareTo(this._traits[b].transform.GetSiblingIndex()));
			this._trait_window.sortTraits(tListPool);
		}
	}

	// Token: 0x06003D92 RID: 15762 RVA: 0x001AE764 File Offset: 0x001AC964
	public ObjectPoolGenericMono<TTraitButton> getTraitPool()
	{
		return this._pool_traits;
	}

	// Token: 0x06003D93 RID: 15763 RVA: 0x001AE76C File Offset: 0x001AC96C
	public IReadOnlyCollection<TTraitButton> getTraitButtons()
	{
		return this._traits.Values;
	}

	// Token: 0x04002C81 RID: 11393
	[SerializeField]
	private TTraitButton _prefab_trait;

	// Token: 0x04002C82 RID: 11394
	[SerializeField]
	private Transform _regular_title;

	// Token: 0x04002C83 RID: 11395
	[SerializeField]
	private Transform _unlocked_title;

	// Token: 0x04002C84 RID: 11396
	[SerializeField]
	private Transform _grid;

	// Token: 0x04002C85 RID: 11397
	private LayoutGroupExtended _layout_grid;

	// Token: 0x04002C86 RID: 11398
	private ObjectPoolGenericMono<TTraitButton> _pool_traits;

	// Token: 0x04002C87 RID: 11399
	private ITraitWindow<TTrait, TTraitButton> _trait_window;

	// Token: 0x04002C88 RID: 11400
	private Dictionary<TTrait, TTraitButton> _traits = new Dictionary<TTrait, TTraitButton>();
}
