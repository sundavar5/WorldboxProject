using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002CD RID: 717
public class NameplateText : MonoBehaviour
{
	// Token: 0x06001A5A RID: 6746 RVA: 0x000F81C8 File Offset: 0x000F63C8
	public Vector2 getLastScreenPosition()
	{
		return this._last_position;
	}

	// Token: 0x06001A5B RID: 6747 RVA: 0x000F81D0 File Offset: 0x000F63D0
	private void Awake()
	{
		this._rect = base.GetComponent<RectTransform>();
		this._text_rect = this._text_name.GetComponent<RectTransform>();
		this._canvas_group = base.GetComponent<CanvasGroup>();
	}

	// Token: 0x06001A5C RID: 6748 RVA: 0x000F81FC File Offset: 0x000F63FC
	public void prepare(NameplateAsset pAsset, NanoObject pMeta, float pGlobalScale, NameplateRenderingType pNameplateMode, bool pNanoObjectSet, NanoObject pSelectedNanoObject)
	{
		if (pNanoObjectSet)
		{
			if (pSelectedNanoObject == pMeta)
			{
				pNameplateMode = NameplateRenderingType.Full;
			}
			else
			{
				pNameplateMode = NameplateRenderingType.BannerOnly;
			}
		}
		if (pNameplateMode != this._last_mode)
		{
			this.clearCaches();
			this._active_check_dirty = true;
			this._last_mode = pNameplateMode;
			NameplateRenderingType last_mode = this._last_mode;
			if (last_mode != NameplateRenderingType.Full)
			{
				if (last_mode == NameplateRenderingType.BannerOnly)
				{
					this._background_image.transform.localScale = new Vector3(pAsset.banner_only_mode_scale, pAsset.banner_only_mode_scale, 1f);
					this._background_image.enabled = false;
				}
			}
			else
			{
				this._background_image.transform.localScale = new Vector3(1f, 1f, 1f);
				this._background_image.enabled = true;
			}
		}
		this.updateScale(pMeta, pGlobalScale, pNanoObjectSet, pSelectedNanoObject);
		this.resetElements();
		this.setShowing(true);
		this.setAssetAndMeta(pAsset, pMeta);
		if (((IFavoriteable)pMeta).isFavorite())
		{
			this.showFavoriteIcon();
		}
		else
		{
			this._show_icon_favorite = false;
		}
		this.checkSetActive(this._icon_favorite, this._show_icon_favorite);
	}

	// Token: 0x06001A5D RID: 6749 RVA: 0x000F8300 File Offset: 0x000F6500
	private void updateScale(NanoObject pMeta, float pGlobalScale, bool pNanoObjectSet, NanoObject pSelectedNanoObject)
	{
		float tNewScaleValue = pGlobalScale;
		if (pNanoObjectSet)
		{
			if (pSelectedNanoObject == pMeta)
			{
				tNewScaleValue = pGlobalScale * 1.2f;
			}
			else
			{
				tNewScaleValue = pGlobalScale * 0.8f;
			}
		}
		if (this._last_scale == tNewScaleValue)
		{
			return;
		}
		this._last_scale = tNewScaleValue;
		base.transform.localScale = new Vector3(tNewScaleValue, tNewScaleValue, 1f);
	}

	// Token: 0x06001A5E RID: 6750 RVA: 0x000F8351 File Offset: 0x000F6551
	public void forceScale(Vector3 pScale)
	{
		this._last_scale = pScale.x;
		base.transform.localScale = pScale;
	}

	// Token: 0x06001A5F RID: 6751 RVA: 0x000F836B File Offset: 0x000F656B
	public void newNameplate(NameplateManager pManager, string pName)
	{
		this.clearFull();
		this._manager = pManager;
		base.name = pName;
	}

	// Token: 0x06001A60 RID: 6752 RVA: 0x000F8381 File Offset: 0x000F6581
	public bool isShowing()
	{
		return this._showing;
	}

	// Token: 0x06001A61 RID: 6753 RVA: 0x000F8389 File Offset: 0x000F6589
	public void setShowing(bool pVal)
	{
		this._showing = pVal;
	}

	// Token: 0x06001A62 RID: 6754 RVA: 0x000F8394 File Offset: 0x000F6594
	public void checkActive()
	{
		if (this._showing)
		{
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
		}
		else if (base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(false);
		}
		if (this._showing && this._active_check_dirty)
		{
			this._active_check_dirty = false;
			this.checkActiveElements();
		}
	}

	// Token: 0x06001A63 RID: 6755 RVA: 0x000F83FC File Offset: 0x000F65FC
	private void checkActiveElements()
	{
		this.checkSetActive(this._container_capture, this._show_capture_counter);
		this.checkSetActive(this._icon_species, this._show_icon_species);
		this.checkSetActive(this._icon_special, this._show_icon_special);
		this.checkSetActive(this._banner_alliance, this._show_banner_alliance);
		this.checkSetActive(this._banner_clan, this._show_banner_clan);
		this.checkSetActive(this._banner_culture, this._show_banner_culture);
		this.checkSetActive(this._banner_kingdoms, this._show_banner_kingdom);
		this.checkSetActive(this._banner_religion, this._show_banner_religion);
		this.checkSetActive(this._banner_family, this._show_banner_family);
		this.checkSetActive(this._banner_language, this._show_banner_language);
		this.checkSetActive(this._banner_subspecies, this._show_banner_subspecies);
		this.checkSetActive(this._banner_army, this._show_banner_army);
		this.checkSetActive(this._banner_city, this._show_banner_city);
	}

	// Token: 0x06001A64 RID: 6756 RVA: 0x000F84F3 File Offset: 0x000F66F3
	private void checkSetActive(Component pComponent, bool pShouldBeActive)
	{
		this.checkSetActive(pComponent.gameObject, pShouldBeActive);
	}

	// Token: 0x06001A65 RID: 6757 RVA: 0x000F8502 File Offset: 0x000F6702
	private void checkSetActive(GameObject pObject, bool pShouldBeActive)
	{
		if (pShouldBeActive)
		{
			if (!pObject.activeSelf)
			{
				pObject.SetActive(true);
				return;
			}
		}
		else if (pObject.activeSelf)
		{
			pObject.SetActive(false);
		}
	}

	// Token: 0x06001A66 RID: 6758 RVA: 0x000F8526 File Offset: 0x000F6726
	public void clearFull()
	{
		this.nano_object = null;
		this.clearCaches();
		this.setShowing(false);
		this.resetElements();
	}

	// Token: 0x06001A67 RID: 6759 RVA: 0x000F8542 File Offset: 0x000F6742
	public void clearCaches()
	{
		this._asset = null;
		this._last_position = Globals.POINT_IN_VOID_2;
		this._last_scale = -1f;
		this._old_text = "!";
		this._last_mode = NameplateRenderingType.Clear;
	}

	// Token: 0x06001A68 RID: 6760 RVA: 0x000F8574 File Offset: 0x000F6774
	private void resetElements()
	{
		this.priority_capital = false;
		this.priority_population = 0;
		this.favorited = false;
		this._show_capture_counter = false;
		this._show_icon_favorite = false;
		this._show_icon_species = false;
		this._show_icon_special = false;
		this._show_banner_alliance = false;
		this._show_banner_clan = false;
		this._show_banner_culture = false;
		this._show_banner_army = false;
		this._show_banner_kingdom = false;
		this._show_banner_religion = false;
		this._show_banner_subspecies = false;
		this._show_banner_language = false;
		this._show_banner_family = false;
		this._show_banner_city = false;
	}

	// Token: 0x06001A69 RID: 6761 RVA: 0x000F85F8 File Offset: 0x000F67F8
	private void setupMeta(MetaObjectData pData, ColorAsset pColorAsset)
	{
		this.favorited = pData.favorite;
		Color tColor = pColorAsset.getColorText();
		this._text_name.color = tColor;
		this.updateAlpha(pData);
	}

	// Token: 0x06001A6A RID: 6762 RVA: 0x000F862C File Offset: 0x000F682C
	private void updateAlpha(MetaObjectData pData)
	{
		float tAlpha;
		if (this.checkShouldDrawObject(pData))
		{
			tAlpha = 1f;
		}
		else
		{
			tAlpha = 0.5f;
		}
		if (this._canvas_group.alpha != tAlpha)
		{
			this._canvas_group.alpha = tAlpha;
		}
	}

	// Token: 0x06001A6B RID: 6763 RVA: 0x000F866C File Offset: 0x000F686C
	internal void showTextKingdom(Kingdom pMetaObject, Vector2 pPosition)
	{
		this.setupMeta(pMetaObject.data, pMetaObject.getColor());
		int tUnitsCount = pMetaObject.getPopulationPeople();
		string tStringName = this.getStringForNameplate(pMetaObject.name, tUnitsCount);
		if (this.is_full)
		{
			if (DebugConfig.isOn(DebugOption.ShowWarriorsCityText))
			{
				tStringName = string.Concat(new string[]
				{
					tStringName,
					" | ",
					pMetaObject.countTotalWarriors().ToString(),
					"/",
					pMetaObject.countWarriorsMax().ToString()
				});
			}
			if (DebugConfig.isOn(DebugOption.ShowCityWeaponsText))
			{
				tStringName = tStringName + " | w" + pMetaObject.countWeapons().ToString();
			}
		}
		this.setText(tStringName, pPosition, 10);
		this.setPriority(tUnitsCount);
		this.showSpecies(pMetaObject.getSpriteIcon());
		this._show_banner_kingdom = true;
		this._banner_kingdoms.load(pMetaObject);
		if (this.is_full)
		{
			Clan tClan = pMetaObject.getKingClan();
			if (tClan != null)
			{
				this._show_banner_clan = true;
				this._banner_clan.load(tClan);
			}
		}
	}

	// Token: 0x06001A6C RID: 6764 RVA: 0x000F8770 File Offset: 0x000F6970
	internal void showTextReligion(Religion pMetaObject, Vector3 pPosition)
	{
		this.setupMeta(pMetaObject.data, pMetaObject.getColor());
		int tUnitsCount = pMetaObject.units.Count;
		string tStringName = this.getStringForNameplate(pMetaObject.name, tUnitsCount);
		this.setText(tStringName, pPosition, 10);
		this.setPriority(tUnitsCount);
		this._show_banner_religion = true;
		this._banner_religion.load(pMetaObject);
		this.showSpecies(pMetaObject.getActorAsset().getSpriteIcon());
	}

	// Token: 0x06001A6D RID: 6765 RVA: 0x000F87E0 File Offset: 0x000F69E0
	internal void showTextSubspecies(Subspecies pMetaObject, Vector3 pPosition)
	{
		this.setupMeta(pMetaObject.data, pMetaObject.getColor());
		int tUnitsCount = pMetaObject.units.Count;
		string tStringName = this.getStringForNameplate(pMetaObject.name, tUnitsCount);
		this.setText(tStringName, pPosition, 10);
		this.setPriority(tUnitsCount);
		this._show_banner_subspecies = true;
		this._banner_subspecies.load(pMetaObject);
		this.showSpecies(pMetaObject.getSpriteIcon());
	}

	// Token: 0x06001A6E RID: 6766 RVA: 0x000F884C File Offset: 0x000F6A4C
	internal void showTextFamily(Family pMetaObject, Vector3 pPosition)
	{
		this.setupMeta(pMetaObject.data, pMetaObject.getColor());
		int tUnitsCount = pMetaObject.units.Count;
		string tStringName = this.getStringForNameplate(pMetaObject.name, tUnitsCount);
		this.setText(tStringName, pPosition, 10);
		this.setPriority(tUnitsCount);
		this._show_banner_family = true;
		this._banner_family.load(pMetaObject);
	}

	// Token: 0x06001A6F RID: 6767 RVA: 0x000F88AC File Offset: 0x000F6AAC
	internal void showTextArmy(Army pMetaObject, Vector3 pPosition)
	{
		this.setupMeta(pMetaObject.data, pMetaObject.getColor());
		int tUnitsCount = pMetaObject.units.Count;
		string tStringName = this.getStringForNameplateLine(pMetaObject.name, tUnitsCount);
		this.setText(tStringName, pPosition, 10);
		this.setPriority(tUnitsCount);
		this._show_banner_army = true;
		this._banner_army.load(pMetaObject);
		if (pMetaObject.hasCaptain())
		{
			this.showSpecies(pMetaObject.getCaptain().getActorAsset().getSpriteIcon());
			return;
		}
		this.showSpecies(pMetaObject.getActorAsset().getSpriteIcon());
	}

	// Token: 0x06001A70 RID: 6768 RVA: 0x000F893C File Offset: 0x000F6B3C
	internal void showTextCulture(Culture pMetaObject, Vector3 pPosition)
	{
		this.setupMeta(pMetaObject.data, pMetaObject.getColor());
		int tUnitsCount = pMetaObject.units.Count;
		string tStringName = this.getStringForNameplate(pMetaObject.name, tUnitsCount);
		this.setText(tStringName, pPosition, 10);
		this.setPriority(tUnitsCount);
		this._show_banner_culture = true;
		this._banner_culture.load(pMetaObject);
		this.showSpecies(pMetaObject.getActorAsset().getSpriteIcon());
	}

	// Token: 0x06001A71 RID: 6769 RVA: 0x000F89AC File Offset: 0x000F6BAC
	internal void showTextLanguage(Language pMetaObject, Vector3 pPosition)
	{
		this.setupMeta(pMetaObject.data, pMetaObject.getColor());
		int tUnitsCount = pMetaObject.units.Count;
		string tStringName = this.getStringForNameplate(pMetaObject.name, tUnitsCount);
		this.setText(tStringName, pPosition, 10);
		this.setPriority(tUnitsCount);
		this._show_banner_language = true;
		this._banner_language.load(pMetaObject);
		this.showSpecies(pMetaObject.getActorAsset().getSpriteIcon());
	}

	// Token: 0x06001A72 RID: 6770 RVA: 0x000F8A1C File Offset: 0x000F6C1C
	internal void showTextAlliance(Alliance pMetaObject, City pCity)
	{
		this.setupMeta(pMetaObject.data, pMetaObject.getColor());
		int tUnitsCount = pMetaObject.countPopulation();
		this._show_icon_species = false;
		string tStringName = this.getStringForNameplate(pMetaObject.name, tUnitsCount);
		if (this.is_full && DebugConfig.isOn(DebugOption.ShowWarriorsCityText))
		{
			tStringName = tStringName + " | " + pMetaObject.countWarriors().ToString();
		}
		this.setText(tStringName, pCity.city_center, 10);
		this.setPriority(tUnitsCount);
		this._show_banner_alliance = true;
		this._banner_alliance.load(pMetaObject);
	}

	// Token: 0x06001A73 RID: 6771 RVA: 0x000F8AB0 File Offset: 0x000F6CB0
	internal void showTextClanFluid(Clan pMetaObject, Vector3 pPosition)
	{
		this.setupMeta(pMetaObject.data, pMetaObject.getColor());
		int tUnitsCount = pMetaObject.units.Count;
		string tStringName = this.getStringForNameplate(pMetaObject.name, tUnitsCount);
		this.setText(tStringName, pPosition, 10);
		this.setPriority(tUnitsCount);
		this._show_banner_clan = true;
		this._banner_clan.load(pMetaObject);
		this.showSpecies(pMetaObject.getActorAsset().getSpriteIcon());
	}

	// Token: 0x06001A74 RID: 6772 RVA: 0x000F8B20 File Offset: 0x000F6D20
	internal void showTextClanCity(Clan pMetaObject, City pCity)
	{
		this.setupMeta(pMetaObject.data, pMetaObject.getColor());
		int tUnitsCount = pMetaObject.units.Count;
		string tStringName = this.getStringForNameplate(pMetaObject.name, tUnitsCount);
		this.setText(tStringName, pCity.city_center, 10);
		this.setPriority(tUnitsCount);
		ActorAsset tActorAsset = pMetaObject.getActorAsset();
		this.showSpecies(tActorAsset.getSpriteIcon());
		this._show_banner_clan = true;
		this._banner_clan.load(pMetaObject);
	}

	// Token: 0x06001A75 RID: 6773 RVA: 0x000F8B9C File Offset: 0x000F6D9C
	internal void showTextCity(City pMetaObject, Vector2 pPosition)
	{
		this.setupMeta(pMetaObject.data, pMetaObject.kingdom.getColor());
		if (pMetaObject.isCapitalCity())
		{
			this.setNameplateSprite("ui/nameplates/nameplate_city_capital");
		}
		else
		{
			this.setNameplateSprite("ui/nameplates/nameplate_city");
		}
		int tUnitsCount = pMetaObject.getPopulationPeople();
		string tStringName = this.getStringForNameplate(pMetaObject.name, tUnitsCount);
		if (this.is_full)
		{
			if (DebugConfig.isOn(DebugOption.ShowWarriorsCityText))
			{
				tStringName = string.Concat(new string[]
				{
					tStringName,
					" | ",
					pMetaObject.countWarriors().ToString(),
					"/",
					pMetaObject.getMaxWarriors().ToString()
				});
				if (Config.isEditor)
				{
					string tPercent = "  :  " + ((int)(pMetaObject.getArmyMaxMultiplier() * 100f)).ToString() + "%";
					tStringName += tPercent;
				}
			}
			if (DebugConfig.isOn(DebugOption.ShowCityWeaponsText))
			{
				tStringName = tStringName + " | w" + pMetaObject.countWeapons().ToString();
			}
			if (DebugConfig.isOn(DebugOption.ShowFoodCityText))
			{
				tStringName = tStringName + " | F" + pMetaObject.getTotalFood().ToString();
			}
		}
		this.setText(tStringName, pPosition, 10);
		if (pMetaObject.getMainSubspecies() != null)
		{
			this.showSpecies(pMetaObject.getMainSubspecies().getActorAsset().getSpriteIcon());
		}
		if (pMetaObject.last_visual_capture_ticks != 0)
		{
			this._show_capture_counter = true;
			this._active_check_dirty = true;
			if (pMetaObject.being_captured_by != null && pMetaObject.being_captured_by.isAlive())
			{
				this._conquer_text.color = pMetaObject.being_captured_by.getColor().getColorText();
			}
			this._conquer_text.text = pMetaObject.last_visual_capture_ticks.ToString() + "%";
		}
		else
		{
			this._show_capture_counter = false;
			this._active_check_dirty = true;
		}
		if (this._show_capture_counter)
		{
			Vector2 tAnchoredPos;
			if (this.is_full)
			{
				tAnchoredPos = new Vector2(0f, -1f);
			}
			else
			{
				tAnchoredPos = new Vector2(3f, -25f);
			}
			this._container_capture.anchoredPosition = tAnchoredPos;
		}
		this._show_banner_city = true;
		this._banner_city.load(pMetaObject);
		this.priority_capital = pMetaObject.isCapitalCity();
		this.setPriority(tUnitsCount);
	}

	// Token: 0x06001A76 RID: 6774 RVA: 0x000F8DCF File Offset: 0x000F6FCF
	private void showSpecies(string pPath)
	{
		this.showSpecies(SpriteTextureLoader.getSprite(pPath));
	}

	// Token: 0x06001A77 RID: 6775 RVA: 0x000F8DDD File Offset: 0x000F6FDD
	private void showSpecies(Sprite pSprite)
	{
		if (this.is_mini)
		{
			return;
		}
		this._show_icon_species = true;
		this._icon_species.sprite = pSprite;
	}

	// Token: 0x06001A78 RID: 6776 RVA: 0x000F8DFB File Offset: 0x000F6FFB
	public void showFavoriteIcon()
	{
		this._show_icon_favorite = true;
	}

	// Token: 0x06001A79 RID: 6777 RVA: 0x000F8E04 File Offset: 0x000F7004
	private void showSpecial(string pPath)
	{
		this._show_icon_special = true;
		this._icon_special.sprite = SpriteTextureLoader.getSprite(pPath);
	}

	// Token: 0x06001A7A RID: 6778 RVA: 0x000F8E20 File Offset: 0x000F7020
	private void setText(string pNewText, Vector3 pPos, int pAdditionalWidth = 10)
	{
		if (this.is_mini)
		{
			pAdditionalWidth = 0;
		}
		this.updatePositionAndRect(pPos);
		if (this._old_text == pNewText)
		{
			return;
		}
		this._old_text = pNewText;
		this._text_name.text = pNewText;
		this._text_width = this._text_name.preferredWidth + (float)pAdditionalWidth;
		float tHeight = this._rect_background.sizeDelta.y;
		this._text_rect.sizeDelta = new Vector2(this._text_width, tHeight);
		this._last_position = Globals.POINT_IN_VOID_2;
	}

	// Token: 0x06001A7B RID: 6779 RVA: 0x000F8EA8 File Offset: 0x000F70A8
	private void updatePositionAndRect(Vector3 pPos)
	{
		Vector2 tNewPos = this.transformPosition(pPos);
		if (tNewPos != this._last_position)
		{
			this._last_position = tNewPos;
			base.transform.position = tNewPos;
			this.recalcScaledOverlapRect(this._rect_background, ref this.map_text_rect_click);
			float tShrinkX = this.map_text_rect_click.width * 0f * 0.5f;
			float tShrinkY = this.map_text_rect_click.height * 0f * 0.5f;
			this.map_text_rect_overlap.x = this.map_text_rect_click.x + tShrinkX;
			this.map_text_rect_overlap.y = this.map_text_rect_click.y + tShrinkY;
			this.map_text_rect_overlap.width = this.map_text_rect_click.width - tShrinkX * 2f;
			this.map_text_rect_overlap.height = this.map_text_rect_click.height - tShrinkY * 2f;
		}
	}

	// Token: 0x06001A7C RID: 6780 RVA: 0x000F8F94 File Offset: 0x000F7194
	private string getStringForNameplate(string pName, int pCount)
	{
		if (this.is_mini)
		{
			return string.Empty;
		}
		return pName + " " + pCount.ToString();
	}

	// Token: 0x06001A7D RID: 6781 RVA: 0x000F8FB6 File Offset: 0x000F71B6
	private string getStringForNameplateLine(string pName, int pCount)
	{
		if (this.is_mini)
		{
			return string.Empty;
		}
		return pName + " - " + pCount.ToString();
	}

	// Token: 0x06001A7E RID: 6782 RVA: 0x000F8FD8 File Offset: 0x000F71D8
	public bool overlapsWithOtherPlate(NameplateText pText)
	{
		return this.map_text_rect_overlap.Overlaps(pText.map_text_rect_overlap);
	}

	// Token: 0x06001A7F RID: 6783 RVA: 0x000F8FEB File Offset: 0x000F71EB
	private void recalcScaledOverlapRect(RectTransform pRectBackground, ref Rect pMapTextRect)
	{
		this.recalcScaledOverlapRectSimple(pRectBackground, ref pMapTextRect);
	}

	// Token: 0x06001A80 RID: 6784 RVA: 0x000F8FF8 File Offset: 0x000F71F8
	private void recalcScaledOverlapRectCorners(RectTransform pRectBackground, ref Rect pMapTextRect)
	{
		Vector3[] tCorners = new Vector3[4];
		pRectBackground.GetWorldCorners(tCorners);
		float tMinX = Mathf.Min(new float[]
		{
			tCorners[0].x,
			tCorners[1].x,
			tCorners[2].x,
			tCorners[3].x
		});
		float tMaxX = Mathf.Max(new float[]
		{
			tCorners[0].x,
			tCorners[1].x,
			tCorners[2].x,
			tCorners[3].x
		});
		float tMinY = Mathf.Min(new float[]
		{
			tCorners[0].y,
			tCorners[1].y,
			tCorners[2].y,
			tCorners[3].y
		});
		float tMaxY = Mathf.Max(new float[]
		{
			tCorners[0].y,
			tCorners[1].y,
			tCorners[2].y,
			tCorners[3].y
		});
		pMapTextRect.x = tMinX;
		pMapTextRect.y = tMinY;
		pMapTextRect.width = tMaxX - tMinX;
		pMapTextRect.height = tMaxY - tMinY;
	}

	// Token: 0x06001A81 RID: 6785 RVA: 0x000F9158 File Offset: 0x000F7358
	private void recalcScaledOverlapRectSimple(RectTransform pRectBackground, ref Rect pMapTextRect)
	{
		float tScale = this._manager.cached_canvas_scale;
		Vector2 tSizeDelta = pRectBackground.sizeDelta;
		if (this.is_mini)
		{
			tSizeDelta.Set(60f, 60f);
		}
		float tRectTransformWidth = tSizeDelta.x * 0.55f * tScale;
		float tRectTransformHeight = tSizeDelta.y * 0.55f * tScale;
		Vector3 tPosition = pRectBackground.position;
		pMapTextRect.x = tPosition.x - tRectTransformWidth * 0.5f;
		pMapTextRect.y = tPosition.y - tRectTransformHeight * 0.5f;
		pMapTextRect.width = tRectTransformWidth;
		pMapTextRect.height = tRectTransformHeight;
	}

	// Token: 0x06001A82 RID: 6786 RVA: 0x000F91EF File Offset: 0x000F73EF
	private Vector2 transformPosition(Vector3 pVec)
	{
		return World.world.camera.WorldToScreenPoint(pVec);
	}

	// Token: 0x06001A83 RID: 6787 RVA: 0x000F9206 File Offset: 0x000F7406
	private bool checkShouldDrawObject(MetaObjectData pData)
	{
		return !this._manager.cached_favorites_only || pData.favorite;
	}

	// Token: 0x06001A84 RID: 6788 RVA: 0x000F9220 File Offset: 0x000F7420
	public void setAssetAndMeta(NameplateAsset pAsset, NanoObject pNano)
	{
		this.nano_object = pNano;
		if (this._asset == pAsset)
		{
			return;
		}
		this._active_check_dirty = true;
		this._asset = pAsset;
		this.setNameplateSprite(pAsset.path_sprite);
		RectOffset tPaddingOffset = this.layout_group.padding;
		if (this.is_mini)
		{
			tPaddingOffset.left = 0;
			tPaddingOffset.right = 0;
		}
		else
		{
			tPaddingOffset.left = pAsset.padding_left;
			tPaddingOffset.right = pAsset.padding_right;
		}
		tPaddingOffset.top = pAsset.padding_top;
	}

	// Token: 0x06001A85 RID: 6789 RVA: 0x000F92A0 File Offset: 0x000F74A0
	public void setNameplateSprite(string pPath)
	{
		Sprite tSprite = SpriteTextureLoader.getSprite(pPath);
		this._background_image.sprite = tSprite;
	}

	// Token: 0x06001A86 RID: 6790 RVA: 0x000F92C0 File Offset: 0x000F74C0
	private void setPriority(int pValue)
	{
		this.priority_population = pValue;
	}

	// Token: 0x1700018D RID: 397
	// (get) Token: 0x06001A87 RID: 6791 RVA: 0x000F92C9 File Offset: 0x000F74C9
	private bool is_full
	{
		get
		{
			return this._last_mode == NameplateRenderingType.Full;
		}
	}

	// Token: 0x1700018E RID: 398
	// (get) Token: 0x06001A88 RID: 6792 RVA: 0x000F92D4 File Offset: 0x000F74D4
	public bool is_mini
	{
		get
		{
			return this._last_mode == NameplateRenderingType.BannerOnly;
		}
	}

	// Token: 0x0400146D RID: 5229
	private NameplateManager _manager;

	// Token: 0x0400146E RID: 5230
	[SerializeField]
	private Image _icon_species;

	// Token: 0x0400146F RID: 5231
	[SerializeField]
	private Image _icon_special;

	// Token: 0x04001470 RID: 5232
	[SerializeField]
	private Image _icon_favorite;

	// Token: 0x04001471 RID: 5233
	[SerializeField]
	private Image _background_image;

	// Token: 0x04001472 RID: 5234
	[SerializeField]
	private KingdomBanner _banner_kingdoms;

	// Token: 0x04001473 RID: 5235
	[SerializeField]
	private CultureBanner _banner_culture;

	// Token: 0x04001474 RID: 5236
	[SerializeField]
	private LanguageBanner _banner_language;

	// Token: 0x04001475 RID: 5237
	[SerializeField]
	private AllianceBanner _banner_alliance;

	// Token: 0x04001476 RID: 5238
	[SerializeField]
	private ReligionBanner _banner_religion;

	// Token: 0x04001477 RID: 5239
	[SerializeField]
	private SubspeciesBanner _banner_subspecies;

	// Token: 0x04001478 RID: 5240
	[SerializeField]
	private FamilyBanner _banner_family;

	// Token: 0x04001479 RID: 5241
	[SerializeField]
	private ClanBanner _banner_clan;

	// Token: 0x0400147A RID: 5242
	[SerializeField]
	private ArmyBanner _banner_army;

	// Token: 0x0400147B RID: 5243
	[SerializeField]
	private CityBanner _banner_city;

	// Token: 0x0400147C RID: 5244
	[SerializeField]
	private RectTransform _container_capture;

	// Token: 0x0400147D RID: 5245
	public HorizontalLayoutGroup layout_group;

	// Token: 0x0400147E RID: 5246
	private NameplateAsset _asset;

	// Token: 0x0400147F RID: 5247
	private bool _show_icon_species;

	// Token: 0x04001480 RID: 5248
	private bool _show_icon_special;

	// Token: 0x04001481 RID: 5249
	private bool _show_icon_favorite;

	// Token: 0x04001482 RID: 5250
	private bool _show_banner_army;

	// Token: 0x04001483 RID: 5251
	private bool _show_banner_kingdom;

	// Token: 0x04001484 RID: 5252
	private bool _show_banner_culture;

	// Token: 0x04001485 RID: 5253
	private bool _show_banner_alliance;

	// Token: 0x04001486 RID: 5254
	private bool _show_banner_clan;

	// Token: 0x04001487 RID: 5255
	private bool _show_banner_religion;

	// Token: 0x04001488 RID: 5256
	private bool _show_banner_family;

	// Token: 0x04001489 RID: 5257
	private bool _show_banner_subspecies;

	// Token: 0x0400148A RID: 5258
	private bool _show_banner_language;

	// Token: 0x0400148B RID: 5259
	private bool _show_banner_city;

	// Token: 0x0400148C RID: 5260
	private bool _show_capture_counter;

	// Token: 0x0400148D RID: 5261
	private CanvasGroup _canvas_group;

	// Token: 0x0400148E RID: 5262
	[SerializeField]
	private RectTransform _rect_background;

	// Token: 0x0400148F RID: 5263
	[SerializeField]
	private Text _text_name;

	// Token: 0x04001490 RID: 5264
	[SerializeField]
	private Text _conquer_text;

	// Token: 0x04001491 RID: 5265
	private RectTransform _rect;

	// Token: 0x04001492 RID: 5266
	private RectTransform _text_rect;

	// Token: 0x04001493 RID: 5267
	private bool _showing;

	// Token: 0x04001494 RID: 5268
	internal bool priority_capital;

	// Token: 0x04001495 RID: 5269
	internal int priority_population;

	// Token: 0x04001496 RID: 5270
	internal bool favorited;

	// Token: 0x04001497 RID: 5271
	internal Rect map_text_rect_click;

	// Token: 0x04001498 RID: 5272
	internal Rect map_text_rect_overlap;

	// Token: 0x04001499 RID: 5273
	public NanoObject nano_object;

	// Token: 0x0400149A RID: 5274
	private float _last_scale;

	// Token: 0x0400149B RID: 5275
	private string _old_text;

	// Token: 0x0400149C RID: 5276
	private float _text_width;

	// Token: 0x0400149D RID: 5277
	private bool _active_check_dirty;

	// Token: 0x0400149E RID: 5278
	private NameplateRenderingType _last_mode;

	// Token: 0x0400149F RID: 5279
	private Vector2 _last_position;
}
