using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020006B2 RID: 1714
public class ButtonGraphScale : MonoBehaviour
{
	// Token: 0x060036D5 RID: 14037 RVA: 0x0018D1A4 File Offset: 0x0018B3A4
	private void Awake()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.setScale));
		this._image = base.GetComponent<Image>();
		this._main_container = base.GetComponentInParent<GraphTimeScaleContainer>();
		this._graph_controller = base.transform.parent.parent.GetComponentInChildren<GraphController>();
		this.checkSpriteStatus();
	}

	// Token: 0x060036D6 RID: 14038 RVA: 0x0018D206 File Offset: 0x0018B406
	private void Update()
	{
		this.checkSpriteStatus();
	}

	// Token: 0x060036D7 RID: 14039 RVA: 0x0018D20E File Offset: 0x0018B40E
	private void checkSpriteStatus()
	{
		if (this._main_container.current_scale == this.button_scale)
		{
			this._image.sprite = this.sprite_on;
			return;
		}
		this._image.sprite = this.sprite_off;
	}

	// Token: 0x060036D8 RID: 14040 RVA: 0x0018D246 File Offset: 0x0018B446
	public void setScale()
	{
		this._main_container.setTimeScale(this.button_scale);
		this._graph_controller.forceUpdateGraph();
	}

	// Token: 0x0400289C RID: 10396
	public Sprite sprite_on;

	// Token: 0x0400289D RID: 10397
	public Sprite sprite_off;

	// Token: 0x0400289E RID: 10398
	public GraphTimeScale button_scale;

	// Token: 0x0400289F RID: 10399
	private GraphTimeScaleContainer _main_container;

	// Token: 0x040028A0 RID: 10400
	private GraphController _graph_controller;

	// Token: 0x040028A1 RID: 10401
	private Image _image;
}
