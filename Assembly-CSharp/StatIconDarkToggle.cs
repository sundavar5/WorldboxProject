using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020005FF RID: 1535
public class StatIconDarkToggle : MonoBehaviour
{
	// Token: 0x06003268 RID: 12904 RVA: 0x0017EC04 File Offset: 0x0017CE04
	private void changeColor()
	{
		if (this._background == null)
		{
			return;
		}
		this._switched_index++;
		if (this._switched_index >= 3)
		{
			this._switched_index = 0;
		}
		float tShade = 1f - (float)this._switched_index / 3f * 0.5f;
		Color tColor = new Color(this._original_color.r * tShade, this._original_color.g * tShade, this._original_color.b * tShade, this._original_color.a);
		this._background.color = tColor;
	}

	// Token: 0x06003269 RID: 12905 RVA: 0x0017ECA0 File Offset: 0x0017CEA0
	private void Awake()
	{
		base.gameObject.AddOrGetComponent<Button>().onClick.AddListener(new UnityAction(this.click));
		this._background = base.GetComponent<Image>();
		if (this._background != null)
		{
			this._original_color = this._background.color;
			return;
		}
		this._original_color = Color.white;
	}

	// Token: 0x0600326A RID: 12906 RVA: 0x0017ED05 File Offset: 0x0017CF05
	private void click()
	{
		this.changeColor();
	}

	// Token: 0x0400261B RID: 9755
	private Color _original_color;

	// Token: 0x0400261C RID: 9756
	private Image _background;

	// Token: 0x0400261D RID: 9757
	private const int INDEX_MAX = 3;

	// Token: 0x0400261E RID: 9758
	private const float SHADE_FACTOR = 0.5f;

	// Token: 0x0400261F RID: 9759
	private int _switched_index;
}
