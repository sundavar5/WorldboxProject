using System;
using UnityEngine;

// Token: 0x020007BD RID: 1981
public class TagButton : MonoBehaviour
{
	// Token: 0x06003E9B RID: 16027 RVA: 0x001B2EEF File Offset: 0x001B10EF
	private void Awake()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06003E9C RID: 16028 RVA: 0x001B2EFC File Offset: 0x001B10FC
	public void showWorldNetTagListWindow()
	{
	}

	// Token: 0x06003E9D RID: 16029 RVA: 0x001B2EFE File Offset: 0x001B10FE
	public bool inListWindow()
	{
		return ScrollWindow.isCurrentWindow("worldnet_list_your_worlds") || ScrollWindow.isCurrentWindow("worldnet_list_more_worlds");
	}

	// Token: 0x04002D9D RID: 11677
	public MapTagType tagType;
}
