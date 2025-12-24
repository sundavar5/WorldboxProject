using System;
using UnityEngine;

// Token: 0x02000467 RID: 1127
public static class InputHelpers
{
	// Token: 0x17000211 RID: 529
	// (get) Token: 0x06002687 RID: 9863 RVA: 0x0013A263 File Offset: 0x00138463
	public static bool touchSupported
	{
		get
		{
			return Input.touchSupported;
		}
	}

	// Token: 0x17000212 RID: 530
	// (get) Token: 0x06002688 RID: 9864 RVA: 0x0013A26A File Offset: 0x0013846A
	public static int touchCount
	{
		get
		{
			return Input.touchCount;
		}
	}

	// Token: 0x17000213 RID: 531
	// (get) Token: 0x06002689 RID: 9865 RVA: 0x0013A271 File Offset: 0x00138471
	public static bool mouseSupported
	{
		get
		{
			return Input.mousePresent && (!Input.touchSupported || Input.touchCount == 0);
		}
	}

	// Token: 0x0600268A RID: 9866 RVA: 0x0013A28D File Offset: 0x0013848D
	public static bool GetMouseButtonDown(int pButton)
	{
		return pButton != -1 && Input.GetMouseButtonDown(pButton);
	}

	// Token: 0x0600268B RID: 9867 RVA: 0x0013A29B File Offset: 0x0013849B
	public static bool GetAnyMouseButtonDown()
	{
		return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2);
	}

	// Token: 0x0600268C RID: 9868 RVA: 0x0013A2B5 File Offset: 0x001384B5
	public static int GetAnyMouseButtonDownIndex()
	{
		if (Input.GetMouseButtonDown(0))
		{
			return 0;
		}
		if (Input.GetMouseButtonDown(1))
		{
			return 1;
		}
		if (Input.GetMouseButtonDown(2))
		{
			return 2;
		}
		return -1;
	}

	// Token: 0x0600268D RID: 9869 RVA: 0x0013A2D6 File Offset: 0x001384D6
	public static bool GetMouseButton(int pButton)
	{
		return pButton != -1 && Input.GetMouseButton(pButton);
	}

	// Token: 0x0600268E RID: 9870 RVA: 0x0013A2E4 File Offset: 0x001384E4
	public static bool GetAnyMouseButton()
	{
		return Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2);
	}

	// Token: 0x0600268F RID: 9871 RVA: 0x0013A2FE File Offset: 0x001384FE
	public static int GetAnyMouseButtonIndex()
	{
		if (Input.GetMouseButton(0))
		{
			return 0;
		}
		if (Input.GetMouseButton(1))
		{
			return 1;
		}
		if (Input.GetMouseButton(2))
		{
			return 2;
		}
		return -1;
	}

	// Token: 0x06002690 RID: 9872 RVA: 0x0013A31F File Offset: 0x0013851F
	public static bool GetMouseButtonUp(int pButton)
	{
		return pButton != -1 && Input.GetMouseButtonUp(pButton);
	}

	// Token: 0x06002691 RID: 9873 RVA: 0x0013A32D File Offset: 0x0013852D
	public static bool GetAnyMouseButtonUp()
	{
		return Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2);
	}

	// Token: 0x06002692 RID: 9874 RVA: 0x0013A347 File Offset: 0x00138547
	public static int GetAnyMouseButtonUpIndex()
	{
		if (Input.GetMouseButtonUp(0))
		{
			return 0;
		}
		if (Input.GetMouseButtonUp(1))
		{
			return 1;
		}
		if (Input.GetMouseButtonUp(2))
		{
			return 2;
		}
		return -1;
	}
}
