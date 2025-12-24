using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007DA RID: 2010
public class MaskAutoEnabler : MonoBehaviour
{
	// Token: 0x06003F5F RID: 16223 RVA: 0x001B53D8 File Offset: 0x001B35D8
	private void Awake()
	{
		base.GetComponent<Mask>().enabled = true;
		base.GetComponent<Image>().enabled = true;
	}
}
