using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007DF RID: 2015
public class MetaNeedsElementUnit : UnitElement
{
	// Token: 0x06003F73 RID: 16243 RVA: 0x001B5588 File Offset: 0x001B3788
	protected override IEnumerator showContent()
	{
		Actor tActor = SelectedUnit.unit;
		if (tActor == null)
		{
			yield break;
		}
		if (!tActor.isAlive())
		{
			yield break;
		}
		string tFinalText = MetaTextReportHelper.addSingleUnitText(tActor, false, false);
		this._text.text = tFinalText;
		if (string.IsNullOrEmpty(tFinalText))
		{
			yield break;
		}
		this._container.gameObject.SetActive(true);
		yield break;
	}

	// Token: 0x06003F74 RID: 16244 RVA: 0x001B5597 File Offset: 0x001B3797
	protected override void clear()
	{
		base.clear();
		this._container.gameObject.SetActive(false);
	}

	// Token: 0x04002E11 RID: 11793
	[SerializeField]
	private GameObject _container;

	// Token: 0x04002E12 RID: 11794
	[SerializeField]
	private Text _text;
}
