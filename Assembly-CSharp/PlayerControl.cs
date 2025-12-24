using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200032E RID: 814
public class PlayerControl
{
	// Token: 0x06001F7C RID: 8060 RVA: 0x0010FE80 File Offset: 0x0010E080
	public bool isSelectionHappens()
	{
		return this.square_selection_started;
	}

	// Token: 0x06001F7D RID: 8061 RVA: 0x0010FE88 File Offset: 0x0010E088
	public bool isAnyInputHappening()
	{
		if (InputHelpers.mouseSupported)
		{
			return InputHelpers.GetMouseButton(0);
		}
		return InputHelpers.touchCount > 0;
	}

	// Token: 0x06001F7E RID: 8062 RVA: 0x0010FEA8 File Offset: 0x0010E0A8
	internal void updateControls()
	{
		this._cached_mouse_tile_pos = this.getMouseTilePos();
		if (this.timer_spawn_pixels > 0f)
		{
			this.timer_spawn_pixels -= World.world.delta_time;
		}
		AssetManager.hotkey_library.checkHotKeyActions();
		if (PlayerControl.controlsLocked())
		{
			return;
		}
		if (this.isOverUI(false))
		{
			this._over_ui_timeout = 0.05f;
		}
		else if (this._over_ui_timeout > 0f)
		{
			this._over_ui_timeout -= Time.deltaTime;
		}
		if (PlayerControl.isControllingUnit())
		{
			this.updateTouch();
			return;
		}
		if (DebugConfig.isOn(DebugOption.MakeUnitsFollowCursor))
		{
			WorldTile tTile = this.getMouseTilePosCachedFrame();
			if (tTile != null && !this.isOverUI(true) && InputHelpers.GetMouseButtonDown(0))
			{
				Bench.bench("test_follow", "main", false);
				foreach (Actor tActor in World.world.units)
				{
					if (tActor.isFavorite() && tActor.current_tile.region.island == tTile.region.island)
					{
						tActor.stopMovement();
						tActor.goTo(this.getMouseTilePosCachedFrame(), false, false, false, 0);
					}
				}
				Bench.benchEnd("test_follow", "main", false, 0L, false);
			}
		}
		this.checkTrailerModeButtons();
		if (!Globals.TRAILER_MODE && !World.world.canvas.gameObject.activeSelf)
		{
			return;
		}
		World.world.magnet.magnetAction(true, null);
		Boulder.checkRelease();
		if (InputHelpers.GetMouseButtonUp(0))
		{
			this.timer_spawn_pixels = 0f;
		}
		if (!this.isAnyInputHappening())
		{
			this.already_used_zoom = false;
			this.already_used_camera_drag = false;
			this.touch_ticks_skip = 0;
			this.already_used_power = false;
		}
		this.updateTouch();
		if (this.controls_lock_timer > 0f)
		{
			this.controls_lock_timer -= Time.deltaTime;
			return;
		}
		if (World.world.isGameplayControlsLocked())
		{
			return;
		}
		if ((InputHelpers.GetMouseButton(1) || InputHelpers.GetMouseButton(0)) && this.isOverUI(true))
		{
			this._last_time_touched_ui = World.world.getCurSessionTime();
		}
		if (!this.checkSquareMultiSelection())
		{
			this.finishSquareSelection();
		}
		if (this.isOverUI(true))
		{
			return;
		}
		if (this.tryToMoveSelectedUnits())
		{
			return;
		}
		if (this.checkClickTouchInspectSelect())
		{
			return;
		}
		if (!this.already_used_zoom && MoveCamera.inSpectatorMode() && !MoveCamera.camera_drag_activated && InputHelpers.GetMouseButtonUp(0) && this.getDistanceBetweenOriginAndCurrentTouch() < 20f)
		{
			Actor tActor2 = ActionLibrary.getActorFromTile(this.getMouseTilePosCachedFrame());
			if (tActor2 != null)
			{
				World.world.locateAndFollow(tActor2, null, null);
				return;
			}
		}
		if (InputHelpers.GetMouseButton(1) || InputHelpers.GetMouseButton(0))
		{
			this.inspect_timer_click += Time.deltaTime;
		}
		else
		{
			this.inspect_timer_click = 0f;
		}
		if (!World.world.isAnyPowerSelected())
		{
			this.checkEmptyClick();
			return;
		}
		if (World.world.selected_buttons.selectedButton.godPower == null)
		{
			return;
		}
		if (this.already_used_zoom)
		{
			return;
		}
		if (this.already_used_camera_drag)
		{
			return;
		}
		GodPower tPower = World.world.selected_buttons.selectedButton.godPower;
		if (!Globals.TRAILER_MODE)
		{
			this.highlightCursor(tPower);
		}
		if (InputHelpers.touchCount > 1)
		{
			this.already_used_zoom = true;
			this.already_used_camera_drag = true;
			this.already_used_power = false;
			return;
		}
		if (InputHelpers.touchCount > 0 && this.touch_ticks_skip < 5 && tPower.hold_action)
		{
			return;
		}
		bool tMultiSpawn = HotkeyLibrary.many_mod.isHolding() || tPower.hold_action;
		if (!tMultiSpawn && !tPower.ignore_fast_spawn && tPower.type == PowerActionType.PowerSpawnActor && DebugConfig.isOn(DebugOption.FastSpawn))
		{
			tMultiSpawn = true;
		}
		if (tMultiSpawn)
		{
			if (!InputHelpers.GetMouseButton(0))
			{
				this._last_click.Set(-1, -1);
				this.first_pressed_tile = null;
				this.first_pressed_type = null;
				this.first_pressed_top_type = null;
				this.first_click = true;
				this.click_started_at = (double)Time.time;
				return;
			}
			if (!this.first_click && DebugConfig.isOn(DebugOption.UltraFastSpawn))
			{
				for (int i = 0; i < 10; i++)
				{
					this.clickedStart();
				}
				return;
			}
			this.clickedStart();
			return;
		}
		else
		{
			bool tClicked = false;
			if (InputHelpers.touchSupported && InputHelpers.touchCount > 0 && InputHelpers.GetMouseButtonUp(0))
			{
				tClicked = true;
			}
			else if (Input.mousePresent && InputHelpers.GetMouseButtonDown(0))
			{
				tClicked = true;
			}
			if (tClicked)
			{
				this.clickedStart();
				return;
			}
			this.first_pressed_tile = null;
			this.first_pressed_type = null;
			this.first_pressed_top_type = null;
			this.first_click = true;
			return;
		}
	}

	// Token: 0x06001F7F RID: 8063 RVA: 0x00110318 File Offset: 0x0010E518
	private void updateTouch()
	{
		if (InputHelpers.touchSupported)
		{
			if (InputHelpers.touchCount > 0 && InputHelpers.GetMouseButtonDown(0) && this.isOverUI(true))
			{
				this.already_used_power = true;
				return;
			}
			if (InputHelpers.touchCount == 0)
			{
				this.touch_ticks_skip = 0;
				this.already_used_zoom = false;
				this.already_used_camera_drag = false;
				this.already_used_power = false;
				this._origin_touch = Vector2.zero;
				this._current_touch = Vector2.zero;
				return;
			}
			if (InputHelpers.touchCount > 0)
			{
				this.touch_ticks_skip++;
			}
			if (InputHelpers.touchCount == 1)
			{
				Vector2 tVectorTouch = Input.GetTouch(0).position;
				if (this._origin_touch == Vector2.zero)
				{
					this._origin_touch = tVectorTouch;
				}
				else
				{
					this._current_touch = tVectorTouch;
				}
			}
			else
			{
				this._origin_touch = Vector2.zero;
			}
			if (!this.already_used_power && InputHelpers.touchCount > 1 && this.isTouchMoreThanDragThreshold())
			{
				this.already_used_camera_drag = true;
			}
		}
	}

	// Token: 0x06001F80 RID: 8064 RVA: 0x00110405 File Offset: 0x0010E605
	public bool isTouchMoreThanDragThreshold()
	{
		return !(this._origin_touch == Vector2.zero) && !(this._current_touch == Vector2.zero) && this.getCurrentDragDistance() > 0.007f;
	}

	// Token: 0x06001F81 RID: 8065 RVA: 0x0011043C File Offset: 0x0010E63C
	public float getCurrentDragDistance()
	{
		if (this._origin_touch == Vector2.zero || this._current_touch == Vector2.zero)
		{
			return 0f;
		}
		float distanceBetweenOriginAndCurrentTouch = this.getDistanceBetweenOriginAndCurrentTouch();
		float tDiag = Mathf.Sqrt((float)(Screen.width * Screen.width + Screen.height * Screen.height));
		return distanceBetweenOriginAndCurrentTouch / tDiag;
	}

	// Token: 0x06001F82 RID: 8066 RVA: 0x00110499 File Offset: 0x0010E699
	public float getDistanceBetweenOriginAndCurrentTouch()
	{
		if (this._origin_touch == Vector2.zero || this._current_touch == Vector2.zero)
		{
			return 0f;
		}
		return Toolbox.DistVec2Float(this._origin_touch, this._current_touch);
	}

	// Token: 0x06001F83 RID: 8067 RVA: 0x001104D8 File Offset: 0x0010E6D8
	private bool checkSquareMultiSelection()
	{
		if (ControllableUnit.isControllingUnit())
		{
			return false;
		}
		if (World.world.isAnyPowerSelected())
		{
			return false;
		}
		if (ScrollWindow.isWindowActive() || ScrollWindow.isAnimationActive())
		{
			return false;
		}
		if (MapBox.isRenderMiniMap())
		{
			this.square_selection_started = false;
			this._ignore_square_selection = true;
			return true;
		}
		if (!InputHelpers.mouseSupported)
		{
			return false;
		}
		if (!InputHelpers.GetMouseButton(0))
		{
			return false;
		}
		if (InputHelpers.GetMouseButton(1))
		{
			return false;
		}
		if (InputHelpers.GetMouseButton(2))
		{
			return false;
		}
		if (!this.square_selection_started && !this._ignore_square_selection)
		{
			if (this.isOverUI(true))
			{
				this._ignore_square_selection = true;
			}
			else
			{
				this.square_selection_started = true;
				this.square_selection_position_current = this.getMousePos();
			}
		}
		return true;
	}

	// Token: 0x06001F84 RID: 8068 RVA: 0x00110580 File Offset: 0x0010E780
	private void finishSquareSelection()
	{
		if (!this._ignore_square_selection && this.square_selection_started)
		{
			this.checkSelectedUnits();
			this.square_selection_position_start_last.Set(this.square_selection_position_current.x, this.square_selection_position_current.y);
			this._square_selection_ended_frame = Time.frameCount;
		}
		this._ignore_square_selection = false;
		this.square_selection_started = false;
		this.square_selection_position_current.Set(-1f, -1f);
	}

	// Token: 0x06001F85 RID: 8069 RVA: 0x001105F4 File Offset: 0x0010E7F4
	private void checkSelectedUnits()
	{
		using (ListPool<Actor> tList = this.getUnitsToBeSelected())
		{
			if (tList != null && tList.Count != 0)
			{
				if (!HotkeyLibrary.isHoldingControlForSelection() && !HotkeyLibrary.many_mod.isHolding())
				{
					SelectedUnit.clear();
				}
				if (HotkeyLibrary.isHoldingControlForSelection())
				{
					using (ListPool<Actor>.Enumerator enumerator = tList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ref Actor ptr = ref enumerator.Current;
							SelectedUnit.unselect(ptr);
						}
						goto IL_6B;
					}
				}
				SelectedUnit.selectMultiple(tList);
				IL_6B:
				if (SelectedUnit.isSet())
				{
					SelectedObjects.setNanoObject(SelectedUnit.unit);
					if (tList.Count == 1)
					{
						PowerTabController.showTabSelectedUnit();
					}
					else
					{
						PowerTabController.showTabMultipleUnits();
					}
				}
			}
		}
	}

	// Token: 0x06001F86 RID: 8070 RVA: 0x001106BC File Offset: 0x0010E8BC
	public bool isSquareSelectionMinSizeAchievedLast()
	{
		Vector2 vector = this.square_selection_position_start_last;
		Vector2 pEnd = this.getMousePos();
		int x0 = Mathf.FloorToInt(vector.x);
		int y0 = Mathf.FloorToInt(vector.y);
		int x = Mathf.FloorToInt(pEnd.x);
		int y = Mathf.FloorToInt(pEnd.y);
		if (x0 == -1 || y0 == -1 || x == -1 || y == -1)
		{
			return false;
		}
		int tXStart = Mathf.Min(x0, x);
		int num = Mathf.Max(x0, x);
		int tYStart = Mathf.Min(y0, y);
		int tYEnd = Mathf.Max(y0, y);
		return num - tXStart >= 2 || tYEnd - tYStart >= 2;
	}

	// Token: 0x06001F87 RID: 8071 RVA: 0x00110750 File Offset: 0x0010E950
	public ListPool<Actor> getUnitsToBeSelected()
	{
		Vector2 vector = this.square_selection_position_current;
		Vector2 pEnd = this.getMousePos();
		int x0 = Mathf.FloorToInt(vector.x);
		int y0 = Mathf.FloorToInt(vector.y);
		int x = Mathf.FloorToInt(pEnd.x);
		int y = Mathf.FloorToInt(pEnd.y);
		if (x0 == -1 || y0 == -1 || x == -1 || y == -1)
		{
			return null;
		}
		int tXStart = Mathf.Min(x0, x);
		int tXEnd = Mathf.Max(x0, x);
		int tYStart = Mathf.Min(y0, y);
		int tYEnd = Mathf.Max(y0, y);
		if (tXEnd - tXStart < 2 && tYEnd - tYStart < 2)
		{
			return null;
		}
		tXStart = Mathf.Clamp(tXStart, 0, MapBox.width - 1);
		tXEnd = Mathf.Clamp(tXEnd, 0, MapBox.width - 1);
		tYStart = Mathf.Clamp(tYStart, 0, MapBox.height - 1);
		tYEnd = Mathf.Clamp(tYEnd, 0, MapBox.height - 1);
		ListPool<Actor> tList = new ListPool<Actor>();
		Action<Actor> <>9__0;
		for (int x2 = tXStart; x2 <= tXEnd; x2++)
		{
			for (int y2 = tYStart; y2 <= tYEnd; y2++)
			{
				WorldTile tTile = World.world.GetTile(x2, y2);
				if (tTile != null)
				{
					WorldTile worldTile = tTile;
					Action<Actor> pAction;
					if ((pAction = <>9__0) == null)
					{
						pAction = (<>9__0 = delegate(Actor tActor)
						{
							if (tActor.isInsideSomething())
							{
								return;
							}
							if (!tActor.asset.can_be_inspected)
							{
								return;
							}
							tList.Add(tActor);
						});
					}
					worldTile.doUnits(pAction);
				}
			}
		}
		return tList;
	}

	// Token: 0x06001F88 RID: 8072 RVA: 0x001108A4 File Offset: 0x0010EAA4
	internal void clickedFinal(Vector2Int pPos, GodPower pPower = null, bool pTrack = true)
	{
		if (pPower == null)
		{
			pPower = World.world.selected_buttons.selectedButton.godPower;
		}
		if (pPower.requires_premium && !Config.hasPremium)
		{
			ScrollWindow.showWindow("steam");
			return;
		}
		WorldTile tTile = World.world.GetTile(pPos.x, pPos.y);
		LogText.log("Clicked", string.Concat(new string[]
		{
			pPower.id,
			" ",
			tTile.pos.x.ToString(),
			":",
			tTile.pos.y.ToString()
		}), "");
		if (pPower.click_special_action != null)
		{
			pPower.click_special_action(tTile, pPower.id);
			return;
		}
		if (this.first_pressed_type == null)
		{
			this.first_pressed_tile = tTile;
			this.first_pressed_type = tTile.main_type;
			this.first_pressed_top_type = tTile.top_type;
		}
		if (pPower.click_interval > 0f)
		{
			if (this._click_timer >= 0f)
			{
				this._click_timer -= World.world.delta_time;
				return;
			}
			this._click_timer = pPower.click_interval;
		}
		if (pPower.click_power_action != null || pPower.click_power_brush_action != null)
		{
			if (pPower.click_power_brush_action != null)
			{
				pPower.click_power_brush_action(tTile, pPower);
			}
			else if (pPower.click_power_action != null)
			{
				pPower.click_power_action(tTile, pPower);
			}
			if (pTrack)
			{
				PowerTracker.PlusOne(pPower);
			}
			return;
		}
		if (pPower.click_action != null || pPower.click_brush_action != null)
		{
			if (pPower.click_brush_action != null)
			{
				pPower.click_brush_action(tTile, pPower.id);
			}
			else if (pPower.click_action != null)
			{
				pPower.click_action(tTile, pPower.id);
			}
			if (pTrack)
			{
				PowerTracker.PlusOne(pPower);
			}
			return;
		}
	}

	// Token: 0x06001F89 RID: 8073 RVA: 0x00110A80 File Offset: 0x0010EC80
	private void checkEmptyClick()
	{
		if (!InputHelpers.GetMouseButtonUp(0))
		{
			return;
		}
		Vector2Int tPos;
		if (!PixelDetector.GetSpritePixelColorUnderMousePointer(World.world, out tPos) || tPos.x == -1)
		{
			this._last_click.Set(-1, -1);
			return;
		}
		if (World.world.GetTile(tPos.x, tPos.y) == null)
		{
			return;
		}
		foreach (Actor tActor in World.world.units)
		{
			if (Toolbox.Dist(tActor.current_tile.posV3.x, tActor.current_tile.posV3.y, (float)tPos.x, (float)tPos.y) <= 10f)
			{
				WorldAction action_click = tActor.asset.action_click;
				if (action_click != null)
				{
					action_click(tActor, tActor.current_tile);
				}
			}
		}
	}

	// Token: 0x06001F8A RID: 8074 RVA: 0x00110B70 File Offset: 0x0010ED70
	internal void clickedStart()
	{
		this.already_used_power = true;
		Vector2Int tPos;
		if (!PixelDetector.GetSpritePixelColorUnderMousePointer(World.world, out tPos) || tPos.x == -1)
		{
			this._last_click.Set(-1, -1);
			return;
		}
		GodPower tPower = World.world.selected_buttons.selectedButton.godPower;
		string tBrushId = Config.current_brush;
		if (tPower != null && !string.IsNullOrEmpty(tPower.force_brush))
		{
			tBrushId = tPower.force_brush;
		}
		BrushData tBrushData = Brush.get(tBrushId);
		if (tBrushData.continuous && tPower.draw_lines && this._last_click.x != -1 && (tPos.x != this._last_click.x || tPos.y != this._last_click.y))
		{
			int tLen = (int)(Toolbox.Dist(tPos.x, tPos.y, this._last_click.x, this._last_click.y) / (float)(tBrushData.size + 1)) + 1;
			for (int i = 0; i < tLen; i++)
			{
				Vector2 a = new Vector2((float)tPos.x, (float)tPos.y);
				Vector2 b = new Vector2((float)this._last_click.x, (float)this._last_click.y);
				Vector2 tVec = Vector2.Lerp(a, b, (float)i / (float)tLen);
				Vector2Int tVec2 = new Vector2Int((int)tVec.x, (int)tVec.y);
				if (tVec2.x >= 0 && tVec2.x < MapBox.width && tVec2.y >= 0 && tVec2.y < MapBox.height)
				{
					this.clickedFinal(tVec2, tPower, false);
				}
			}
		}
		this.clickedFinal(tPos, tPower, true);
		this.first_click = false;
		this._last_click.Set(tPos.x, tPos.y);
	}

	// Token: 0x06001F8B RID: 8075 RVA: 0x00110D44 File Offset: 0x0010EF44
	private bool tryToMoveSelectedUnits()
	{
		if (InputHelpers.mouseSupported)
		{
			if (!InputHelpers.GetMouseButtonUp(1))
			{
				return false;
			}
		}
		else if (!InputHelpers.GetMouseButtonUp(0))
		{
			return false;
		}
		if (!SelectedUnit.isSet())
		{
			return false;
		}
		if (MoveCamera.camera_drag_activated)
		{
			return false;
		}
		if (this.already_used_camera_drag)
		{
			return false;
		}
		if (this.inspect_timer_click > 0.15f)
		{
			return false;
		}
		WorldTile tTile = this.getMouseTilePosCachedFrame();
		Vector3 tPosEffect = this.getMousePos();
		if (tTile != null)
		{
			tPosEffect = tTile.posV3;
		}
		BaseEffect tEffect = EffectsLibrary.spawnAt("fx_move", tPosEffect, 0.1f);
		if (tEffect != null)
		{
			tEffect.sprite_renderer.color = World.world.getArchitectColor();
		}
		this.inspect_timer_click = 0.16f;
		if (tTile == null)
		{
			return false;
		}
		bool tControlPressed = HotkeyLibrary.isHoldingControlForSelection();
		Actor tActorToAttack = this.getActorTargetNearCursor();
		bool result;
		using (ListPool<Actor> tList = new ListPool<Actor>(SelectedUnit.getAllSelected()))
		{
			tList.Remove(SelectedUnit.unit);
			tList.Insert(0, SelectedUnit.unit);
			foreach (Actor ptr in tList)
			{
				Actor tActor = ptr;
				bool tResult = false;
				if (tControlPressed && tActorToAttack != null)
				{
					tActor.cancelAllBeh();
					tActor.addAggro(tActorToAttack);
					tActor.startFightingWith(tActorToAttack);
					if (Toolbox.DistTile(tActor.current_tile, tActorToAttack.current_tile) > 7f)
					{
						this.tryToMoveSelectedUnit(tActor, tTile, false);
					}
				}
				else
				{
					tResult = this.tryToMoveSelectedUnit(tActor, tTile, true);
				}
				if (tResult)
				{
					tTile = tTile.getNeighbourTileSameIsland();
				}
			}
			QuantumSpriteLibrary.last_order_timestamp = World.world.getCurSessionTime();
			result = true;
		}
		return result;
	}

	// Token: 0x06001F8C RID: 8076 RVA: 0x00110EF4 File Offset: 0x0010F0F4
	private Actor getActorTargetNearCursor()
	{
		Actor tActorToHit = World.world.getActorNearCursor();
		if (tActorToHit == null || !tActorToHit.isAlive())
		{
			return null;
		}
		return tActorToHit;
	}

	// Token: 0x06001F8D RID: 8077 RVA: 0x00110F1C File Offset: 0x0010F11C
	private bool tryToMoveSelectedUnit(Actor pActor, WorldTile pTile, bool pCancelBeh = true)
	{
		if (!pActor.asset.allow_strange_urge_movement)
		{
			return false;
		}
		if (pTile.Type.block)
		{
			return false;
		}
		if (pActor.asset.id == "dragon" && !pActor.isFlying())
		{
			return false;
		}
		pActor.stopSleeping();
		bool tAllowWater = pTile.Type.liquid;
		if (pActor.isWaterCreature() || pActor.asset.is_boat)
		{
			tAllowWater = true;
		}
		if (!tAllowWater && !pTile.isSameIsland(pActor.current_tile))
		{
			tAllowWater = true;
		}
		bool tAllowLava = pTile.Type.lava;
		if (pCancelBeh)
		{
			pActor.cancelAllBeh();
			pActor.stopMovement();
		}
		pActor.goTo(pTile, tAllowWater, false, tAllowLava, 0);
		pActor.addStatusEffect("strange_urge", 100f, false);
		pActor.clearWait();
		return true;
	}

	// Token: 0x06001F8E RID: 8078 RVA: 0x00110FE4 File Offset: 0x0010F1E4
	private bool checkClickTouchInspectSelect()
	{
		if (!this.canInspectWithMainTouch() && !this.canInspectWithRightClick())
		{
			return false;
		}
		if (!DebugConfig.isOn(DebugOption.InspectObjectsOnClick))
		{
			return false;
		}
		if (MoveCamera.camera_drag_activated)
		{
			return false;
		}
		if (this._over_ui_timeout > 0f)
		{
			return false;
		}
		if (this.already_used_zoom)
		{
			return false;
		}
		if (this.already_used_camera_drag)
		{
			return false;
		}
		if (this.isActionHappening())
		{
			return false;
		}
		if (this.inspect_timer_click > 0.15f)
		{
			return false;
		}
		if (MoveCamera.inSpectatorMode())
		{
			return false;
		}
		if (World.world.getCurSessionTime() - this._last_time_touched_ui < 0.20000000298023224)
		{
			return false;
		}
		if (Toolbox.DistVec2Float(this._origin_touch, this._current_touch) >= 20f)
		{
			return false;
		}
		NameplateText tNameplateOverCursor = World.world.nameplate_manager.cursor_over_text;
		if (tNameplateOverCursor != null)
		{
			NanoObject tNanoObjectOverCursor = tNameplateOverCursor.nano_object;
			tNanoObjectOverCursor.getMetaType().getAsset().selectAndInspect(tNanoObjectOverCursor, true, true, false);
			return true;
		}
		WorldTile tCursorTile = this.getMouseTilePosCachedFrame();
		if (tCursorTile == null)
		{
			return true;
		}
		MetaTypeAsset tMetaAsset = World.world.getCachedMapMetaAsset();
		if (MapBox.isRenderMiniMap() && tMetaAsset != null && Zones.showMapBorders())
		{
			tMetaAsset.click_action_zone(tCursorTile, null);
		}
		else
		{
			bool tMultiSelection = false;
			if (HotkeyLibrary.many_mod.isHolding())
			{
				if (SelectedUnit.isSet())
				{
					tMultiSelection = true;
				}
				if (ActionLibrary.inspectUnitSelectedMeta(null, null))
				{
					return true;
				}
			}
			if (this.isAllowedToSelectUnits())
			{
				Actor tResult = World.world.getActorNearCursor();
				if (tResult != null)
				{
					if (SelectedUnit.isSelected(tResult))
					{
						if (SelectedUnit.isMainSelected(tResult))
						{
							if (this.isSquareSelectionMinSizeAchievedLast())
							{
								if (Time.frameCount != this._square_selection_ended_frame)
								{
									ActionLibrary.inspectUnit(null, null);
								}
							}
							else
							{
								ActionLibrary.inspectUnit(null, null);
							}
						}
						else
						{
							SelectedUnit.makeMainSelected(tResult);
						}
					}
					else
					{
						if (!tMultiSelection)
						{
							SelectedUnit.clear();
						}
						SelectedUnit.select(tResult, true);
						SelectedObjects.setNanoObject(tResult);
						PowerTabController.showTabSelectedUnit();
					}
				}
			}
			else if (!ScrollWindow.isWindowActive())
			{
				ActionLibrary.inspectUnit(null, null);
			}
		}
		return true;
	}

	// Token: 0x06001F8F RID: 8079 RVA: 0x001111B8 File Offset: 0x0010F3B8
	public void updateCurrentPosition()
	{
		if (this._last_check == Time.frameCount)
		{
			return;
		}
		this._last_check = Time.frameCount;
		bool tFound = false;
		if (InputHelpers.touchSupported && InputHelpers.touchCount != 0)
		{
			if (this._event_data_current_position == null)
			{
				this._event_data_current_position = new PointerEventData(EventSystem.current);
			}
			this._event_data_current_position.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
			tFound = true;
		}
		if (!tFound && InputHelpers.mouseSupported && Input.mousePosition.x >= 0f && Input.mousePosition.y >= 0f && Input.mousePosition.x <= (float)Screen.width && Input.mousePosition.y <= (float)Screen.height)
		{
			if (this._event_data_current_position == null)
			{
				this._event_data_current_position = new PointerEventData(EventSystem.current);
			}
			this._event_data_current_position.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			tFound = true;
		}
		if (!tFound)
		{
			this._event_data_current_position = null;
		}
	}

	// Token: 0x06001F90 RID: 8080 RVA: 0x001112DC File Offset: 0x0010F4DC
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isBusyWithUI()
	{
		return ScrollWindow.isWindowActive() || ScrollWindow.isAnimationActive() || this.isOverUI(true);
	}

	// Token: 0x06001F91 RID: 8081 RVA: 0x001112F5 File Offset: 0x0010F4F5
	public bool isOverUI(bool pCheckTimeout = true)
	{
		return (pCheckTimeout && this._over_ui_timeout > 0f) || this.isPointerOverUIObject();
	}

	// Token: 0x06001F92 RID: 8082 RVA: 0x00111314 File Offset: 0x0010F514
	private bool isAllowedToSelectUnits()
	{
		return true;
	}

	// Token: 0x06001F93 RID: 8083 RVA: 0x00111317 File Offset: 0x0010F517
	private bool canInspectUnitWithCurrentPower()
	{
		return !World.world.isAnyPowerSelected() || World.world.selected_buttons.selectedButton.godPower.allow_unit_selection;
	}

	// Token: 0x06001F94 RID: 8084 RVA: 0x00111345 File Offset: 0x0010F545
	private bool canInspectWithRightClick()
	{
		return InputHelpers.mouseSupported && this.canInspectUnitWithCurrentPower() && Input.GetMouseButtonUp(1);
	}

	// Token: 0x06001F95 RID: 8085 RVA: 0x00111360 File Offset: 0x0010F560
	private bool canInspectWithMainTouch()
	{
		return this.canInspectUnitWithCurrentPower() && Input.GetMouseButtonUp(0);
	}

	// Token: 0x06001F96 RID: 8086 RVA: 0x00111372 File Offset: 0x0010F572
	public bool isPointerInGame()
	{
		this.updateCurrentPosition();
		return this._event_data_current_position != null;
	}

	// Token: 0x06001F97 RID: 8087 RVA: 0x00111388 File Offset: 0x0010F588
	public bool isPointerOverUIObject()
	{
		if (PlayerControl._is_pointer_over_ui_object != null)
		{
			return PlayerControl._is_pointer_over_ui_object.Value;
		}
		this.updateCurrentPosition();
		if (this._event_data_current_position == null)
		{
			return false;
		}
		List<RaycastResult> tResults = this._results;
		EventSystem.current.RaycastAll(this._event_data_current_position, tResults);
		PlayerControl._is_pointer_over_ui_object = new bool?(tResults.Count > 0);
		return PlayerControl._is_pointer_over_ui_object.Value;
	}

	// Token: 0x06001F98 RID: 8088 RVA: 0x001113F4 File Offset: 0x0010F5F4
	public bool isPointerOverUIButton()
	{
		this.updateCurrentPosition();
		if (this._event_data_current_position == null)
		{
			return false;
		}
		List<RaycastResult> tResults = this._results;
		EventSystem.current.RaycastAll(this._event_data_current_position, tResults);
		for (int i = 0; i < tResults.Count; i++)
		{
			RaycastResult tObj = tResults[i];
			if (tObj.isValid)
			{
				GameObject tGameObject = tObj.gameObject;
				if (tGameObject.HasComponent<Button>())
				{
					return true;
				}
				if (tGameObject.HasComponent<EventTrigger>())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001F99 RID: 8089 RVA: 0x00111468 File Offset: 0x0010F668
	public bool isTouchOverUI(Touch pTouch)
	{
		return pTouch.phase != TouchPhase.Ended && pTouch.phase != TouchPhase.Canceled && EventSystem.current.IsPointerOverGameObject(pTouch.fingerId);
	}

	// Token: 0x06001F9A RID: 8090 RVA: 0x00111494 File Offset: 0x0010F694
	public bool isPointerOverUIScroll()
	{
		this.updateCurrentPosition();
		if (this._event_data_current_position == null)
		{
			return false;
		}
		List<RaycastResult> tResults = this._results;
		if (tResults.Count == 0)
		{
			EventSystem.current.RaycastAll(this._event_data_current_position, tResults);
		}
		for (int i = 0; i < tResults.Count; i++)
		{
			RaycastResult obj = tResults[i];
			if (!obj.isValid)
			{
				return false;
			}
			this._gui_check_game_object = obj.gameObject;
			if (this._gui_check_game_object == null)
			{
				return false;
			}
			if (this._gui_check_game_object.HasComponent<ScrollRectExtended>())
			{
				return true;
			}
			if (this._gui_check_game_object.name == "Scroll View")
			{
				Transform content = this._gui_check_game_object.transform.Find("Viewport/Content");
				if (content != null)
				{
					Vector2 scrollViewSize = this._gui_check_game_object.GetComponent<RectTransform>().sizeDelta;
					if (content.GetComponent<RectTransform>().sizeDelta.y > scrollViewSize.y)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06001F9B RID: 8091 RVA: 0x0011158C File Offset: 0x0010F78C
	public bool isActionHappening()
	{
		if (Earthquake.isQuakeActive())
		{
			return true;
		}
		AutoTesterBot auto_tester = World.world.auto_tester;
		return (auto_tester == null || !auto_tester.active) && ((Input.touchSupported && Input.touchCount > 1) || (Input.mousePresent && (Input.GetMouseButton(0) || Input.GetMouseButton(2))));
	}

	// Token: 0x06001F9C RID: 8092 RVA: 0x001115E7 File Offset: 0x0010F7E7
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool controlsLocked()
	{
		return MapBox.instance.tutorial.isActive() || Config.lockGameControls || Config.isDraggingItem();
	}

	// Token: 0x06001F9D RID: 8093 RVA: 0x0011160F File Offset: 0x0010F80F
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool isControllingUnit()
	{
		return ControllableUnit.isControllingUnit() || MapBox.instance.stack_effects.isLocked();
	}

	// Token: 0x06001F9E RID: 8094 RVA: 0x00111630 File Offset: 0x0010F830
	private void highlightCursor(GodPower pPower)
	{
		if (!Config.isComputer && !Config.isEditor)
		{
			return;
		}
		WorldTile tCursorTile = World.world.getMouseTilePos();
		if (tCursorTile == null)
		{
			return;
		}
		World.world.flash_effects.flashPixel(tCursorTile, 10, ColorType.White);
		if (pPower != null)
		{
			if (!string.IsNullOrEmpty(pPower.force_brush))
			{
				this.highlightFrom(tCursorTile, Brush.get(pPower.force_brush));
				return;
			}
			if (pPower.highlight || pPower.show_tool_sizes)
			{
				this.highlightFrom(tCursorTile, Config.current_brush_data);
			}
		}
	}

	// Token: 0x06001F9F RID: 8095 RVA: 0x001116B0 File Offset: 0x0010F8B0
	private void highlightFrom(WorldTile pTile, BrushData pBrushData)
	{
		for (int i = 0; i < pBrushData.pos.Length; i++)
		{
			WorldTile tTile = World.world.GetTile(pBrushData.pos[i].x + pTile.x, pBrushData.pos[i].y + pTile.y);
			if (tTile != null)
			{
				World.world.flash_effects.flashPixel(tTile, 20, ColorType.White);
			}
		}
	}

	// Token: 0x06001FA0 RID: 8096 RVA: 0x00111721 File Offset: 0x0010F921
	public void clearLateUpdate()
	{
		PlayerControl._is_pointer_over_ui_object = null;
		this._results.Clear();
	}

	// Token: 0x06001FA1 RID: 8097 RVA: 0x00111739 File Offset: 0x0010F939
	public void clear()
	{
		this._results.Clear();
		this.first_click = true;
		this._cached_mouse_tile_pos = null;
	}

	// Token: 0x06001FA2 RID: 8098 RVA: 0x00111754 File Offset: 0x0010F954
	public Vector2 getMousePos()
	{
		Vector2 tMousePos = Input.mousePosition;
		return World.world.camera.ScreenToWorldPoint(tMousePos);
	}

	// Token: 0x06001FA3 RID: 8099 RVA: 0x00111786 File Offset: 0x0010F986
	public WorldTile getMouseTilePosCachedFrame()
	{
		return this._cached_mouse_tile_pos;
	}

	// Token: 0x06001FA4 RID: 8100 RVA: 0x00111790 File Offset: 0x0010F990
	public WorldTile getMouseTilePos()
	{
		Vector2Int tPos;
		if (!PixelDetector.GetSpritePixelColorUnderMousePointer(World.world, out tPos))
		{
			return null;
		}
		return World.world.GetTile(tPos.x, tPos.y);
	}

	// Token: 0x06001FA5 RID: 8101 RVA: 0x001117C8 File Offset: 0x0010F9C8
	public bool getTouchPos(out Touch pTouch, bool pOnlyGameplay = false)
	{
		pTouch = default(Touch);
		bool tResult = false;
		foreach (Touch tTouch in Input.touches)
		{
			if (!pOnlyGameplay || !World.world.isTouchOverUI(tTouch))
			{
				pTouch = tTouch;
				tResult = true;
				break;
			}
		}
		return tResult;
	}

	// Token: 0x06001FA6 RID: 8102 RVA: 0x00111818 File Offset: 0x0010FA18
	internal void checkTrailerModeButtons()
	{
		if (!Globals.TRAILER_MODE)
		{
			return;
		}
		if (!ScrollWindow.isWindowActive() && Input.GetKeyDown(KeyCode.F9))
		{
			World.world.canvas.gameObject.SetActive(!World.world.canvas.gameObject.activeSelf);
		}
		if (Input.GetKeyDown(KeyCode.BackQuote))
		{
			TrailerModeSettings.startEvent();
		}
		if (Input.GetKeyDown(KeyCode.F8))
		{
			DebugConfig.switchOption(DebugOption.FastSpawn);
		}
		if (Input.GetKeyDown(KeyCode.F7))
		{
			DebugConfig.switchOption(DebugOption.SonicSpeed);
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Config.paused = !Config.paused;
		}
		if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
		{
			World.world.selected_buttons.clickPowerButton(PowerButton.get("seeds"));
			return;
		}
		if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
		{
			World.world.selected_buttons.clickPowerButton(PowerButton.get("tile_shallow_waters"));
			return;
		}
		if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
		{
			World.world.selected_buttons.clickPowerButton(PowerButton.get("fruit_bush"));
			return;
		}
		if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
		{
			World.world.selected_buttons.clickPowerButton(PowerButton.get("cat"));
			return;
		}
		if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5))
		{
			World.world.selected_buttons.clickPowerButton(PowerButton.get("atomic_bomb"));
			return;
		}
		if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6))
		{
			World.world.selected_buttons.clickPowerButton(PowerButton.get("czar_bomba"));
		}
	}

	// Token: 0x040016EB RID: 5867
	public const int TOUCH_POWER_ACTIVATION_FRAMES = 5;

	// Token: 0x040016EC RID: 5868
	internal float timer_spawn_pixels;

	// Token: 0x040016ED RID: 5869
	private float _over_ui_timeout;

	// Token: 0x040016EE RID: 5870
	private GameObject _gui_check_game_object;

	// Token: 0x040016EF RID: 5871
	private static bool? _is_pointer_over_ui_object;

	// Token: 0x040016F0 RID: 5872
	private Vector2Int _last_click;

	// Token: 0x040016F1 RID: 5873
	private Vector2 _origin_touch;

	// Token: 0x040016F2 RID: 5874
	private Vector2 _current_touch;

	// Token: 0x040016F3 RID: 5875
	internal WorldTile first_pressed_tile;

	// Token: 0x040016F4 RID: 5876
	internal TileType first_pressed_type;

	// Token: 0x040016F5 RID: 5877
	internal TopTileType first_pressed_top_type;

	// Token: 0x040016F6 RID: 5878
	internal bool first_click = true;

	// Token: 0x040016F7 RID: 5879
	public double click_started_at;

	// Token: 0x040016F8 RID: 5880
	private float _click_timer;

	// Token: 0x040016F9 RID: 5881
	private int _last_check = -1;

	// Token: 0x040016FA RID: 5882
	internal float inspect_timer_click;

	// Token: 0x040016FB RID: 5883
	internal int touch_ticks_skip;

	// Token: 0x040016FC RID: 5884
	private double _last_time_touched_ui;

	// Token: 0x040016FD RID: 5885
	internal bool already_used_zoom;

	// Token: 0x040016FE RID: 5886
	internal bool already_used_camera_drag;

	// Token: 0x040016FF RID: 5887
	internal bool already_used_power;

	// Token: 0x04001700 RID: 5888
	internal float controls_lock_timer;

	// Token: 0x04001701 RID: 5889
	private PointerEventData _event_data_current_position = new PointerEventData(EventSystem.current);

	// Token: 0x04001702 RID: 5890
	private readonly List<RaycastResult> _results = new List<RaycastResult>();

	// Token: 0x04001703 RID: 5891
	public Vector2 square_selection_position_current;

	// Token: 0x04001704 RID: 5892
	private Vector2 square_selection_position_start_last;

	// Token: 0x04001705 RID: 5893
	public bool square_selection_started;

	// Token: 0x04001706 RID: 5894
	private bool _ignore_square_selection;

	// Token: 0x04001707 RID: 5895
	private int _square_selection_ended_frame;

	// Token: 0x04001708 RID: 5896
	private WorldTile _cached_mouse_tile_pos;

	// Token: 0x04001709 RID: 5897
	public const float DRAG_DETECTION_THRESHOLD_PERCENT = 0.007f;
}
