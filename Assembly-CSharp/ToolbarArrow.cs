using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000775 RID: 1909
public class ToolbarArrow : MonoBehaviour
{
	// Token: 0x06003C71 RID: 15473 RVA: 0x001A3C4C File Offset: 0x001A1E4C
	private void Awake()
	{
		this.arrow_transform = this.arrow.transform;
		this.scroll_rect.onValueChanged.AddListener(new UnityAction<Vector2>(this.onScroll));
		this._button = this.arrow.GetComponent<Button>();
		this._button.onClick.AddListener(new UnityAction(this.scrollTab));
	}

	// Token: 0x06003C72 RID: 15474 RVA: 0x001A3CB4 File Offset: 0x001A1EB4
	protected virtual void onScroll(Vector2 pVal)
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003C73 RID: 15475 RVA: 0x001A3CBB File Offset: 0x001A1EBB
	protected virtual float getEndPosition()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003C74 RID: 15476 RVA: 0x001A3CC2 File Offset: 0x001A1EC2
	protected virtual float getScrollPosition()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003C75 RID: 15477 RVA: 0x001A3CC9 File Offset: 0x001A1EC9
	protected virtual void setScrollPosition(float pValue)
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003C76 RID: 15478 RVA: 0x001A3CD0 File Offset: 0x001A1ED0
	protected virtual void Update()
	{
		if (!this.should_show)
		{
			this.timer += Time.deltaTime * 2f;
		}
		else
		{
			this.timer -= Time.deltaTime * 2f;
		}
		this.timer = Mathf.Clamp(this.timer, 0f, 1f);
	}

	// Token: 0x06003C77 RID: 15479 RVA: 0x001A3D34 File Offset: 0x001A1F34
	private void scrollTab()
	{
		float tEndPosition = this.getEndPosition();
		this._tweener.Kill(false);
		this._tweener = DOTween.To(() => this.getScrollPosition(), delegate(float pPos)
		{
			this.setScrollPosition(pPos);
		}, tEndPosition, 0.3f).SetEase(Ease.InOutCirc);
	}

	// Token: 0x06003C78 RID: 15480 RVA: 0x001A3D84 File Offset: 0x001A1F84
	private void OnDisable()
	{
		this._tweener.Kill(true);
	}

	// Token: 0x04002BE4 RID: 11236
	protected const float POSITION_SCROLL_BOUND_BEGIN = 0.1f;

	// Token: 0x04002BE5 RID: 11237
	protected const float POSITION_SCROLL_BOUND_END = 0.98f;

	// Token: 0x04002BE6 RID: 11238
	protected const float POSITION_SCROLL_END = 1f;

	// Token: 0x04002BE7 RID: 11239
	protected const float POSITION_SCROLL_MIN = 0.5f;

	// Token: 0x04002BE8 RID: 11240
	private const float POSITION_UPDATE_SPEED = 2f;

	// Token: 0x04002BE9 RID: 11241
	private const float TWEEN_DURATION = 0.3f;

	// Token: 0x04002BEA RID: 11242
	[SerializeField]
	private Image arrow;

	// Token: 0x04002BEB RID: 11243
	[SerializeField]
	protected Vector3 hide_position;

	// Token: 0x04002BEC RID: 11244
	[SerializeField]
	protected ScrollRectExtended scroll_rect;

	// Token: 0x04002BED RID: 11245
	protected float timer;

	// Token: 0x04002BEE RID: 11246
	protected bool should_show = true;

	// Token: 0x04002BEF RID: 11247
	protected Transform arrow_transform;

	// Token: 0x04002BF0 RID: 11248
	private Button _button;

	// Token: 0x04002BF1 RID: 11249
	private Tweener _tweener;
}
