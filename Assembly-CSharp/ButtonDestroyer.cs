using System;
using UnityEngine;

// Token: 0x020005CB RID: 1483
public class ButtonDestroyer : MonoBehaviour
{
	// Token: 0x060030B7 RID: 12471 RVA: 0x00177764 File Offset: 0x00175964
	private void Awake()
	{
		if (Globals.specialAbstudio)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
