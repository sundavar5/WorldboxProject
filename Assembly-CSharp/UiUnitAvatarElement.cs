using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200060E RID: 1550
public class UiUnitAvatarElement : MonoBehaviour, IBanner, IBaseMono, IRefreshElement
{
	// Token: 0x170002BC RID: 700
	// (get) Token: 0x060032C8 RID: 13000 RVA: 0x00180B2A File Offset: 0x0017ED2A
	public MetaCustomizationAsset meta_asset
	{
		get
		{
			return AssetManager.meta_customization_library.getAsset(MetaType.Unit);
		}
	}

	// Token: 0x170002BD RID: 701
	// (get) Token: 0x060032C9 RID: 13001 RVA: 0x00180B38 File Offset: 0x0017ED38
	public MetaTypeAsset meta_type_asset
	{
		get
		{
			return AssetManager.meta_type_library.getAsset(MetaType.Unit);
		}
	}

	// Token: 0x060032CA RID: 13002 RVA: 0x00180B46 File Offset: 0x0017ED46
	private void Start()
	{
		this.setupTooltip();
	}

	// Token: 0x060032CB RID: 13003 RVA: 0x00180B50 File Offset: 0x0017ED50
	public void setupTooltip()
	{
		TipButton tTipButton;
		if (!base.TryGetComponent<TipButton>(out tTipButton))
		{
			return;
		}
		tTipButton.hoverAction = delegate()
		{
			if (!InputHelpers.mouseSupported)
			{
				return;
			}
			this.tooltipActionActor();
		};
	}

	// Token: 0x060032CC RID: 13004 RVA: 0x00180B7A File Offset: 0x0017ED7A
	public void showTooltip()
	{
		this.tooltipActionActor();
	}

	// Token: 0x060032CD RID: 13005 RVA: 0x00180B82 File Offset: 0x0017ED82
	public void tooltipActionActor()
	{
		if (this._actor.isRekt())
		{
			return;
		}
		this._actor.showTooltip(this);
	}

	// Token: 0x060032CE RID: 13006 RVA: 0x00180B9E File Offset: 0x0017ED9E
	public void load(NanoObject pActor)
	{
		this.show((Actor)pActor);
	}

	// Token: 0x060032CF RID: 13007 RVA: 0x00180BAC File Offset: 0x0017EDAC
	public void show(Actor pActor)
	{
		if (pActor == null)
		{
			base.gameObject.SetActive(false);
			return;
		}
		base.gameObject.SetActive(true);
		this._actor = pActor;
		this.checkSpecialBg(pActor);
		this.avatarLoader.load(pActor);
		if (this.show_banner_kingdom)
		{
			if (pActor.isAlive() && pActor.isKingdomCiv())
			{
				this.kingdomBanner.gameObject.SetActive(true);
				this.kingdomBanner.load(pActor.kingdom);
			}
			else
			{
				this.kingdomBanner.gameObject.SetActive(false);
			}
		}
		if (this.show_banner_clan)
		{
			if (pActor.isAlive() && pActor.hasClan())
			{
				this.clanBanner.gameObject.SetActive(true);
				this.clanBanner.load(pActor.clan);
			}
			else
			{
				this.clanBanner.gameObject.SetActive(false);
			}
		}
		this.updateTileSprite();
		base.gameObject.name = "UnitAvatar_" + pActor.data.id.ToString();
	}

	// Token: 0x060032D0 RID: 13008 RVA: 0x00180CB8 File Offset: 0x0017EEB8
	public void updateTileSprite()
	{
		if (this._actor.isRekt() || this._actor.current_tile == null)
		{
			this._tile_graphics_1.gameObject.SetActive(false);
			this._tile_graphics_2.gameObject.SetActive(false);
			return;
		}
		this._tile_graphics_1.gameObject.SetActive(true);
		this._tile_graphics_2.gameObject.SetActive(true);
		if (this._actor.is_inside_boat)
		{
			this._tile_graphics_1.sprite = this._tile_inside_boat;
			this._tile_graphics_2.sprite = this._tile_inside_boat;
			return;
		}
		this._tile_graphics_1.sprite = this._actor.current_tile.Type.sprites.main.sprite;
		this._tile_graphics_2.sprite = this._actor.current_tile.Type.sprites.main.sprite;
	}

	// Token: 0x060032D1 RID: 13009 RVA: 0x00180DA8 File Offset: 0x0017EFA8
	private void checkSpecialBg(Actor pActor)
	{
		this.unit_type_bg.gameObject.SetActive(false);
		if (!pActor.isAlive())
		{
			return;
		}
		if (pActor.hasKingdom() && pActor.isKing())
		{
			this.unit_type_bg.sprite = this._type_king;
			this.unit_type_bg.gameObject.SetActive(true);
			return;
		}
		if (pActor.hasCity() && pActor.isCityLeader())
		{
			this.unit_type_bg.sprite = this._type_leader;
			this.unit_type_bg.gameObject.SetActive(true);
			return;
		}
		if (pActor.is_army_captain)
		{
			this.unit_type_bg.sprite = this._type_captain;
			this.unit_type_bg.gameObject.SetActive(true);
		}
	}

	// Token: 0x060032D2 RID: 13010 RVA: 0x00180E5F File Offset: 0x0017F05F
	public void showThisActor()
	{
		if (this._actor.isRekt())
		{
			return;
		}
		if (!InputHelpers.mouseSupported && !Tooltip.isShowingFor(this))
		{
			this.tooltipActionActor();
			return;
		}
		ActionLibrary.openUnitWindow(this._actor);
	}

	// Token: 0x060032D3 RID: 13011 RVA: 0x00180E90 File Offset: 0x0017F090
	public Actor getActor()
	{
		return this._actor;
	}

	// Token: 0x060032D4 RID: 13012 RVA: 0x00180E98 File Offset: 0x0017F098
	private void OnDisable()
	{
		this._actor = null;
	}

	// Token: 0x060032D5 RID: 13013 RVA: 0x00180EA1 File Offset: 0x0017F0A1
	public bool isSameActor(Actor pActor)
	{
		return this._actor == pActor;
	}

	// Token: 0x060032D6 RID: 13014 RVA: 0x00180EAC File Offset: 0x0017F0AC
	public MetaCustomizationAsset GetMetaAsset()
	{
		return AssetManager.meta_customization_library.get("unit");
	}

	// Token: 0x060032D7 RID: 13015 RVA: 0x00180EBD File Offset: 0x0017F0BD
	public string getName()
	{
		return this._actor.getName();
	}

	// Token: 0x060032D8 RID: 13016 RVA: 0x00180ECA File Offset: 0x0017F0CA
	public NanoObject GetNanoObject()
	{
		return this._actor;
	}

	// Token: 0x060032DA RID: 13018 RVA: 0x00180EE8 File Offset: 0x0017F0E8
	Transform IBaseMono.get_transform()
	{
		return base.transform;
	}

	// Token: 0x060032DB RID: 13019 RVA: 0x00180EF0 File Offset: 0x0017F0F0
	GameObject IBaseMono.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x060032DC RID: 13020 RVA: 0x00180EF8 File Offset: 0x0017F0F8
	T IBaseMono.GetComponent<T>()
	{
		return base.GetComponent<T>();
	}

	// Token: 0x04002676 RID: 9846
	public Image unit_type_bg;

	// Token: 0x04002677 RID: 9847
	[SerializeField]
	private Sprite _type_king;

	// Token: 0x04002678 RID: 9848
	[SerializeField]
	private Sprite _type_leader;

	// Token: 0x04002679 RID: 9849
	[SerializeField]
	private Sprite _type_captain;

	// Token: 0x0400267A RID: 9850
	public UnitAvatarLoader avatarLoader;

	// Token: 0x0400267B RID: 9851
	public KingdomBanner kingdomBanner;

	// Token: 0x0400267C RID: 9852
	public ClanBanner clanBanner;

	// Token: 0x0400267D RID: 9853
	public bool show_banner_kingdom = true;

	// Token: 0x0400267E RID: 9854
	public bool show_banner_clan = true;

	// Token: 0x0400267F RID: 9855
	[SerializeField]
	private Image _tile_graphics_1;

	// Token: 0x04002680 RID: 9856
	[SerializeField]
	private Image _tile_graphics_2;

	// Token: 0x04002681 RID: 9857
	[SerializeField]
	private Sprite _tile_inside_boat;

	// Token: 0x04002682 RID: 9858
	private Actor _actor;
}
