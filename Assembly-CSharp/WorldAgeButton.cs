using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000812 RID: 2066
public class WorldAgeButton : BaseWorldAgeElement
{
	// Token: 0x060040A3 RID: 16547 RVA: 0x001BAA44 File Offset: 0x001B8C44
	protected override void prepare()
	{
		base.prepare();
		DraggableLayoutElement tDraggableLayoutElement;
		if (base.TryGetComponent<DraggableLayoutElement>(out tDraggableLayoutElement))
		{
			DraggableLayoutElement draggableLayoutElement = tDraggableLayoutElement;
			draggableLayoutElement.start_being_dragged = (Action<DraggableLayoutElement>)Delegate.Combine(draggableLayoutElement.start_being_dragged, new Action<DraggableLayoutElement>(this.onStartDrag));
		}
	}

	// Token: 0x060040A4 RID: 16548 RVA: 0x001BAA83 File Offset: 0x001B8C83
	private void onStartDrag(DraggableLayoutElement pOriginalElement)
	{
		this._selected.enabled = false;
	}

	// Token: 0x060040A5 RID: 16549 RVA: 0x001BAA91 File Offset: 0x001B8C91
	public void toggleSelectedButton(bool pState)
	{
		if (this._selected != null)
		{
			this._selected.color = this.asset.pie_selection_color;
			this._selected.enabled = pState;
		}
	}

	// Token: 0x04002EDE RID: 11998
	[SerializeField]
	private Image _selected;
}
