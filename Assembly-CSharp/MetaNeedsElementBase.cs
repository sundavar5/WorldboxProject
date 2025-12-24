using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020007DE RID: 2014
public class MetaNeedsElementBase : WindowMetaElementBase, IRefreshElement
{
	// Token: 0x06003F6A RID: 16234 RVA: 0x001B5493 File Offset: 0x001B3693
	protected override void Awake()
	{
		base.Awake();
		this.setupTooltip();
		this.setupClickAction();
		this._window = base.GetComponentInParent<IMetaWindow>();
	}

	// Token: 0x06003F6B RID: 16235 RVA: 0x001B54B3 File Offset: 0x001B36B3
	protected override IEnumerator showContent()
	{
		IMetaObject tMeta = this._window.getCoreObject() as IMetaObject;
		if (tMeta == null)
		{
			yield break;
		}
		if (!tMeta.isAlive())
		{
			yield break;
		}
		Actor tActorResult;
		string tFinalText = this.getText(tMeta, out tActorResult);
		this._text.text = tFinalText;
		this._actor = tActorResult;
		if (string.IsNullOrEmpty(tFinalText))
		{
			yield break;
		}
		this._container.gameObject.SetActive(true);
		yield break;
	}

	// Token: 0x06003F6C RID: 16236 RVA: 0x001B54C2 File Offset: 0x001B36C2
	protected virtual string getText(IMetaObject pMeta, out Actor pActorResult)
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003F6D RID: 16237 RVA: 0x001B54CC File Offset: 0x001B36CC
	private void setupTooltip()
	{
		TipButton tTipButton;
		if (!this._container.TryGetComponent<TipButton>(out tTipButton))
		{
			return;
		}
		tTipButton.hoverAction = new TooltipAction(this.tooltipAction);
	}

	// Token: 0x06003F6E RID: 16238 RVA: 0x001B54FC File Offset: 0x001B36FC
	private void setupClickAction()
	{
		Button tButton;
		if (!this._container.TryGetComponent<Button>(out tButton))
		{
			return;
		}
		tButton.onClick.AddListener(new UnityAction(this.buttonAction));
	}

	// Token: 0x06003F6F RID: 16239 RVA: 0x001B5530 File Offset: 0x001B3730
	private void tooltipAction()
	{
		if (this._actor.isRekt())
		{
			return;
		}
		this._actor.showTooltip(this);
	}

	// Token: 0x06003F70 RID: 16240 RVA: 0x001B554C File Offset: 0x001B374C
	private void buttonAction()
	{
		if (this._actor.isRekt())
		{
			return;
		}
		ActionLibrary.openUnitWindow(this._actor);
	}

	// Token: 0x06003F71 RID: 16241 RVA: 0x001B5567 File Offset: 0x001B3767
	protected override void clear()
	{
		base.clear();
		this._container.gameObject.SetActive(false);
	}

	// Token: 0x04002E0D RID: 11789
	[SerializeField]
	private GameObject _container;

	// Token: 0x04002E0E RID: 11790
	[SerializeField]
	private Text _text;

	// Token: 0x04002E0F RID: 11791
	private Actor _actor;

	// Token: 0x04002E10 RID: 11792
	private IMetaWindow _window;
}
