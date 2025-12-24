using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020004D8 RID: 1240
public struct ButtonTrigger
{
	// Token: 0x06002A03 RID: 10755 RVA: 0x0014A9A4 File Offset: 0x00148BA4
	public ButtonTrigger(Button pButton, EventTrigger.Entry pEntry, int pIndex)
	{
		this.button = pButton;
		this.entry = pEntry;
		this.index = pIndex;
	}

	// Token: 0x1700023E RID: 574
	// (get) Token: 0x06002A04 RID: 10756 RVA: 0x0014A9BB File Offset: 0x00148BBB
	public readonly Button button { get; }

	// Token: 0x1700023F RID: 575
	// (get) Token: 0x06002A05 RID: 10757 RVA: 0x0014A9C3 File Offset: 0x00148BC3
	public readonly EventTrigger.Entry entry { get; }

	// Token: 0x17000240 RID: 576
	// (get) Token: 0x06002A06 RID: 10758 RVA: 0x0014A9CB File Offset: 0x00148BCB
	public readonly int index { get; }
}
