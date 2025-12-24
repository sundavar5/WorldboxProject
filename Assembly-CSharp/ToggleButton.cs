using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020007F6 RID: 2038
public class ToggleButton : MonoBehaviour
{
	// Token: 0x06003FF6 RID: 16374 RVA: 0x001B6A20 File Offset: 0x001B4C20
	private void Awake()
	{
		if (ToggleButton._sprite_on == null)
		{
			ToggleButton._sprite_on = SpriteTextureLoader.getSprite("ui/tab_button_sort_selected");
			ToggleButton._sprite_off = SpriteTextureLoader.getSprite("ui/tab_button_sort");
		}
		this._background.sprite = ToggleButton._sprite_off;
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.click));
	}

	// Token: 0x06003FF7 RID: 16375 RVA: 0x001B6A84 File Offset: 0x001B4C84
	public void init(string pIcon, string pTooltip, ToggleButtonSelectAction pAction, ToggleButtonAction pShowAction)
	{
		PowerButton component = base.GetComponent<PowerButton>();
		component.icon.sprite = SpriteTextureLoader.getSprite(pIcon);
		component.GetComponent<TipButton>().textOnClick = pTooltip;
		this._action = pAction;
		this._post_action = pShowAction;
		base.gameObject.name = pTooltip;
	}

	// Token: 0x06003FF8 RID: 16376 RVA: 0x001B6AC3 File Offset: 0x001B4CC3
	public void click()
	{
		this.is_on = !this.is_on;
		this.checkSprite();
		ToggleButtonSelectAction action = this._action;
		if (action != null)
		{
			action(this);
		}
		ToggleButtonAction post_action = this._post_action;
		if (post_action == null)
		{
			return;
		}
		post_action();
	}

	// Token: 0x06003FF9 RID: 16377 RVA: 0x001B6AFC File Offset: 0x001B4CFC
	private void checkSprite()
	{
		this._background.sprite = (this.is_on ? ToggleButton._sprite_on : ToggleButton._sprite_off);
	}

	// Token: 0x04002E5E RID: 11870
	[SerializeField]
	private Image _background;

	// Token: 0x04002E5F RID: 11871
	private ToggleButtonSelectAction _action;

	// Token: 0x04002E60 RID: 11872
	private ToggleButtonAction _post_action;

	// Token: 0x04002E61 RID: 11873
	private static Sprite _sprite_on;

	// Token: 0x04002E62 RID: 11874
	private static Sprite _sprite_off;

	// Token: 0x04002E63 RID: 11875
	public bool is_on;
}
