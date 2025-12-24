using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000655 RID: 1621
public class CityLeaderElement : CityElement
{
	// Token: 0x0600348A RID: 13450 RVA: 0x00186120 File Offset: 0x00184320
	protected override IEnumerator showContent()
	{
		if (!base.city.hasLeader())
		{
			yield break;
		}
		this.track_objects.Add(base.city.leader);
		this._title_element.gameObject.SetActive(true);
		this._ruler_element.gameObject.SetActive(true);
		this._ruler_element.show(base.city.leader);
		yield break;
	}

	// Token: 0x0600348B RID: 13451 RVA: 0x0018612F File Offset: 0x0018432F
	protected override void clear()
	{
		this._title_element.gameObject.SetActive(false);
		this._ruler_element.gameObject.SetActive(false);
		base.clear();
	}

	// Token: 0x0600348C RID: 13452 RVA: 0x00186159 File Offset: 0x00184359
	public override bool checkRefreshWindow()
	{
		return (this._ruler_element.gameObject.activeSelf && !base.city.hasLeader()) || base.checkRefreshWindow();
	}

	// Token: 0x0400279C RID: 10140
	[SerializeField]
	private GameObject _title_element;

	// Token: 0x0400279D RID: 10141
	[SerializeField]
	private PrefabUnitElement _ruler_element;
}
