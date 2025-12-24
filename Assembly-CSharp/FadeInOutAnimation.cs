using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007A7 RID: 1959
public class FadeInOutAnimation : MonoBehaviour
{
	// Token: 0x06003DFF RID: 15871 RVA: 0x001B07E3 File Offset: 0x001AE9E3
	public void Awake()
	{
		this.checkInit();
	}

	// Token: 0x06003E00 RID: 15872 RVA: 0x001B07EB File Offset: 0x001AE9EB
	public void checkInit()
	{
		this._image = base.GetComponent<Image>();
	}

	// Token: 0x06003E01 RID: 15873 RVA: 0x001B07FC File Offset: 0x001AE9FC
	private void updateAlpha()
	{
		this._timer -= Time.deltaTime;
		if (this._timer < 0f)
		{
			this._timer = 0.025f;
			if (this._fade_out)
			{
				this._current_alpha -= 0.015f;
				if (this._current_alpha <= 0.1f)
				{
					this._current_alpha = 0.1f;
					this._fade_out = false;
				}
			}
			else
			{
				this._current_alpha += 0.015f;
				if (this._current_alpha >= this.alpha_max)
				{
					this._current_alpha = this.alpha_max;
					this._fade_out = true;
				}
			}
			Color tColor = this._image.color;
			tColor.a = this._current_alpha;
			this._image.color = tColor;
		}
	}

	// Token: 0x06003E02 RID: 15874 RVA: 0x001B08C9 File Offset: 0x001AEAC9
	public void resetToFadeOut()
	{
		this._current_alpha = 1f;
		this._fade_out = true;
		this.updateAlpha();
	}

	// Token: 0x06003E03 RID: 15875 RVA: 0x001B08E3 File Offset: 0x001AEAE3
	public void resetToFadeIn()
	{
		this._current_alpha = 0f;
		this._fade_out = false;
		this.updateAlpha();
	}

	// Token: 0x06003E04 RID: 15876 RVA: 0x001B08FD File Offset: 0x001AEAFD
	public void reset()
	{
		this.resetToFadeOut();
	}

	// Token: 0x06003E05 RID: 15877 RVA: 0x001B0905 File Offset: 0x001AEB05
	private void OnEnable()
	{
		this.reset();
	}

	// Token: 0x06003E06 RID: 15878 RVA: 0x001B090D File Offset: 0x001AEB0D
	private void Update()
	{
		this.updateAlpha();
	}

	// Token: 0x04002D02 RID: 11522
	private const float FADE_OUT_BOUND = 0.1f;

	// Token: 0x04002D03 RID: 11523
	private const float FADE_SPEED = 0.015f;

	// Token: 0x04002D04 RID: 11524
	private const float INTERVAL = 0.025f;

	// Token: 0x04002D05 RID: 11525
	public float alpha_max = 1f;

	// Token: 0x04002D06 RID: 11526
	private float _current_alpha;

	// Token: 0x04002D07 RID: 11527
	private float _timer = 0.025f;

	// Token: 0x04002D08 RID: 11528
	private bool _fade_out = true;

	// Token: 0x04002D09 RID: 11529
	[SerializeField]
	private Image _image;
}
