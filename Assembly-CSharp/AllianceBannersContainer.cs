using System;
using UnityEngine;

// Token: 0x02000624 RID: 1572
public class AllianceBannersContainer<TBanner, TMetaObject, TData> : AllianceElement where TBanner : BannerGeneric<TMetaObject, TData> where TMetaObject : CoreSystemObject<TData> where TData : BaseSystemData
{
	// Token: 0x0600336E RID: 13166 RVA: 0x00183285 File Offset: 0x00181485
	protected override void Awake()
	{
		this.pool_elements = new ObjectPoolGenericMono<TBanner>(this._prefab, this._container);
		base.Awake();
	}

	// Token: 0x0600336F RID: 13167 RVA: 0x001832A4 File Offset: 0x001814A4
	protected override void clear()
	{
		this.pool_elements.clear(true);
		base.clear();
	}

	// Token: 0x04002703 RID: 9987
	protected ObjectPoolGenericMono<TBanner> pool_elements;

	// Token: 0x04002704 RID: 9988
	[SerializeField]
	private TBanner _prefab;

	// Token: 0x04002705 RID: 9989
	[SerializeField]
	private Transform _container;
}
