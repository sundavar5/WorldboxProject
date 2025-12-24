using System;
using System.Collections;
using UnityEngine;

// Token: 0x020006ED RID: 1773
public class KingdomCitiesContainer : KingdomElement
{
	// Token: 0x060038F9 RID: 14585 RVA: 0x00197809 File Offset: 0x00195A09
	protected override void Awake()
	{
		this._pool_elements = new ObjectPoolGenericMono<CityListElement>(this._prefab, base.transform);
		base.Awake();
	}

	// Token: 0x060038FA RID: 14586 RVA: 0x00197828 File Offset: 0x00195A28
	protected override IEnumerator showContent()
	{
		KingdomCitiesContainer.<showContent>d__3 <showContent>d__ = new KingdomCitiesContainer.<showContent>d__3(0);
		<showContent>d__.<>4__this = this;
		return <showContent>d__;
	}

	// Token: 0x060038FB RID: 14587 RVA: 0x00197837 File Offset: 0x00195A37
	private void showCityElement(City pCity)
	{
		this._pool_elements.getNext().show(pCity);
	}

	// Token: 0x060038FC RID: 14588 RVA: 0x0019784A File Offset: 0x00195A4A
	protected override void clear()
	{
		this._pool_elements.clear(true);
		base.clear();
	}

	// Token: 0x04002A2D RID: 10797
	private ObjectPoolGenericMono<CityListElement> _pool_elements;

	// Token: 0x04002A2E RID: 10798
	[SerializeField]
	private CityListElement _prefab;
}
