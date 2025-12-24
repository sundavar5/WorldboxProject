using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x020005E9 RID: 1513
public class LogoButton : MonoBehaviour
{
	// Token: 0x0600319A RID: 12698 RVA: 0x0017B221 File Offset: 0x00179421
	private void Awake()
	{
		this.initScale = base.transform.localScale.x;
		this.loadLetters();
	}

	// Token: 0x0600319B RID: 12699 RVA: 0x0017B240 File Offset: 0x00179440
	private void loadLetters()
	{
		this.listLetters = new List<UiCreature>();
		Transform letters = base.transform.FindRecursive("Letters").transform;
		int num = letters.childCount;
		for (int i = 0; i < num; i++)
		{
			UiCreature tLetter = letters.GetChild(i).GetComponent<UiCreature>();
			if (tLetter.dropped)
			{
				tLetter.resetPosition();
			}
			this.listLetters.Add(tLetter);
		}
	}

	// Token: 0x0600319C RID: 12700 RVA: 0x0017B2A8 File Offset: 0x001794A8
	private void letterFall()
	{
		if (this.listLetters.Count == 0)
		{
			this.loadLetters();
			AchievementLibrary.destroy_worldbox.check(null);
			return;
		}
		this.listLetters.ShuffleOne<UiCreature>();
		UiCreature uiCreature = this.listLetters[0];
		this.listLetters.RemoveAt(0);
		uiCreature.click();
	}

	// Token: 0x0600319D RID: 12701 RVA: 0x0017B300 File Offset: 0x00179500
	public void clickLogo()
	{
		MusicBox.playSound("event:/SFX/EXPLOSIONS/ExplosionHuge", -1f, -1f, false, false);
		if (this.tweener != null && this.tweener.active)
		{
			this.tweener.Kill(false);
		}
		float tScale = this.initScale * 1.2f;
		if (this.listLetters.Count == 0)
		{
			tScale = 1.6f;
			base.transform.localScale = new Vector3(tScale, tScale, tScale);
			this.tweener = base.transform.DOScale(new Vector3(this.initScale, this.initScale, this.initScale), 0.3f).SetEase(Ease.OutBack);
		}
		else
		{
			base.transform.localScale = new Vector3(tScale, tScale, tScale);
			this.tweener = base.transform.DOScale(new Vector3(this.initScale, this.initScale, this.initScale), 0.3f).SetEase(Ease.OutBack);
		}
		this.letterFall();
	}

	// Token: 0x04002577 RID: 9591
	private List<UiCreature> listLetters;

	// Token: 0x04002578 RID: 9592
	private float initScale = 1f;

	// Token: 0x04002579 RID: 9593
	private Tweener tweener;
}
