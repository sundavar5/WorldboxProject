using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000694 RID: 1684
public class FamilyOriginElement : FamilyElement
{
	// Token: 0x060035E4 RID: 13796 RVA: 0x00189E57 File Offset: 0x00188057
	protected override void Awake()
	{
		this._pool_elements = new ObjectPoolGenericMono<FamilyListElement>(this._prefab, this._container);
		base.Awake();
	}

	// Token: 0x060035E5 RID: 13797 RVA: 0x00189E76 File Offset: 0x00188076
	protected override IEnumerator showContent()
	{
		FamilyOriginElement.<showContent>d__5 <showContent>d__ = new FamilyOriginElement.<showContent>d__5(0);
		<showContent>d__.<>4__this = this;
		return <showContent>d__;
	}

	// Token: 0x060035E6 RID: 13798 RVA: 0x00189E85 File Offset: 0x00188085
	protected override void clear()
	{
		this._pool_elements.clear(true);
		this._family_origin_title.SetActive(false);
		base.clear();
	}

	// Token: 0x060035E7 RID: 13799 RVA: 0x00189EA8 File Offset: 0x001880A8
	protected override void clearInitial()
	{
		for (int i = 0; i < this._container.childCount; i++)
		{
			Object.Destroy(this._container.GetChild(i).gameObject);
		}
		base.clearInitial();
	}

	// Token: 0x04002809 RID: 10249
	[SerializeField]
	private GameObject _family_origin_title;

	// Token: 0x0400280A RID: 10250
	[SerializeField]
	private FamilyListElement _prefab;

	// Token: 0x0400280B RID: 10251
	private ObjectPoolGenericMono<FamilyListElement> _pool_elements;

	// Token: 0x0400280C RID: 10252
	[SerializeField]
	private Transform _container;
}
