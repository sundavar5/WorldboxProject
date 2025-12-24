using System;
using UnityEngine;

// Token: 0x02000757 RID: 1879
public interface ISelectedMetaWithUnit
{
	// Token: 0x1700036C RID: 876
	// (get) Token: 0x06003B6F RID: 15215
	SelectedMetaUnitElement unit_element { get; }

	// Token: 0x1700036D RID: 877
	// (get) Token: 0x06003B70 RID: 15216
	GameObject unit_element_separator { get; }

	// Token: 0x1700036E RID: 878
	// (get) Token: 0x06003B71 RID: 15217
	string unit_title_locale_key { get; }

	// Token: 0x1700036F RID: 879
	// (get) Token: 0x06003B72 RID: 15218
	// (set) Token: 0x06003B73 RID: 15219
	int last_dirty_stats_unit { get; set; }

	// Token: 0x17000370 RID: 880
	// (get) Token: 0x06003B74 RID: 15220
	// (set) Token: 0x06003B75 RID: 15221
	Actor last_unit { get; set; }

	// Token: 0x06003B76 RID: 15222 RVA: 0x001A0C60 File Offset: 0x0019EE60
	bool checkUnitElement()
	{
		if (!this.hasUnit())
		{
			this.setUnitElementVisible(false);
			return true;
		}
		this.setUnitElementVisible(true);
		Actor tActor = this.getUnit();
		UiUnitAvatarElement tAvatar = this.unit_element.getAvatar();
		if (this.unitChanged(tActor) || tAvatar.avatarLoader.actorStateChanged())
		{
			this.unit_element.show(tActor, this.unit_title_locale_key);
			this.last_dirty_stats_unit = tActor.getStatsDirtyVersion();
			this.last_unit = tActor;
			return true;
		}
		tAvatar.updateTileSprite();
		return false;
	}

	// Token: 0x06003B77 RID: 15223 RVA: 0x001A0CDC File Offset: 0x0019EEDC
	void setUnitElementVisible(bool pState)
	{
		this.unit_element.gameObject.SetActive(pState);
		this.unit_element_separator.SetActive(pState);
	}

	// Token: 0x06003B78 RID: 15224 RVA: 0x001A0CFB File Offset: 0x0019EEFB
	void avatarTouch()
	{
		if (!this.hasUnit())
		{
			return;
		}
		Actor unit = this.getUnit();
		SelectedUnit.select(unit, true);
		SelectedObjects.setNanoObject(unit);
		PowerTabController.showTabSelectedUnit();
		((IShakable)ToolbarButtons.instance).shake();
	}

	// Token: 0x06003B79 RID: 15225
	bool hasUnit();

	// Token: 0x06003B7A RID: 15226
	Actor getUnit();

	// Token: 0x06003B7B RID: 15227 RVA: 0x001A0D28 File Offset: 0x0019EF28
	bool unitChanged(Actor pActor)
	{
		return pActor.getStatsDirtyVersion() != this.last_dirty_stats_unit || pActor != this.last_unit;
	}

	// Token: 0x06003B7C RID: 15228 RVA: 0x001A0D46 File Offset: 0x0019EF46
	void clearLastUnit()
	{
		this.last_unit = null;
		this.last_dirty_stats_unit = -1;
	}
}
