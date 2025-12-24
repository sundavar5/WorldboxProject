using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000813 RID: 2067
public class WorldAgesWindow : MonoBehaviour
{
	// Token: 0x170003B1 RID: 945
	// (get) Token: 0x060040A7 RID: 16551 RVA: 0x001BAACB File Offset: 0x001B8CCB
	private WorldAgeManager _era_manager
	{
		get
		{
			return World.world.era_manager;
		}
	}

	// Token: 0x170003B2 RID: 946
	// (get) Token: 0x060040A8 RID: 16552 RVA: 0x001BAAD7 File Offset: 0x001B8CD7
	private MapStats _map_stats
	{
		get
		{
			return World.world.map_stats;
		}
	}

	// Token: 0x060040A9 RID: 16553 RVA: 0x001BAAE3 File Offset: 0x001B8CE3
	private void Awake()
	{
		WorldAgesWindow._instance = this;
		this._age_wheel.init(new WorldAgeElementAction(this.wheelPieceAction));
		this.initButtons();
	}

	// Token: 0x060040AA RID: 16554 RVA: 0x001BAB08 File Offset: 0x001B8D08
	private void OnEnable()
	{
		this.selectPiece(this._era_manager.getCurrentSlotIndex());
		this.updateElements();
	}

	// Token: 0x060040AB RID: 16555 RVA: 0x001BAB21 File Offset: 0x001B8D21
	private void OnDisable()
	{
		this.updateElements();
	}

	// Token: 0x060040AC RID: 16556 RVA: 0x001BAB29 File Offset: 0x001B8D29
	public static void setAgeAndSelectPiece(WorldAgeAsset pAsset, WorldAgeWheelPiece pPiece)
	{
		WorldAgesWindow._instance.setAgeAndSelectPieceInstance(pAsset, pPiece);
	}

	// Token: 0x060040AD RID: 16557 RVA: 0x001BAB37 File Offset: 0x001B8D37
	private void setAgeAndSelectPieceInstance(WorldAgeAsset pAsset, WorldAgeWheelPiece pPiece)
	{
		pPiece.setAge(pAsset);
		this._era_manager.setAgeToSlot(pAsset, pPiece.getIndex());
		this.selectPiece(pPiece);
		this._era_manager.setCurrentSlotIndex(pPiece.getIndex(), 0.01f);
		this.updateElements();
	}

	// Token: 0x060040AE RID: 16558 RVA: 0x001BAB78 File Offset: 0x001B8D78
	private void selectPiece(int pIndex)
	{
		WorldAgeWheelPiece tPiece = this._age_wheel.getPiece(pIndex);
		this.selectPiece(tPiece);
	}

	// Token: 0x060040AF RID: 16559 RVA: 0x001BAB99 File Offset: 0x001B8D99
	private void selectPiece(WorldAgeWheelPiece pPiece)
	{
		this._selected_piece = pPiece;
	}

	// Token: 0x060040B0 RID: 16560 RVA: 0x001BABA4 File Offset: 0x001B8DA4
	private void initButtons()
	{
		for (int i = 0; i < AssetManager.era_library.list.Count; i++)
		{
			WorldAgeAsset tAsset = AssetManager.era_library.list[i];
			WorldAgeButton tButton = this.initButton(tAsset);
			this._buttons.Add(tAsset, tButton);
		}
	}

	// Token: 0x060040B1 RID: 16561 RVA: 0x001BABF1 File Offset: 0x001B8DF1
	private WorldAgeButton initButton(WorldAgeAsset pAsset)
	{
		WorldAgeButton worldAgeButton = Object.Instantiate<WorldAgeButton>(this._age_button_prefab, this._grid_age_buttons);
		worldAgeButton.setAge(pAsset);
		worldAgeButton.addClickCallback(new WorldAgeElementAction(this.ageButtonAction));
		return worldAgeButton;
	}

	// Token: 0x060040B2 RID: 16562 RVA: 0x001BAC1D File Offset: 0x001B8E1D
	private void wheelPieceAction(BaseWorldAgeElement pPiece)
	{
		if (this._selected_piece == pPiece)
		{
			return;
		}
		this.selectPiece(pPiece as WorldAgeWheelPiece);
		this.updateElements();
	}

	// Token: 0x060040B3 RID: 16563 RVA: 0x001BAC40 File Offset: 0x001B8E40
	private void ageButtonAction(BaseWorldAgeElement pElement)
	{
		if (!InputHelpers.mouseSupported)
		{
			if (!Tooltip.isShowingFor(pElement.gameObject))
			{
				return;
			}
			Tooltip.hideTooltip();
		}
		WorldAgeAsset tAsset = pElement.getAsset();
		this._selected_piece.setAge(tAsset);
		this._era_manager.setAgeToSlot(tAsset, this._selected_piece.getIndex());
		this.updateElements();
	}

	// Token: 0x060040B4 RID: 16564 RVA: 0x001BAC97 File Offset: 0x001B8E97
	public void nextAgeAction()
	{
		this._era_manager.startNextAge(0.5f);
		this.updateElements();
	}

	// Token: 0x060040B5 RID: 16565 RVA: 0x001BACAF File Offset: 0x001B8EAF
	public void pauseAgesAction()
	{
		this._era_manager.togglePlay(this._era_manager.isPaused());
		this.updateElements();
	}

	// Token: 0x060040B6 RID: 16566 RVA: 0x001BACD0 File Offset: 0x001B8ED0
	public void randomizeAgesAction()
	{
		foreach (WorldAgeWheelPiece tPiece in this._age_wheel.getPieces())
		{
			WorldAgeAsset tAsset = AssetManager.era_library.list.GetRandom<WorldAgeAsset>();
			tPiece.setAge(tAsset);
			this._era_manager.setAgeToSlot(tAsset, tPiece.getIndex());
		}
		this._era_manager.setCurrentSlotIndex(0, 0.01f);
		this.selectPiece(0);
		this.updateElements();
	}

	// Token: 0x060040B7 RID: 16567 RVA: 0x001BAD64 File Offset: 0x001B8F64
	public void toggleAgeSpeedAction()
	{
		float world_ages_speed_multiplier = this._map_stats.world_ages_speed_multiplier;
		float num;
		if (world_ages_speed_multiplier <= 1f)
		{
			if (world_ages_speed_multiplier == 0.5f)
			{
				num = 1f;
				goto IL_6E;
			}
			if (world_ages_speed_multiplier == 1f)
			{
				num = 2f;
				goto IL_6E;
			}
		}
		else
		{
			if (world_ages_speed_multiplier == 2f)
			{
				num = 5f;
				goto IL_6E;
			}
			if (world_ages_speed_multiplier == 5f)
			{
				num = 10f;
				goto IL_6E;
			}
			if (world_ages_speed_multiplier == 10f)
			{
				num = 20f;
				goto IL_6E;
			}
		}
		num = 1f;
		IL_6E:
		float tValue = num;
		this._era_manager.setAgesSpeedMultiplier(tValue);
		this.updateElements();
	}

	// Token: 0x060040B8 RID: 16568 RVA: 0x001BADF4 File Offset: 0x001B8FF4
	private void updateElements()
	{
		WorldAgeAsset tCurrentAge = this._era_manager.getCurrentAge();
		this._age_name.text = LocalizedTextManager.getText(tCurrentAge.getLocaleID(), null, false);
		this._age_name.color = tCurrentAge.title_color;
		this.updatePiePieces();
		this.updateAgeButtonSelectors();
		this._age_wheel.updateElements();
		this._pause_button_icon.sprite = (this._era_manager.isPaused() ? this._play_sprite : this._pause_sprite);
		Image age_speed_button_icon = this._age_speed_button_icon;
		float world_ages_speed_multiplier = this._map_stats.world_ages_speed_multiplier;
		Sprite sprite;
		if (world_ages_speed_multiplier <= 2f)
		{
			if (world_ages_speed_multiplier == 0.5f)
			{
				sprite = this._age_speed_sprite_slow;
				goto IL_10C;
			}
			if (world_ages_speed_multiplier == 1f)
			{
				sprite = this._age_speed_sprite_normal;
				goto IL_10C;
			}
			if (world_ages_speed_multiplier == 2f)
			{
				sprite = this._age_speed_sprite_fast;
				goto IL_10C;
			}
		}
		else
		{
			if (world_ages_speed_multiplier == 5f)
			{
				sprite = this._age_speed_sprite_fast_very;
				goto IL_10C;
			}
			if (world_ages_speed_multiplier == 10f)
			{
				sprite = this._age_speed_sprite_fast_ultra;
				goto IL_10C;
			}
			if (world_ages_speed_multiplier == 20f)
			{
				sprite = this._age_speed_sprite_fast_sonic;
				goto IL_10C;
			}
		}
		sprite = this._age_speed_sprite_normal;
		IL_10C:
		age_speed_button_icon.sprite = sprite;
		this._selected_age_background.sprite = World.world_era.getBackground();
		float tColorValue = 0.8f;
		if (this._era_manager.isPaused())
		{
			tColorValue = 0.4f;
		}
		Color tBGColor = new Color(tColorValue, tColorValue, tColorValue);
		this._selected_age_background.color = tBGColor;
		Color tFilterColor = this._background_filter.color;
		tFilterColor.r = World.world_era.title_color.r;
		tFilterColor.g = World.world_era.title_color.g;
		tFilterColor.b = World.world_era.title_color.b;
		this._background_filter.color = tFilterColor;
		this.updateTextTimeInfo();
	}

	// Token: 0x060040B9 RID: 16569 RVA: 0x001BAFB8 File Offset: 0x001B91B8
	private void updatePiePieces()
	{
		foreach (WorldAgeWheelPiece tPiece in this._age_wheel.getPieces())
		{
			bool tCurrentPiece = this.isPieceSelected(tPiece);
			tPiece.setAge(this._era_manager.getAgeFromSlot(tPiece.getIndex()));
			tPiece.toggleHighlight(tPiece.isCurrentAge());
			tPiece.toggleIconFrame(!tCurrentPiece);
			tPiece.setIconActiveColor(tPiece.isCurrentAge());
		}
	}

	// Token: 0x060040BA RID: 16570 RVA: 0x001BB044 File Offset: 0x001B9244
	private void updateAgeButtonSelectors()
	{
		int tCurrentIndex = this._era_manager.getCurrentSlotIndex();
		WorldAgeAsset tCurrentAge = this._age_wheel.getPiece(tCurrentIndex).getAsset();
		foreach (WorldAgeButton worldAgeButton in this._buttons.Values)
		{
			bool tEnabled = worldAgeButton.getAsset() == tCurrentAge;
			worldAgeButton.toggleSelectedButton(tEnabled);
			worldAgeButton.setIconActiveColor(tEnabled);
		}
	}

	// Token: 0x060040BB RID: 16571 RVA: 0x001BB0CC File Offset: 0x001B92CC
	private void updateTextTimeInfo()
	{
		using (StringBuilderPool tStringBuilderPool = new StringBuilderPool())
		{
			tStringBuilderPool.Append(Date.getUIStringYearMonth());
			tStringBuilderPool.AppendLine();
			tStringBuilderPool.Append("a: ");
			tStringBuilderPool.Append(this._map_stats.current_age_progress.ToString("P0"));
			tStringBuilderPool.AppendLine();
			tStringBuilderPool.Append("w: ");
			tStringBuilderPool.Append(string.Format("{0}/{1}", this._map_stats.world_age_slot_index + 1, 8));
			this._text_time_info.text = tStringBuilderPool.ToString();
		}
	}

	// Token: 0x060040BC RID: 16572 RVA: 0x001BB184 File Offset: 0x001B9384
	private bool isPieceSelected(WorldAgeWheelPiece pPiece)
	{
		return pPiece == this._selected_piece;
	}

	// Token: 0x04002EDF RID: 11999
	private const float SLOW = 0.5f;

	// Token: 0x04002EE0 RID: 12000
	private const float NORMAL = 1f;

	// Token: 0x04002EE1 RID: 12001
	private const float FAST = 2f;

	// Token: 0x04002EE2 RID: 12002
	private const float FAST_VERY = 5f;

	// Token: 0x04002EE3 RID: 12003
	private const float FAST_ULTRA = 10f;

	// Token: 0x04002EE4 RID: 12004
	private const float FAST_SONIC = 20f;

	// Token: 0x04002EE5 RID: 12005
	private static WorldAgesWindow _instance;

	// Token: 0x04002EE6 RID: 12006
	[SerializeField]
	private Text _age_name;

	// Token: 0x04002EE7 RID: 12007
	[SerializeField]
	private WorldAgeButton _age_button_prefab;

	// Token: 0x04002EE8 RID: 12008
	[SerializeField]
	private Sprite _play_sprite;

	// Token: 0x04002EE9 RID: 12009
	[SerializeField]
	private Sprite _pause_sprite;

	// Token: 0x04002EEA RID: 12010
	[SerializeField]
	private Sprite _age_speed_sprite_slow;

	// Token: 0x04002EEB RID: 12011
	[SerializeField]
	private Sprite _age_speed_sprite_normal;

	// Token: 0x04002EEC RID: 12012
	[SerializeField]
	private Sprite _age_speed_sprite_fast;

	// Token: 0x04002EED RID: 12013
	[SerializeField]
	private Sprite _age_speed_sprite_fast_very;

	// Token: 0x04002EEE RID: 12014
	[SerializeField]
	private Sprite _age_speed_sprite_fast_ultra;

	// Token: 0x04002EEF RID: 12015
	[SerializeField]
	private Sprite _age_speed_sprite_fast_sonic;

	// Token: 0x04002EF0 RID: 12016
	[SerializeField]
	private WorldAgeWheel _age_wheel;

	// Token: 0x04002EF1 RID: 12017
	[SerializeField]
	private Transform _grid_age_buttons;

	// Token: 0x04002EF2 RID: 12018
	[SerializeField]
	private Image _pause_button_icon;

	// Token: 0x04002EF3 RID: 12019
	[SerializeField]
	private Image _age_speed_button_icon;

	// Token: 0x04002EF4 RID: 12020
	[SerializeField]
	private Image _selected_age_background;

	// Token: 0x04002EF5 RID: 12021
	[SerializeField]
	private Image _background_filter;

	// Token: 0x04002EF6 RID: 12022
	private Dictionary<WorldAgeAsset, WorldAgeButton> _buttons = new Dictionary<WorldAgeAsset, WorldAgeButton>();

	// Token: 0x04002EF7 RID: 12023
	private WorldAgeWheelPiece _selected_piece;

	// Token: 0x04002EF8 RID: 12024
	[SerializeField]
	private Text _text_time_info;
}
