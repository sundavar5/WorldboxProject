using System;
using UnityEngine;

// Token: 0x02000774 RID: 1908
public class HorizontalToolbarArrow : ToolbarArrow
{
	// Token: 0x06003C6B RID: 15467 RVA: 0x001A3AB8 File Offset: 0x001A1CB8
	protected override void Update()
	{
		base.Update();
		float tVal = iTween.easeInOutCirc(0f, this.hide_position.x, this.timer);
		if (this.arrow_transform.localPosition.x == tVal)
		{
			return;
		}
		this.arrow_transform.localPosition = new Vector3(tVal, 0f);
	}

	// Token: 0x06003C6C RID: 15468 RVA: 0x001A3B11 File Offset: 0x001A1D11
	protected override float getScrollPosition()
	{
		return this.scroll_rect.horizontalNormalizedPosition;
	}

	// Token: 0x06003C6D RID: 15469 RVA: 0x001A3B1E File Offset: 0x001A1D1E
	protected override void setScrollPosition(float pValue)
	{
		this.scroll_rect.horizontalNormalizedPosition = pValue;
	}

	// Token: 0x06003C6E RID: 15470 RVA: 0x001A3B2C File Offset: 0x001A1D2C
	protected override float getEndPosition()
	{
		float tEndPos = this.getScrollPosition();
		float tS = (float)Screen.width / CanvasMain.instance.canvas_ui.scaleFactor / this.scroll_rect.content.rect.width;
		if (this.is_left)
		{
			tEndPos -= Mathf.Min(tS, 0.5f);
		}
		else
		{
			tEndPos += Mathf.Min(tS, 0.5f);
		}
		return tEndPos;
	}

	// Token: 0x06003C6F RID: 15471 RVA: 0x001A3B98 File Offset: 0x001A1D98
	protected override void onScroll(Vector2 pVal)
	{
		float tWidth = (float)Screen.width / CanvasMain.instance.canvas_ui.scaleFactor;
		this.should_show = true;
		if (this.scroll_rect.content.rect.width < tWidth)
		{
			this.should_show = false;
			return;
		}
		if (this.is_left)
		{
			if (this.getScrollPosition() > 0.1f)
			{
				this.should_show = true;
				return;
			}
			this.should_show = false;
			return;
		}
		else
		{
			if (this.getScrollPosition() == 1f)
			{
				this.should_show = false;
				return;
			}
			if (this.getScrollPosition() < 0.98f)
			{
				this.should_show = true;
				return;
			}
			this.should_show = false;
			return;
		}
	}

	// Token: 0x04002BE3 RID: 11235
	public bool is_left = true;
}
