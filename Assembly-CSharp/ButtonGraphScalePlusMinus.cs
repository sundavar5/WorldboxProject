using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020006B4 RID: 1716
public class ButtonGraphScalePlusMinus : MonoBehaviour
{
	// Token: 0x060036DA RID: 14042 RVA: 0x0018D26C File Offset: 0x0018B46C
	private void Awake()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.setScale));
		this._main_container = base.GetComponentInParent<GraphTimeScaleContainer>();
		this._graph_controller = base.transform.parent.parent.GetComponentInChildren<GraphController>();
	}

	// Token: 0x060036DB RID: 14043 RVA: 0x0018D2BC File Offset: 0x0018B4BC
	public void setScale()
	{
		if (this.button_scale_type == ButtonGraphScaleType.Plus)
		{
			this._main_container.timeScaleMinus();
		}
		else
		{
			this._main_container.timeScalePlus();
		}
		this._graph_controller.forceUpdateGraph();
	}

	// Token: 0x040028A5 RID: 10405
	public ButtonGraphScaleType button_scale_type;

	// Token: 0x040028A6 RID: 10406
	private GraphTimeScaleContainer _main_container;

	// Token: 0x040028A7 RID: 10407
	private GraphController _graph_controller;
}
