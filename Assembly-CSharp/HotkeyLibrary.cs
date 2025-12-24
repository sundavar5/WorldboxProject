using System;
using System.Collections.Generic;
using System.Globalization;
using Beebyte.Obfuscator;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200010C RID: 268
[ObfuscateLiterals]
[Serializable]
public class HotkeyLibrary : AssetLibrary<HotkeyAsset>
{
	// Token: 0x0600082A RID: 2090 RVA: 0x000720E0 File Offset: 0x000702E0
	public unsafe override void init()
	{
		base.init();
		this.addHotkeysForUnitControlLayer();
		HotkeyAsset hotkeyAsset = new HotkeyAsset();
		hotkeyAsset.id = "fullscreen_switch";
		hotkeyAsset.default_key_1 = KeyCode.Return;
		hotkeyAsset.default_key_mod_1 = KeyCode.LeftAlt;
		hotkeyAsset.just_pressed_action = delegate(HotkeyAsset _)
		{
			PlayerConfig.toggleFullScreen();
		};
		HotkeyLibrary.fullscreen_switch = this.add(hotkeyAsset);
		HotkeyAsset hotkeyAsset2 = new HotkeyAsset();
		hotkeyAsset2.id = "console";
		hotkeyAsset2.default_key_1 = KeyCode.Tilde;
		hotkeyAsset2.default_key_2 = KeyCode.BackQuote;
		hotkeyAsset2.check_controls_locked = true;
		hotkeyAsset2.just_pressed_action = delegate(HotkeyAsset _)
		{
			if (EventSystem.current.currentSelectedGameObject == null)
			{
				World.world.console.Toggle();
			}
		};
		HotkeyLibrary.console = this.add(hotkeyAsset2);
		HotkeyLibrary.cancel = this.add(new HotkeyAsset
		{
			id = "cancel",
			default_key_1 = KeyCode.Escape,
			just_pressed_action = new HotkeyAction(this.escapeAction)
		});
		this.add(new HotkeyAsset
		{
			id = "back",
			default_key_1 = KeyCode.Mouse3,
			just_pressed_action = new HotkeyAction(this.backAction)
		});
		HotkeyAsset hotkeyAsset3 = new HotkeyAsset();
		hotkeyAsset3.id = "pause";
		hotkeyAsset3.default_key_1 = KeyCode.Space;
		hotkeyAsset3.check_window_not_active = true;
		hotkeyAsset3.check_controls_locked = true;
		hotkeyAsset3.just_pressed_action = delegate(HotkeyAsset _)
		{
			Config.paused = !Config.paused;
		};
		HotkeyLibrary.pause = this.add(hotkeyAsset3);
		HotkeyAsset hotkeyAsset4 = new HotkeyAsset();
		hotkeyAsset4.id = "hide_ui";
		hotkeyAsset4.default_key_1 = KeyCode.H;
		hotkeyAsset4.check_window_not_active = true;
		hotkeyAsset4.check_controls_locked = true;
		hotkeyAsset4.just_pressed_action = delegate(HotkeyAsset _)
		{
			Config.ui_main_hidden = !Config.ui_main_hidden;
		};
		HotkeyLibrary.hide_ui = this.add(hotkeyAsset4);
		HotkeyAsset hotkeyAsset5 = new HotkeyAsset();
		hotkeyAsset5.id = "remove";
		hotkeyAsset5.default_key_1 = KeyCode.Delete;
		hotkeyAsset5.default_key_2 = KeyCode.Backspace;
		hotkeyAsset5.check_window_not_active = true;
		hotkeyAsset5.check_controls_locked = true;
		hotkeyAsset5.just_pressed_action = delegate(HotkeyAsset _)
		{
			if (SelectedUnit.isSet())
			{
				SelectedUnit.killSelected();
				return;
			}
			string tPowerToSelect = "life_eraser";
			if (World.world.isSelectedPower("life_eraser"))
			{
				tPowerToSelect = "demolish";
			}
			World.world.selected_buttons.clickPowerButton(PowerButton.get(tPowerToSelect));
		};
		HotkeyLibrary.remove = this.add(hotkeyAsset5);
		HotkeyAsset hotkeyAsset6 = new HotkeyAsset();
		hotkeyAsset6.id = "zoom";
		hotkeyAsset6.use_mouse_wheel = true;
		hotkeyAsset6.holding_cooldown = 0f;
		hotkeyAsset6.check_window_not_active = true;
		hotkeyAsset6.check_controls_locked = true;
		hotkeyAsset6.allow_unit_control = true;
		hotkeyAsset6.holding_action = delegate(HotkeyAsset pAsset)
		{
			if (!World.world.isPointerInGame())
			{
				return;
			}
			if (World.world.isOverUI() && !MoveCamera.inSpectatorMode())
			{
				return;
			}
			float tWheelValue = Input.mouseScrollDelta.y;
			if (tWheelValue < 0f)
			{
				MoveCamera.zoomOutWheel(pAsset);
				return;
			}
			if (tWheelValue > 0f)
			{
				MoveCamera.zoomInWheel(pAsset);
			}
		};
		HotkeyLibrary.zoom = this.add(hotkeyAsset6);
		HotkeyAsset hotkeyAsset7 = new HotkeyAsset();
		hotkeyAsset7.id = "world_speed";
		hotkeyAsset7.default_key_mod_1 = KeyCode.LeftControl;
		hotkeyAsset7.default_key_mod_2 = KeyCode.RightControl;
		hotkeyAsset7.default_key_mod_3 = KeyCode.LeftMeta;
		hotkeyAsset7.check_window_not_active = true;
		hotkeyAsset7.check_controls_locked = true;
		hotkeyAsset7.use_mouse_wheel = true;
		hotkeyAsset7.holding_cooldown = 0f;
		hotkeyAsset7.holding_action = delegate(HotkeyAsset _)
		{
			float tWheelValue = Input.mouseScrollDelta.y;
			WorldTimeScaleAsset tOldScaleAsset = Config.time_scale_asset;
			if (tWheelValue < 0f)
			{
				Config.prevWorldSpeed();
			}
			else if (tWheelValue > 0f)
			{
				Config.nextWorldSpeed(false);
			}
			if (tOldScaleAsset != Config.time_scale_asset)
			{
				string tLocalizedHotkeyText = LocalizedTextManager.getText("changed_worldspeed", null, false);
				string tLocalizedBrush;
				if (Config.time_scale_asset.getLocaleID() != null)
				{
					tLocalizedBrush = Toolbox.coloredText(Config.time_scale_asset.getLocaleID(), "#95DD5D", true);
				}
				else
				{
					tLocalizedBrush = Toolbox.coloredText(Config.time_scale_asset.id, "#95DD5D", false);
				}
				tLocalizedHotkeyText = tLocalizedHotkeyText.Replace("$speed$", tLocalizedBrush);
				WorldTip.instance.showToolbarText(tLocalizedHotkeyText);
			}
		};
		HotkeyLibrary.world_speed = this.add(hotkeyAsset7);
		HotkeyAsset hotkeyAsset8 = new HotkeyAsset();
		hotkeyAsset8.id = "brush";
		hotkeyAsset8.default_key_mod_1 = KeyCode.LeftAlt;
		hotkeyAsset8.default_key_mod_2 = KeyCode.RightAlt;
		hotkeyAsset8.check_window_not_active = true;
		hotkeyAsset8.check_controls_locked = true;
		hotkeyAsset8.use_mouse_wheel = true;
		hotkeyAsset8.holding_cooldown = 0f;
		hotkeyAsset8.holding_action = delegate(HotkeyAsset _)
		{
			float tWheelValue = Input.mouseScrollDelta.y;
			string tOldBrush = Config.current_brush;
			if (tWheelValue < 0f)
			{
				BrushLibrary.nextBrush();
			}
			else if (tWheelValue > 0f)
			{
				BrushLibrary.previousBrush();
			}
			if (tOldBrush != Config.current_brush)
			{
				BrushData tNewBrush = Brush.get(Config.current_brush);
				string localeID = tNewBrush.getLocaleID();
				string tLocalizedHotkeyText = LocalizedTextManager.getText("changed_brush", null, false);
				string tLocalizedBrush = Toolbox.coloredText(localeID, "#95DD5D", true);
				tLocalizedBrush = tLocalizedBrush + " (" + Toolbox.coloredText(tNewBrush.size.ToString(), "#95DD5D", false) + ")";
				tLocalizedHotkeyText = tLocalizedHotkeyText.Replace("$brush$", tLocalizedBrush);
				WorldTip.instance.showToolbarText(tLocalizedHotkeyText);
			}
		};
		HotkeyLibrary.brush = this.add(hotkeyAsset8);
		HotkeyLibrary.many_mod = this.add(new HotkeyAsset
		{
			id = "many_mod",
			default_key_mod_1 = KeyCode.RightShift,
			default_key_mod_2 = KeyCode.LeftShift,
			disable_for_controlled_unit = true,
			check_only_not_controllable_unit = true
		});
		HotkeyLibrary.fast_civ_mod = this.add(new HotkeyAsset
		{
			id = "fast_civ_mod",
			default_key_mod_1 = KeyCode.RightControl,
			default_key_mod_2 = KeyCode.LeftControl
		});
		HotkeyLibrary.left = this.add(new HotkeyAsset
		{
			id = "left",
			default_key_1 = KeyCode.A,
			default_key_2 = KeyCode.LeftArrow,
			holding_action = new HotkeyAction(MoveCamera.move),
			holding_cooldown = 0f,
			check_window_not_active = true,
			check_controls_locked = true,
			allow_unit_control = true
		});
		HotkeyLibrary.right = this.clone("right", "left");
		this.t.default_key_1 = KeyCode.D;
		this.t.default_key_2 = KeyCode.RightArrow;
		HotkeyLibrary.up = this.clone("up", "left");
		this.t.default_key_1 = KeyCode.W;
		this.t.default_key_2 = KeyCode.UpArrow;
		HotkeyLibrary.down = this.clone("down", "left");
		this.t.default_key_1 = KeyCode.S;
		this.t.default_key_2 = KeyCode.DownArrow;
		this.clone("fast_left", "left");
		this.t.default_key_mod_1 = KeyCode.RightShift;
		this.t.default_key_mod_2 = KeyCode.LeftShift;
		this.clone("fast_right", "right");
		this.t.default_key_mod_1 = KeyCode.RightShift;
		this.t.default_key_mod_2 = KeyCode.LeftShift;
		this.clone("fast_up", "up");
		this.t.default_key_mod_1 = KeyCode.RightShift;
		this.t.default_key_mod_2 = KeyCode.LeftShift;
		this.clone("fast_down", "down");
		this.t.default_key_mod_1 = KeyCode.RightShift;
		this.t.default_key_mod_2 = KeyCode.LeftShift;
		HotkeyLibrary.zoom_in = this.add(new HotkeyAsset
		{
			id = "zoom_in",
			default_key_1 = KeyCode.Q,
			default_key_2 = KeyCode.Plus,
			default_key_3 = KeyCode.KeypadPlus,
			check_window_not_active = true,
			check_controls_locked = true,
			holding_action = new HotkeyAction(MoveCamera.zoomIn),
			holding_cooldown = 0f
		});
		HotkeyLibrary.zoom_out = this.add(new HotkeyAsset
		{
			id = "zoom_out",
			default_key_1 = KeyCode.E,
			default_key_2 = KeyCode.Minus,
			default_key_3 = KeyCode.KeypadMinus,
			check_window_not_active = true,
			check_controls_locked = true,
			holding_action = new HotkeyAction(MoveCamera.zoomOut),
			holding_cooldown = 0f
		});
		this.add(new HotkeyAsset
		{
			id = "power_left",
			default_key_1 = KeyCode.LeftArrow,
			default_key_2 = KeyCode.A,
			default_key_mod_1 = KeyCode.LeftControl,
			default_key_mod_2 = KeyCode.LeftMeta,
			default_key_mod_3 = KeyCode.RightControl,
			check_window_not_active = true,
			check_controls_locked = true,
			just_pressed_action = new HotkeyAction(this.powerMove),
			holding_action = new HotkeyAction(this.powerMove)
		});
		this.clone("power_right", "power_left");
		this.t.default_key_1 = KeyCode.RightArrow;
		this.t.default_key_2 = KeyCode.D;
		this.clone("power_up", "power_left");
		this.t.default_key_1 = KeyCode.UpArrow;
		this.t.default_key_2 = KeyCode.W;
		this.clone("power_down", "power_left");
		this.t.default_key_1 = KeyCode.DownArrow;
		this.t.default_key_2 = KeyCode.S;
		HotkeyAsset hotkeyAsset9 = new HotkeyAsset();
		hotkeyAsset9.id = "toggle_power";
		hotkeyAsset9.default_key_1 = KeyCode.Return;
		hotkeyAsset9.default_key_2 = KeyCode.KeypadEnter;
		hotkeyAsset9.check_controls_locked = true;
		hotkeyAsset9.just_pressed_action = delegate(HotkeyAsset _)
		{
			PowerButton tButton = PowersTab.getActiveTab().getActiveButton();
			if (tButton == null)
			{
				return;
			}
			if (tButton.godPower != null)
			{
				string id = tButton.godPower.id;
				if (id == "clock")
				{
					Config.nextWorldSpeed(true);
					return;
				}
				if (id == "pause")
				{
					tButton.clickSpecial();
					return;
				}
				PowerButtonClickAction select_button_action = tButton.godPower.select_button_action;
				if (select_button_action != null)
				{
					select_button_action(tButton.godPower.id);
				}
				if (tButton.godPower.toggle_action != null)
				{
					PowerToggleAction toggle_action = tButton.godPower.toggle_action;
					if (toggle_action != null)
					{
						toggle_action(tButton.godPower.id);
					}
					PowerButtonSelector.instance.checkToggleIcons();
					return;
				}
			}
			else
			{
				if (tButton.type == PowerButtonType.Options)
				{
					tButton.gameObject.GetComponent<Button>().onClick.Invoke();
					return;
				}
				tButton.clickButton();
			}
		};
		this.add(hotkeyAsset9);
		this.clone("toggle_power2", "toggle_power");
		this.t.default_key_mod_1 = KeyCode.LeftControl;
		this.t.default_key_mod_2 = KeyCode.LeftMeta;
		HotkeyAsset hotkeyAsset10 = new HotkeyAsset();
		hotkeyAsset10.id = "next_tab";
		hotkeyAsset10.default_key_1 = KeyCode.Tab;
		hotkeyAsset10.check_window_not_active = true;
		hotkeyAsset10.check_controls_locked = true;
		hotkeyAsset10.check_no_multi_unit_selection = true;
		hotkeyAsset10.just_pressed_action = delegate(HotkeyAsset _)
		{
			Button next = PowerTabController.instance.getNext(PowersTab.getActiveTab().name);
			PowersTab.showTabFromButton(next, false);
			TipButton tTip = next.gameObject.GetComponent<TipButton>();
			string tTipKey = LocalizedTextManager.getText(tTip.textOnClick, null, false) + "\n" + LocalizedTextManager.getText(tTip.textOnClickDescription, null, false);
			WorldTip.instance.showToolbarText(tTipKey);
		};
		HotkeyLibrary.next_tab = this.add(hotkeyAsset10);
		HotkeyAsset hotkeyAsset11 = new HotkeyAsset();
		hotkeyAsset11.id = "prev_tab";
		hotkeyAsset11.default_key_1 = KeyCode.Tab;
		hotkeyAsset11.default_key_mod_1 = KeyCode.LeftShift;
		hotkeyAsset11.default_key_mod_2 = KeyCode.RightShift;
		hotkeyAsset11.check_window_not_active = true;
		hotkeyAsset11.check_controls_locked = true;
		hotkeyAsset11.check_no_multi_unit_selection = true;
		hotkeyAsset11.just_pressed_action = delegate(HotkeyAsset _)
		{
			PowersTab.showTabFromButton(PowerTabController.instance.getPrev(PowersTab.getActiveTab().name), false);
		};
		HotkeyLibrary.prev_tab = this.add(hotkeyAsset11);
		this.add(new HotkeyAsset
		{
			id = "hotkey_1",
			default_key_1 = KeyCode.Alpha1,
			default_key_2 = KeyCode.Keypad1,
			check_window_not_active = true,
			check_controls_locked = true,
			just_pressed_action = delegate(HotkeyAsset pAsset)
			{
				string tHotkey = pAsset.id;
				string tHotkeyTabData = *this.getHotkeyFromData(tHotkey);
				if (!string.IsNullOrEmpty(tHotkeyTabData))
				{
					this.hotkeySelectNano(pAsset, tHotkeyTabData);
					return;
				}
				string tValue = PlayerConfig.dict[tHotkey].stringVal;
				this.hotkeySelectPower(pAsset, tValue);
			}
		});
		this.clone("hotkey_2", "hotkey_1");
		this.t.default_key_1 = KeyCode.Alpha2;
		this.t.default_key_2 = KeyCode.Keypad2;
		this.clone("hotkey_3", "hotkey_1");
		this.t.default_key_1 = KeyCode.Alpha3;
		this.t.default_key_2 = KeyCode.Keypad3;
		this.clone("hotkey_4", "hotkey_1");
		this.t.default_key_1 = KeyCode.Alpha4;
		this.t.default_key_2 = KeyCode.Keypad4;
		this.clone("hotkey_5", "hotkey_1");
		this.t.default_key_1 = KeyCode.Alpha5;
		this.t.default_key_2 = KeyCode.Keypad5;
		this.clone("hotkey_6", "hotkey_1");
		this.t.default_key_1 = KeyCode.Alpha6;
		this.t.default_key_2 = KeyCode.Keypad6;
		this.clone("hotkey_7", "hotkey_1");
		this.t.default_key_1 = KeyCode.Alpha7;
		this.t.default_key_2 = KeyCode.Keypad7;
		this.clone("hotkey_8", "hotkey_1");
		this.t.default_key_1 = KeyCode.Alpha8;
		this.t.default_key_2 = KeyCode.Keypad8;
		this.clone("hotkey_9", "hotkey_1");
		this.t.default_key_1 = KeyCode.Alpha9;
		this.t.default_key_2 = KeyCode.Keypad9;
		this.clone("hotkey_0", "hotkey_1");
		this.t.default_key_1 = KeyCode.Alpha0;
		this.t.default_key_2 = KeyCode.Keypad0;
		this.add(new HotkeyAsset
		{
			id = "save_hotkey_1",
			default_key_1 = KeyCode.Alpha1,
			default_key_2 = KeyCode.Keypad1,
			default_key_mod_1 = KeyCode.LeftControl,
			default_key_mod_2 = KeyCode.LeftMeta,
			check_window_not_active = true,
			check_controls_locked = true,
			just_pressed_action = delegate(HotkeyAsset pAsset)
			{
				if (SelectedObjects.isNanoObjectSet())
				{
					this.hotkeySaveTab(pAsset);
					return;
				}
				this.hotkeySavePower(pAsset);
			}
		});
		this.clone("save_hotkey_2", "save_hotkey_1");
		this.t.default_key_1 = KeyCode.Alpha2;
		this.t.default_key_2 = KeyCode.Keypad2;
		this.clone("save_hotkey_3", "save_hotkey_1");
		this.t.default_key_1 = KeyCode.Alpha3;
		this.t.default_key_2 = KeyCode.Keypad3;
		this.clone("save_hotkey_4", "save_hotkey_1");
		this.t.default_key_1 = KeyCode.Alpha4;
		this.t.default_key_2 = KeyCode.Keypad4;
		this.clone("save_hotkey_5", "save_hotkey_1");
		this.t.default_key_1 = KeyCode.Alpha5;
		this.t.default_key_2 = KeyCode.Keypad5;
		this.clone("save_hotkey_6", "save_hotkey_1");
		this.t.default_key_1 = KeyCode.Alpha6;
		this.t.default_key_2 = KeyCode.Keypad6;
		this.clone("save_hotkey_7", "save_hotkey_1");
		this.t.default_key_1 = KeyCode.Alpha7;
		this.t.default_key_2 = KeyCode.Keypad7;
		this.clone("save_hotkey_8", "save_hotkey_1");
		this.t.default_key_1 = KeyCode.Alpha8;
		this.t.default_key_2 = KeyCode.Keypad8;
		this.clone("save_hotkey_9", "save_hotkey_1");
		this.t.default_key_1 = KeyCode.Alpha9;
		this.t.default_key_2 = KeyCode.Keypad9;
		this.clone("save_hotkey_0", "save_hotkey_1");
		this.t.default_key_1 = KeyCode.Alpha0;
		this.t.default_key_2 = KeyCode.Keypad0;
		this.add(new HotkeyAsset
		{
			id = "zone_type_previous",
			default_key_1 = KeyCode.Z,
			check_window_not_active = true,
			check_controls_locked = true,
			just_pressed_action = delegate(HotkeyAsset _)
			{
				this.switchZones(-1);
			}
		});
		this.clone("zone_type_next", "zone_type_previous");
		this.t.just_pressed_action = delegate(HotkeyAsset _)
		{
			this.switchZones(1);
		};
		this.t.default_key_1 = KeyCode.X;
		this.add(new HotkeyAsset
		{
			id = "zone_type_state_next",
			default_key_1 = KeyCode.C,
			check_window_not_active = true,
			check_controls_locked = true,
			just_pressed_action = delegate(HotkeyAsset _)
			{
				this.toggleZones(1);
			}
		});
		this.clone("zone_type_state_previous", "zone_type_state_next");
		this.t.just_pressed_action = delegate(HotkeyAsset _)
		{
			this.toggleZones(-1);
		};
		this.t.default_key_mod_1 = KeyCode.LeftControl;
		this.t.default_key_mod_2 = KeyCode.LeftMeta;
		HotkeyAsset hotkeyAsset12 = new HotkeyAsset();
		hotkeyAsset12.id = "follow_unit";
		hotkeyAsset12.default_key_1 = KeyCode.F;
		hotkeyAsset12.check_window_not_active = false;
		hotkeyAsset12.check_controls_locked = true;
		hotkeyAsset12.just_pressed_action = delegate(HotkeyAsset _)
		{
			Actor tSelectedActor = SelectedUnit.unit;
			if (!ScrollWindow.isWindowActive())
			{
				if (MapBox.isRenderGameplay())
				{
					Actor tActor = World.world.getActorNearCursor();
					if (tActor == null)
					{
						if (MoveCamera.hasFocusUnit())
						{
							MoveCamera.clearFocusUnitOnly();
							return;
						}
						if (SelectedUnit.isSet())
						{
							World.world.followUnit(tSelectedActor);
							return;
						}
						return;
					}
					else
					{
						if (tActor.isCameraFollowingUnit())
						{
							MoveCamera.clearFocusUnitOnly();
							return;
						}
						World.world.followUnit(tActor);
					}
				}
				return;
			}
			ScrollWindow tActiveWindow = ScrollWindow.getCurrentWindow();
			if (tActiveWindow.screen_id != "unit")
			{
				return;
			}
			if (tActiveWindow.GetComponent<UnitWindow>().name_input.inputField.isFocused)
			{
				return;
			}
			if (SelectedUnit.isSet())
			{
				World.world.followUnit(tSelectedActor);
				ScrollWindow.hideAllEvent(true);
			}
		};
		HotkeyLibrary.follow_unit = this.add(hotkeyAsset12);
		HotkeyAsset hotkeyAsset13 = new HotkeyAsset();
		hotkeyAsset13.id = "control_unit";
		hotkeyAsset13.default_key_1 = KeyCode.G;
		hotkeyAsset13.check_window_not_active = false;
		hotkeyAsset13.just_pressed_action = delegate(HotkeyAsset _)
		{
			if (MoveCamera.hasFocusUnit())
			{
				World.world.move_camera.clearFocusUnitAndUnselect();
			}
			Actor tSelectedActor = SelectedUnit.unit;
			if (!ScrollWindow.isWindowActive())
			{
				if (MapBox.isRenderGameplay())
				{
					Actor tActorCursor = World.world.getActorNearCursor();
					if (ControllableUnit.isControllingUnit())
					{
						if (ControllableUnit.isControllingUnit(tActorCursor))
						{
							ControllableUnit.clear(true);
							return;
						}
						if (tActorCursor != null)
						{
							ControllableUnit.clear(true);
							ControllableUnit.setControllableCreature(tActorCursor);
							return;
						}
						if (tActorCursor == null)
						{
							ControllableUnit.clear(true);
							return;
						}
					}
					if (tActorCursor == null)
					{
						if (SelectedUnit.isSet())
						{
							ControllableUnit.setControllableCreatureAndSelected(tSelectedActor);
							return;
						}
						return;
					}
					else
					{
						ControllableUnit.setControllableCreatureAndSelected(tActorCursor);
					}
				}
				return;
			}
			ScrollWindow tActiveWindow = ScrollWindow.getCurrentWindow();
			if (tActiveWindow.screen_id != "unit")
			{
				return;
			}
			if (tActiveWindow.GetComponent<UnitWindow>().name_input.inputField.isFocused)
			{
				return;
			}
			if (SelectedUnit.isSet())
			{
				ControllableUnit.setControllableCreature(tSelectedActor);
				ScrollWindow.hideAllEvent(true);
			}
		};
		HotkeyLibrary.control_unit = this.add(hotkeyAsset13);
		HotkeyAsset hotkeyAsset14 = new HotkeyAsset();
		hotkeyAsset14.id = "meta_window_previous";
		hotkeyAsset14.default_key_1 = KeyCode.LeftArrow;
		hotkeyAsset14.default_key_2 = KeyCode.Q;
		hotkeyAsset14.default_key_3 = KeyCode.A;
		hotkeyAsset14.just_pressed_action = delegate(HotkeyAsset _)
		{
			MetaSwitchManager.switchWindows(MetaSwitchManager.Direction.Left);
		};
		hotkeyAsset14.check_controls_locked = true;
		hotkeyAsset14.check_window_active = true;
		this.add(hotkeyAsset14);
		this.clone("meta_window_next", "meta_window_previous");
		this.t.default_key_1 = KeyCode.RightArrow;
		this.t.default_key_2 = KeyCode.E;
		this.t.default_key_3 = KeyCode.D;
		this.t.just_pressed_action = delegate(HotkeyAsset _)
		{
			MetaSwitchManager.switchWindows(MetaSwitchManager.Direction.Right);
		};
		this.add(new HotkeyAsset
		{
			id = "window_tab_next",
			default_key_1 = KeyCode.Tab,
			default_key_2 = KeyCode.S,
			default_key_3 = KeyCode.DownArrow,
			just_pressed_action = new HotkeyAction(this.windowTabsSwitch),
			check_controls_locked = true,
			check_window_active = true
		});
		this.clone("window_tab_previous", "window_tab_next");
		this.t.default_key_mod_1 = KeyCode.LeftShift;
		this.t.default_key_mod_2 = KeyCode.RightShift;
		this.clone("window_tab_previous_2", "window_tab_next");
		this.t.default_key_1 = KeyCode.W;
		this.t.default_key_2 = KeyCode.UpArrow;
		this.t.default_key_3 = KeyCode.None;
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x000730CC File Offset: 0x000712CC
	private void addHotkeysForUnitControlLayer()
	{
		HotkeyAsset hotkeyAsset = new HotkeyAsset();
		hotkeyAsset.id = "next_unit_in_multi_selection";
		hotkeyAsset.default_key_1 = KeyCode.Tab;
		hotkeyAsset.check_window_not_active = true;
		hotkeyAsset.check_controls_locked = true;
		hotkeyAsset.check_multi_unit_selection = true;
		hotkeyAsset.ignore_same_key_diagnostic = true;
		hotkeyAsset.just_pressed_action = delegate(HotkeyAsset _)
		{
			SelectedUnit.nextMainUnit();
		};
		HotkeyLibrary.next_unit_in_multi_selection = this.add(hotkeyAsset);
		HotkeyLibrary.action_jump = this.add(new HotkeyAsset
		{
			id = "action_jump",
			default_key_1 = KeyCode.Space,
			ignore_same_key_diagnostic = true,
			check_window_not_active = true,
			check_controls_locked = true,
			check_only_controllable_unit = true
		});
		HotkeyLibrary.action_dash = this.add(new HotkeyAsset
		{
			id = "action_dash",
			default_key_1 = KeyCode.LeftShift,
			default_key_2 = KeyCode.RightShift,
			ignore_same_key_diagnostic = true,
			check_window_not_active = true,
			check_controls_locked = true,
			ignore_mod_keys = true,
			check_only_controllable_unit = true
		});
		HotkeyLibrary.action_backstep = this.add(new HotkeyAsset
		{
			id = "action_backstep",
			default_key_1 = KeyCode.LeftControl,
			default_key_2 = KeyCode.RightControl,
			ignore_same_key_diagnostic = true,
			check_window_not_active = true,
			check_controls_locked = true,
			ignore_mod_keys = true,
			check_only_controllable_unit = true
		});
		HotkeyLibrary.action_swear = this.add(new HotkeyAsset
		{
			id = "action_swear",
			default_key_1 = KeyCode.F,
			ignore_same_key_diagnostic = true,
			check_window_not_active = true,
			check_controls_locked = true,
			check_only_controllable_unit = true
		});
		HotkeyLibrary.action_steal = this.add(new HotkeyAsset
		{
			id = "action_steal",
			default_key_1 = KeyCode.Q,
			ignore_same_key_diagnostic = true,
			check_window_not_active = true,
			check_controls_locked = true,
			check_only_controllable_unit = true
		});
		HotkeyLibrary.action_talk = this.add(new HotkeyAsset
		{
			id = "action_talk",
			default_key_1 = KeyCode.T,
			ignore_same_key_diagnostic = true,
			check_window_not_active = true,
			check_controls_locked = true,
			check_only_controllable_unit = true
		});
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x000732E4 File Offset: 0x000714E4
	private void switchZones(int pIndexChange)
	{
		MetaType tMetaType = Zones.getCurrentMapBorderMode(true);
		int tCurrentEnabledIndex = Array.IndexOf<MetaType>(this._meta_zones, tMetaType);
		tCurrentEnabledIndex += pIndexChange;
		tCurrentEnabledIndex = Toolbox.loopIndex(tCurrentEnabledIndex, this._meta_zones.Length);
		tMetaType = this._meta_zones[tCurrentEnabledIndex];
		MetaTypeAsset tMetaTypeAsset = AssetManager.meta_type_library.getAsset(tMetaType);
		AssetManager.powers.get(tMetaTypeAsset.power_option_zone_id).toggle_action(tMetaTypeAsset.power_option_zone_id);
		PowerButtonSelector.instance.checkToggleIcons();
		GodPower tPower = AssetManager.powers.get(tMetaTypeAsset.power_option_zone_id);
		WorldTip.instance.showToolbarText(tPower, true);
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x00073374 File Offset: 0x00071574
	private void toggleZones(int pIndexChange)
	{
		MetaType tMetaType = Zones.getCurrentMapBorderMode(true);
		if (tMetaType == MetaType.None)
		{
			return;
		}
		MetaTypeAsset tMetaTypeAsset = AssetManager.meta_type_library.getAsset(tMetaType);
		GodPower tPower = AssetManager.powers.get(tMetaTypeAsset.power_option_zone_id);
		if (!tPower.multi_toggle)
		{
			return;
		}
		tMetaTypeAsset.toggleOptionZone(tPower, pIndexChange, false);
		PowerButtonSelector.instance.checkToggleIcons();
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x000733C8 File Offset: 0x000715C8
	private void windowTabsSwitch(HotkeyAsset pAsset)
	{
		ScrollWindow tWindow = ScrollWindow.getCurrentWindow();
		List<WindowMetaTab> tContentTabs = tWindow.tabs.getContentTabs();
		if (tContentTabs.Count < 2)
		{
			return;
		}
		WindowMetaTab tActiveTab = tWindow.tabs.getActiveTab();
		int tIndex = tContentTabs.IndexOf(tActiveTab);
		string id = pAsset.id;
		if (!(id == "window_tab_next"))
		{
			if (id == "window_tab_previous" || id == "window_tab_previous_2")
			{
				tIndex--;
			}
		}
		else
		{
			tIndex++;
		}
		tIndex = Toolbox.loopIndex(tIndex, tContentTabs.Count);
		WindowMetaTab windowMetaTab = tContentTabs[tIndex];
		windowMetaTab.doAction();
		WorldTip.showNowTop(windowMetaTab.getWorldTipText(), false);
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x00073468 File Offset: 0x00071668
	private bool navigateWindowBack(HotkeyAsset pAsset)
	{
		if (!ScrollWindow.isWindowActive())
		{
			return false;
		}
		if (ScrollWindow.isAnimationActive())
		{
			ScrollWindow.finishAnimations();
		}
		WindowHistory.clickBack();
		return true;
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x00073485 File Offset: 0x00071685
	private bool navigateTabBack(HotkeyAsset pAsset)
	{
		return !ScrollWindow.isWindowActive() && SelectedTabsHistory.showPreviousTab();
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x0007349A File Offset: 0x0007169A
	private void backAction(HotkeyAsset pAsset)
	{
		if (this.navigateWindowBack(pAsset))
		{
			return;
		}
		if (!this.navigateTabBack(pAsset))
		{
			if (PowersTab.getActiveTab().getAsset().tab_type_main)
			{
				return;
			}
			PowerTabController.showMainTab();
		}
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x000734C8 File Offset: 0x000716C8
	private void escapeAction(HotkeyAsset pAsset)
	{
		if (World.world.console.isActive())
		{
			World.world.console.Hide();
			return;
		}
		if (ControllableUnit.isControllingUnit())
		{
			ControllableUnit.clear(true);
			return;
		}
		if (World.world.tutorial.isActive())
		{
			World.world.tutorial.endTutorial();
			return;
		}
		if (MapBox.controlsLocked())
		{
			return;
		}
		if (MapBox.isControllingUnit())
		{
			return;
		}
		if (MoveCamera.hasFocusUnit())
		{
			MoveCamera.clearFocusUnitOnly();
			return;
		}
		if (this.navigateWindowBack(pAsset))
		{
			return;
		}
		if (Config.ui_main_hidden)
		{
			Config.ui_main_hidden = false;
			return;
		}
		if (this.navigateTabBack(pAsset))
		{
			return;
		}
		if (World.world.selected_buttons.selectedButton != null)
		{
			World.world.selected_buttons.unselectAll();
			return;
		}
		if (SelectedUnit.isSet())
		{
			SelectedUnit.clear();
			return;
		}
		if (PowersTab.isTabSelected())
		{
			World.world.selected_buttons.unselectTabs();
			SelectedObjects.unselectNanoObject();
			return;
		}
		ScrollWindow.showWindow("quit_game");
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x000735C0 File Offset: 0x000717C0
	private void powerMove(HotkeyAsset pAsset)
	{
		PowersTab tActiveTab = PowersTab.getActiveTab();
		string id = pAsset.id;
		if (id == "power_left")
		{
			tActiveTab.leftButton();
			return;
		}
		if (id == "power_right")
		{
			tActiveTab.rightButton();
			return;
		}
		if (id == "power_up")
		{
			tActiveTab.upButton();
			return;
		}
		if (!(id == "power_down"))
		{
			return;
		}
		tActiveTab.downButton();
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x0007362C File Offset: 0x0007182C
	public override void linkAssets()
	{
		base.linkAssets();
		HashSet<KeyCode> tModKeys = new HashSet<KeyCode>();
		HashSet<HotkeyAsset> tActionHotkeys = new HashSet<HotkeyAsset>();
		foreach (HotkeyAsset tAsset in this.list)
		{
			tAsset.overridden_key_1 = tAsset.default_key_1;
			tAsset.overridden_key_2 = tAsset.default_key_2;
			tAsset.overridden_key_3 = tAsset.default_key_3;
			tAsset.overridden_key_mod_1 = tAsset.default_key_mod_1;
			tAsset.overridden_key_mod_2 = tAsset.default_key_mod_2;
			tAsset.overridden_key_mod_3 = tAsset.default_key_mod_3;
			if (tAsset.default_key_mod_1 != KeyCode.None)
			{
				tModKeys.Add(tAsset.default_key_mod_1);
			}
			if (tAsset.default_key_mod_2 != KeyCode.None)
			{
				tModKeys.Add(tAsset.default_key_mod_2);
			}
			if (tAsset.default_key_mod_3 != KeyCode.None)
			{
				tModKeys.Add(tAsset.default_key_mod_3);
			}
			if (tAsset.just_pressed_action != null)
			{
				tActionHotkeys.Add(tAsset);
			}
			else if (tAsset.holding_action != null)
			{
				tActionHotkeys.Add(tAsset);
			}
		}
		HotkeyLibrary.mod_keys = tModKeys.ToArray<KeyCode>();
		this.action_hotkeys = tActionHotkeys.ToArray<HotkeyAsset>();
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x00073750 File Offset: 0x00071950
	public override void editorDiagnostic()
	{
		base.editorDiagnostic();
		Dictionary<string, HotkeyAsset> tKeys = new Dictionary<string, HotkeyAsset>();
		foreach (HotkeyAsset tAsset in this.list)
		{
			if (!tAsset.ignore_same_key_diagnostic)
			{
				string tPre = "";
				if (tAsset.check_window_active)
				{
					tPre += "ui+";
				}
				using (ListPool<string> tAllKeys = new ListPool<string>())
				{
					bool tHasMod = tAsset.default_key_mod_1 > KeyCode.None;
					if (tAsset.default_key_1 != KeyCode.None)
					{
						if (tHasMod)
						{
							if (tAsset.default_key_mod_1 != KeyCode.None)
							{
								tAllKeys.Add(tPre + tAsset.default_key_1.ToString() + "+" + tAsset.default_key_mod_1.ToString());
							}
							if (tAsset.default_key_mod_2 != KeyCode.None)
							{
								tAllKeys.Add(tPre + tAsset.default_key_1.ToString() + "+" + tAsset.default_key_mod_2.ToString());
							}
							if (tAsset.default_key_mod_3 != KeyCode.None)
							{
								tAllKeys.Add(tPre + tAsset.default_key_1.ToString() + "+" + tAsset.default_key_mod_3.ToString());
							}
						}
						else
						{
							tAllKeys.Add(tPre + tAsset.default_key_1.ToString());
						}
					}
					if (tAsset.default_key_2 != KeyCode.None)
					{
						if (tHasMod)
						{
							if (tAsset.default_key_mod_1 != KeyCode.None)
							{
								tAllKeys.Add(tPre + tAsset.default_key_2.ToString() + "+" + tAsset.default_key_mod_1.ToString());
							}
							if (tAsset.default_key_mod_2 != KeyCode.None)
							{
								tAllKeys.Add(tPre + tAsset.default_key_2.ToString() + "+" + tAsset.default_key_mod_2.ToString());
							}
							if (tAsset.default_key_mod_3 != KeyCode.None)
							{
								tAllKeys.Add(tPre + tAsset.default_key_2.ToString() + "+" + tAsset.default_key_mod_3.ToString());
							}
						}
						else
						{
							tAllKeys.Add(tPre + tAsset.default_key_2.ToString());
						}
					}
					if (tAsset.default_key_3 != KeyCode.None)
					{
						if (tHasMod)
						{
							if (tAsset.default_key_mod_1 != KeyCode.None)
							{
								tAllKeys.Add(tPre + tAsset.default_key_3.ToString() + "+" + tAsset.default_key_mod_1.ToString());
							}
							if (tAsset.default_key_mod_2 != KeyCode.None)
							{
								tAllKeys.Add(tPre + tAsset.default_key_3.ToString() + "+" + tAsset.default_key_mod_2.ToString());
							}
							if (tAsset.default_key_mod_3 != KeyCode.None)
							{
								tAllKeys.Add(tPre + tAsset.default_key_3.ToString() + "+" + tAsset.default_key_mod_3.ToString());
							}
						}
						else
						{
							tAllKeys.Add(tPre + tAsset.default_key_3.ToString());
						}
					}
					foreach (string ptr in tAllKeys)
					{
						string tKey = ptr;
						if (tKeys.ContainsKey(tKey))
						{
							BaseAssetLibrary.logAssetError(string.Concat(new string[]
							{
								"<e>",
								tAsset.id,
								"</e> has the same key as asset: <e>",
								tKeys[tKey].id,
								"</e>"
							}), tKey);
						}
						else
						{
							tKeys.Add(tKey, tAsset);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x00073B68 File Offset: 0x00071D68
	public static bool isHoldingControlForSelection()
	{
		return Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x00073B82 File Offset: 0x00071D82
	public static bool isHoldingAlt()
	{
		return Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x00073B9C File Offset: 0x00071D9C
	public static bool isHoldingAnyMod()
	{
		return AssetManager.hotkey_library != null && AssetManager.hotkey_library.isHoldingAnyModKey();
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x00073BB4 File Offset: 0x00071DB4
	public void reset()
	{
		foreach (HotkeyAsset hotkeyAsset in this.list)
		{
			hotkeyAsset.overridden_key_1 = hotkeyAsset.default_key_1;
			hotkeyAsset.overridden_key_2 = hotkeyAsset.default_key_2;
			hotkeyAsset.overridden_key_3 = hotkeyAsset.default_key_3;
			hotkeyAsset.overridden_key_mod_1 = hotkeyAsset.default_key_mod_1;
			hotkeyAsset.overridden_key_mod_2 = hotkeyAsset.default_key_mod_2;
			hotkeyAsset.overridden_key_mod_3 = hotkeyAsset.default_key_mod_3;
		}
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x00073C48 File Offset: 0x00071E48
	public string replaceSpecialTextKeys(string pText)
	{
		if (!pText.Contains("$"))
		{
			return pText;
		}
		foreach (HotkeyAsset tAsset in this.list)
		{
			if (pText.Contains(tAsset.id))
			{
				string tKeyToReplace = "$" + tAsset.id + "$";
				string tHotKeyCode = tAsset.getLocalizedKeys();
				pText = pText.Replace(tKeyToReplace, tHotKeyCode);
				if (pText.Contains("$mouse_wheel$"))
				{
					string tLocalizedText = Toolbox.coloredText("mouse_wheel", "#95DD5D", true);
					pText = pText.Replace("$mouse_wheel$", tLocalizedText);
				}
				if (!pText.Contains("$"))
				{
					return pText;
				}
			}
		}
		return pText;
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x00073D20 File Offset: 0x00071F20
	public bool isHoldingAnyModKey()
	{
		if (!Input.anyKey)
		{
			return false;
		}
		if (this.runModKeyCheck)
		{
			this.runModKeyCheck = false;
			this.holdingAnyModKey = false;
			KeyCode[] array = HotkeyLibrary.mod_keys;
			for (int i = 0; i < array.Length; i++)
			{
				if (Input.GetKey(array[i]))
				{
					this.holdingAnyModKey = true;
					break;
				}
			}
		}
		return this.holdingAnyModKey;
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x00073D7C File Offset: 0x00071F7C
	public void checkHotKeyActions()
	{
		this.runModKeyCheck = true;
		bool tScrollWheel = Input.mouseScrollDelta.y != 0f;
		if (!World.world.has_focus)
		{
			return;
		}
		if (!Input.anyKey && !tScrollWheel)
		{
			return;
		}
		bool tIsInputActive = this.isInputActive();
		bool tEscThisFrame = this._last_input_active && !tIsInputActive;
		this._last_input_active = tIsInputActive;
		if (tIsInputActive || tEscThisFrame)
		{
			return;
		}
		bool tControlsLocked = MapBox.controlsLocked();
		bool tIsControllingUnit = MapBox.isControllingUnit();
		foreach (HotkeyAsset tAsset in this.action_hotkeys)
		{
			if ((!tAsset.use_mouse_wheel || tScrollWheel) && (!tAsset.check_controls_locked || (!tControlsLocked && (!tIsControllingUnit || tAsset.allow_unit_control))) && tAsset.checkIsPossible())
			{
				if (tAsset.just_pressed_action != null && tAsset.isJustPressed())
				{
					tAsset.just_pressed_action(tAsset);
					if (tAsset.holding_action != null)
					{
						this.holding_times[tAsset.id] = tAsset.holding_cooldown_first_action;
					}
				}
				else if (tAsset.holding_action != null && tAsset.isHolding())
				{
					float tHoldingTime;
					this.holding_times.TryGetValue(tAsset.id, out tHoldingTime);
					tHoldingTime -= Time.deltaTime;
					if (tHoldingTime > 0f)
					{
						this.holding_times[tAsset.id] = tHoldingTime;
					}
					else
					{
						tAsset.holding_action(tAsset);
						this.holding_times[tAsset.id] = tAsset.holding_cooldown;
					}
				}
			}
		}
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x00073F10 File Offset: 0x00072110
	private bool isInputActive()
	{
		if (!EventSystem.current.isFocused)
		{
			return false;
		}
		GameObject tSelected = EventSystem.current.currentSelectedGameObject;
		if (tSelected == null)
		{
			return false;
		}
		InputField tInput = tSelected.GetComponent<InputField>();
		return !(tInput == null) && tInput.isFocused;
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x00073F59 File Offset: 0x00072159
	public static bool allowedToUsePowers()
	{
		return !ScrollWindow.isWindowActive();
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x00073F65 File Offset: 0x00072165
	public void changeKey(HotkeyAsset pAsset, KeyCode pCode)
	{
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x00073F67 File Offset: 0x00072167
	public void load()
	{
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x00073F6C File Offset: 0x0007216C
	public void hotkeySelectPower(HotkeyAsset pAsset, string pSelectPower)
	{
		if (!string.IsNullOrEmpty(pSelectPower) && AssetManager.powers.get(pSelectPower) == null)
		{
			return;
		}
		if (string.IsNullOrEmpty(pSelectPower))
		{
			this.showTipNothing(pAsset);
			return;
		}
		PowerButton tPowerButton = PowerButton.get(pSelectPower);
		if (tPowerButton == null)
		{
			return;
		}
		if (tPowerButton.isSelected())
		{
			tPowerButton.cancelSelection();
			return;
		}
		tPowerButton.selectPowerTab(delegate
		{
			World.world.selected_buttons.clickPowerButton(tPowerButton);
			if (tPowerButton.isSelected())
			{
				WorldTip.instance.showToolbarText(tPowerButton.godPower, true);
			}
		});
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x00073FF0 File Offset: 0x000721F0
	public void hotkeySelectNano(HotkeyAsset pAsset, string pSelectNano)
	{
		if (string.IsNullOrEmpty(pSelectNano))
		{
			this.showTipNothing(pAsset);
			return;
		}
		string[] tSelectData = pSelectNano.Split("|", StringSplitOptions.None);
		string tMetaTypeId = tSelectData[0];
		long tFirstNanoId = long.Parse(tSelectData[1]);
		MetaTypeAsset tAsset = AssetManager.meta_type_library.get(tMetaTypeId);
		NanoObject tObject = tAsset.get(tFirstNanoId);
		if (tObject.isRekt() && tSelectData.Length < 3)
		{
			this.showTipNothing(pAsset);
			return;
		}
		NanoObject tLastSelectedNano = SelectedObjects.getSelectedNanoObject();
		if (!SelectedObjects.isNanoObjectSet() || SelectedObjects.getSelectedNanoObject() != tObject)
		{
			if (World.world.isAnyPowerSelected())
			{
				PowerButtonSelector.instance.unselectAll();
			}
			SelectedObjects.unselectNanoObject();
			SelectedUnit.clear();
			if (tMetaTypeId == "unit")
			{
				if (tSelectData.Length >= 3)
				{
					using (ListPool<Actor> tPool = new ListPool<Actor>(tSelectData.Length))
					{
						for (int i = 1; i < tSelectData.Length; i++)
						{
							long tActorId = long.Parse(tSelectData[i]);
							Actor tActor = World.world.units.get(tActorId);
							if (!tActor.isRekt())
							{
								tPool.Add(tActor);
							}
						}
						if (tPool.Count > 0)
						{
							SelectedUnit.selectMultiple(tPool);
							SelectedObjects.setNanoObject(SelectedUnit.unit);
							if (tLastSelectedNano == SelectedUnit.unit)
							{
								World.world.locatePosition(SelectedUnit.unit.current_position);
							}
						}
						if (tPool.Count == 0)
						{
							this.showTipNothing(pAsset);
							return;
						}
						if (tPool.Count == 1)
						{
							PowerTabController.showTabSelectedUnit();
							return;
						}
						PowerTabController.showTabMultipleUnits();
						return;
					}
				}
				SelectedUnit.select(tObject as Actor, true);
				SelectedObjects.setNanoObject(SelectedUnit.unit);
				PowerTabController.showTabSelectedUnit();
				return;
			}
			tAsset.selectAndInspect(tObject, false, false, false);
			return;
		}
		if (tLastSelectedNano == SelectedUnit.unit)
		{
			World.world.locatePosition(SelectedUnit.unit.current_position);
			return;
		}
		if (tObject is IMetaObject)
		{
			Actor tActorTarget = (tObject as IMetaObject).getRandomUnit();
			if (tActorTarget != null)
			{
				World.world.locatePosition(tActorTarget.current_position);
			}
		}
	}

	// Token: 0x06000843 RID: 2115 RVA: 0x000741F8 File Offset: 0x000723F8
	public void showTipNothing(HotkeyAsset pAsset)
	{
		string tLocalizedHotkeyText = LocalizedTextManager.getText("hotkey_tip_empty_tip", null, false);
		tLocalizedHotkeyText = tLocalizedHotkeyText.Replace("$save_hotkey$", "$save_" + pAsset.id + "$");
		tLocalizedHotkeyText = AssetManager.hotkey_library.replaceSpecialTextKeys(tLocalizedHotkeyText);
		WorldTip.instance.showToolbarText(tLocalizedHotkeyText);
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x0007424C File Offset: 0x0007244C
	public unsafe void hotkeySavePower(HotkeyAsset pAsset)
	{
		string tSelectedPower = World.world.getSelectedPowerID();
		string tHotkey = pAsset.id.Replace("save_", "");
		string tLocalizedHotkeyText;
		if (string.IsNullOrEmpty(tSelectedPower))
		{
			tSelectedPower = string.Empty;
			tLocalizedHotkeyText = LocalizedTextManager.getText("hotkey_tip_cleared", null, false);
		}
		else
		{
			tLocalizedHotkeyText = LocalizedTextManager.getText("hotkey_tip_saved_power", null, false);
		}
		tLocalizedHotkeyText = tLocalizedHotkeyText.Replace("$save_hotkey$", "$" + tHotkey + "$");
		tLocalizedHotkeyText = AssetManager.hotkey_library.replaceSpecialTextKeys(tLocalizedHotkeyText);
		WorldTip.instance.showToolbarText(tLocalizedHotkeyText);
		PlayerConfig.dict[tHotkey].stringVal = tSelectedPower;
		PlayerConfig.saveData();
		*this.getHotkeyFromData(tHotkey) = string.Empty;
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x00074300 File Offset: 0x00072500
	public unsafe void hotkeySaveTab(HotkeyAsset pAsset)
	{
		string tHotkey = pAsset.id.Replace("save_", "");
		string tLocalizedHotkeyText = "";
		string tSelectedNano;
		if (!SelectedObjects.isNanoObjectSet())
		{
			tLocalizedHotkeyText = LocalizedTextManager.getText("hotkey_tip_cleared", null, false);
			tSelectedNano = string.Empty;
		}
		else
		{
			tLocalizedHotkeyText = LocalizedTextManager.getText("hotkey_tip_saved_nano", null, false);
			NanoObject tNano = SelectedObjects.getSelectedNanoObject();
			tSelectedNano = (tNano.getMetaTypeAsset().id ?? "");
			if (SelectedUnit.isSet())
			{
				using (List<Actor>.Enumerator enumerator = SelectedUnit.getAllSelectedList().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Actor tActor = enumerator.Current;
						tSelectedNano += string.Format("|{0}", tActor.id);
					}
					goto IL_D3;
				}
			}
			tSelectedNano += string.Format("|{0}", tNano.id);
		}
		IL_D3:
		tLocalizedHotkeyText = tLocalizedHotkeyText.Replace("$save_hotkey$", "$" + tHotkey + "$");
		tLocalizedHotkeyText = AssetManager.hotkey_library.replaceSpecialTextKeys(tLocalizedHotkeyText);
		*this.getHotkeyFromData(tHotkey) = tSelectedNano;
		WorldTip.instance.showToolbarText(tLocalizedHotkeyText);
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x0007442C File Offset: 0x0007262C
	public ref string getHotkeyFromData(string pHotkeyId)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(pHotkeyId);
		if (num <= 2425502108U)
		{
			if (num <= 2375169251U)
			{
				if (num != 2358391632U)
				{
					if (num == 2375169251U)
					{
						if (pHotkeyId == "hotkey_5")
						{
							return ref World.world.hotkey_tabs_data.hotkey_data_5;
						}
					}
				}
				else if (pHotkeyId == "hotkey_4")
				{
					return ref World.world.hotkey_tabs_data.hotkey_data_4;
				}
			}
			else if (num != 2391946870U)
			{
				if (num != 2408724489U)
				{
					if (num == 2425502108U)
					{
						if (pHotkeyId == "hotkey_0")
						{
							return ref World.world.hotkey_tabs_data.hotkey_data_0;
						}
					}
				}
				else if (pHotkeyId == "hotkey_7")
				{
					return ref World.world.hotkey_tabs_data.hotkey_data_7;
				}
			}
			else if (pHotkeyId == "hotkey_6")
			{
				return ref World.world.hotkey_tabs_data.hotkey_data_6;
			}
		}
		else if (num <= 2459057346U)
		{
			if (num != 2442279727U)
			{
				if (num == 2459057346U)
				{
					if (pHotkeyId == "hotkey_2")
					{
						return ref World.world.hotkey_tabs_data.hotkey_data_2;
					}
				}
			}
			else if (pHotkeyId == "hotkey_1")
			{
				return ref World.world.hotkey_tabs_data.hotkey_data_1;
			}
		}
		else if (num != 2475834965U)
		{
			if (num != 2559723060U)
			{
				if (num == 2576500679U)
				{
					if (pHotkeyId == "hotkey_9")
					{
						return ref World.world.hotkey_tabs_data.hotkey_data_9;
					}
				}
			}
			else if (pHotkeyId == "hotkey_8")
			{
				return ref World.world.hotkey_tabs_data.hotkey_data_8;
			}
		}
		else if (pHotkeyId == "hotkey_3")
		{
			return ref World.world.hotkey_tabs_data.hotkey_data_3;
		}
		return ref World.world.hotkey_tabs_data.hotkey_data_1;
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x00074654 File Offset: 0x00072854
	public void initDebugHotkeys()
	{
		this.initDebugHotkeysBase();
		this.initUnitDebugHotkeys();
		this.initDebugWindowHotkeys();
		this.add(new HotkeyAsset
		{
			id = "debug_autosave",
			default_key_1 = KeyCode.S,
			default_key_mod_1 = KeyCode.LeftAlt,
			just_pressed_action = new HotkeyAction(this.debugAutosave)
		});
		HotkeyAsset hotkeyAsset = new HotkeyAsset();
		hotkeyAsset.id = "debug_next_test_map";
		hotkeyAsset.default_key_1 = KeyCode.PageUp;
		hotkeyAsset.just_pressed_action = delegate(HotkeyAsset _)
		{
			if (SmoothLoader.isLoading())
			{
				return;
			}
			World.world.transition_screen.startTransition(new LoadingScreen.TransitionAction(TestMaps.loadNextMap));
		};
		this.add(hotkeyAsset);
		HotkeyAsset hotkeyAsset2 = new HotkeyAsset();
		hotkeyAsset2.id = "debug_prev_test_map";
		hotkeyAsset2.default_key_1 = KeyCode.PageDown;
		hotkeyAsset2.just_pressed_action = delegate(HotkeyAsset _)
		{
			if (SmoothLoader.isLoading())
			{
				return;
			}
			World.world.transition_screen.startTransition(new LoadingScreen.TransitionAction(TestMaps.loadPrevMap));
		};
		this.add(hotkeyAsset2);
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x00074740 File Offset: 0x00072940
	private void initDebugHotkeysBase()
	{
		HotkeyAsset hotkeyAsset = new HotkeyAsset();
		hotkeyAsset.id = "export_unit_sprites";
		hotkeyAsset.default_key_1 = KeyCode.Y;
		hotkeyAsset.check_window_not_active = true;
		hotkeyAsset.check_controls_locked = true;
		hotkeyAsset.just_pressed_action = delegate(HotkeyAsset _)
		{
			WorldTip.instance.showToolbarText("Exporting unit sprites");
			AssetManager.dynamic_sprites_library.export();
		};
		this.add(hotkeyAsset);
		HotkeyAsset hotkeyAsset2 = new HotkeyAsset();
		hotkeyAsset2.id = "autotester";
		hotkeyAsset2.default_key_1 = KeyCode.U;
		hotkeyAsset2.check_window_not_active = true;
		hotkeyAsset2.check_controls_locked = true;
		hotkeyAsset2.just_pressed_action = delegate(HotkeyAsset _)
		{
			World.world.auto_tester.toggleAutoTester();
		};
		this.add(hotkeyAsset2);
		HotkeyAsset hotkeyAsset3 = new HotkeyAsset();
		hotkeyAsset3.id = "test_zones_border_growth";
		hotkeyAsset3.default_key_1 = KeyCode.O;
		hotkeyAsset3.check_window_not_active = true;
		hotkeyAsset3.check_controls_locked = true;
		hotkeyAsset3.just_pressed_action = delegate(HotkeyAsset _)
		{
			DebugZonesTool.actionGrowBorder();
		};
		this.add(hotkeyAsset3);
		HotkeyAsset hotkeyAsset4 = new HotkeyAsset();
		hotkeyAsset4.id = "test_zones_abandon_zones";
		hotkeyAsset4.default_key_1 = KeyCode.P;
		hotkeyAsset4.check_window_not_active = true;
		hotkeyAsset4.check_controls_locked = true;
		hotkeyAsset4.just_pressed_action = delegate(HotkeyAsset _)
		{
			foreach (WorldTile tTile in World.world.tiles_list)
			{
				World.world.buildings.addBuilding("poop", tTile, false, false, BuildPlacingType.New);
			}
		};
		this.add(hotkeyAsset4);
		HotkeyAsset hotkeyAsset5 = new HotkeyAsset();
		hotkeyAsset5.id = "test_colors";
		hotkeyAsset5.default_key_1 = KeyCode.R;
		hotkeyAsset5.check_window_not_active = true;
		hotkeyAsset5.check_controls_locked = true;
		hotkeyAsset5.just_pressed_action = delegate(HotkeyAsset _)
		{
			foreach (Kingdom kingdom in World.world.kingdoms)
			{
				kingdom.generateBanner();
				ColorAsset tColor = AssetManager.kingdom_colors_library.list.GetRandom<ColorAsset>();
				kingdom.data.setColorID(AssetManager.kingdom_colors_library.list.IndexOf(tColor));
				if (kingdom.updateColor(tColor))
				{
					World.world.zone_calculator.dirtyAndClear();
				}
			}
		};
		this.add(hotkeyAsset5);
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x000748E8 File Offset: 0x00072AE8
	private void initDebugWindowHotkeys()
	{
		this.add(new HotkeyAsset
		{
			id = "debug_building_shadow_x_increase",
			default_key_1 = KeyCode.X,
			default_key_mod_1 = KeyCode.LeftControl,
			just_pressed_action = new HotkeyAction(this.debugShadow),
			check_controls_locked = true,
			check_window_active = true,
			check_debug_active = true
		});
		this.clone("debug_building_shadow_x_reduce", "debug_building_shadow_x_increase");
		this.t.default_key_mod_1 = KeyCode.LeftShift;
		this.clone("debug_building_shadow_y_increase", "debug_building_shadow_x_increase");
		this.t.default_key_1 = KeyCode.Y;
		this.clone("debug_building_shadow_y_reduce", "debug_building_shadow_y_increase");
		this.t.default_key_mod_1 = KeyCode.LeftShift;
		this.clone("debug_building_shadow_distortion_increase", "debug_building_shadow_x_increase");
		this.t.default_key_1 = KeyCode.D;
		this.clone("debug_building_shadow_distortion_reduce", "debug_building_shadow_distortion_increase");
		this.t.default_key_mod_1 = KeyCode.LeftShift;
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x000749E8 File Offset: 0x00072BE8
	private void initUnitDebugHotkeys()
	{
		HotkeyAsset hotkeyAsset = new HotkeyAsset();
		hotkeyAsset.id = "debug_unit_set_task";
		hotkeyAsset.default_key_1 = KeyCode.V;
		hotkeyAsset.default_key_mod_1 = KeyCode.LeftControl;
		hotkeyAsset.check_window_not_active = true;
		hotkeyAsset.check_controls_locked = true;
		hotkeyAsset.check_render_gameplay = true;
		hotkeyAsset.check_debug_active = true;
		hotkeyAsset.just_pressed_action = delegate(HotkeyAsset _)
		{
			if (!DebugConfig.isOn(DebugOption.DebugUnitHotkeys))
			{
				return;
			}
			Actor tActor = World.world.getActorNearCursor();
			if (tActor == null)
			{
				return;
			}
			tActor.addStatusEffect("budding", 0f, true);
		};
		this.add(hotkeyAsset);
		HotkeyAsset hotkeyAsset2 = new HotkeyAsset();
		hotkeyAsset2.id = "debug_general_key";
		hotkeyAsset2.default_key_1 = KeyCode.N;
		hotkeyAsset2.check_debug_active = true;
		hotkeyAsset2.just_pressed_action = delegate(HotkeyAsset _)
		{
			if (!DebugConfig.isOn(DebugOption.DebugUnitHotkeys))
			{
				return;
			}
			if (SelectedUnit.isSet())
			{
				using (ListPool<Actor> tListPool = new ListPool<Actor>(SelectedUnit.getAllSelected()))
				{
					foreach (Actor ptr in tListPool)
					{
						ptr.getHitFullHealth(AttackType.Divine);
					}
				}
			}
		};
		this.add(hotkeyAsset2);
		HotkeyAsset hotkeyAsset3 = new HotkeyAsset();
		hotkeyAsset3.id = "debug_monolith";
		hotkeyAsset3.default_key_1 = KeyCode.M;
		hotkeyAsset3.default_key_mod_1 = KeyCode.LeftControl;
		hotkeyAsset3.check_window_not_active = true;
		hotkeyAsset3.check_controls_locked = true;
		hotkeyAsset3.check_render_gameplay = true;
		hotkeyAsset3.check_debug_active = true;
		hotkeyAsset3.just_pressed_action = delegate(HotkeyAsset _)
		{
			if (!DebugConfig.isOn(DebugOption.DebugMonolith))
			{
				return;
			}
			foreach (Building tBuilding in World.world.buildings)
			{
				if (tBuilding.asset.id == "monolith")
				{
					BuildingMonolith component_monolith = tBuilding.component_monolith;
					component_monolith.doMonolithAction(component_monolith.building.current_tile, true);
				}
			}
		};
		this.add(hotkeyAsset3);
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x00074B16 File Offset: 0x00072D16
	private void debugAutosave(HotkeyAsset pAsset)
	{
		if (!Config.isEditor)
		{
			return;
		}
		AutoSaveManager.autoSave(true, true);
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x00074B28 File Offset: 0x00072D28
	private void debugShadow(HotkeyAsset pAsset)
	{
		if (!DebugConfig.isOn(DebugOption.DebugWindowHotkeys))
		{
			return;
		}
		if (ScrollWindow.getCurrentWindow().name != "building_asset")
		{
			return;
		}
		BuildingAsset tAsset = BaseDebugAssetWindow<BuildingAsset, BuildingDebugAssetElement>.current_element.asset;
		if (!tAsset.shadow)
		{
			return;
		}
		string id = pAsset.id;
		if (!(id == "debug_building_shadow_x_increase"))
		{
			if (!(id == "debug_building_shadow_x_reduce"))
			{
				if (!(id == "debug_building_shadow_y_increase"))
				{
					if (!(id == "debug_building_shadow_y_reduce"))
					{
						if (!(id == "debug_building_shadow_distortion_increase"))
						{
							if (id == "debug_building_shadow_distortion_reduce")
							{
								tAsset.shadow_distortion -= 0.05f;
							}
						}
						else
						{
							tAsset.shadow_distortion += 0.05f;
						}
					}
					else
					{
						BuildingAsset buildingAsset = tAsset;
						buildingAsset.shadow_bound.y = buildingAsset.shadow_bound.y - 0.05f;
					}
				}
				else
				{
					BuildingAsset buildingAsset2 = tAsset;
					buildingAsset2.shadow_bound.y = buildingAsset2.shadow_bound.y + 0.05f;
				}
			}
			else
			{
				BuildingAsset buildingAsset3 = tAsset;
				buildingAsset3.shadow_bound.x = buildingAsset3.shadow_bound.x - 0.05f;
			}
		}
		else
		{
			BuildingAsset buildingAsset4 = tAsset;
			buildingAsset4.shadow_bound.x = buildingAsset4.shadow_bound.x + 0.05f;
		}
		Debug.Log(string.Concat(new string[]
		{
			"t.setShadow(",
			tAsset.shadow_bound.x.ToString(CultureInfo.InvariantCulture),
			"f, ",
			tAsset.shadow_bound.y.ToString(CultureInfo.InvariantCulture),
			"f, ",
			tAsset.shadow_distortion.ToString(CultureInfo.InvariantCulture),
			"f);"
		}));
		BuildingAssetWindow.reloadSprites();
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x00074CBC File Offset: 0x00072EBC
	public void debug(DebugTool pTool)
	{
		foreach (HotkeyAsset tAsset in this.list)
		{
			if (tAsset.just_pressed_action == null && tAsset.holding_action == null)
			{
				if (tAsset.isJustPressed())
				{
					pTool.setText(tAsset.id, "just_pressed", 0f, false, 0L, false, false, "");
				}
				if (tAsset.isHolding())
				{
					pTool.setText(tAsset.id, "holding", 0f, false, 0L, false, false, "");
				}
			}
		}
	}

	// Token: 0x0400088B RID: 2187
	public static HotkeyAsset cancel;

	// Token: 0x0400088C RID: 2188
	public static HotkeyAsset console;

	// Token: 0x0400088D RID: 2189
	public static HotkeyAsset remove;

	// Token: 0x0400088E RID: 2190
	public static HotkeyAsset pause;

	// Token: 0x0400088F RID: 2191
	public static HotkeyAsset hide_ui;

	// Token: 0x04000890 RID: 2192
	public static HotkeyAsset action_jump;

	// Token: 0x04000891 RID: 2193
	public static HotkeyAsset action_dash;

	// Token: 0x04000892 RID: 2194
	public static HotkeyAsset action_backstep;

	// Token: 0x04000893 RID: 2195
	public static HotkeyAsset action_talk;

	// Token: 0x04000894 RID: 2196
	public static HotkeyAsset action_steal;

	// Token: 0x04000895 RID: 2197
	public static HotkeyAsset action_swear;

	// Token: 0x04000896 RID: 2198
	public static HotkeyAsset left;

	// Token: 0x04000897 RID: 2199
	public static HotkeyAsset right;

	// Token: 0x04000898 RID: 2200
	public static HotkeyAsset up;

	// Token: 0x04000899 RID: 2201
	public static HotkeyAsset down;

	// Token: 0x0400089A RID: 2202
	public static HotkeyAsset next_unit_in_multi_selection;

	// Token: 0x0400089B RID: 2203
	public static HotkeyAsset next_tab;

	// Token: 0x0400089C RID: 2204
	public static HotkeyAsset prev_tab;

	// Token: 0x0400089D RID: 2205
	public static HotkeyAsset zoom_in;

	// Token: 0x0400089E RID: 2206
	public static HotkeyAsset zoom_out;

	// Token: 0x0400089F RID: 2207
	public static HotkeyAsset zoom;

	// Token: 0x040008A0 RID: 2208
	public static HotkeyAsset world_speed;

	// Token: 0x040008A1 RID: 2209
	public static HotkeyAsset brush;

	// Token: 0x040008A2 RID: 2210
	public static HotkeyAsset follow_unit;

	// Token: 0x040008A3 RID: 2211
	public static HotkeyAsset control_unit;

	// Token: 0x040008A4 RID: 2212
	public static HotkeyAsset fullscreen_switch;

	// Token: 0x040008A5 RID: 2213
	public static HotkeyAsset many_mod;

	// Token: 0x040008A6 RID: 2214
	public static HotkeyAsset fast_civ_mod;

	// Token: 0x040008A7 RID: 2215
	public static KeyCode[] mod_keys = new KeyCode[0];

	// Token: 0x040008A8 RID: 2216
	private HotkeyAsset[] action_hotkeys = new HotkeyAsset[0];

	// Token: 0x040008A9 RID: 2217
	private Dictionary<string, float> holding_times = new Dictionary<string, float>();

	// Token: 0x040008AA RID: 2218
	private bool holdingAnyModKey;

	// Token: 0x040008AB RID: 2219
	private bool runModKeyCheck = true;

	// Token: 0x040008AC RID: 2220
	private bool _last_input_active;

	// Token: 0x040008AD RID: 2221
	private MetaType[] _meta_zones = new MetaType[]
	{
		MetaType.Army,
		MetaType.Alliance,
		MetaType.Kingdom,
		MetaType.City,
		MetaType.Clan,
		MetaType.Religion,
		MetaType.Culture,
		MetaType.Language,
		MetaType.Family,
		MetaType.Subspecies
	};
}
