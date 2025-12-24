using System;
using System.Collections;
using UnityEngine;

// Token: 0x020006E3 RID: 1763
public class UnitLoverElement : UnitElement
{
	// Token: 0x06003884 RID: 14468 RVA: 0x0019572B File Offset: 0x0019392B
	protected override IEnumerator showContent()
	{
		if (!this.actor.hasLover())
		{
			yield break;
		}
		if (this.actor.lover.isRekt())
		{
			yield break;
		}
		this.track_objects.Add(this.actor.lover);
		this._lover_element.show(this.actor.lover);
		this._lover_title.SetActive(true);
		yield return new WaitForSecondsRealtime(0.025f);
		this._lover_element.gameObject.SetActive(true);
		yield break;
	}

	// Token: 0x06003885 RID: 14469 RVA: 0x0019573A File Offset: 0x0019393A
	protected override void clear()
	{
		this._lover_title.SetActive(false);
		this._lover_element.gameObject.SetActive(false);
		base.clear();
	}

	// Token: 0x06003886 RID: 14470 RVA: 0x0019575F File Offset: 0x0019395F
	public override bool checkRefreshWindow()
	{
		if (this._lover_element.gameObject.activeSelf)
		{
			if (!this.actor.hasLover())
			{
				return true;
			}
			if (this.actor.lover.isRekt())
			{
				return true;
			}
		}
		return base.checkRefreshWindow();
	}

	// Token: 0x040029F6 RID: 10742
	[SerializeField]
	private PrefabUnitElement _lover_element;

	// Token: 0x040029F7 RID: 10743
	[SerializeField]
	private GameObject _lover_title;
}
