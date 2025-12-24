using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000630 RID: 1584
public class ArmyCaptainElement : ArmyElement
{
	// Token: 0x060033AC RID: 13228 RVA: 0x00183B6B File Offset: 0x00181D6B
	protected override IEnumerator showContent()
	{
		if (!base.army.hasCaptain())
		{
			yield break;
		}
		this.track_objects.Add(base.army.getCaptain());
		this._title_element.gameObject.SetActive(true);
		this._captain_element.gameObject.SetActive(true);
		this._captain_element.show(base.army.getCaptain());
		yield break;
	}

	// Token: 0x060033AD RID: 13229 RVA: 0x00183B7A File Offset: 0x00181D7A
	protected override void clear()
	{
		this._title_element.gameObject.SetActive(false);
		this._captain_element.gameObject.SetActive(false);
		base.clear();
	}

	// Token: 0x060033AE RID: 13230 RVA: 0x00183BA4 File Offset: 0x00181DA4
	public override bool checkRefreshWindow()
	{
		return (this._captain_element.gameObject.activeSelf && !base.army.hasCaptain()) || base.checkRefreshWindow();
	}

	// Token: 0x0400271D RID: 10013
	[SerializeField]
	private GameObject _title_element;

	// Token: 0x0400271E RID: 10014
	[SerializeField]
	private PrefabUnitElement _captain_element;
}
