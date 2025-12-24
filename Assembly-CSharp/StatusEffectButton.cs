using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020006CD RID: 1741
public class StatusEffectButton : MonoBehaviour
{
	// Token: 0x17000319 RID: 793
	// (get) Token: 0x060037CE RID: 14286 RVA: 0x00191C7D File Offset: 0x0018FE7D
	public Status status
	{
		get
		{
			return this._status;
		}
	}

	// Token: 0x060037CF RID: 14287 RVA: 0x00191C88 File Offset: 0x0018FE88
	private void Awake()
	{
		this.button = base.GetComponent<Button>();
		this.image = base.transform.Find("icon").GetComponent<Image>();
		DraggableLayoutElement tDraggableLayoutElement;
		if (base.TryGetComponent<DraggableLayoutElement>(out tDraggableLayoutElement))
		{
			DraggableLayoutElement draggableLayoutElement = tDraggableLayoutElement;
			draggableLayoutElement.start_being_dragged = (Action<DraggableLayoutElement>)Delegate.Combine(draggableLayoutElement.start_being_dragged, new Action<DraggableLayoutElement>(this.onStartDrag));
		}
	}

	// Token: 0x060037D0 RID: 14288 RVA: 0x00191CEC File Offset: 0x0018FEEC
	private void Start()
	{
		this.button.onClick.AddListener(new UnityAction(this.showTooltip));
		this.button.OnHover(new UnityAction(this.showHoverTooltip));
		this.button.OnHoverOut(new UnityAction(Tooltip.hideTooltip));
	}

	// Token: 0x060037D1 RID: 14289 RVA: 0x00191D43 File Offset: 0x0018FF43
	internal void load(Status pData)
	{
		if (pData == null)
		{
			return;
		}
		this._status = pData;
		this.image.sprite = pData.asset.getSprite();
	}

	// Token: 0x060037D2 RID: 14290 RVA: 0x00191D68 File Offset: 0x0018FF68
	protected virtual void onStartDrag(DraggableLayoutElement pOriginalElement)
	{
		StatusEffectButton tOriginalButton = pOriginalElement.GetComponent<StatusEffectButton>();
		this.load(tOriginalButton._status);
	}

	// Token: 0x060037D3 RID: 14291 RVA: 0x00191D88 File Offset: 0x0018FF88
	private void OnDisable()
	{
		Tooltip.hideTooltip();
	}

	// Token: 0x060037D4 RID: 14292 RVA: 0x00191D8F File Offset: 0x0018FF8F
	private void showHoverTooltip()
	{
		if (!Config.tooltips_active)
		{
			return;
		}
		this.showTooltip();
	}

	// Token: 0x060037D5 RID: 14293 RVA: 0x00191DA0 File Offset: 0x0018FFA0
	private void showTooltip()
	{
		if (!this.tooltip_enabled)
		{
			return;
		}
		string tTooltipType = this._updatable_tooltip ? "status_updatable" : "status";
		string tTipTitle = this._status.asset.getLocaleID();
		string tTipDescription = this._status.asset.getDescriptionID();
		Tooltip.show(this, tTooltipType, new TooltipData
		{
			tip_name = tTipTitle,
			tip_description = tTipDescription,
			status = this._status
		});
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		base.transform.DOKill(false);
		base.transform.DOScale(0.8f, 0.1f).SetEase(Ease.InBack);
	}

	// Token: 0x060037D6 RID: 14294 RVA: 0x00191E5C File Offset: 0x0019005C
	public void setUpdatableTooltip(bool pState)
	{
		this._updatable_tooltip = pState;
	}

	// Token: 0x060037D7 RID: 14295 RVA: 0x00191E65 File Offset: 0x00190065
	private void OnDestroy()
	{
		base.transform.DOKill(false);
	}

	// Token: 0x0400295D RID: 10589
	private Status _status;

	// Token: 0x0400295E RID: 10590
	internal Image image;

	// Token: 0x0400295F RID: 10591
	internal bool tooltip_enabled = true;

	// Token: 0x04002960 RID: 10592
	internal Button button;

	// Token: 0x04002961 RID: 10593
	private bool _updatable_tooltip;
}
