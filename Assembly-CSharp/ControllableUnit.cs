using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000328 RID: 808
public static class ControllableUnit
{
	// Token: 0x06001F15 RID: 7957 RVA: 0x0010EC96 File Offset: 0x0010CE96
	public static bool isControllingUnit(Actor pUnit)
	{
		return ControllableUnit.isControllingUnit() && ControllableUnit._units.Contains(pUnit);
	}

	// Token: 0x06001F16 RID: 7958 RVA: 0x0010ECAC File Offset: 0x0010CEAC
	public static HashSet<Actor> getCotrolledUnits()
	{
		return ControllableUnit._units;
	}

	// Token: 0x06001F17 RID: 7959 RVA: 0x0010ECB3 File Offset: 0x0010CEB3
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool isControllingUnit()
	{
		return ControllableUnit._units.Any<Actor>();
	}

	// Token: 0x06001F18 RID: 7960 RVA: 0x0010ECBF File Offset: 0x0010CEBF
	public static int count()
	{
		return ControllableUnit._units.Count;
	}

	// Token: 0x06001F19 RID: 7961 RVA: 0x0010ECCB File Offset: 0x0010CECB
	public static bool isControllingCrabzilla()
	{
		return ControllableUnit.isControllingUnit() && ControllableUnit._unit_main.asset.id == "crabzilla";
	}

	// Token: 0x06001F1A RID: 7962 RVA: 0x0010ECEF File Offset: 0x0010CEEF
	public static bool isControllingNormalUnits()
	{
		return ControllableUnit.isControllingUnit() && ControllableUnit._unit_main.asset.show_controllable_tip;
	}

	// Token: 0x06001F1B RID: 7963 RVA: 0x0010ED09 File Offset: 0x0010CF09
	public static Actor getControllableUnit()
	{
		return ControllableUnit._unit_main;
	}

	// Token: 0x06001F1C RID: 7964 RVA: 0x0010ED10 File Offset: 0x0010CF10
	public static bool isAttackPressedLeft()
	{
		return ControllableUnit._attack_pressed_button_left;
	}

	// Token: 0x06001F1D RID: 7965 RVA: 0x0010ED17 File Offset: 0x0010CF17
	public static bool isAttackPressedRight()
	{
		return ControllableUnit._attack_pressed_button_right;
	}

	// Token: 0x06001F1E RID: 7966 RVA: 0x0010ED1E File Offset: 0x0010CF1E
	public static bool isAttackJustPressedLeft()
	{
		return ControllableUnit._attack_just_pressed_button_left;
	}

	// Token: 0x06001F1F RID: 7967 RVA: 0x0010ED25 File Offset: 0x0010CF25
	public static bool isAttackJustPressedRight()
	{
		return ControllableUnit._attack_just_pressed_button_right;
	}

	// Token: 0x06001F20 RID: 7968 RVA: 0x0010ED2C File Offset: 0x0010CF2C
	public static void setControllableCreatures(ListPool<Actor> pListActors)
	{
		foreach (Actor ptr in pListActors)
		{
			ControllableUnit.setControllableCreature(ptr);
		}
	}

	// Token: 0x06001F21 RID: 7969 RVA: 0x0010ED78 File Offset: 0x0010CF78
	public static void setControllableCreatureAndSelected(Actor pActor)
	{
		using (ListPool<Actor> tList = new ListPool<Actor>())
		{
			foreach (Actor tActor in SelectedUnit.getAllSelected())
			{
				if (tActor.canBePossessed())
				{
					tList.Add(tActor);
				}
			}
			ControllableUnit.setControllableCreatures(tList);
			ControllableUnit.setControllableCreature(pActor);
			SelectedUnit.clear();
		}
	}

	// Token: 0x06001F22 RID: 7970 RVA: 0x0010EE04 File Offset: 0x0010D004
	public static void setControllableCreatureCrabzilla(Actor pActor)
	{
		ControllableUnit.setControllableCreature(pActor);
	}

	// Token: 0x06001F23 RID: 7971 RVA: 0x0010EE0C File Offset: 0x0010D00C
	public static void setControllableCreature(Actor pActor)
	{
		if (!pActor.canBePossessed())
		{
			return;
		}
		SelectedUnit.clear();
		ControllableUnit._unit_main = pActor;
		ControllableUnit._units.Add(pActor);
		ControllableUnit.addStatus(pActor);
		if (ControllableUnit.isControllingUnit())
		{
			Config.setWorldSpeed("x1", true);
			Config.paused = false;
		}
		if (Config.joyControls)
		{
			if (ControllableUnit.isControllingUnit())
			{
				World.world.joys.SetActive(true);
				if (ControllableUnit.isControllingCrabzilla())
				{
					UltimateJoystick.EnableJoystick("JoyRight");
					TouchPossessionController.instance.gameObject.SetActive(false);
				}
				else
				{
					UltimateJoystick.DisableJoystick("JoyRight");
					if (!InputHelpers.mouseSupported)
					{
						TouchPossessionController.instance.gameObject.SetActive(true);
					}
				}
			}
			else
			{
				World.world.joys.SetActive(false);
			}
			UltimateJoystick.ResetJoysticks();
		}
		else if (World.world.joys != null)
		{
			Object.Destroy(World.world.joys, 0.5f);
			World.world.joys = null;
		}
		ControllableUnit._movement_vector = Vector2.zero;
		ControllableUnit.resetClickVector();
		ControllableUnit._attack_pressed_button_left = false;
		ControllableUnit._attack_pressed_button_right = false;
		ControllableUnit._attack_just_pressed_button_left = false;
		ControllableUnit._attack_just_pressed_button_right = false;
		if (ControllableUnit.isControllingNormalUnits() && InputHelpers.mouseSupported)
		{
			PossessionUI.toggle(true);
		}
	}

	// Token: 0x06001F24 RID: 7972 RVA: 0x0010EF3E File Offset: 0x0010D13E
	private static void resetClickVector()
	{
		ControllableUnit._click_vector = Vector2.zero;
	}

	// Token: 0x06001F25 RID: 7973 RVA: 0x0010EF4A File Offset: 0x0010D14A
	public static Vector2 getMovementVector()
	{
		return ControllableUnit._movement_vector;
	}

	// Token: 0x06001F26 RID: 7974 RVA: 0x0010EF51 File Offset: 0x0010D151
	public static Vector2 getClickVector()
	{
		return ControllableUnit._click_vector;
	}

	// Token: 0x06001F27 RID: 7975 RVA: 0x0010EF58 File Offset: 0x0010D158
	public static bool isActionPressedJump()
	{
		return ControllableUnit._action_pressed_jump;
	}

	// Token: 0x06001F28 RID: 7976 RVA: 0x0010EF5F File Offset: 0x0010D15F
	public static bool isActionPressedTalk()
	{
		return ControllableUnit._action_pressed_talk;
	}

	// Token: 0x06001F29 RID: 7977 RVA: 0x0010EF66 File Offset: 0x0010D166
	public static bool isActionPressedDash()
	{
		return ControllableUnit._action_pressed_dash;
	}

	// Token: 0x06001F2A RID: 7978 RVA: 0x0010EF6D File Offset: 0x0010D16D
	public static bool isActionPressedBackstep()
	{
		return ControllableUnit._action_pressed_backstep;
	}

	// Token: 0x06001F2B RID: 7979 RVA: 0x0010EF74 File Offset: 0x0010D174
	public static bool isActionPressedSteal()
	{
		return ControllableUnit._action_pressed_steal;
	}

	// Token: 0x06001F2C RID: 7980 RVA: 0x0010EF7B File Offset: 0x0010D17B
	public static bool isActionPressedSwear()
	{
		return ControllableUnit._action_pressed_swear;
	}

	// Token: 0x06001F2D RID: 7981 RVA: 0x0010EF82 File Offset: 0x0010D182
	public static void remove(Actor pActor)
	{
		ControllableUnit._units.Remove(pActor);
		if (ControllableUnit._unit_main == pActor)
		{
			ControllableUnit._unit_main = null;
			ControllableUnit.trySelectNewMain();
		}
	}

	// Token: 0x06001F2E RID: 7982 RVA: 0x0010EFA3 File Offset: 0x0010D1A3
	private static void trySelectNewMain()
	{
		if (ControllableUnit._units.Count == 0)
		{
			ControllableUnit.clear(true);
			return;
		}
		ControllableUnit._unit_main = ControllableUnit._units.GetRandom<Actor>();
	}

	// Token: 0x06001F2F RID: 7983 RVA: 0x0010EFC8 File Offset: 0x0010D1C8
	public static void clear(bool pCallKill = true)
	{
		PossessionUI.toggle(false);
		if (Config.joyControls)
		{
			World.world.joys.SetActive(false);
			UltimateJoystick.ResetJoysticks();
		}
		if (!ControllableUnit.isControllingUnit())
		{
			return;
		}
		foreach (Actor ptr in new ListPool<Actor>(ControllableUnit.getCotrolledUnits()))
		{
			Actor tActor = ptr;
			tActor.finishStatusEffect("possessed");
			tActor.cancelAllBeh();
			tActor.applyRandomForce(1.5f, 2f);
			tActor.makeStunned(1f);
			tActor.makeConfused(6f, false);
			tActor.setPossessedMovement(false);
			if (pCallKill && tActor.asset.id == "crabzilla")
			{
				tActor.getHitFullHealth(AttackType.Divine);
			}
		}
		ControllableUnit._unit_main = null;
		ControllableUnit._units.Clear();
		World.world.selected_buttons.unselectAll();
	}

	// Token: 0x06001F30 RID: 7984 RVA: 0x0010F0C4 File Offset: 0x0010D2C4
	public static void updateControllableUnit()
	{
		if (!ControllableUnit.isControllingUnit())
		{
			return;
		}
		if (InputHelpers.GetAnyMouseButtonUp())
		{
			foreach (Actor actor in ControllableUnit.getCotrolledUnits())
			{
				actor.resetAttackTimeout();
			}
		}
		ControllableUnit.updateCamera();
		ControllableUnit.updateMovementVector();
		ControllableUnit.updateClick();
		ControllableUnit.updateMouseAttackPosition();
		ControllableUnit.checkActions();
		ControllableUnit.checkPossessionStatus();
	}

	// Token: 0x06001F31 RID: 7985 RVA: 0x0010F140 File Offset: 0x0010D340
	private static bool isAnyActionsPressed()
	{
		return ControllableUnit._action_pressed_jump || ControllableUnit._action_pressed_dash || ControllableUnit._action_pressed_steal || ControllableUnit._action_pressed_swear || ControllableUnit._action_pressed_talk || ControllableUnit._action_pressed_backstep;
	}

	// Token: 0x06001F32 RID: 7986 RVA: 0x0010F16C File Offset: 0x0010D36C
	private static void checkActions()
	{
		ControllableUnit._action_pressed_jump = (HotkeyLibrary.action_jump.isJustPressed() || TouchPossessionController.isActionPressedJump());
		ControllableUnit._action_pressed_dash = (HotkeyLibrary.action_dash.isJustPressed() || TouchPossessionController.isActionPressedDash());
		ControllableUnit._action_pressed_backstep = (HotkeyLibrary.action_backstep.isJustPressed() || TouchPossessionController.isActionPressedBackStep());
		bool tTouchAction = ControllableUnit._attack_pressed_button_left;
		ControllableUnit._action_pressed_steal = (HotkeyLibrary.action_steal.isJustPressed() || (tTouchAction && TouchPossessionController.isSelectedActionSteal()));
		ControllableUnit._action_pressed_swear = (HotkeyLibrary.action_swear.isJustPressed() || (tTouchAction && TouchPossessionController.isSelectedActionSwear()));
		ControllableUnit._action_pressed_talk = (HotkeyLibrary.action_talk.isJustPressed() || (tTouchAction && TouchPossessionController.isSelectedActionTalk()));
		ControllableUnit._attack_just_pressed_button_right = (ControllableUnit._attack_just_pressed_button_right || (tTouchAction && TouchPossessionController.isSelectedActionKick()));
	}

	// Token: 0x06001F33 RID: 7987 RVA: 0x0010F241 File Offset: 0x0010D441
	private static void updateMouseAttackPosition()
	{
		ControllableUnit.resetClickVector();
		if (!InputHelpers.mouseSupported && Input.touchSupported)
		{
			ControllableUnit._click_vector = ControllableUnit.getTouchAttackPosition();
			return;
		}
		ControllableUnit._click_vector = World.world.getMousePos();
	}

	// Token: 0x06001F34 RID: 7988 RVA: 0x0010F270 File Offset: 0x0010D470
	private static bool getAttackTouch(out Touch pTouch)
	{
		pTouch = default(Touch);
		if (World.world.player_control.already_used_zoom)
		{
			return false;
		}
		UltimateJoystick ultimateJoystick = UltimateJoystick.GetUltimateJoystick("JoyLeft");
		bool tJoyActive = ultimateJoystick.GetJoystickState();
		int tTouchId = ultimateJoystick.getTouchId();
		bool tResult = false;
		foreach (Touch tTouch in Input.touches)
		{
			if (!World.world.isTouchOverUI(tTouch) && (!tJoyActive || tTouch.fingerId != tTouchId))
			{
				pTouch = tTouch;
				tResult = true;
				break;
			}
		}
		return tResult;
	}

	// Token: 0x06001F35 RID: 7989 RVA: 0x0010F2FC File Offset: 0x0010D4FC
	private static Vector2 getTouchAttackPosition()
	{
		Vector2 tResult = Vector2.zero;
		Touch tTouch;
		if (ControllableUnit.getAttackTouch(out tTouch))
		{
			tResult = World.world.camera.ScreenToWorldPoint(tTouch.position);
		}
		return tResult;
	}

	// Token: 0x06001F36 RID: 7990 RVA: 0x0010F33C File Offset: 0x0010D53C
	private static void checkPossessionStatus()
	{
		if (ControllableUnit._movement_vector != Vector2.zero || ControllableUnit._attack_pressed_button_right || ControllableUnit._attack_pressed_button_left || ControllableUnit.isAnyActionsPressed())
		{
			foreach (Actor actor in ControllableUnit.getCotrolledUnits())
			{
				ControllableUnit.addStatus(actor);
				actor.stopSleeping();
				ControllableUnit.fixNextStep(actor);
			}
		}
	}

	// Token: 0x06001F37 RID: 7991 RVA: 0x0010F3C0 File Offset: 0x0010D5C0
	private static void fixNextStep(Actor pActor)
	{
		pActor.next_step_position = pActor.next_step_position_possession;
	}

	// Token: 0x06001F38 RID: 7992 RVA: 0x0010F3D0 File Offset: 0x0010D5D0
	private static void addStatus(Actor pActor)
	{
		bool tHadStatus = false;
		if (pActor.hasStatus("possessed"))
		{
			tHadStatus = true;
		}
		pActor.addStatusEffect("possessed", 10f, false);
		pActor.cancelAllBeh();
		if (!tHadStatus && pActor.hasTag("strong_mind"))
		{
			pActor.spawnSlashYell(World.world.getMousePos());
			pActor.addStatusEffect("swearing", 2f, false);
			pActor.punchTargetAnimation(World.world.getMousePos(), false, false, -40f);
			string tRandomIcon = ControllableUnit._possessed_icons.GetRandom<string>();
			pActor.forceSocializeTopic(tRandomIcon);
		}
	}

	// Token: 0x06001F39 RID: 7993 RVA: 0x0010F468 File Offset: 0x0010D668
	private static void updateClick()
	{
		if (!Config.joyControls)
		{
			ControllableUnit._attack_pressed_button_left = Input.GetMouseButton(0);
			ControllableUnit._attack_pressed_button_right = Input.GetMouseButton(1);
			ControllableUnit._attack_just_pressed_button_left = Input.GetMouseButtonDown(0);
			ControllableUnit._attack_just_pressed_button_right = Input.GetMouseButtonDown(1);
			return;
		}
		if (UltimateJoystick.GetUltimateJoystick("JoyRight").gameObject.activeSelf)
		{
			if (ControllableUnit._attack_pressed_button_left && !UltimateJoystick.GetJoystickState("JoyRight"))
			{
				ControllableUnit._attack_pressed_button_left = false;
				return;
			}
			if (UltimateJoystick.GetTapCount("JoyRight"))
			{
				ControllableUnit._attack_pressed_button_left = !ControllableUnit._attack_pressed_button_left;
			}
			return;
		}
		else
		{
			bool tLeftJoyActive = UltimateJoystick.GetJoystickState("JoyLeft");
			if (tLeftJoyActive && Input.touchCount <= 1)
			{
				ControllableUnit._attack_pressed_button_left = false;
				ControllableUnit._attack_pressed_button_right = false;
				ControllableUnit._attack_just_pressed_button_left = false;
				ControllableUnit._attack_just_pressed_button_right = false;
				return;
			}
			Touch tAttack;
			if (!ControllableUnit.getAttackTouch(out tAttack))
			{
				ControllableUnit._attack_pressed_button_left = false;
				ControllableUnit._attack_pressed_button_right = false;
				ControllableUnit._attack_just_pressed_button_left = false;
				ControllableUnit._attack_just_pressed_button_right = false;
				return;
			}
			if (tAttack.phase == TouchPhase.Began)
			{
				ControllableUnit._touch_attack_started_at = Time.time;
				ControllableUnit._touch_attack_just_started = true;
				return;
			}
			ControllableUnit._attack_pressed_button_left = (tAttack.phase == TouchPhase.Stationary || tAttack.phase == TouchPhase.Moved);
			ControllableUnit._attack_pressed_button_right = false;
			ControllableUnit._attack_just_pressed_button_left = ControllableUnit._touch_attack_just_started;
			ControllableUnit._attack_just_pressed_button_right = false;
			ControllableUnit._touch_attack_just_started = false;
			return;
		}
	}

	// Token: 0x06001F3A RID: 7994 RVA: 0x0010F5A0 File Offset: 0x0010D7A0
	public static bool isMovementActionActive()
	{
		if (Config.joyControls)
		{
			UltimateJoystick tLeft = UltimateJoystick.GetUltimateJoystick("JoyLeft");
			if (tLeft == null)
			{
				return false;
			}
			if (tLeft.GetJoystickState())
			{
				return true;
			}
		}
		else
		{
			if (HotkeyLibrary.up.isHolding())
			{
				return true;
			}
			if (HotkeyLibrary.down.isHolding())
			{
				return true;
			}
			if (HotkeyLibrary.left.isHolding())
			{
				return true;
			}
			if (HotkeyLibrary.right.isHolding())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001F3B RID: 7995 RVA: 0x0010F60D File Offset: 0x0010D80D
	private static void updateMovementVector()
	{
		if (Config.joyControls)
		{
			ControllableUnit.updateMovementVectorJoystick();
			return;
		}
		ControllableUnit.updateMovementVectorKeyboard();
	}

	// Token: 0x06001F3C RID: 7996 RVA: 0x0010F624 File Offset: 0x0010D824
	private static void updateMovementVectorJoystick()
	{
		if (ControllableUnit.isMovementActionActive())
		{
			float tVerticalJoyAxis = ControllableUnit.getJoyAxisVerticalLeft();
			float tHorizontalJoyAxis = ControllableUnit.getJoyAxisHorizontalLeft();
			ControllableUnit._movement_vector.x = tHorizontalJoyAxis;
			ControllableUnit._movement_vector.y = tVerticalJoyAxis;
		}
	}

	// Token: 0x06001F3D RID: 7997 RVA: 0x0010F65C File Offset: 0x0010D85C
	private static void updateMovementVectorKeyboard()
	{
		ControllableUnit._movement_vector = Vector2.zero;
		if (HotkeyLibrary.up.isHolding())
		{
			ControllableUnit._movement_vector.y = 1f;
		}
		else if (HotkeyLibrary.down.isHolding())
		{
			ControllableUnit._movement_vector.y = -1f;
		}
		if (HotkeyLibrary.right.isHolding())
		{
			ControllableUnit._movement_vector.x = 1f;
			return;
		}
		if (HotkeyLibrary.left.isHolding())
		{
			ControllableUnit._movement_vector.x = -1f;
		}
	}

	// Token: 0x06001F3E RID: 7998 RVA: 0x0010F6E4 File Offset: 0x0010D8E4
	public static void updateCamera()
	{
		Vector2 tPos = ControllableUnit._unit_main.current_position;
		Vector3 tCam = World.world.camera.transform.position;
		tCam.x = tPos.x;
		tCam.y = tPos.y;
		float tSpeed = 1f / World.world.camera.orthographicSize;
		World.world.camera.transform.position = Vector3.Lerp(World.world.camera.transform.position, tCam, tSpeed);
	}

	// Token: 0x06001F3F RID: 7999 RVA: 0x0010F770 File Offset: 0x0010D970
	public static float getJoyAxisVerticalRight()
	{
		return UltimateJoystick.GetVerticalAxis("JoyRight");
	}

	// Token: 0x06001F40 RID: 8000 RVA: 0x0010F77C File Offset: 0x0010D97C
	public static float getJoyAxisHorizontalRight()
	{
		return UltimateJoystick.GetHorizontalAxis("JoyRight");
	}

	// Token: 0x06001F41 RID: 8001 RVA: 0x0010F788 File Offset: 0x0010D988
	private static float getJoyAxisVerticalLeft()
	{
		return UltimateJoystick.GetVerticalAxis("JoyLeft");
	}

	// Token: 0x06001F42 RID: 8002 RVA: 0x0010F794 File Offset: 0x0010D994
	private static float getJoyAxisHorizontalLeft()
	{
		return UltimateJoystick.GetHorizontalAxis("JoyLeft");
	}

	// Token: 0x040016BB RID: 5819
	private const float TOUCH_ATTACK_START_DELAY = 0.05f;

	// Token: 0x040016BC RID: 5820
	private static Actor _unit_main = null;

	// Token: 0x040016BD RID: 5821
	private static HashSet<Actor> _units = new HashSet<Actor>();

	// Token: 0x040016BE RID: 5822
	private static Vector2 _movement_vector;

	// Token: 0x040016BF RID: 5823
	private static Vector2 _click_vector;

	// Token: 0x040016C0 RID: 5824
	private static bool _action_pressed_jump = false;

	// Token: 0x040016C1 RID: 5825
	private static bool _action_pressed_dash = false;

	// Token: 0x040016C2 RID: 5826
	private static bool _action_pressed_backstep = false;

	// Token: 0x040016C3 RID: 5827
	private static bool _action_pressed_steal = false;

	// Token: 0x040016C4 RID: 5828
	private static bool _action_pressed_swear = false;

	// Token: 0x040016C5 RID: 5829
	private static bool _action_pressed_talk = false;

	// Token: 0x040016C6 RID: 5830
	private static bool _attack_pressed_button_left = false;

	// Token: 0x040016C7 RID: 5831
	private static bool _attack_pressed_button_right = false;

	// Token: 0x040016C8 RID: 5832
	private static bool _attack_just_pressed_button_left = false;

	// Token: 0x040016C9 RID: 5833
	private static bool _attack_just_pressed_button_right = false;

	// Token: 0x040016CA RID: 5834
	private static float _touch_attack_started_at;

	// Token: 0x040016CB RID: 5835
	private static bool _touch_attack_just_started;

	// Token: 0x040016CC RID: 5836
	private static string[] _possessed_icons = new string[]
	{
		"ui/Icons/iconBre",
		"ui/Icons/iconCrying",
		"ui/Icons/iconAngry",
		"ui/Icons/actor_traits/iconStupid",
		"ui/Icons/actor_traits/iconStrongMinded",
		"ui/Icons/iconDead"
	};
}
