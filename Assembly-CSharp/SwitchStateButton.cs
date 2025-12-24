using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000840 RID: 2112
public class SwitchStateButton : MonoBehaviour
{
	// Token: 0x06004233 RID: 16947 RVA: 0x001C089D File Offset: 0x001BEA9D
	private void Awake()
	{
		this._power_button = base.GetComponent<PowerButton>();
	}

	// Token: 0x06004234 RID: 16948 RVA: 0x001C08AC File Offset: 0x001BEAAC
	public void setState(bool pState)
	{
		this._state = pState;
		if (this._state)
		{
			this._background.sprite = this._sprite_enabled;
			this._icon.color = Color.white;
		}
		else
		{
			this._background.sprite = this._sprite_disabled;
			this._icon.color = Toolbox.color_grey_dark;
		}
		this._button.enabled = this._state;
		if (this._power_button != null)
		{
			this._power_button.is_selectable = this._state;
		}
	}

	// Token: 0x0400304E RID: 12366
	[SerializeField]
	private Image _icon;

	// Token: 0x0400304F RID: 12367
	[SerializeField]
	private Image _background;

	// Token: 0x04003050 RID: 12368
	[SerializeField]
	private Button _button;

	// Token: 0x04003051 RID: 12369
	[SerializeField]
	private Sprite _sprite_enabled;

	// Token: 0x04003052 RID: 12370
	[SerializeField]
	private Sprite _sprite_disabled;

	// Token: 0x04003053 RID: 12371
	private bool _state = true;

	// Token: 0x04003054 RID: 12372
	private PowerButton _power_button;
}
