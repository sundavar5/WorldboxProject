using System;
using UnityEngine;

// Token: 0x020001E3 RID: 483
public class RateUsRemover : MonoBehaviour
{
	// Token: 0x06000E04 RID: 3588 RVA: 0x000BF4E8 File Offset: 0x000BD6E8
	public void clickedRateUs()
	{
		PlayerConfig.instance.data.lastRateID = 12;
		base.gameObject.SetActive(false);
		PlayerConfig.saveData();
	}
}
