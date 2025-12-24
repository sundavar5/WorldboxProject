using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200075F RID: 1887
public class SpectateUnit : MonoBehaviour
{
	// Token: 0x06003BC0 RID: 15296 RVA: 0x001A1630 File Offset: 0x0019F830
	public void updateStats()
	{
		this.unitName.text = this._actor.coloredName;
		this.text_age.text = Toolbox.formatNumber((long)this._actor.getAge());
		this.text_kills.text = Toolbox.formatNumber((long)this._actor.data.kills);
		this.health_bar.setBar((float)this._actor.getHealth(), (float)this._actor.getMaxHealth(), "/" + this._actor.getMaxHealth().ToText(4), false, false, true, 0.3f);
		if (this._actor.hasKingdom() && this._actor.isKingdomCiv())
		{
			this.kingdomBannerR.gameObject.SetActive(true);
			this.kingdomBannerL.gameObject.SetActive(true);
			this.kingdomBannerR.load(this._actor.kingdom);
			this.kingdomBannerL.load(this._actor.kingdom);
			return;
		}
		this.kingdomBannerR.gameObject.SetActive(false);
		this.kingdomBannerL.gameObject.SetActive(false);
	}

	// Token: 0x06003BC1 RID: 15297 RVA: 0x001A1761 File Offset: 0x0019F961
	public void clickKingdomElement()
	{
		if (Input.touchCount > 1)
		{
			return;
		}
		SelectedMetas.selected_kingdom = this._actor.kingdom;
		ScrollWindow.showWindow("kingdom");
	}

	// Token: 0x06003BC2 RID: 15298 RVA: 0x001A1786 File Offset: 0x0019F986
	public void clickLocate()
	{
		if (Input.touchCount > 1)
		{
			return;
		}
		if (ScrollWindow.isAnimationActive())
		{
			return;
		}
		WorldLog.locationFollow(this._actor);
	}

	// Token: 0x06003BC3 RID: 15299 RVA: 0x001A17A4 File Offset: 0x0019F9A4
	public void clickInspect()
	{
		if (Input.touchCount > 1)
		{
			return;
		}
		if (ScrollWindow.isAnimationActive())
		{
			return;
		}
		if (this._actor == null)
		{
			return;
		}
		if (!this._actor.isAlive())
		{
			return;
		}
		ScrollWindow.moveAllToLeftAndRemove(true);
		ActionLibrary.openUnitWindow(this._actor);
	}

	// Token: 0x06003BC4 RID: 15300 RVA: 0x001A17DF File Offset: 0x0019F9DF
	private void OnEnable()
	{
	}

	// Token: 0x06003BC5 RID: 15301 RVA: 0x001A17E1 File Offset: 0x0019F9E1
	private void Update()
	{
		if (!MoveCamera.inSpectatorMode())
		{
			return;
		}
		if (!MoveCamera.isCameraFollowingUnit(this._actor))
		{
			this._actor = MoveCamera.getFocusUnit();
		}
		if (this._actor != null)
		{
			this.updateStats();
		}
	}

	// Token: 0x04002BC1 RID: 11201
	private Actor _actor;

	// Token: 0x04002BC2 RID: 11202
	public Text unitName;

	// Token: 0x04002BC3 RID: 11203
	public UnitAvatarLoader avatarLoader;

	// Token: 0x04002BC4 RID: 11204
	public KingdomBanner kingdomBannerR;

	// Token: 0x04002BC5 RID: 11205
	public KingdomBanner kingdomBannerL;

	// Token: 0x04002BC6 RID: 11206
	public Text text_age;

	// Token: 0x04002BC7 RID: 11207
	public Text text_kills;

	// Token: 0x04002BC8 RID: 11208
	public StatBar health_bar;
}
