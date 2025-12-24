using System;
using UnityEngine;

// Token: 0x02000525 RID: 1317
public class CenterTipCaller : MonoBehaviour
{
	// Token: 0x06002B24 RID: 11044 RVA: 0x00156171 File Offset: 0x00154371
	public void Show()
	{
		Tooltip.show(this, "normal", new TooltipData
		{
			tip_name = this.tip_title,
			tip_description = this.tip_id
		});
	}

	// Token: 0x0400204A RID: 8266
	public string tip_title;

	// Token: 0x0400204B RID: 8267
	public string tip_id;
}
