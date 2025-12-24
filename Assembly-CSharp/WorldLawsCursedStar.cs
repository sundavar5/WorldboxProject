using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000818 RID: 2072
public class WorldLawsCursedStar : MonoBehaviour
{
	// Token: 0x060040E8 RID: 16616 RVA: 0x001BB600 File Offset: 0x001B9800
	public void setStarsTransparency(float pValue)
	{
		float tEmpty = 1f - pValue;
		Color tColor = this._empty_star.color;
		tColor.a = tEmpty;
		this._empty_star.color = tColor;
		tColor.a = pValue;
		this._filled_star.color = tColor;
	}

	// Token: 0x060040E9 RID: 16617 RVA: 0x001BB64C File Offset: 0x001B984C
	public void setColorMultiplyAlphaBoth(Color pColor, float pValue)
	{
		if (pValue < 0f)
		{
			pValue = 0f;
		}
		pColor.a = this._empty_star.color.a * pValue;
		this._empty_star.color = pColor;
		pColor.a = this._filled_star.color.a * pValue;
		this._filled_star.color = pColor;
	}

	// Token: 0x060040EA RID: 16618 RVA: 0x001BB6B2 File Offset: 0x001B98B2
	public void toggleEgg(bool pState)
	{
		if (pState)
		{
			this._filled_star.sprite = this._egg_sprite;
			return;
		}
		this._filled_star.sprite = this._filled_star_sprite;
	}

	// Token: 0x060040EB RID: 16619 RVA: 0x001BB6DA File Offset: 0x001B98DA
	public void toggleFilled(bool pState)
	{
		this._filled = pState;
	}

	// Token: 0x060040EC RID: 16620 RVA: 0x001BB6E3 File Offset: 0x001B98E3
	public bool isFilled()
	{
		return this._filled;
	}

	// Token: 0x04002F11 RID: 12049
	[SerializeField]
	private Image _empty_star;

	// Token: 0x04002F12 RID: 12050
	[SerializeField]
	private Image _filled_star;

	// Token: 0x04002F13 RID: 12051
	[SerializeField]
	private Sprite _filled_star_sprite;

	// Token: 0x04002F14 RID: 12052
	[SerializeField]
	private Sprite _egg_sprite;

	// Token: 0x04002F15 RID: 12053
	private bool _filled;
}
