using System;
using UnityEngine;

// Token: 0x020001F3 RID: 499
public class WorldStatus : MonoBehaviour
{
	// Token: 0x06000EA2 RID: 3746 RVA: 0x000C2999 File Offset: 0x000C0B99
	public void setCurrentSlot(int pSlotId)
	{
		WorldStatus.currentSlot = pSlotId;
	}

	// Token: 0x04000ECE RID: 3790
	public static int currentSlot;
}
