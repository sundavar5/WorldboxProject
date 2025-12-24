using System;
using UnityEngine;

// Token: 0x020005F5 RID: 1525
public class RateUsChecker : MonoBehaviour
{
	// Token: 0x060031D4 RID: 12756 RVA: 0x0017C191 File Offset: 0x0017A391
	private void OnEnable()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		if (this.rateUs != null && this.rateUs.gameObject != null)
		{
			this.rateUs.gameObject.SetActive(false);
		}
	}

	// Token: 0x060031D5 RID: 12757 RVA: 0x0017C1D0 File Offset: 0x0017A3D0
	private void Update()
	{
		if (VersionCheck.isOutdated())
		{
			if (this.rateUs != null && this.rateUs.gameObject != null)
			{
				this.rateUs.gameObject.SetActive(false);
			}
			if (this.updateAvailable != null && this.updateAvailable.gameObject != null)
			{
				this.updateAvailable.gameObject.SetActive(true);
				return;
			}
		}
		else if (this.updateAvailable != null && this.updateAvailable.gameObject != null)
		{
			this.updateAvailable.gameObject.SetActive(false);
		}
	}

	// Token: 0x040025C4 RID: 9668
	public GameObject rateUs;

	// Token: 0x040025C5 RID: 9669
	public GameObject updateAvailable;
}
