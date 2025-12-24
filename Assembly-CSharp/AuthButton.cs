using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007B5 RID: 1973
public class AuthButton : MonoBehaviour
{
	// Token: 0x06003E62 RID: 15970 RVA: 0x001B2745 File Offset: 0x001B0945
	private void Awake()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06003E63 RID: 15971 RVA: 0x001B2752 File Offset: 0x001B0952
	public void showWorldNetOwnWorldsWindow()
	{
	}

	// Token: 0x06003E64 RID: 15972 RVA: 0x001B2754 File Offset: 0x001B0954
	public void showWorldNetWorldsListWindow()
	{
	}

	// Token: 0x06003E65 RID: 15973 RVA: 0x001B2756 File Offset: 0x001B0956
	public void showWorldNetMainWindow()
	{
	}

	// Token: 0x06003E66 RID: 15974 RVA: 0x001B2758 File Offset: 0x001B0958
	public void showWorldNetUploadWindow()
	{
	}

	// Token: 0x06003E67 RID: 15975 RVA: 0x001B275A File Offset: 0x001B095A
	public void showBrowseByTagWindow()
	{
	}

	// Token: 0x06003E68 RID: 15976 RVA: 0x001B275C File Offset: 0x001B095C
	public void wbbConfirm()
	{
	}

	// Token: 0x06003E69 RID: 15977 RVA: 0x001B275E File Offset: 0x001B095E
	public void uploadWorldButton()
	{
	}

	// Token: 0x06003E6A RID: 15978 RVA: 0x001B2760 File Offset: 0x001B0960
	public void checkAuthAndOpenWindow()
	{
	}

	// Token: 0x04002D88 RID: 11656
	private static string windowId;

	// Token: 0x04002D89 RID: 11657
	private static List<string> worldnetNoSub = new List<string>
	{
		"worldnet_main"
	};
}
