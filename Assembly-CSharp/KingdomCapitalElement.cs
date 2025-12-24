using System;
using System.Collections;
using UnityEngine;

// Token: 0x020006EC RID: 1772
public class KingdomCapitalElement : KingdomElement
{
	// Token: 0x060038F5 RID: 14581 RVA: 0x001977B0 File Offset: 0x001959B0
	protected override IEnumerator showContent()
	{
		if (!base.kingdom.hasCapital())
		{
			yield break;
		}
		this.track_objects.Add(base.kingdom.capital);
		this._capital_element.gameObject.SetActive(true);
		this._capital_element.show(base.kingdom.capital);
		yield break;
	}

	// Token: 0x060038F6 RID: 14582 RVA: 0x001977BF File Offset: 0x001959BF
	protected override void clear()
	{
		this._capital_element.gameObject.SetActive(false);
		base.clear();
	}

	// Token: 0x060038F7 RID: 14583 RVA: 0x001977D8 File Offset: 0x001959D8
	public override bool checkRefreshWindow()
	{
		return (this._capital_element.gameObject.activeSelf && !base.kingdom.hasCapital()) || base.checkRefreshWindow();
	}

	// Token: 0x04002A2C RID: 10796
	[SerializeField]
	private CityListElement _capital_element;
}
