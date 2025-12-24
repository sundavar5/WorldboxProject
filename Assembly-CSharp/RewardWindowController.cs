using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004B2 RID: 1202
public class RewardWindowController : MonoBehaviour
{
	// Token: 0x060029A1 RID: 10657 RVA: 0x00149194 File Offset: 0x00147394
	private void Update()
	{
		double tDiff = PlayerConfig.instance.data.nextAdTimestamp;
		double tCurr = Epoch.Current();
		tDiff -= tCurr;
		if (Config.isEditor && Config.editor_test_rewards_from_ads)
		{
			PlayerConfig.instance.data.nextAdTimestamp = -1.0;
			tDiff = 0.0;
		}
		if (tDiff > 0.0)
		{
			this.watchVideoButton.SetActive(false);
			this.waitTimeElement.SetActive(true);
			this.textElement.text = Toolbox.formatTimer((float)tDiff);
			return;
		}
		this.watchVideoButton.SetActive(true);
		this.waitTimeElement.SetActive(false);
	}

	// Token: 0x04001F17 RID: 7959
	public GameObject watchVideoButton;

	// Token: 0x04001F18 RID: 7960
	public GameObject waitTimeElement;

	// Token: 0x04001F19 RID: 7961
	public Text textElement;
}
