using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020007EF RID: 2031
public class TabToggle : MonoBehaviour
{
	// Token: 0x06003FCD RID: 16333 RVA: 0x001B636A File Offset: 0x001B456A
	private void Awake()
	{
		if (TabToggle._tab_toggle_on == null)
		{
			TabToggle._tab_toggle_on = SpriteTextureLoader.getSprite("ui/tab_button_sort_selected");
			TabToggle._tab_toggle_off = SpriteTextureLoader.getSprite("ui/tab_button_sort");
		}
		this.unselect();
	}

	// Token: 0x06003FCE RID: 16334 RVA: 0x001B639D File Offset: 0x001B459D
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.click));
	}

	// Token: 0x06003FCF RID: 16335 RVA: 0x001B63BB File Offset: 0x001B45BB
	public TabToggleState getState()
	{
		return this._state;
	}

	// Token: 0x06003FD0 RID: 16336 RVA: 0x001B63C3 File Offset: 0x001B45C3
	private void setState(TabToggleState pState)
	{
		this._state = pState;
	}

	// Token: 0x06003FD1 RID: 16337 RVA: 0x001B63CC File Offset: 0x001B45CC
	public void click()
	{
		if (this._state == TabToggleState.Selected)
		{
			return;
		}
		TabToggleClearAction tabToggleClearAction = this.select_action;
		if (tabToggleClearAction != null)
		{
			tabToggleClearAction(this);
		}
		this.select();
		TabToggleAction tabToggleAction = this.action;
		if (tabToggleAction != null)
		{
			tabToggleAction();
		}
		TabToggleAction tabToggleAction2 = this.post_action;
		if (tabToggleAction2 == null)
		{
			return;
		}
		tabToggleAction2();
	}

	// Token: 0x06003FD2 RID: 16338 RVA: 0x001B641C File Offset: 0x001B461C
	public void select()
	{
		this.setState(TabToggleState.Selected);
		this.background.sprite = TabToggle._tab_toggle_on;
		this.icon.color = Color.white;
	}

	// Token: 0x06003FD3 RID: 16339 RVA: 0x001B6448 File Offset: 0x001B4648
	public void unselect()
	{
		this.setState(TabToggleState.None);
		this.background.sprite = TabToggle._tab_toggle_off;
		Color tColor = Color.white;
		tColor.a = 0.5f;
		this.icon.color = tColor;
	}

	// Token: 0x04002E43 RID: 11843
	public Image icon;

	// Token: 0x04002E44 RID: 11844
	public Image background;

	// Token: 0x04002E45 RID: 11845
	private TabToggleState _state;

	// Token: 0x04002E46 RID: 11846
	public TabToggleAction action;

	// Token: 0x04002E47 RID: 11847
	public TabToggleAction post_action;

	// Token: 0x04002E48 RID: 11848
	public TabToggleClearAction select_action;

	// Token: 0x04002E49 RID: 11849
	protected static Sprite _tab_toggle_on;

	// Token: 0x04002E4A RID: 11850
	protected static Sprite _tab_toggle_off;
}
