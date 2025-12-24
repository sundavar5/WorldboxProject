using System;
using UnityEngine;

// Token: 0x0200044B RID: 1099
public static class CoroutineHelper
{
	// Token: 0x17000206 RID: 518
	// (get) Token: 0x0600260D RID: 9741 RVA: 0x00137F54 File Offset: 0x00136154
	public static WaitForSecondsRealtime wait_for_0_5_s
	{
		get
		{
			return new WaitForSecondsRealtime(0.5f);
		}
	}

	// Token: 0x17000207 RID: 519
	// (get) Token: 0x0600260E RID: 9742 RVA: 0x00137F60 File Offset: 0x00136160
	public static WaitForSecondsRealtime wait_for_0_01_s
	{
		get
		{
			return new WaitForSecondsRealtime(0.01f);
		}
	}

	// Token: 0x17000208 RID: 520
	// (get) Token: 0x0600260F RID: 9743 RVA: 0x00137F6C File Offset: 0x0013616C
	public static WaitForSecondsRealtime wait_for_0_05_s
	{
		get
		{
			return new WaitForSecondsRealtime(0.05f);
		}
	}

	// Token: 0x17000209 RID: 521
	// (get) Token: 0x06002610 RID: 9744 RVA: 0x00137F78 File Offset: 0x00136178
	public static WaitForSecondsRealtime wait_for_0_025_s
	{
		get
		{
			return new WaitForSecondsRealtime(0.025f);
		}
	}

	// Token: 0x1700020A RID: 522
	// (get) Token: 0x06002611 RID: 9745 RVA: 0x00137F84 File Offset: 0x00136184
	public static YieldInstruction wait_for_end_of_frame
	{
		get
		{
			return new WaitForEndOfFrame();
		}
	}

	// Token: 0x1700020B RID: 523
	// (get) Token: 0x06002612 RID: 9746 RVA: 0x00137F8B File Offset: 0x0013618B
	public static YieldInstruction wait_for_next_frame
	{
		get
		{
			return null;
		}
	}
}
