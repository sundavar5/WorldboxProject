using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006A5 RID: 1701
[RequireComponent(typeof(Text))]
public class TweenedColoredText : MonoBehaviour
{
	// Token: 0x06003667 RID: 13927 RVA: 0x0018B88C File Offset: 0x00189A8C
	private void Awake()
	{
		this._text = base.GetComponent<Text>();
	}

	// Token: 0x06003668 RID: 13928 RVA: 0x0018B89C File Offset: 0x00189A9C
	private void OnEnable()
	{
		this._text.DOKill(true);
		this._text.color = this.color1;
		this._text.DOColor(this.color2, this.duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
	}

	// Token: 0x04002855 RID: 10325
	public Color color1 = Color.blue;

	// Token: 0x04002856 RID: 10326
	public Color color2 = Color.red;

	// Token: 0x04002857 RID: 10327
	public float duration = 1f;

	// Token: 0x04002858 RID: 10328
	private Text _text;
}
