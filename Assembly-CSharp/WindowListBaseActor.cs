using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200069C RID: 1692
public class WindowListBaseActor : MonoBehaviour, IComponentList, IShouldRefreshWindow
{
	// Token: 0x06003634 RID: 13876 RVA: 0x0018B036 File Offset: 0x00189236
	private void checkCreate()
	{
		if (this._created)
		{
			return;
		}
		this._created = true;
		this.create();
	}

	// Token: 0x06003635 RID: 13877 RVA: 0x0018B04E File Offset: 0x0018924E
	protected virtual void create()
	{
		this.pool_elements = new ObjectPoolGenericMono<PrefabUnitElement>(this.element_prefab, this.transformContent);
		this._scrollWindow = base.gameObject.GetComponent<ScrollWindow>();
		this.showSortingTabs();
	}

	// Token: 0x06003636 RID: 13878 RVA: 0x0018B07E File Offset: 0x0018927E
	protected virtual void setupSortingTabs()
	{
	}

	// Token: 0x06003637 RID: 13879 RVA: 0x0018B080 File Offset: 0x00189280
	protected virtual void showSortingTabs()
	{
		this.sorting_tab.clearButtons();
		this.setupSortingTabs();
		this.sorting_tab.enableFirstIfNone();
	}

	// Token: 0x06003638 RID: 13880 RVA: 0x0018B09E File Offset: 0x0018929E
	public void init(GameObject pNoItems, SortingTab pSortingTab, GameObject pListElementPrefab, Transform pListTransform, ScrollRect pScrollRect, Text pTitleCounter, Text pFavoritesCounter, Text pDeadCounter)
	{
		this.noItems = pNoItems;
		this.sorting_tab = pSortingTab;
		this.element_prefab = pListElementPrefab.GetComponent<PrefabUnitElement>();
		this.transformContent = pListTransform;
		this._title_counter = pTitleCounter;
	}

	// Token: 0x06003639 RID: 13881 RVA: 0x0018B0CA File Offset: 0x001892CA
	private void showElement(Actor pObject)
	{
		this.pool_elements.getNext().show(pObject);
	}

	// Token: 0x0600363A RID: 13882 RVA: 0x0018B0DD File Offset: 0x001892DD
	protected virtual List<Actor> getObjects()
	{
		return null;
	}

	// Token: 0x0600363B RID: 13883 RVA: 0x0018B0E0 File Offset: 0x001892E0
	private void OnEnable()
	{
		this.checkCreate();
		this.showSortingTabs();
		this.show();
	}

	// Token: 0x0600363C RID: 13884 RVA: 0x0018B0F4 File Offset: 0x001892F4
	protected virtual void show()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		this.clear();
		if (this.isEmpty())
		{
			this.noItems.SetActive(true);
		}
		else
		{
			this.noItems.SetActive(false);
			this.showElements();
		}
		this.pool_elements.disableInactive();
		ScrollWindow.checkElements();
	}

	// Token: 0x0600363D RID: 13885 RVA: 0x0018B148 File Offset: 0x00189348
	public ListPool<NanoObject> getElements()
	{
		this.meta_list.Clear();
		this.meta_list.AddRange(this.getObjects());
		this.meta_list.Sort((NanoObject a, NanoObject b) => this.current_sort(a as Actor, b as Actor));
		SortButton currentButton = this.sorting_tab.getCurrentButton();
		if (currentButton != null && currentButton.getState() == SortButtonState.Down)
		{
			this.meta_list.Reverse();
		}
		return new ListPool<NanoObject>(this.meta_list);
	}

	// Token: 0x0600363E RID: 13886 RVA: 0x0018B1BC File Offset: 0x001893BC
	private void showElements()
	{
		using (ListPool<NanoObject> tTempList = this.getElements())
		{
			for (int i = 0; i < tTempList.Count; i++)
			{
				NanoObject tObject = tTempList[i];
				this.showElement(tObject as Actor);
			}
			AssetManager.meta_type_library.getAsset(MetaType.Unit).setListGetter(new MetaTypeListPoolAction(this.getElements));
		}
	}

	// Token: 0x0600363F RID: 13887 RVA: 0x0018B230 File Offset: 0x00189430
	private bool isEmpty()
	{
		List<Actor> tList = this.getObjects();
		return tList == null || tList.Count == 0;
	}

	// Token: 0x06003640 RID: 13888 RVA: 0x0018B252 File Offset: 0x00189452
	private void clear()
	{
		this.pool_elements.clear(false);
		this.meta_list.Clear();
		AssetManager.meta_type_library.getAsset(MetaType.Unit).setListGetter(null);
	}

	// Token: 0x06003641 RID: 13889 RVA: 0x0018B27D File Offset: 0x0018947D
	private void OnDisable()
	{
		this.clear();
	}

	// Token: 0x06003642 RID: 13890 RVA: 0x0018B285 File Offset: 0x00189485
	public void setShowFavoritesOnly()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003643 RID: 13891 RVA: 0x0018B28C File Offset: 0x0018948C
	public void setShowDeadOnly()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003644 RID: 13892 RVA: 0x0018B293 File Offset: 0x00189493
	public void setShowAliveOnly()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003645 RID: 13893 RVA: 0x0018B29A File Offset: 0x0018949A
	public void setShowAll()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003646 RID: 13894 RVA: 0x0018B2A1 File Offset: 0x001894A1
	public void setDefault()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003647 RID: 13895 RVA: 0x0018B2A8 File Offset: 0x001894A8
	public virtual bool checkRefreshWindow()
	{
		using (List<NanoObject>.Enumerator enumerator = this.meta_list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isRekt())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0400282F RID: 10287
	public GameObject noItems;

	// Token: 0x04002830 RID: 10288
	protected ObjectPoolGenericMono<PrefabUnitElement> pool_elements;

	// Token: 0x04002831 RID: 10289
	public Transform transformContent;

	// Token: 0x04002832 RID: 10290
	public PrefabUnitElement element_prefab;

	// Token: 0x04002833 RID: 10291
	public SortingTab sorting_tab;

	// Token: 0x04002834 RID: 10292
	[SerializeField]
	protected Text _title_counter;

	// Token: 0x04002835 RID: 10293
	private bool _created;

	// Token: 0x04002836 RID: 10294
	protected Comparison<Actor> current_sort;

	// Token: 0x04002837 RID: 10295
	internal ScrollWindow _scrollWindow;

	// Token: 0x04002838 RID: 10296
	public readonly List<NanoObject> meta_list = new List<NanoObject>();
}
