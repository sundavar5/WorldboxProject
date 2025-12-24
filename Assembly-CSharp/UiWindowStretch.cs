using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000544 RID: 1348
public class UiWindowStretch : EventTrigger
{
	// Token: 0x06002C08 RID: 11272 RVA: 0x0015B320 File Offset: 0x00159520
	private void Start()
	{
	}

	// Token: 0x06002C09 RID: 11273 RVA: 0x0015B324 File Offset: 0x00159524
	public void Update()
	{
		if (this.dragging)
		{
			Vector3 tNewPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			this.newSize = this.posClicked - tNewPos;
			this.stretchTarget.sizeDelta = new Vector2(this.originSizeDelta.x - this.newSize.x, this.originSizeDelta.y + this.newSize.y);
		}
	}

	// Token: 0x06002C0A RID: 11274 RVA: 0x0015B3A8 File Offset: 0x001595A8
	public override void OnPointerDown(PointerEventData eventData)
	{
		if (!this.dragging)
		{
			this.posClicked = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			this.originSizeDelta = this.stretchTarget.sizeDelta;
		}
		this.dragging = true;
	}

	// Token: 0x06002C0B RID: 11275 RVA: 0x0015B3F9 File Offset: 0x001595F9
	public override void OnPointerUp(PointerEventData eventData)
	{
		this.dragging = false;
	}

	// Token: 0x040021D0 RID: 8656
	public RectTransform stretchTarget;

	// Token: 0x040021D1 RID: 8657
	private bool dragging;

	// Token: 0x040021D2 RID: 8658
	private Transform mainTransform;

	// Token: 0x040021D3 RID: 8659
	private Transform canvasContainer;

	// Token: 0x040021D4 RID: 8660
	public Vector3 posClicked;

	// Token: 0x040021D5 RID: 8661
	public Vector3 newSize;

	// Token: 0x040021D6 RID: 8662
	public Vector2 originSizeDelta;
}
