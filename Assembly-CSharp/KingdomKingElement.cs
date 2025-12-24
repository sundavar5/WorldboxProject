using System;
using System.Collections;
using UnityEngine;

// Token: 0x020006F1 RID: 1777
public class KingdomKingElement : KingdomElement
{
	// Token: 0x06003907 RID: 14599 RVA: 0x001978FD File Offset: 0x00195AFD
	protected override IEnumerator showContent()
	{
		if (!base.kingdom.hasKing())
		{
			yield break;
		}
		this.track_objects.Add(base.kingdom.king);
		this._title_element.SetActive(true);
		this._king_element.gameObject.SetActive(true);
		this._king_element.show(base.kingdom.king);
		yield break;
	}

	// Token: 0x06003908 RID: 14600 RVA: 0x0019790C File Offset: 0x00195B0C
	protected override void clear()
	{
		this._title_element.SetActive(false);
		this._king_element.gameObject.SetActive(false);
		base.clear();
	}

	// Token: 0x06003909 RID: 14601 RVA: 0x00197931 File Offset: 0x00195B31
	public override bool checkRefreshWindow()
	{
		return (this._king_element.gameObject.activeSelf && !base.kingdom.hasKing()) || base.checkRefreshWindow();
	}

	// Token: 0x04002A32 RID: 10802
	[SerializeField]
	private GameObject _title_element;

	// Token: 0x04002A33 RID: 10803
	[SerializeField]
	private PrefabUnitElement _king_element;
}
