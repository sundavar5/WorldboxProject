using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000609 RID: 1545
public class UiGameStat : MonoBehaviour
{
	// Token: 0x060032B2 RID: 12978 RVA: 0x001802D5 File Offset: 0x0017E4D5
	private void Awake()
	{
		this._localized_text = this.nameText.GetComponent<LocalizedText>();
	}

	// Token: 0x060032B3 RID: 12979 RVA: 0x001802E8 File Offset: 0x0017E4E8
	public void setAsset(StatisticsAsset pAsset)
	{
		this._asset = pAsset;
	}

	// Token: 0x060032B4 RID: 12980 RVA: 0x001802F1 File Offset: 0x0017E4F1
	private void Update()
	{
		if (this._timeout > 0f)
		{
			this._timeout -= Time.deltaTime;
			return;
		}
		this._timeout = 1f;
		this.updateText();
	}

	// Token: 0x060032B5 RID: 12981 RVA: 0x00180324 File Offset: 0x0017E524
	internal void updateText()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		if (LocalizedTextManager.instance == null)
		{
			return;
		}
		if (this._asset == null)
		{
			return;
		}
		if (StatsHelper.getStat(this._asset.id) > 0L)
		{
			long tNewStat = StatsHelper.getStat(this._asset.id);
			if (tNewStat != this.lastStat)
			{
				this.checkDestroyTween();
				float tDuration = 0.95f;
				this.curTween = this.valueText.DORandomCounter(this.lastStat, tNewStat, tDuration);
				this.lastStat = tNewStat;
			}
		}
		else
		{
			this.valueText.text = StatsHelper.getStatistic(this._asset.id);
		}
		this._localized_text.setKeyAndUpdate(this._asset.getLocaleID());
		if (LocalizedTextManager.current_language.isRTL())
		{
			this.nameText.alignment = TextAnchor.MiddleRight;
			this.valueText.alignment = TextAnchor.MiddleLeft;
			return;
		}
		this.nameText.alignment = TextAnchor.MiddleLeft;
		this.valueText.alignment = TextAnchor.MiddleRight;
	}

	// Token: 0x060032B6 RID: 12982 RVA: 0x00180414 File Offset: 0x0017E614
	private void OnEnable()
	{
		this.updateText();
	}

	// Token: 0x060032B7 RID: 12983 RVA: 0x0018041C File Offset: 0x0017E61C
	private void OnDisable()
	{
		this.checkDestroyTween();
		this.lastStat = 0L;
	}

	// Token: 0x060032B8 RID: 12984 RVA: 0x0018042C File Offset: 0x0017E62C
	private void checkDestroyTween()
	{
		if (this.curTween != null && this.curTween.active)
		{
			this.curTween.Complete(false);
			this.curTween.Kill(false);
			this.curTween = null;
		}
	}

	// Token: 0x04002653 RID: 9811
	public Text nameText;

	// Token: 0x04002654 RID: 9812
	public Text valueText;

	// Token: 0x04002655 RID: 9813
	private LocalizedText _localized_text;

	// Token: 0x04002656 RID: 9814
	internal long lastStat;

	// Token: 0x04002657 RID: 9815
	internal Tweener curTween;

	// Token: 0x04002658 RID: 9816
	private StatisticsAsset _asset;

	// Token: 0x04002659 RID: 9817
	private float _timeout;
}
