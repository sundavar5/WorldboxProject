using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200084F RID: 2127
public class SpriteSwitcher : MonoBehaviour
{
	// Token: 0x06004284 RID: 17028 RVA: 0x001C2DBC File Offset: 0x001C0FBC
	private void Awake()
	{
		this._image = base.GetComponent<Image>();
		this._icon = base.transform.Find("Icon").GetComponent<Image>();
	}

	// Token: 0x06004285 RID: 17029 RVA: 0x001C2DE5 File Offset: 0x001C0FE5
	private void OnEnable()
	{
		SpriteSwitcher._all_buttons.Add(this);
		this.checkState();
	}

	// Token: 0x06004286 RID: 17030 RVA: 0x001C2DF8 File Offset: 0x001C0FF8
	private void OnDisable()
	{
		SpriteSwitcher._all_buttons.Remove(this);
	}

	// Token: 0x06004287 RID: 17031 RVA: 0x001C2E08 File Offset: 0x001C1008
	public static void checkAllStates()
	{
		foreach (SpriteSwitcher spriteSwitcher in SpriteSwitcher._all_buttons)
		{
			spriteSwitcher.checkState();
		}
	}

	// Token: 0x06004288 RID: 17032 RVA: 0x001C2E58 File Offset: 0x001C1058
	private void checkState()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		bool tHasAny = this.hasAny();
		bool? last_state = this._last_state;
		bool flag = tHasAny;
		if (!(last_state.GetValueOrDefault() == flag & last_state != null))
		{
			this._last_state = new bool?(tHasAny);
			if (tHasAny)
			{
				this.setPrimary();
				return;
			}
			this.setSecondary();
		}
	}

	// Token: 0x06004289 RID: 17033 RVA: 0x001C2EBB File Offset: 0x001C10BB
	protected virtual bool hasAny()
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600428A RID: 17034 RVA: 0x001C2EC4 File Offset: 0x001C10C4
	private void setPrimary()
	{
		this._image.sprite = this.primary_sprite;
		Color tColor = this._icon.color;
		tColor.a = 1f;
		this._icon.color = tColor;
	}

	// Token: 0x0600428B RID: 17035 RVA: 0x001C2F08 File Offset: 0x001C1108
	private void setSecondary()
	{
		this._image.sprite = this.secondary_sprite;
		Color tColor = this._icon.color;
		tColor.a = 0.9f;
		this._icon.color = tColor;
	}

	// Token: 0x040030B9 RID: 12473
	public Sprite primary_sprite;

	// Token: 0x040030BA RID: 12474
	public Sprite secondary_sprite;

	// Token: 0x040030BB RID: 12475
	private Image _image;

	// Token: 0x040030BC RID: 12476
	private Image _icon;

	// Token: 0x040030BD RID: 12477
	private bool? _last_state;

	// Token: 0x040030BE RID: 12478
	private static List<SpriteSwitcher> _all_buttons = new List<SpriteSwitcher>();
}
