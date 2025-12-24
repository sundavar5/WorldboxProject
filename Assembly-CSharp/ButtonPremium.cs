using System;
using UnityEngine;

// Token: 0x02000828 RID: 2088
public class ButtonPremium : MonoBehaviour
{
	// Token: 0x06004128 RID: 16680 RVA: 0x001BC55A File Offset: 0x001BA75A
	public void clickPremium()
	{
		PlayerConfig.setFirebaseProp("clicked_buy_premium", "yes");
		Analytics.LogEvent("clicked_buy_premium", true, true);
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			ScrollWindow.showWindow("premium_purchase_error");
			return;
		}
		InAppManager.instance.buyPremium();
	}
}
