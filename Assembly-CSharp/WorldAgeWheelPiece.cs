using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000817 RID: 2071
public class WorldAgeWheelPiece : BaseWorldAgeElement, IDropHandler, IEventSystemHandler
{
	// Token: 0x060040E0 RID: 16608 RVA: 0x001BB4C4 File Offset: 0x001B96C4
	public void init(int pIndex)
	{
		this._index = pIndex;
		this._tip_button.setHoverAction(new TooltipAction(this._tip_button.showTooltipDefault), false);
		this.button.onClick.AddListener(new UnityAction(this.clickThisPiece));
	}

	// Token: 0x060040E1 RID: 16609 RVA: 0x001BB514 File Offset: 0x001B9714
	public void toggleHighlight(bool pState)
	{
		this._highlight.enabled = pState;
		this._highlight.color = this.asset.pie_selection_color;
		if (this._highlight.HasComponent<FadeInOutAnimation>())
		{
			this._highlight.GetComponent<FadeInOutAnimation>().resetToFadeIn();
		}
	}

	// Token: 0x060040E2 RID: 16610 RVA: 0x001BB560 File Offset: 0x001B9760
	private void clickThisPiece()
	{
		World.world.era_manager.setCurrentSlotIndex(this._index, 0f);
	}

	// Token: 0x060040E3 RID: 16611 RVA: 0x001BB57C File Offset: 0x001B977C
	public void toggleIconFrame(bool pState)
	{
		if (this._icon_frame != null)
		{
			this._icon_frame.enabled = pState;
		}
	}

	// Token: 0x060040E4 RID: 16612 RVA: 0x001BB598 File Offset: 0x001B9798
	public void OnDrop(PointerEventData pEventData)
	{
		if (pEventData.pointerDrag == null)
		{
			return;
		}
		WorldAgeButton tAgeButton = pEventData.pointerDrag.GetComponent<WorldAgeButton>();
		if (tAgeButton == null)
		{
			return;
		}
		WorldAgesWindow.setAgeAndSelectPiece(tAgeButton.getAsset(), this);
	}

	// Token: 0x060040E5 RID: 16613 RVA: 0x001BB5D6 File Offset: 0x001B97D6
	public bool isCurrentAge()
	{
		return this._index == World.world.era_manager.getCurrentSlotIndex();
	}

	// Token: 0x060040E6 RID: 16614 RVA: 0x001BB5EF File Offset: 0x001B97EF
	public int getIndex()
	{
		return this._index;
	}

	// Token: 0x04002F0D RID: 12045
	public Image mask;

	// Token: 0x04002F0E RID: 12046
	[SerializeField]
	private Image _highlight;

	// Token: 0x04002F0F RID: 12047
	[SerializeField]
	private Image _icon_frame;

	// Token: 0x04002F10 RID: 12048
	private int _index;
}
