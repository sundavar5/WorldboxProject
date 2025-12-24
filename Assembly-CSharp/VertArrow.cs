using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000613 RID: 1555
public class VertArrow : MonoBehaviour
{
	// Token: 0x060032F0 RID: 13040 RVA: 0x001813B8 File Offset: 0x0017F5B8
	private void Awake()
	{
		this._arrow_transform = this.arrow.transform;
		this.scrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.onScroll));
		this.button = this.arrow.GetComponent<Button>();
		this.button.onClick.AddListener(new UnityAction(this.scrollTab));
	}

	// Token: 0x060032F1 RID: 13041 RVA: 0x00181420 File Offset: 0x0017F620
	private void onScroll(Vector2 pVal)
	{
		this.shouldShow = true;
		if (this.contentContainer.rect.width < this.scrollRect.rectTransform.rect.width)
		{
			this.shouldShow = false;
			return;
		}
		if (this.isLeft)
		{
			if (this.scrollRect.horizontalNormalizedPosition > 0.1f)
			{
				this.shouldShow = true;
				return;
			}
			this.shouldShow = false;
			return;
		}
		else
		{
			if (this.scrollRect.horizontalNormalizedPosition == 1f)
			{
				this.shouldShow = false;
				return;
			}
			if (this.scrollRect.horizontalNormalizedPosition < 0.98f)
			{
				this.shouldShow = true;
				return;
			}
			this.shouldShow = false;
			return;
		}
	}

	// Token: 0x060032F2 RID: 13042 RVA: 0x001814D0 File Offset: 0x0017F6D0
	private void Update()
	{
		if (!this.shouldShow)
		{
			this.timer += Time.deltaTime * 2f;
		}
		else
		{
			this.timer -= Time.deltaTime * 2f;
		}
		this.timer = Mathf.Clamp(this.timer, 0f, 1f);
		float tVal = iTween.easeInOutCirc(0f, this.hidPos.x, this.timer);
		if (this._arrow_transform.localPosition.x == tVal)
		{
			return;
		}
		this._arrow_transform.localPosition = new Vector3(tVal, 0f);
	}

	// Token: 0x060032F3 RID: 13043 RVA: 0x00181578 File Offset: 0x0017F778
	private void scrollTab()
	{
		float tEndPos = this.scrollRect.horizontalNormalizedPosition;
		float tS = this.scrollRect.rectTransform.rect.width / this.scrollRect.content.rect.width;
		if (this.isLeft)
		{
			tEndPos -= Mathf.Min(tS, 0.5f);
		}
		else
		{
			tEndPos += Mathf.Min(tS, 0.5f);
		}
		this._tweener.Kill(false);
		this._tweener = DOTween.To(() => this.scrollRect.horizontalNormalizedPosition, delegate(float pPos)
		{
			this.scrollRect.horizontalNormalizedPosition = pPos;
		}, tEndPos, 0.3f).SetEase(Ease.InOutCirc);
	}

	// Token: 0x060032F4 RID: 13044 RVA: 0x00181625 File Offset: 0x0017F825
	private void OnDisable()
	{
		this._tweener.Kill(true);
	}

	// Token: 0x0400269D RID: 9885
	public Image arrow;

	// Token: 0x0400269E RID: 9886
	private Transform _arrow_transform;

	// Token: 0x0400269F RID: 9887
	public Vector3 hidPos;

	// Token: 0x040026A0 RID: 9888
	public bool isLeft = true;

	// Token: 0x040026A1 RID: 9889
	public ScrollRectExtended scrollRect;

	// Token: 0x040026A2 RID: 9890
	public RectTransform contentContainer;

	// Token: 0x040026A3 RID: 9891
	private float timer;

	// Token: 0x040026A4 RID: 9892
	private bool shouldShow = true;

	// Token: 0x040026A5 RID: 9893
	private Button button;

	// Token: 0x040026A6 RID: 9894
	private Tweener _tweener;
}
