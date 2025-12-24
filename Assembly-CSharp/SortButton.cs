using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020007E8 RID: 2024
public class SortButton : MonoBehaviour
{
	// Token: 0x06003FAA RID: 16298 RVA: 0x001B5D80 File Offset: 0x001B3F80
	private void Awake()
	{
		if (SortButton._tab_button_on == null)
		{
			SortButton._tab_button_on = SpriteTextureLoader.getSprite("ui/tab_button_sort_selected");
			SortButton._tab_button_off = SpriteTextureLoader.getSprite("ui/tab_button_sort");
		}
		this.arrow_sprite.gameObject.SetActive(false);
		this.setState(SortButtonState.None);
		this.background.sprite = SortButton._tab_button_off;
	}

	// Token: 0x06003FAB RID: 16299 RVA: 0x001B5DE0 File Offset: 0x001B3FE0
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.click));
	}

	// Token: 0x06003FAC RID: 16300 RVA: 0x001B5DFE File Offset: 0x001B3FFE
	public SortButtonState getState()
	{
		return this._state;
	}

	// Token: 0x06003FAD RID: 16301 RVA: 0x001B5E06 File Offset: 0x001B4006
	private void setState(SortButtonState pState)
	{
		this._state = pState;
	}

	// Token: 0x06003FAE RID: 16302 RVA: 0x001B5E10 File Offset: 0x001B4010
	internal void turnOff()
	{
		this.setState(SortButtonState.None);
		this.arrow_sprite.gameObject.SetActive(false);
		this.background.sprite = SortButton._tab_button_off;
		Color tColor = Color.white;
		tColor.a = 0.5f;
		this.icon.color = tColor;
		base.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(27f, 37f);
	}

	// Token: 0x06003FAF RID: 16303 RVA: 0x001B5E88 File Offset: 0x001B4088
	public void click()
	{
		SortButtonClearAction sortButtonClearAction = this.select_action;
		if (sortButtonClearAction != null)
		{
			sortButtonClearAction(this);
		}
		switch (this._state)
		{
		case SortButtonState.None:
			this.setSortUP();
			break;
		case SortButtonState.Up:
			this.setSortDOWN();
			break;
		case SortButtonState.Down:
			this.setSortUP();
			break;
		}
		SortButtonAction sortButtonAction = this.action;
		if (sortButtonAction != null)
		{
			sortButtonAction();
		}
		SortButtonAction sortButtonAction2 = this.post_action;
		if (sortButtonAction2 == null)
		{
			return;
		}
		sortButtonAction2();
	}

	// Token: 0x06003FB0 RID: 16304 RVA: 0x001B5EF9 File Offset: 0x001B40F9
	public void callAction()
	{
	}

	// Token: 0x06003FB1 RID: 16305 RVA: 0x001B5EFC File Offset: 0x001B40FC
	public void setSortUP()
	{
		this.setState(SortButtonState.Up);
		this.arrow_sprite.gameObject.SetActive(true);
		this.arrow_sprite.sprite = SpriteTextureLoader.getSprite("ui/Icons/iconArrowUP");
		this.background.sprite = SortButton._tab_button_on;
		this.icon.color = Color.white;
		base.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(33f, 37f);
	}

	// Token: 0x06003FB2 RID: 16306 RVA: 0x001B5F7C File Offset: 0x001B417C
	public void setSortDOWN()
	{
		this.setState(SortButtonState.Down);
		this.arrow_sprite.gameObject.SetActive(true);
		this.arrow_sprite.sprite = SpriteTextureLoader.getSprite("ui/Icons/iconArrowDOWN");
		this.background.sprite = SortButton._tab_button_on;
		this.icon.color = Color.white;
		base.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(33f, 37f);
	}

	// Token: 0x04002E2C RID: 11820
	public Image arrow_sprite;

	// Token: 0x04002E2D RID: 11821
	public Image icon;

	// Token: 0x04002E2E RID: 11822
	public Image background;

	// Token: 0x04002E2F RID: 11823
	private SortButtonState _state;

	// Token: 0x04002E30 RID: 11824
	public SortButtonAction action;

	// Token: 0x04002E31 RID: 11825
	public SortButtonAction post_action;

	// Token: 0x04002E32 RID: 11826
	public SortButtonClearAction select_action;

	// Token: 0x04002E33 RID: 11827
	protected static Sprite _tab_button_on;

	// Token: 0x04002E34 RID: 11828
	protected static Sprite _tab_button_off;
}
