using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006E8 RID: 1768
public class UnitTextPhrases : MonoBehaviour
{
	// Token: 0x060038BA RID: 14522 RVA: 0x00196078 File Offset: 0x00194278
	private void Awake()
	{
		this.finish();
	}

	// Token: 0x060038BB RID: 14523 RVA: 0x00196080 File Offset: 0x00194280
	public void startNewTween(string pText, Transform pFollowObject)
	{
		base.gameObject.SetActive(true);
		this.killTweens();
		Vector3 tRotation = new Vector3(0f, 0f, Randy.randomFloat(-30f, 30f));
		this._size_parent.localRotation = Quaternion.Euler(tRotation);
		this._text.text = pText;
		Vector3 tInitialPos = new Vector3(0f, (float)Randy.randomInt(8, 12), 0f);
		if (pFollowObject == null)
		{
			this._text.transform.localPosition = tInitialPos;
		}
		else
		{
			this._text.transform.position = pFollowObject.position + tInitialPos;
		}
		this._text.fontSize = Randy.randomInt(7, 9);
		Vector3 tTarget = new Vector3(0f, Randy.randomFloat(30f, 60f), 0f);
		this._text_tweener.Kill(false);
		if (pFollowObject == null)
		{
			this._text_tweener = this._text.transform.DOLocalMove(tTarget, 3f, false);
		}
		else
		{
			this._text_tweener = this._text.transform.DOMove(tTarget + pFollowObject.position, 3f, false);
		}
		this._text_tweener.SetEase(Ease.OutCubic);
		this._text.DOColor(Color.white, 1.25f).onComplete = new TweenCallback(this.doTextFade);
	}

	// Token: 0x060038BC RID: 14524 RVA: 0x001961F1 File Offset: 0x001943F1
	private void doTextFade()
	{
		this._text.DOFade(0f, 2f).onComplete = new TweenCallback(this.finish);
	}

	// Token: 0x060038BD RID: 14525 RVA: 0x00196219 File Offset: 0x00194419
	public bool isTweening()
	{
		return this._text_tweener.IsActive();
	}

	// Token: 0x060038BE RID: 14526 RVA: 0x00196226 File Offset: 0x00194426
	private void finish()
	{
		this.killTweens();
		this._text.color = Toolbox.color_white_transparent;
		base.gameObject.SetActive(false);
	}

	// Token: 0x060038BF RID: 14527 RVA: 0x0019624A File Offset: 0x0019444A
	private void killTweens()
	{
		Tweener text_tweener = this._text_tweener;
		if (text_tweener != null)
		{
			text_tweener.Kill(false);
		}
		this._text.DOKill(false);
	}

	// Token: 0x04002A0F RID: 10767
	[SerializeField]
	private RectTransform _size_parent;

	// Token: 0x04002A10 RID: 10768
	[SerializeField]
	private Text _text;

	// Token: 0x04002A11 RID: 10769
	private Tweener _text_tweener;
}
