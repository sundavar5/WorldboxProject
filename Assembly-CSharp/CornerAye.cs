using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x0200069E RID: 1694
public class CornerAye : MonoBehaviour
{
	// Token: 0x0600364B RID: 13899 RVA: 0x0018B338 File Offset: 0x00189538
	private void Awake()
	{
		this._rect = this.sprite.GetComponent<RectTransform>();
		this.reset();
	}

	// Token: 0x0600364C RID: 13900 RVA: 0x0018B351 File Offset: 0x00189551
	private void reset()
	{
		this._rect.anchoredPosition = new Vector2(100f, 0f);
		this.sprite.transform.DOKill(false);
	}

	// Token: 0x0600364D RID: 13901 RVA: 0x0018B37F File Offset: 0x0018957F
	private void Start()
	{
		CornerAye.instance = this;
	}

	// Token: 0x0600364E RID: 13902 RVA: 0x0018B388 File Offset: 0x00189588
	public void startAye()
	{
		this.reset();
		float tTime = 0.3f;
		Vector3 tVec = default(Vector3);
		this.sprite.transform.DOLocalMove(tVec, tTime, false).SetEase(Ease.OutBack).onComplete = new TweenCallback(this.moveBack);
	}

	// Token: 0x0600364F RID: 13903 RVA: 0x0018B3D4 File Offset: 0x001895D4
	private void moveBack()
	{
		Vector3 tVec = new Vector3(100f, 0f);
		float tTime = 0.3f;
		this.sprite.transform.DOLocalMove(tVec, tTime, false).SetEase(Ease.InOutQuad).SetDelay(0.1f);
	}

	// Token: 0x04002839 RID: 10297
	public static CornerAye instance;

	// Token: 0x0400283A RID: 10298
	public Transform sprite;

	// Token: 0x0400283B RID: 10299
	private RectTransform _rect;
}
