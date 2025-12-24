using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020006A7 RID: 1703
public class ChromosomeElement : MonoBehaviour
{
	// Token: 0x06003673 RID: 13939 RVA: 0x0018BA80 File Offset: 0x00189C80
	private void Start()
	{
		this.setupTooltip();
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.clickChromosome));
		DraggableLayoutElement tDraggableLayoutElement;
		if (base.TryGetComponent<DraggableLayoutElement>(out tDraggableLayoutElement))
		{
			DraggableLayoutElement draggableLayoutElement = tDraggableLayoutElement;
			draggableLayoutElement.start_being_dragged = (Action<DraggableLayoutElement>)Delegate.Combine(draggableLayoutElement.start_being_dragged, new Action<DraggableLayoutElement>(this.onStartDrag));
		}
	}

	// Token: 0x06003674 RID: 13940 RVA: 0x0018BADC File Offset: 0x00189CDC
	protected virtual void onStartDrag(DraggableLayoutElement pOriginalElement)
	{
		ChromosomeElement tOriginalButton = pOriginalElement.GetComponent<ChromosomeElement>();
		this.show(tOriginalButton.chromosome, null);
	}

	// Token: 0x06003675 RID: 13941 RVA: 0x0018BAFD File Offset: 0x00189CFD
	private void clickChromosome()
	{
		ChromosomeClickEvent click_event = this._click_event;
		if (click_event == null)
		{
			return;
		}
		click_event(this.chromosome);
	}

	// Token: 0x06003676 RID: 13942 RVA: 0x0018BB18 File Offset: 0x00189D18
	public void show(Chromosome pChromosome, ChromosomeClickEvent pClickEvent)
	{
		this.chromosome = pChromosome;
		this._click_event = pClickEvent;
		if (pChromosome.isAllLociSynergy())
		{
			this.image.sprite = this.chromosome.getSpriteGolden();
			return;
		}
		this.image.sprite = this.chromosome.getSpriteNormal();
	}

	// Token: 0x06003677 RID: 13943 RVA: 0x0018BB68 File Offset: 0x00189D68
	protected virtual void setupTooltip()
	{
		TipButton tTipButton;
		if (!base.TryGetComponent<TipButton>(out tTipButton))
		{
			return;
		}
		tTipButton.setHoverAction(new TooltipAction(this.tooltipAction), true);
	}

	// Token: 0x06003678 RID: 13944 RVA: 0x0018BB93 File Offset: 0x00189D93
	protected void tooltipAction()
	{
		Tooltip.show(this, "chromosome", new TooltipData
		{
			chromosome = this.chromosome
		});
	}

	// Token: 0x0400285E RID: 10334
	private static readonly Color color_synergy_gold = Toolbox.makeColor("#FFF841");

	// Token: 0x0400285F RID: 10335
	private static readonly Color color_normal_blue = Toolbox.makeColor("#00B0FF");

	// Token: 0x04002860 RID: 10336
	internal Chromosome chromosome;

	// Token: 0x04002861 RID: 10337
	private ChromosomeClickEvent _click_event;

	// Token: 0x04002862 RID: 10338
	public Image image;
}
