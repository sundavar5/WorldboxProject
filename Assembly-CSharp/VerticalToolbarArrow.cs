using System;
using UnityEngine;

// Token: 0x02000776 RID: 1910
public class VerticalToolbarArrow : ToolbarArrow
{
	// Token: 0x06003C7C RID: 15484 RVA: 0x001A3DB4 File Offset: 0x001A1FB4
	protected override void Update()
	{
		base.Update();
		float tVal = iTween.easeInOutCirc(0f, this.hide_position.y, this.timer);
		if (this.arrow_transform.localPosition.y == tVal)
		{
			return;
		}
		this.arrow_transform.localPosition = new Vector3(0f, tVal);
	}

	// Token: 0x06003C7D RID: 15485 RVA: 0x001A3E0D File Offset: 0x001A200D
	protected override float getScrollPosition()
	{
		return this.scroll_rect.verticalNormalizedPosition;
	}

	// Token: 0x06003C7E RID: 15486 RVA: 0x001A3E1A File Offset: 0x001A201A
	protected override void setScrollPosition(float pValue)
	{
		this.scroll_rect.verticalNormalizedPosition = pValue;
	}

	// Token: 0x06003C7F RID: 15487 RVA: 0x001A3E28 File Offset: 0x001A2028
	protected override float getEndPosition()
	{
		float tEndPos = this.getScrollPosition();
		float tS = (float)Screen.height / CanvasMain.instance.canvas_ui.scaleFactor / this.scroll_rect.content.rect.height;
		if (this.is_bottom)
		{
			tEndPos -= Mathf.Min(tS, 0.5f);
		}
		else
		{
			tEndPos += Mathf.Min(tS, 0.5f);
		}
		return tEndPos;
	}

	// Token: 0x06003C80 RID: 15488 RVA: 0x001A3E94 File Offset: 0x001A2094
	protected override void onScroll(Vector2 pVal)
	{
		float tHeight = (float)Screen.height / CanvasMain.instance.canvas_ui.scaleFactor;
		this.should_show = true;
		if (this.scroll_rect.content.rect.height < tHeight)
		{
			this.should_show = false;
			return;
		}
		if (this.is_bottom)
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

	// Token: 0x04002BF2 RID: 11250
	public bool is_bottom = true;
}
