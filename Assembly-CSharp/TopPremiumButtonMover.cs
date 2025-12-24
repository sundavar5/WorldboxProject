using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x02000731 RID: 1841
public class TopPremiumButtonMover : MonoBehaviour
{
	// Token: 0x06003AA3 RID: 15011 RVA: 0x0019E7AC File Offset: 0x0019C9AC
	private void Update()
	{
		if (this.shouldShow())
		{
			if (this.target_pos != this.pos_show)
			{
				this.target_pos = this.pos_show;
				this.updateRandomText();
				base.transform.GetComponentInChildren<LocalizedTextPrice>().updateText(true);
				base.transform.DOLocalMoveY(this.target_pos, 0.5f, false);
				return;
			}
		}
		else if (this.target_pos != this.pos_hide)
		{
			this.target_pos = this.pos_hide;
			base.transform.DOLocalMoveY(this.target_pos, 0.5f, false);
		}
	}

	// Token: 0x06003AA4 RID: 15012 RVA: 0x0019E840 File Offset: 0x0019CA40
	private void updateRandomText()
	{
		int rand = Randy.randomInt(1, 5);
		if (rand > 1)
		{
			this.button_text.key = "premium_get_it_" + rand.ToString();
		}
		this.button_text.updateText(true);
	}

	// Token: 0x06003AA5 RID: 15013 RVA: 0x0019E884 File Offset: 0x0019CA84
	private bool shouldShow()
	{
		bool tResult = false;
		if (Config.hasPremium)
		{
			return false;
		}
		string tSelectedPowerID = World.world.getSelectedPowerID();
		if (!string.IsNullOrEmpty(tSelectedPowerID) && AssetManager.powers.get(tSelectedPowerID).requires_premium)
		{
			tResult = true;
		}
		return tResult;
	}

	// Token: 0x04002B4B RID: 11083
	public LocalizedText button_text;

	// Token: 0x04002B4C RID: 11084
	private float target_pos = -1f;

	// Token: 0x04002B4D RID: 11085
	private float pos_hide;

	// Token: 0x04002B4E RID: 11086
	private float pos_show = -45f;

	// Token: 0x04002B4F RID: 11087
	private DOTween _tween;
}
