using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200069A RID: 1690
public class PrefabUnitElement : WindowListElementBaseActor, IPointerMoveHandler, IEventSystemHandler
{
	// Token: 0x0600361B RID: 13851 RVA: 0x0018AB72 File Offset: 0x00188D72
	private void Awake()
	{
		this.initTooltip();
	}

	// Token: 0x0600361C RID: 13852 RVA: 0x0018AB7C File Offset: 0x00188D7C
	internal void show(Actor pActor)
	{
		this._actor = pActor;
		this.unitName.text = pActor.coloredName;
		this.avatarElement.show(pActor);
		this.health_bar.setBar((float)pActor.getHealth(), (float)pActor.getMaxHealth(), "", true, false, true, 0.3f);
		this.text_level.setValue(pActor.level, "");
		this.text_kills.setValue(pActor.data.kills, "");
		this.text_age.setValue(pActor.getAge(), "");
		if (pActor.asset.inspect_sex)
		{
			this.icon_sex.gameObject.SetActive(true);
			if (pActor.isSexMale())
			{
				this.icon_sex.sprite = SpriteTextureLoader.getSprite("ui/icons/IconMale");
			}
			else
			{
				this.icon_sex.sprite = SpriteTextureLoader.getSprite("ui/icons/IconFemale");
			}
		}
		else
		{
			this.icon_sex.gameObject.SetActive(false);
		}
		this._icon_species.sprite = this._actor.asset.getSpriteIcon();
		this.toggleFavorited(this._actor.isFavorite());
	}

	// Token: 0x0600361D RID: 13853 RVA: 0x0018ACAB File Offset: 0x00188EAB
	public void clickLocate()
	{
		WorldLog.locationFollow(this._actor);
	}

	// Token: 0x0600361E RID: 13854 RVA: 0x0018ACB8 File Offset: 0x00188EB8
	public void clickInspect()
	{
		if (!InputHelpers.mouseSupported && !Tooltip.isShowingFor(this))
		{
			this.tooltipAction();
			return;
		}
		ActionLibrary.openUnitWindow(this._actor);
	}

	// Token: 0x0600361F RID: 13855 RVA: 0x0018ACDB File Offset: 0x00188EDB
	public Actor getActor()
	{
		return this._actor;
	}

	// Token: 0x06003620 RID: 13856 RVA: 0x0018ACE3 File Offset: 0x00188EE3
	public void toggleFavorited(bool pState)
	{
		this._icon_favorite.SetActive(pState);
	}

	// Token: 0x06003621 RID: 13857 RVA: 0x0018ACF1 File Offset: 0x00188EF1
	private void OnDisable()
	{
		this._actor = null;
	}

	// Token: 0x06003622 RID: 13858 RVA: 0x0018ACFA File Offset: 0x00188EFA
	public void OnPointerMove(PointerEventData pData)
	{
		if (!InputHelpers.mouseSupported)
		{
			return;
		}
		if (Tooltip.anyActive())
		{
			return;
		}
		this.tooltipAction();
	}

	// Token: 0x06003623 RID: 13859 RVA: 0x0018AD12 File Offset: 0x00188F12
	private void tooltipAction()
	{
		this._actor.showTooltip(this);
	}

	// Token: 0x06003624 RID: 13860 RVA: 0x0018AD20 File Offset: 0x00188F20
	private void initTooltip()
	{
		base.GetComponent<Button>().OnHoverOut(delegate()
		{
			Tooltip.hideTooltip();
		});
	}

	// Token: 0x04002823 RID: 10275
	private Actor _actor;

	// Token: 0x04002824 RID: 10276
	public Text unitName;

	// Token: 0x04002825 RID: 10277
	public UiUnitAvatarElement avatarElement;

	// Token: 0x04002826 RID: 10278
	public StatBar health_bar;

	// Token: 0x04002827 RID: 10279
	public CountUpOnClick text_damage;

	// Token: 0x04002828 RID: 10280
	public CountUpOnClick text_level;

	// Token: 0x04002829 RID: 10281
	public CountUpOnClick text_kills;

	// Token: 0x0400282A RID: 10282
	public CountUpOnClick text_age;

	// Token: 0x0400282B RID: 10283
	public Image icon_sex;

	// Token: 0x0400282C RID: 10284
	[SerializeField]
	private Image _icon_species;

	// Token: 0x0400282D RID: 10285
	[SerializeField]
	private GameObject _icon_favorite;
}
