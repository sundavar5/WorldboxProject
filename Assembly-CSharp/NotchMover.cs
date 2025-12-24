using System;
using UnityEngine;

// Token: 0x020005EC RID: 1516
public class NotchMover : MonoBehaviour
{
	// Token: 0x060031A3 RID: 12707 RVA: 0x0017B4AB File Offset: 0x001796AB
	private void Start()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
		this.originalTopPosition = this.rectTransform.anchoredPosition.y;
		this._canvas = base.gameObject.transform.GetComponentInParent<Canvas>();
	}

	// Token: 0x060031A4 RID: 12708 RVA: 0x0017B4E8 File Offset: 0x001796E8
	private void Update()
	{
		if ((float)Screen.height == Screen.safeArea.height)
		{
			return;
		}
		if (this._canvas == null)
		{
			return;
		}
		float diffY = ((float)Screen.height - Screen.safeArea.height) / this._canvas.scaleFactor;
		this.rectTransform.anchoredPosition = new Vector3(this.rectTransform.anchoredPosition.x, this.originalTopPosition - diffY);
	}

	// Token: 0x0400257E RID: 9598
	private float originalTopPosition;

	// Token: 0x0400257F RID: 9599
	private RectTransform rectTransform;

	// Token: 0x04002580 RID: 9600
	private Canvas _canvas;
}
