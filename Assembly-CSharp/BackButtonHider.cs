using System;
using UnityEngine;

// Token: 0x02000826 RID: 2086
internal class BackButtonHider : MonoBehaviour
{
	// Token: 0x06004122 RID: 16674 RVA: 0x001BC3F5 File Offset: 0x001BA5F5
	private void OnEnable()
	{
		if (WindowHistory.hasHistory())
		{
			base.gameObject.SetActive(true);
			return;
		}
		base.gameObject.SetActive(false);
	}
}
