using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007D8 RID: 2008
public class ListMetaContainer<TListElement, TMetaObject, TMetaData> : WindowMetaElementBase where TListElement : WindowListElementBase<TMetaObject, TMetaData> where TMetaObject : CoreSystemObject<TMetaData> where TMetaData : BaseSystemData
{
	// Token: 0x06003F55 RID: 16213 RVA: 0x001B52BA File Offset: 0x001B34BA
	protected override void Awake()
	{
		this._window = base.GetComponentInParent<StatsWindow>();
		this._pool_elements = new ObjectPoolGenericMono<TListElement>(this._prefab, this._container);
		base.Awake();
	}

	// Token: 0x06003F56 RID: 16214 RVA: 0x001B52E5 File Offset: 0x001B34E5
	protected override IEnumerator showContent()
	{
		ListMetaContainer<TListElement, TMetaObject, TMetaData>.<showContent>d__5 <showContent>d__ = new ListMetaContainer<TListElement, TMetaObject, TMetaData>.<showContent>d__5(0);
		<showContent>d__.<>4__this = this;
		return <showContent>d__;
	}

	// Token: 0x06003F57 RID: 16215 RVA: 0x001B52F4 File Offset: 0x001B34F4
	private void showElement(TMetaObject pMeta)
	{
		this._pool_elements.getNext().show(pMeta);
	}

	// Token: 0x06003F58 RID: 16216 RVA: 0x001B530C File Offset: 0x001B350C
	protected override void clear()
	{
		this._pool_elements.clear(true);
		base.clear();
	}

	// Token: 0x06003F59 RID: 16217 RVA: 0x001B5320 File Offset: 0x001B3520
	protected IMetaObject getMeta()
	{
		return AssetManager.meta_type_library.getAsset(this._window.meta_type).get_selected() as IMetaObject;
	}

	// Token: 0x06003F5A RID: 16218 RVA: 0x001B5346 File Offset: 0x001B3546
	protected virtual IEnumerable<TMetaObject> getMetaList()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003F5B RID: 16219 RVA: 0x001B534D File Offset: 0x001B354D
	protected virtual Comparison<TMetaObject> getSorting()
	{
		throw new NotImplementedException();
	}

	// Token: 0x04002E06 RID: 11782
	[SerializeField]
	private TListElement _prefab;

	// Token: 0x04002E07 RID: 11783
	[SerializeField]
	private Transform _container;

	// Token: 0x04002E08 RID: 11784
	private StatsWindow _window;

	// Token: 0x04002E09 RID: 11785
	private ObjectPoolGenericMono<TListElement> _pool_elements;
}
