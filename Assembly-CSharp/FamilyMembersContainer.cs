using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000693 RID: 1683
public class FamilyMembersContainer : FamilyElement
{
	// Token: 0x060035DC RID: 13788 RVA: 0x00189CE4 File Offset: 0x00187EE4
	protected override void Awake()
	{
		this._pool_children = new ObjectPoolGenericMono<PrefabUnitElement>(this._prefab, this._list_children);
		this._pool_parents = new ObjectPoolGenericMono<PrefabUnitElement>(this._prefab, this._list_parents);
		base.Awake();
	}

	// Token: 0x060035DD RID: 13789 RVA: 0x00189D1A File Offset: 0x00187F1A
	protected override IEnumerator showContent()
	{
		FamilyMembersContainer.<showContent>d__8 <showContent>d__ = new FamilyMembersContainer.<showContent>d__8(0);
		<showContent>d__.<>4__this = this;
		return <showContent>d__;
	}

	// Token: 0x060035DE RID: 13790 RVA: 0x00189D29 File Offset: 0x00187F29
	private void showParents()
	{
		this._title_parents.gameObject.SetActive(true);
		this._list_parents.gameObject.SetActive(true);
	}

	// Token: 0x060035DF RID: 13791 RVA: 0x00189D4D File Offset: 0x00187F4D
	private void showChildren()
	{
		this._title_children.gameObject.SetActive(true);
		this._list_children.gameObject.SetActive(true);
	}

	// Token: 0x060035E0 RID: 13792 RVA: 0x00189D71 File Offset: 0x00187F71
	private int sortByMainParent(Actor pActor1, Actor pActor2)
	{
		if (base.family.isMainFounder(pActor1) && !base.family.isMainFounder(pActor2))
		{
			return -1;
		}
		if (!base.family.isMainFounder(pActor1) && base.family.isMainFounder(pActor2))
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x060035E1 RID: 13793 RVA: 0x00189DB0 File Offset: 0x00187FB0
	private void showMember(Actor pActor, ObjectPoolGenericMono<PrefabUnitElement> pPool)
	{
		PrefabUnitElement next = pPool.getNext();
		next.transform.localScale = new Vector3(0.9f, 0.9f, 1f);
		next.show(pActor);
	}

	// Token: 0x060035E2 RID: 13794 RVA: 0x00189DE0 File Offset: 0x00187FE0
	protected override void clear()
	{
		this._title_parents.gameObject.SetActive(false);
		this._list_parents.gameObject.SetActive(false);
		this._title_children.gameObject.SetActive(false);
		this._list_children.gameObject.SetActive(false);
		this._pool_children.clear(true);
		this._pool_parents.clear(true);
		base.clear();
	}

	// Token: 0x04002802 RID: 10242
	private ObjectPoolGenericMono<PrefabUnitElement> _pool_parents;

	// Token: 0x04002803 RID: 10243
	private ObjectPoolGenericMono<PrefabUnitElement> _pool_children;

	// Token: 0x04002804 RID: 10244
	[SerializeField]
	private RectTransform _list_parents;

	// Token: 0x04002805 RID: 10245
	[SerializeField]
	private RectTransform _list_children;

	// Token: 0x04002806 RID: 10246
	[SerializeField]
	private LocalizedText _title_parents;

	// Token: 0x04002807 RID: 10247
	[SerializeField]
	private LocalizedText _title_children;

	// Token: 0x04002808 RID: 10248
	[SerializeField]
	private PrefabUnitElement _prefab;
}
