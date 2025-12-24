using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000535 RID: 1333
public class DebugUiDrag : EventTrigger
{
	// Token: 0x06002BA6 RID: 11174 RVA: 0x00159408 File Offset: 0x00157608
	private void Start()
	{
		this._tool = base.transform.GetComponentInParent<DebugTool>();
		this._canvas = base.transform.GetComponentInParent<Canvas>();
		this.mainTransform = this._tool.transform;
		this.canvasContainer = this.mainTransform.parent;
	}

	// Token: 0x06002BA7 RID: 11175 RVA: 0x0015945C File Offset: 0x0015765C
	public void Update()
	{
		if (this.dragging)
		{
			Vector3 tNewPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			this.mainTransform.SetParent(null, true);
			this.mainTransform.SetParent(this.canvasContainer, true);
			Vector2 tSizeDelta = this._tool.GetComponent<RectTransform>().sizeDelta;
			tNewPos.x += tSizeDelta.x / 2f - 75f;
			tNewPos.y += 20f;
			this.mainTransform.position = tNewPos;
		}
	}

	// Token: 0x06002BA8 RID: 11176 RVA: 0x001594FC File Offset: 0x001576FC
	public override void OnPointerDown(PointerEventData eventData)
	{
		this.dragging = true;
	}

	// Token: 0x06002BA9 RID: 11177 RVA: 0x00159505 File Offset: 0x00157705
	public override void OnPointerUp(PointerEventData eventData)
	{
		this.dragging = false;
	}

	// Token: 0x0400216D RID: 8557
	private bool dragging;

	// Token: 0x0400216E RID: 8558
	private Transform mainTransform;

	// Token: 0x0400216F RID: 8559
	private Transform canvasContainer;

	// Token: 0x04002170 RID: 8560
	private DebugTool _tool;

	// Token: 0x04002171 RID: 8561
	private Canvas _canvas;
}
