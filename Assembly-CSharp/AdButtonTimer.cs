using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004A1 RID: 1185
public class AdButtonTimer : MonoBehaviour
{
	// Token: 0x060028C1 RID: 10433 RVA: 0x001469A4 File Offset: 0x00144BA4
	private void Awake()
	{
		AdButtonTimer.instance = this;
		this.adTimer = 10.0;
	}

	// Token: 0x060028C2 RID: 10434 RVA: 0x001469BC File Offset: 0x00144BBC
	internal static void setAdTimer()
	{
		if (PlayerConfig.instance == null)
		{
			return;
		}
		double tDiff = PlayerConfig.instance.data.nextAdTimestamp;
		tDiff -= Epoch.Current();
		AdButtonTimer.instance.adTimer = tDiff;
		if (AdButtonTimer.instance.adTimer < 0.0 || PlayerConfig.instance.data.nextAdTimestamp == -1.0)
		{
			AdButtonTimer.instance.adTimer = -1.0;
		}
	}

	// Token: 0x060028C3 RID: 10435 RVA: 0x00146A36 File Offset: 0x00144C36
	private void OnEnable()
	{
		AdButtonTimer.setAdTimer();
		this.updateButton();
	}

	// Token: 0x060028C4 RID: 10436 RVA: 0x00146A43 File Offset: 0x00144C43
	private void Update()
	{
		if (Config.hasPremium)
		{
			base.gameObject.SetActive(false);
			return;
		}
		if (this.adTimer > 0.0)
		{
			this.adTimer -= (double)Time.deltaTime;
		}
		this.updateButton();
	}

	// Token: 0x060028C5 RID: 10437 RVA: 0x00146A84 File Offset: 0x00144C84
	private void updateButton()
	{
		if (this.tRecalc > 0)
		{
			this.tRecalc--;
		}
		else
		{
			this.tRecalc = 10;
			AdButtonTimer.setAdTimer();
		}
		if (this.adTimer > 0.0)
		{
			this.timer.gameObject.SetActive(true);
			this.timer.text = Toolbox.formatTimer((float)this.adTimer);
			this.icon.color = this.transparent;
			return;
		}
		this.timer.gameObject.SetActive(false);
		this.icon.color = Color.white;
	}

	// Token: 0x04001E9D RID: 7837
	internal static AdButtonTimer instance;

	// Token: 0x04001E9E RID: 7838
	public Text timer;

	// Token: 0x04001E9F RID: 7839
	public Button button;

	// Token: 0x04001EA0 RID: 7840
	public Image icon;

	// Token: 0x04001EA1 RID: 7841
	private double adTimer;

	// Token: 0x04001EA2 RID: 7842
	private Color transparent = new Color(1f, 1f, 1f, 0.3f);

	// Token: 0x04001EA3 RID: 7843
	private int tRecalc;
}
