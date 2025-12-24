using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007FF RID: 2047
public class WindowMetaTab : MonoBehaviour
{
	// Token: 0x0600403A RID: 16442 RVA: 0x001B7A00 File Offset: 0x001B5C00
	private void Awake()
	{
		base.GetComponent<Button>().onClick.AddListener(delegate()
		{
			this.doAction();
		});
		this._tip_button = base.GetComponent<TipButton>();
		this._worldtip_text = this.getWorldTipText();
		this._tip_button.setHoverAction(new TooltipAction(this.checkShowTooltip), true);
	}

	// Token: 0x0600403B RID: 16443 RVA: 0x001B7A59 File Offset: 0x001B5C59
	public void doAction()
	{
		this.tab_action.Invoke(this);
		this.checkShowWorldTip();
	}

	// Token: 0x0600403C RID: 16444 RVA: 0x001B7A6D File Offset: 0x001B5C6D
	public void checkShowWorldTip()
	{
		if (this._tip_button == null)
		{
			return;
		}
		if (InputHelpers.mouseSupported)
		{
			return;
		}
		WorldTip.showNowTop(this._worldtip_text, false);
	}

	// Token: 0x0600403D RID: 16445 RVA: 0x001B7A94 File Offset: 0x001B5C94
	private void checkShowTooltip()
	{
		if (!InputHelpers.mouseSupported)
		{
			return;
		}
		Tooltip.show(this, "tip", new TooltipData
		{
			tip_name = this._tip_button.textOnClick,
			tip_description = this._tip_button.textOnClickDescription,
			tip_description_2 = this._tip_button.text_description_2
		});
	}

	// Token: 0x0600403E RID: 16446 RVA: 0x001B7AEC File Offset: 0x001B5CEC
	private void OnDestroy()
	{
		this.destroyed = true;
		if (!base.gameObject.HasComponent<PlatformRemover>())
		{
			return;
		}
		this.container.removeTab(this);
	}

	// Token: 0x0600403F RID: 16447 RVA: 0x001B7B0F File Offset: 0x001B5D0F
	public bool getState()
	{
		return this._state;
	}

	// Token: 0x06004040 RID: 16448 RVA: 0x001B7B18 File Offset: 0x001B5D18
	public void toggleActive(bool pState)
	{
		this._state = pState;
		if (this._state)
		{
			this._canvas_group.alpha = 1f;
		}
		else
		{
			this._canvas_group.alpha = 0f;
		}
		this._canvas_group.interactable = this._state;
		this._canvas_group.blocksRaycasts = this._state;
	}

	// Token: 0x06004041 RID: 16449 RVA: 0x001B7B78 File Offset: 0x001B5D78
	public string getWorldTipText()
	{
		string tText = LocalizedTextManager.getText(this._tip_button.textOnClick, null, false);
		if (!string.IsNullOrEmpty(this._tip_button.textOnClickDescription))
		{
			tText = tText + "\n<size=9>" + LocalizedTextManager.getText(this._tip_button.textOnClickDescription, null, false) + "</size>";
		}
		return tText;
	}

	// Token: 0x04002E8E RID: 11918
	[SerializeField]
	private CanvasGroup _canvas_group;

	// Token: 0x04002E8F RID: 11919
	public List<Transform> tab_elements = new List<Transform>();

	// Token: 0x04002E90 RID: 11920
	public WindowMetaTabEvent tab_action;

	// Token: 0x04002E91 RID: 11921
	internal WindowMetaTabButtonsContainer container;

	// Token: 0x04002E92 RID: 11922
	internal bool destroyed;

	// Token: 0x04002E93 RID: 11923
	private TipButton _tip_button;

	// Token: 0x04002E94 RID: 11924
	private string _worldtip_text;

	// Token: 0x04002E95 RID: 11925
	private bool _state = true;
}
