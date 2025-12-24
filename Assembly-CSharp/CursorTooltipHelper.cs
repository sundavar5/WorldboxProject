using System;
using UnityEngine;

// Token: 0x0200067F RID: 1663
public static class CursorTooltipHelper
{
	// Token: 0x06003579 RID: 13689 RVA: 0x00188B14 File Offset: 0x00186D14
	public static void update()
	{
		if (!InputHelpers.mouseSupported)
		{
			return;
		}
		if (World.world.isBusyWithUI())
		{
			CursorTooltipHelper.cancel();
			return;
		}
		if (CursorTooltipHelper.isInputHappening())
		{
			CursorTooltipHelper.cancel();
			return;
		}
		bool tShown = CursorTooltipHelper.updateGameplayTooltip();
		if (!tShown)
		{
			tShown = CursorTooltipHelper.updateMapTooltip();
		}
		if (!tShown)
		{
			CursorTooltipHelper.cancel();
		}
	}

	// Token: 0x0600357A RID: 13690 RVA: 0x00188B64 File Offset: 0x00186D64
	private static bool updateGameplayTooltip()
	{
		if (!PlayerConfig.optionBoolEnabled("tooltip_units"))
		{
			return false;
		}
		if (!MapBox.isRenderGameplay())
		{
			return false;
		}
		Actor tActor = UnitSelectionEffect.last_actor;
		if (tActor == null)
		{
			return false;
		}
		if (!tActor.isAlive())
		{
			return false;
		}
		if (CursorTooltipHelper._timeout > 0f)
		{
			CursorTooltipHelper._timeout -= World.world.delta_time;
			return true;
		}
		string tType = "actor";
		if (!HotkeyLibrary.many_mod.isHolding() || !CursorTooltipHelper.showTooltipForSelectedMeta(tActor))
		{
			if (tActor.isKing())
			{
				tType = "actor_king";
			}
			else if (tActor.isCityLeader())
			{
				tType = "actor_leader";
			}
			Tooltip.hideTooltip(tActor, true, tType);
			Tooltip.show(tActor, tType, new TooltipData
			{
				actor = tActor,
				tooltip_scale = 0.7f,
				is_sim_tooltip = true,
				sound_allowed = false
			});
		}
		return true;
	}

	// Token: 0x0600357B RID: 13691 RVA: 0x00188C30 File Offset: 0x00186E30
	private static bool showTooltipForSelectedMeta(Actor pActor)
	{
		MetaType tMeta = Zones.getCurrentMapBorderMode(false);
		TooltipData tData = new TooltipData
		{
			tooltip_scale = 0.7f,
			is_sim_tooltip = true
		};
		string tType;
		object tMetaTarget;
		switch (tMeta)
		{
		case MetaType.Subspecies:
			if (!pActor.hasSubspecies())
			{
				return false;
			}
			tType = "subspecies";
			tData.subspecies = pActor.subspecies;
			tMetaTarget = pActor.subspecies;
			break;
		case MetaType.Family:
			if (!pActor.hasFamily())
			{
				return false;
			}
			tType = "family";
			tData.family = pActor.family;
			tMetaTarget = pActor.family;
			break;
		case MetaType.Language:
			if (!pActor.hasLanguage())
			{
				return false;
			}
			tType = "language";
			tData.language = pActor.language;
			tMetaTarget = pActor.language;
			break;
		case MetaType.Culture:
			if (!pActor.hasCulture())
			{
				return false;
			}
			tType = "culture";
			tData.culture = pActor.culture;
			tMetaTarget = pActor.culture;
			break;
		case MetaType.Religion:
			if (!pActor.hasReligion())
			{
				return false;
			}
			tType = "religion";
			tData.religion = pActor.religion;
			tMetaTarget = pActor.religion;
			break;
		case MetaType.Clan:
			if (!pActor.hasClan())
			{
				return false;
			}
			tType = "clan";
			tData.clan = pActor.clan;
			tMetaTarget = pActor.clan;
			break;
		case MetaType.City:
			if (!pActor.hasCity())
			{
				return false;
			}
			tType = "city";
			tData.city = pActor.city;
			tMetaTarget = pActor.city;
			break;
		case MetaType.Kingdom:
			if (!pActor.isKingdomCiv())
			{
				return false;
			}
			tType = "kingdom";
			tData.kingdom = pActor.kingdom;
			tMetaTarget = pActor.kingdom;
			break;
		case MetaType.Alliance:
			if (!pActor.kingdom.hasAlliance())
			{
				return false;
			}
			tType = "alliance";
			tData.alliance = pActor.kingdom.getAlliance();
			tMetaTarget = pActor.kingdom.getAlliance();
			break;
		default:
			return false;
		}
		Tooltip.hideTooltip(tMetaTarget, true, tType);
		Tooltip.show(tMetaTarget, tType, tData);
		return true;
	}

	// Token: 0x0600357C RID: 13692 RVA: 0x00188E10 File Offset: 0x00187010
	private static bool updateMapTooltip()
	{
		if (!PlayerConfig.optionBoolEnabled("tooltip_zones"))
		{
			return false;
		}
		if (!MapBox.isRenderMiniMap())
		{
			return false;
		}
		if (!Zones.showMapBorders())
		{
			return false;
		}
		if (CursorTooltipHelper._timeout > 0f)
		{
			CursorTooltipHelper._timeout -= World.world.delta_time;
			return true;
		}
		bool tShowing = false;
		WorldTile tMouseTile = World.world.getMouseTilePosCachedFrame();
		MetaTypeAsset tMetaAsset = World.world.getCachedMapMetaAsset();
		if (tMouseTile != null && tMetaAsset != null)
		{
			tShowing = tMetaAsset.check_cursor_tooltip(tMouseTile.zone, tMetaAsset, tMetaAsset.getZoneOptionState());
		}
		return tShowing;
	}

	// Token: 0x0600357D RID: 13693 RVA: 0x00188E98 File Offset: 0x00187098
	private static void cancel()
	{
		Tooltip.hideTooltip(null, true, string.Empty);
		CursorTooltipHelper.resetTimout();
	}

	// Token: 0x0600357E RID: 13694 RVA: 0x00188EAC File Offset: 0x001870AC
	private static bool isInputHappening()
	{
		return Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2) || Input.mouseScrollDelta.y != 0f || (!HotkeyLibrary.many_mod.isHolding() && Input.anyKey);
	}

	// Token: 0x0600357F RID: 13695 RVA: 0x00188EF9 File Offset: 0x001870F9
	private static void resetTimout()
	{
		CursorTooltipHelper._timeout = CursorTooltipHelper._timeout_interval;
	}

	// Token: 0x040027E4 RID: 10212
	private static float _timeout = 0f;

	// Token: 0x040027E5 RID: 10213
	private static float _timeout_interval = 0.2f;

	// Token: 0x040027E6 RID: 10214
	public static bool is_over_meta;
}
