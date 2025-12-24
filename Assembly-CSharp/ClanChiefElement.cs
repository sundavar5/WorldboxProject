using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000663 RID: 1635
public class ClanChiefElement : ClanElement
{
	// Token: 0x06003500 RID: 13568 RVA: 0x00187B25 File Offset: 0x00185D25
	protected override IEnumerator showContent()
	{
		if (!base.clan.hasChief())
		{
			yield break;
		}
		this.track_objects.Add(base.clan.getChief());
		this._title_element.SetActive(true);
		this._chief_element.gameObject.SetActive(true);
		this._chief_element.show(base.clan.getChief());
		yield break;
	}

	// Token: 0x06003501 RID: 13569 RVA: 0x00187B34 File Offset: 0x00185D34
	protected override void clear()
	{
		this._title_element.SetActive(false);
		this._chief_element.gameObject.SetActive(false);
		base.clear();
	}

	// Token: 0x06003502 RID: 13570 RVA: 0x00187B59 File Offset: 0x00185D59
	public override bool checkRefreshWindow()
	{
		return (this._chief_element.gameObject.activeSelf && !base.clan.hasChief()) || base.checkRefreshWindow();
	}

	// Token: 0x040027C8 RID: 10184
	[SerializeField]
	private GameObject _title_element;

	// Token: 0x040027C9 RID: 10185
	[SerializeField]
	private PrefabUnitElement _chief_element;
}
