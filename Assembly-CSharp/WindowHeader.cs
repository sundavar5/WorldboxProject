using System;
using UnityEngine;

// Token: 0x02000843 RID: 2115
public class WindowHeader : MonoBehaviour
{
	// Token: 0x06004240 RID: 16960 RVA: 0x001C0AB7 File Offset: 0x001BECB7
	public void Awake()
	{
		base.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 3f);
	}
}
