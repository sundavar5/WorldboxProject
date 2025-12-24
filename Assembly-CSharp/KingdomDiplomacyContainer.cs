using System;
using UnityEngine;

// Token: 0x020006EF RID: 1775
public class KingdomDiplomacyContainer<TBanner, TMetaObject, TData> : KingdomElement where TBanner : BannerGeneric<TMetaObject, TData> where TMetaObject : CoreSystemObject<TData> where TData : BaseSystemData
{
	// Token: 0x06003902 RID: 14594 RVA: 0x001978B2 File Offset: 0x00195AB2
	protected override void Awake()
	{
		this.pool_elements = new ObjectPoolGenericMono<TBanner>(this._prefab, this._container);
		base.Awake();
	}

	// Token: 0x06003903 RID: 14595 RVA: 0x001978D1 File Offset: 0x00195AD1
	protected override void clear()
	{
		this.pool_elements.clear(true);
		base.clear();
	}

	// Token: 0x04002A2F RID: 10799
	protected ObjectPoolGenericMono<TBanner> pool_elements;

	// Token: 0x04002A30 RID: 10800
	[SerializeField]
	private TBanner _prefab;

	// Token: 0x04002A31 RID: 10801
	[SerializeField]
	private Transform _container;
}
