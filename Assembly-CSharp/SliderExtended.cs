using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020007A5 RID: 1957
public class SliderExtended : Slider, IEndDragHandler, IEventSystemHandler
{
	// Token: 0x06003DF1 RID: 15857 RVA: 0x001B0657 File Offset: 0x001AE857
	public void OnEndDrag(PointerEventData pEventData)
	{
		SliderEndedEvent on_sliding_ended = this._on_sliding_ended;
		if (on_sliding_ended == null)
		{
			return;
		}
		on_sliding_ended();
	}

	// Token: 0x06003DF2 RID: 15858 RVA: 0x001B0669 File Offset: 0x001AE869
	public override void OnPointerDown(PointerEventData pEventData)
	{
		base.OnPointerDown(pEventData);
		ScrollWindow.getCurrentWindow().scrollRect.StopMovement();
		SliderPointerDownEvent on_pointer_down = this._on_pointer_down;
		if (on_pointer_down == null)
		{
			return;
		}
		on_pointer_down();
	}

	// Token: 0x06003DF3 RID: 15859 RVA: 0x001B0691 File Offset: 0x001AE891
	public void addCallbackDragEnd(SliderEndedEvent pCallback)
	{
		this._on_sliding_ended = (SliderEndedEvent)Delegate.Combine(this._on_sliding_ended, pCallback);
	}

	// Token: 0x06003DF4 RID: 15860 RVA: 0x001B06AA File Offset: 0x001AE8AA
	public void removeCallbackDragEnd(SliderEndedEvent pCallback)
	{
		this._on_sliding_ended = (SliderEndedEvent)Delegate.Remove(this._on_sliding_ended, pCallback);
	}

	// Token: 0x06003DF5 RID: 15861 RVA: 0x001B06C3 File Offset: 0x001AE8C3
	public void addCallbackPointerDown(SliderPointerDownEvent pCallback)
	{
		this._on_pointer_down = (SliderPointerDownEvent)Delegate.Combine(this._on_pointer_down, pCallback);
	}

	// Token: 0x06003DF6 RID: 15862 RVA: 0x001B06DC File Offset: 0x001AE8DC
	public void removeCallbackPointerDown(SliderPointerDownEvent pCallback)
	{
		this._on_pointer_down = (SliderPointerDownEvent)Delegate.Remove(this._on_pointer_down, pCallback);
	}

	// Token: 0x04002CFD RID: 11517
	private SliderEndedEvent _on_sliding_ended;

	// Token: 0x04002CFE RID: 11518
	private SliderPointerDownEvent _on_pointer_down;
}
