using System;
using System.Collections;
using UnityEngine;

// Token: 0x020006DB RID: 1755
public class UnitCityElement : UnitElement
{
	// Token: 0x06003851 RID: 14417 RVA: 0x0019508D File Offset: 0x0019328D
	protected override IEnumerator showContent()
	{
		if (!this.actor.hasCity())
		{
			yield break;
		}
		this.track_objects.Add(this.actor.getCity());
		this._title.SetActive(true);
		this._city_element.show(this.actor.getCity());
		this._city_element.gameObject.SetActive(true);
		yield break;
	}

	// Token: 0x06003852 RID: 14418 RVA: 0x0019509C File Offset: 0x0019329C
	protected override void clear()
	{
		this._title.SetActive(false);
		this._city_element.gameObject.SetActive(false);
		base.clear();
	}

	// Token: 0x06003853 RID: 14419 RVA: 0x001950C1 File Offset: 0x001932C1
	public override bool checkRefreshWindow()
	{
		return (this._city_element.gameObject.activeSelf && !this.actor.hasCity()) || base.checkRefreshWindow();
	}

	// Token: 0x040029D4 RID: 10708
	[SerializeField]
	private GameObject _title;

	// Token: 0x040029D5 RID: 10709
	[SerializeField]
	private CityListElement _city_element;
}
