using System;
using System.Collections;
using UnityEngine;

// Token: 0x020006E1 RID: 1761
public class UnitHealthBarElement : UnitElement
{
	// Token: 0x0600387C RID: 14460 RVA: 0x001955B4 File Offset: 0x001937B4
	protected override IEnumerator showContent()
	{
		this._health.setBar((float)this.actor.getHealth(), (float)this.actor.getMaxHealth(), "/" + this.actor.getMaxHealth().ToText(4), true, false, true, 0.3f);
		yield break;
	}

	// Token: 0x040029F2 RID: 10738
	[SerializeField]
	private StatBar _health;
}
