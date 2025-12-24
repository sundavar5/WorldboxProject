using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200060F RID: 1551
public class UiWorldAgeInfo : MonoBehaviour
{
	// Token: 0x060032DE RID: 13022 RVA: 0x00180F10 File Offset: 0x0017F110
	private void Awake()
	{
		this._text_age_title_localized = this._text_age_title.GetComponent<LocalizedText>();
		this._text_year_localized = this._text_year.GetComponent<LocalizedText>();
		this._text_month_localized = this._text_month.GetComponent<LocalizedText>();
		this._bar_age_localized = this._bar_age.textField.GetComponent<LocalizedText>();
	}

	// Token: 0x060032DF RID: 13023 RVA: 0x00180F68 File Offset: 0x0017F168
	private void Update()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		if (World.world == null)
		{
			return;
		}
		if (World.world_era == null)
		{
			return;
		}
		WorldAgeManager era_manager = World.world.era_manager;
		Sprite tClockSprite = era_manager.isPaused() ? this._sprite_pause : this._sprite_play;
		this._icon_clock_left.sprite = tClockSprite;
		this._icon_clock_right.sprite = tClockSprite;
		this._icon_age_left.sprite = World.world_era.getSprite();
		this._icon_age_right.sprite = World.world_era.getSprite();
		this._text_age_title.color = World.world_era.title_color;
		this._text_age_title_localized.setKeyAndUpdate(World.world_era.getLocaleID());
		MapStats tStats = World.world.map_stats;
		this._text_year_localized.setKeyAndUpdate("year_era");
		this._text_month_localized.setKeyAndUpdate(AssetManager.months.getMonth(Date.getCurrentMonth()).getLocaleID());
		float tMonthTime = (float)Date.getCurrentMonth() + Date.getMonthTime() / 5f - 1f;
		this._bar_year.setBar(tMonthTime, 12f, "", false, true, false, 0.3f);
		this._bar_age.setBar(tStats.current_age_progress, 1f, "", false, true, false, 0.3f);
		this._bar_age.gameObject.SetActive(true);
		if (era_manager.isPaused())
		{
			this._bar_age_localized.setKeyAndUpdate("ages_paused");
		}
		else
		{
			this._bar_age_localized.setKeyAndUpdate("next_age_in_moons");
		}
		WorldAgeAsset tNextEraAsset = World.world.era_manager.getNextAge();
		if (tNextEraAsset == null)
		{
			this._icon_age_next_left.sprite = this._random_age_sprite;
			this._icon_age_next_right.sprite = this._random_age_sprite;
			return;
		}
		this._icon_age_next_left.sprite = tNextEraAsset.getSprite();
		this._icon_age_next_right.sprite = tNextEraAsset.getSprite();
	}

	// Token: 0x04002683 RID: 9859
	[SerializeField]
	private Sprite _random_age_sprite;

	// Token: 0x04002684 RID: 9860
	[SerializeField]
	private Image _icon_age_left;

	// Token: 0x04002685 RID: 9861
	[SerializeField]
	private Image _icon_age_right;

	// Token: 0x04002686 RID: 9862
	[SerializeField]
	private Image _icon_age_next_left;

	// Token: 0x04002687 RID: 9863
	[SerializeField]
	private Image _icon_age_next_right;

	// Token: 0x04002688 RID: 9864
	[SerializeField]
	private Image _icon_clock_left;

	// Token: 0x04002689 RID: 9865
	[SerializeField]
	private Image _icon_clock_right;

	// Token: 0x0400268A RID: 9866
	[SerializeField]
	private Text _text_age_title;

	// Token: 0x0400268B RID: 9867
	[SerializeField]
	private Text _text_year;

	// Token: 0x0400268C RID: 9868
	[SerializeField]
	private Text _text_month;

	// Token: 0x0400268D RID: 9869
	[SerializeField]
	private StatBar _bar_age;

	// Token: 0x0400268E RID: 9870
	[SerializeField]
	private StatBar _bar_year;

	// Token: 0x0400268F RID: 9871
	[SerializeField]
	private Sprite _sprite_play;

	// Token: 0x04002690 RID: 9872
	[SerializeField]
	private Sprite _sprite_pause;

	// Token: 0x04002691 RID: 9873
	private LocalizedText _text_age_title_localized;

	// Token: 0x04002692 RID: 9874
	private LocalizedText _text_year_localized;

	// Token: 0x04002693 RID: 9875
	private LocalizedText _text_month_localized;

	// Token: 0x04002694 RID: 9876
	private LocalizedText _bar_age_localized;
}
