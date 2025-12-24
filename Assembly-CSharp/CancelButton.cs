using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005D0 RID: 1488
public class CancelButton : MonoBehaviour
{
	// Token: 0x060030EB RID: 12523 RVA: 0x00178046 File Offset: 0x00176246
	private void Awake()
	{
		this._rect = base.GetComponent<RectTransform>();
	}

	// Token: 0x060030EC RID: 12524 RVA: 0x00178054 File Offset: 0x00176254
	public void setIconFrom(PowerButton pButton)
	{
		if (pButton.godPower == null)
		{
			return;
		}
		if (pButton.icon == null)
		{
			return;
		}
		this.powerIcon.sprite = pButton.icon.sprite;
	}

	// Token: 0x060030ED RID: 12525 RVA: 0x00178084 File Offset: 0x00176284
	private void Update()
	{
		if (this.goDown != this._going_down)
		{
			this._going_down = this.goDown;
			this._timer = 0f;
			if (this.goDown)
			{
				this._timer = 0.95f;
			}
		}
		if (this.goUp != this._going_up)
		{
			this._going_up = this.goUp;
			this._timer = -1f;
		}
		if (this._timer < 1f)
		{
			this._timer += Time.deltaTime / 2f;
			this._timer = Mathf.Clamp(this._timer, 0f, 1f);
			float tVal;
			if (this._going_down)
			{
				tVal = iTween.easeInOutCirc(0f, -90f, this._timer);
			}
			else if (this._going_up)
			{
				tVal = iTween.easeInQuart(0f, 90f, this._timer);
			}
			else
			{
				tVal = iTween.easeInOutCirc(this._rect.anchoredPosition.y, 0f, this._timer);
			}
			this._rect.anchoredPosition = new Vector3(this._rect.anchoredPosition.x, tVal);
		}
	}

	// Token: 0x040024EF RID: 9455
	public Image powerIcon;

	// Token: 0x040024F0 RID: 9456
	public bool goUp;

	// Token: 0x040024F1 RID: 9457
	public bool goDown;

	// Token: 0x040024F2 RID: 9458
	private bool _going_down;

	// Token: 0x040024F3 RID: 9459
	private bool _going_up;

	// Token: 0x040024F4 RID: 9460
	private RectTransform _rect;

	// Token: 0x040024F5 RID: 9461
	private float _timer;

	// Token: 0x040024F6 RID: 9462
	private const float Y_TOP_TARGET = 90f;
}
