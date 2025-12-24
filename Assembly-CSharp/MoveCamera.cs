using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000474 RID: 1140
public class MoveCamera : BaseMapObject
{
	// Token: 0x060026FA RID: 9978 RVA: 0x0013C493 File Offset: 0x0013A693
	private void Awake()
	{
		MoveCamera.instance = this;
		this.main_camera = Camera.main;
	}

	// Token: 0x060026FB RID: 9979 RVA: 0x0013C4A6 File Offset: 0x0013A6A6
	internal override void create()
	{
		base.create();
		this.resetZoom();
		this._target_zoom = this.main_camera.orthographicSize;
	}

	// Token: 0x060026FC RID: 9980 RVA: 0x0013C4C5 File Offset: 0x0013A6C5
	public static Actor getFocusUnit()
	{
		return MoveCamera._focus_unit;
	}

	// Token: 0x060026FD RID: 9981 RVA: 0x0013C4CC File Offset: 0x0013A6CC
	public static void setFocusUnit(Actor pActor)
	{
		MoveCamera._focus_unit = pActor;
	}

	// Token: 0x060026FE RID: 9982 RVA: 0x0013C4D4 File Offset: 0x0013A6D4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool hasFocusUnit()
	{
		return MoveCamera._focus_unit != null;
	}

	// Token: 0x060026FF RID: 9983 RVA: 0x0013C4DE File Offset: 0x0013A6DE
	public static bool isCameraFollowingUnit(Actor pActor)
	{
		return MoveCamera._focus_unit == pActor;
	}

	// Token: 0x06002700 RID: 9984 RVA: 0x0013C4E8 File Offset: 0x0013A6E8
	internal void focusOn(Vector3 pPos)
	{
		this.clearFocusUnitAndUnselect();
		this._target_zoom = 15f;
		this._focus_zoom = this._target_zoom;
		pPos.z = base.transform.position.z;
		base.transform.position = pPos;
	}

	// Token: 0x06002701 RID: 9985 RVA: 0x0013C538 File Offset: 0x0013A738
	internal void focusOn(Vector3 pPos, Action pFocusReachedCallback, Action pFocusCancelCallback)
	{
		this.clearFocusUnitAndUnselect();
		this._target_zoom = 15f;
		this._focus_zoom = this._target_zoom;
		this._focus_reached_callback = pFocusReachedCallback;
		this._focus_cancel_callback = pFocusCancelCallback;
		pPos.z = base.transform.position.z;
		base.transform.position = pPos;
	}

	// Token: 0x06002702 RID: 9986 RVA: 0x0013C594 File Offset: 0x0013A794
	internal void focusOnAndFollow(Actor pActor, Action pFocusReachedCallback, Action pFocusCancelCallback)
	{
		this.clearFocusUnitAndUnselect();
		Config.ui_main_hidden = false;
		this._target_zoom = 15f;
		this._focus_zoom = this._target_zoom;
		this._focus_reached_callback = pFocusReachedCallback;
		this._focus_cancel_callback = pFocusCancelCallback;
		MoveCamera._focus_unit = pActor;
		this._focus_timer = 0f;
		WorldTip.addWordReplacement("$name$", MoveCamera._focus_unit.coloredName);
		WorldTip.showNowTop("tip_following_unit", true);
		PowerTracker.spectatingUnit(MoveCamera._focus_unit.getName());
		PowerButtonSelector.instance.setPower(PowerButtonSelector.instance.followUnit);
	}

	// Token: 0x06002703 RID: 9987 RVA: 0x0013C628 File Offset: 0x0013A828
	internal void resetZoom()
	{
		int tInitialZoom;
		if (Screen.width < Screen.height)
		{
			tInitialZoom = Screen.width / 4;
		}
		else
		{
			tInitialZoom = Screen.height / 4;
		}
		if (MapBox.width > MapBox.height)
		{
			this.orthographic_size_max = (float)((int)((float)MapBox.width * 1.1f));
		}
		else
		{
			this.orthographic_size_max = (float)((int)((float)MapBox.height * 1.1f));
		}
		if ((float)tInitialZoom > this.orthographic_size_max)
		{
			tInitialZoom = (int)this.orthographic_size_max;
		}
		this._target_zoom = (float)tInitialZoom;
		this.main_camera.orthographicSize = Mathf.Clamp(this._target_zoom, 10f, this.orthographic_size_max);
		World.world.setZoomOrthographic(this.main_camera.orthographicSize);
		this._mouse_controls_used_last = false;
		this.main_camera.farClipPlane = (float)MapBox.height * 1.1f;
	}

	// Token: 0x06002704 RID: 9988 RVA: 0x0013C6F7 File Offset: 0x0013A8F7
	public void forceZoom(float pZoom)
	{
		this._target_zoom = pZoom;
		this.zoomToBounds(true);
	}

	// Token: 0x06002705 RID: 9989 RVA: 0x0013C707 File Offset: 0x0013A907
	public void setTargetZoom(float pValue)
	{
		this._target_zoom = pValue;
	}

	// Token: 0x06002706 RID: 9990 RVA: 0x0013C710 File Offset: 0x0013A910
	public float getTargetZoom()
	{
		return this._target_zoom;
	}

	// Token: 0x06002707 RID: 9991 RVA: 0x0013C718 File Offset: 0x0013A918
	private void updateZoomControls()
	{
		if (InputHelpers.touchSupported)
		{
			bool tJoyActive = false;
			if (UltimateJoystick.getJoyCount() == 2)
			{
				tJoyActive = (UltimateJoystick.GetJoystickState("JoyRight") || UltimateJoystick.GetJoystickState("JoyLeft"));
			}
			if (tJoyActive)
			{
				return;
			}
			bool tAllowZoom = !World.world.player_control.already_used_power || ControllableUnit.isControllingUnit();
			if (InputHelpers.touchCount == 2 && tAllowZoom)
			{
				World.world.player_control.already_used_zoom = true;
				Touch tTouchZero = Input.GetTouch(0);
				Touch tTouchOne = Input.GetTouch(1);
				Vector2 a = tTouchZero.position - tTouchZero.deltaPosition;
				Vector2 tTouchOnePrevPos = tTouchOne.position - tTouchOne.deltaPosition;
				float magnitude = (a - tTouchOnePrevPos).magnitude;
				float tTouchDeltaMag = (tTouchZero.position - tTouchOne.position).magnitude;
				float tDeltaMagnitudeDiff = magnitude - tTouchDeltaMag;
				this._target_zoom += tDeltaMagnitudeDiff * 0.2f * (this.main_camera.orthographicSize * 0.015f);
			}
		}
		if (MoveCamera.inSpectatorMode())
		{
			this.followFocusUnit();
		}
	}

	// Token: 0x06002708 RID: 9992 RVA: 0x0013C82D File Offset: 0x0013AA2D
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool inSpectatorMode()
	{
		if (MoveCamera._spectator_mode && !MoveCamera.hasFocusUnit())
		{
			MoveCamera.instance.clearFocusUnitAndUnselect();
		}
		MoveCamera._spectator_mode = MoveCamera.hasFocusUnit();
		return MoveCamera._spectator_mode;
	}

	// Token: 0x06002709 RID: 9993 RVA: 0x0013C858 File Offset: 0x0013AA58
	private void checkFocusReached()
	{
		if (this.main_camera.orthographicSize == this._focus_zoom)
		{
			if (this._focus_reached_callback != null)
			{
				this._focus_reached_callback();
			}
			this.clearFocus();
		}
		if (this._target_zoom != this._focus_zoom)
		{
			if (this._focus_cancel_callback != null)
			{
				this._focus_cancel_callback();
			}
			this.clearFocus();
		}
	}

	// Token: 0x0600270A RID: 9994 RVA: 0x0013C8B8 File Offset: 0x0013AAB8
	private void followFocusUnit()
	{
		if (!MoveCamera.hasFocusUnit())
		{
			return;
		}
		Actor tFocusActor = MoveCamera._focus_unit;
		if (!tFocusActor.isAlive())
		{
			BaseSimObject attackedBy = tFocusActor.attackedBy;
			Actor tAttackedBy = (attackedBy != null) ? attackedBy.a : null;
			if (tAttackedBy != null && tAttackedBy.isAlive())
			{
				WorldTip.addWordReplacement("$name$", tFocusActor.coloredName);
				WorldTip.addWordReplacement("$killer$", tAttackedBy.coloredName);
				WorldTip.showNowTop("tip_followed_unit_killed", true);
				Actor a = tAttackedBy.a;
				tFocusActor.attackedBy = null;
				MoveCamera.setFocusUnit(a);
				this._focus_timer = 0f;
				return;
			}
			WorldTip.addWordReplacement("$name$", tFocusActor.coloredName);
			WorldTip.showNowTop("tip_followed_unit_died", true);
			this.clearFocusUnitAndUnselect();
			return;
		}
		else
		{
			if (MoveCamera.camera_drag_run || InputHelpers.touchCount > 0)
			{
				this._focus_timer = 0f;
				return;
			}
			Vector3 tPos = tFocusActor.current_position;
			tPos.z = base.transform.position.z;
			if (this._focus_timer <= 1f)
			{
				this._focus_timer += Time.deltaTime;
				this._focus_timer = Mathf.Clamp(this._focus_timer, 0f, 1f);
				tPos.x = iTween.easeOutCubic(base.transform.position.x, tPos.x, this._focus_timer);
				tPos.y = iTween.easeOutCubic(base.transform.position.y, tPos.y, this._focus_timer);
			}
			base.transform.position = tPos;
			return;
		}
	}

	// Token: 0x0600270B RID: 9995 RVA: 0x0013CA3B File Offset: 0x0013AC3B
	private void clearFocus()
	{
		this._focus_reached_callback = null;
		this._focus_cancel_callback = null;
		this._focus_zoom = -1000000f;
	}

	// Token: 0x0600270C RID: 9996 RVA: 0x0013CA56 File Offset: 0x0013AC56
	public static void clearFocusUnitOnly()
	{
		MoveCamera._focus_unit = null;
	}

	// Token: 0x0600270D RID: 9997 RVA: 0x0013CA5E File Offset: 0x0013AC5E
	internal void clearFocusUnitAndUnselect()
	{
		MoveCamera.clearFocusUnitOnly();
		this._focus_timer = 0f;
		if (World.world.isSelectedPower("follow_unit"))
		{
			PowerButtonSelector.instance.unselectAll();
		}
	}

	// Token: 0x0600270E RID: 9998 RVA: 0x0013CA8C File Offset: 0x0013AC8C
	private void zoomToBounds(bool pForce = false)
	{
		float tMaxZoom = World.world.player_control.isSelectionHappens() ? World.world.quality_changer.getZoomRateBoundLow() : this.orthographic_size_max;
		this._target_zoom = Mathf.Clamp(this._target_zoom, 10f, tMaxZoom);
		if (this.main_camera.orthographicSize == this._target_zoom)
		{
			return;
		}
		if (this._target_zoom > this.main_camera.orthographicSize)
		{
			this.main_camera.orthographicSize += Time.deltaTime * this.camera_zoom_speed * (Mathf.Abs(this.main_camera.orthographicSize - this._target_zoom) + 5f);
			if (this.main_camera.orthographicSize > this._target_zoom)
			{
				this.main_camera.orthographicSize = Mathf.Clamp(this._target_zoom, 10f, this.orthographic_size_max);
			}
		}
		else if (this._target_zoom < this.main_camera.orthographicSize)
		{
			this.main_camera.orthographicSize -= Time.deltaTime * this.camera_zoom_speed * (Mathf.Abs(this.main_camera.orthographicSize - this._target_zoom) + 5f);
			if (this.main_camera.orthographicSize < this._target_zoom)
			{
				this.main_camera.orthographicSize = Mathf.Clamp(this._target_zoom, 10f, this.orthographic_size_max);
			}
		}
		if (pForce)
		{
			this.main_camera.orthographicSize = this._target_zoom;
		}
		World.world.setZoomOrthographic(this.main_camera.orthographicSize);
	}

	// Token: 0x0600270F RID: 9999 RVA: 0x0013CC24 File Offset: 0x0013AE24
	private void updateMouseCameraDrag()
	{
		if (ControllableUnit.isControllingUnit())
		{
			return;
		}
		MoveCamera.camera_drag_run = false;
		bool tInputDetectedDown = false;
		bool tInputDetected = false;
		if (InputHelpers.mouseSupported)
		{
			tInputDetectedDown = this.checkMouseInputDown();
			tInputDetected = this.checkMouseInput();
		}
		if (!tInputDetected)
		{
			this.clearTouches();
			return;
		}
		if (tInputDetectedDown && World.world.isOverUI())
		{
			this.clearTouches();
			return;
		}
		if (tInputDetectedDown && this._origin.x == -1f && this._origin.z == -1f)
		{
			this._origin = this.getMousePos();
		}
		if (this._origin.x == -1f && this._origin.y == -1f && this._origin.z == -1f)
		{
			return;
		}
		if (tInputDetected)
		{
			MoveCamera.camera_drag_run = true;
			Vector3 tCurTransformPos = base.transform.position;
			tCurTransformPos.z = 0f;
			Vector3 tDifference = this.getMousePos() - tCurTransformPos;
			if (Toolbox.DistVec3(this._origin, this.getMousePos()) > 0.1f)
			{
				MoveCamera.camera_drag_activated = true;
				MoveCamera.camera_drag_activated_frame = Time.frameCount;
			}
			Vector3 tNew = this._origin - tDifference;
			tNew.z = 0f;
			if (InputHelpers.touchSupported)
			{
				MoveCamera._touch_dist = Toolbox.DistVec3(this._first_touch, this.getTouchPos(true));
				if (World.world.player_control.touch_ticks_skip > 5)
				{
					if (MoveCamera._touch_dist >= 20f || (float)World.world.player_control.touch_ticks_skip > 0.3f)
					{
						World.world.player_control.already_used_zoom = true;
						World.world.player_control.already_used_power = false;
					}
				}
				else if (InputHelpers.touchCount == 1)
				{
					return;
				}
			}
			if (!InputHelpers.mouseSupported)
			{
				return;
			}
			Vector3 tOldPosition = tCurTransformPos;
			base.transform.position = tNew;
			Vector2 tMovementDelta = tNew - tOldPosition;
			if (tMovementDelta.magnitude > 0.01f)
			{
				Vector2 tToAdd = tMovementDelta * 0.2f;
				this.addVelocity(tToAdd.x, tToAdd.y);
				this._mouse_controls_used_last = true;
			}
			else
			{
				this._move_velocity = Vector2.zero;
			}
			this.checkDistanceMoved(tOldPosition);
			this.cameraToBounds();
		}
	}

	// Token: 0x06002710 RID: 10000 RVA: 0x0013CE50 File Offset: 0x0013B050
	private void updateVelocity()
	{
		Vector2 move_velocity = this._move_velocity;
		if (move_velocity.x == 0f && move_velocity.y == 0f)
		{
			return;
		}
		float tDecayFactor = this.getDecayFactor();
		this._move_velocity *= tDecayFactor;
		if (Mathf.Abs(this._move_velocity.x) < 0.01f)
		{
			this._move_velocity.x = 0f;
		}
		if (Mathf.Abs(this._move_velocity.y) < 0.01f)
		{
			this._move_velocity.y = 0f;
		}
		if (InputHelpers.mouseSupported && InputHelpers.GetMouseButton(1))
		{
			return;
		}
		Vector3 tVelocityVec3 = this._move_velocity;
		base.transform.position += tVelocityVec3;
		this.setWhooshState(WhooshState.NeedWhoosh);
		this.cameraToBounds();
	}

	// Token: 0x06002711 RID: 10001 RVA: 0x0013CF29 File Offset: 0x0013B129
	private float getDecayFactor()
	{
		if (this._mouse_controls_used_last)
		{
			return Mathf.Pow(0.8f, Time.deltaTime / 0.016666668f);
		}
		return 0.8f;
	}

	// Token: 0x06002712 RID: 10002 RVA: 0x0013CF50 File Offset: 0x0013B150
	private void checkDistanceMoved(Vector3 pOldPosition)
	{
		float num = Toolbox.DistVec3(base.transform.position, pOldPosition);
		Vector3 tBL = this.main_camera.ScreenToWorldPoint(new Vector3(0f, 0f, this.main_camera.nearClipPlane));
		float tDragThreshold = (this.main_camera.ScreenToWorldPoint(new Vector3((float)Screen.width, (float)Screen.height, this.main_camera.nearClipPlane)) - tBL).magnitude * 0.007f;
		if (num > tDragThreshold)
		{
			GodPower selected_power = World.world.selected_power;
			if (selected_power != null && selected_power.set_used_camera_drag_on_long_move)
			{
				World.world.player_control.already_used_camera_drag = true;
			}
		}
		if (num > tDragThreshold * 1.2f)
		{
			this.setWhooshState(WhooshState.NeedWhoosh);
		}
	}

	// Token: 0x06002713 RID: 10003 RVA: 0x0013D00C File Offset: 0x0013B20C
	private bool checkMouseInputDown()
	{
		return InputHelpers.GetMouseButtonDown(1) || InputHelpers.GetMouseButtonDown(2) || (InputHelpers.GetMouseButtonDown(0) && (!Input.mousePresent || MapBox.isRenderMiniMap()));
	}

	// Token: 0x06002714 RID: 10004 RVA: 0x0013D03F File Offset: 0x0013B23F
	private bool checkMouseInput()
	{
		return InputHelpers.GetMouseButton(1) || InputHelpers.GetMouseButton(2) || (InputHelpers.GetMouseButton(0) && !Input.mousePresent);
	}

	// Token: 0x06002715 RID: 10005 RVA: 0x0013D06C File Offset: 0x0013B26C
	private void clearTouches()
	{
		this._first_touch.Set(-1f, -1f, -1f);
		this._origin.Set(-1f, -1f, -1f);
		if (MoveCamera.camera_drag_activated && Time.frameCount > MoveCamera.camera_drag_activated_frame + 2)
		{
			MoveCamera.camera_drag_activated = false;
		}
	}

	// Token: 0x06002716 RID: 10006 RVA: 0x0013D0C8 File Offset: 0x0013B2C8
	private void cameraToBounds()
	{
		Vector3 pos = default(Vector3);
		pos.x = Mathf.Clamp(base.transform.position.x, 0f, (float)MapBox.width);
		pos.y = Mathf.Clamp(base.transform.position.y, 0f, (float)MapBox.height);
		pos.z = -0.5f;
		base.transform.position = pos;
		World.world.nameplate_manager.update();
	}

	// Token: 0x06002717 RID: 10007 RVA: 0x0013D154 File Offset: 0x0013B354
	private Vector3 getTouchPos(bool pScreenCoords = false)
	{
		Vector2 tTouchPositions = default(Vector2);
		int tTouches = 0;
		int tTouchCount = InputHelpers.touchCount;
		for (int i = 0; i < tTouchCount; i++)
		{
			Touch tTouch = Input.GetTouch(i);
			if (tTouch.phase != TouchPhase.Canceled && tTouch.phase != TouchPhase.Ended)
			{
				tTouchPositions += tTouch.position;
				tTouches++;
			}
		}
		Vector3 tVec = tTouchPositions / (float)tTouches;
		if (pScreenCoords)
		{
			return tVec;
		}
		return this.main_camera.ScreenToWorldPoint(tVec);
	}

	// Token: 0x06002718 RID: 10008 RVA: 0x0013D1CF File Offset: 0x0013B3CF
	private Vector3 getMousePos()
	{
		if (InputHelpers.mouseSupported)
		{
			return World.world.getMousePos();
		}
		return Vector3.one;
	}

	// Token: 0x06002719 RID: 10009 RVA: 0x0013D1ED File Offset: 0x0013B3ED
	private void setWhooshState(WhooshState pState)
	{
		if (pState == WhooshState.NeedWhoosh && this._whoosh_state == WhooshState.WhooshPlayed)
		{
			return;
		}
		this._whoosh_state = pState;
	}

	// Token: 0x0600271A RID: 10010 RVA: 0x0013D204 File Offset: 0x0013B404
	private bool isNoInputDetected()
	{
		return this._move_velocity.x == 0f && this._move_velocity.y == 0f && InputHelpers.touchCount == 0 && (!InputHelpers.GetMouseButton(0) && !InputHelpers.GetMouseButton(1)) && !InputHelpers.GetMouseButton(2);
	}

	// Token: 0x0600271B RID: 10011 RVA: 0x0013D259 File Offset: 0x0013B459
	private void LateUpdate()
	{
		this.updateVisibleBounds();
		if (World.world.tutorial.isActive())
		{
			return;
		}
		if (this._whoosh_state == WhooshState.NeedWhoosh)
		{
			this.setWhooshState(WhooshState.WhooshPlayed);
		}
		if (this.isNoInputDetected())
		{
			this.setWhooshState(WhooshState.Idle);
		}
	}

	// Token: 0x0600271C RID: 10012 RVA: 0x0013D294 File Offset: 0x0013B494
	private void updateVisibleBounds()
	{
		Vector3 tBarLeftCorner = ToolbarButtons.instance.getPowerBarLeftCornerViewportPos();
		Vector2 tBarWorldPosition = World.world.camera.ScreenToWorldPoint(tBarLeftCorner);
		this.power_bar_position_y = tBarWorldPosition.y;
		if (this.power_bar_position_y < 0f)
		{
			this.power_bar_position_y = 0f;
		}
		Camera camera = this.main_camera;
		float tZ = camera.nearClipPlane;
		Vector3 tWorldBL = camera.ViewportToWorldPoint(new Vector3(0f, 0f, tZ));
		Vector3 tWorldTR = camera.ViewportToWorldPoint(new Vector3(1f, 1f, tZ));
		this._visible_bounds.x = tWorldBL.x;
		this._visible_bounds.y = tWorldBL.y;
		this._visible_bounds.width = tWorldTR.x - this._visible_bounds.x;
		this._visible_bounds.height = tWorldTR.y - this._visible_bounds.y;
		this._visible_bounds_without_power_bar.x = tWorldBL.x;
		this._visible_bounds_without_power_bar.y = this.power_bar_position_y;
		this._visible_bounds_without_power_bar.width = tWorldTR.x - this._visible_bounds_without_power_bar.x;
		this._visible_bounds_without_power_bar.height = tWorldTR.y - this._visible_bounds_without_power_bar.y;
	}

	// Token: 0x0600271D RID: 10013 RVA: 0x0013D3E0 File Offset: 0x0013B5E0
	public bool isWithinCameraView(Vector2 pPos)
	{
		Rect tBounds = this._visible_bounds;
		return this.checkBounds(pPos, tBounds);
	}

	// Token: 0x0600271E RID: 10014 RVA: 0x0013D3FC File Offset: 0x0013B5FC
	public bool isWithinCameraViewNotPowerBar(Vector2 pPos)
	{
		Rect tBounds = this._visible_bounds_without_power_bar;
		return this.checkBounds(pPos, tBounds);
	}

	// Token: 0x0600271F RID: 10015 RVA: 0x0013D418 File Offset: 0x0013B618
	private bool checkBounds(Vector2 pPos, Rect pBounds)
	{
		return pBounds.Contains(pPos);
	}

	// Token: 0x06002720 RID: 10016 RVA: 0x0013D424 File Offset: 0x0013B624
	public void update()
	{
		if (World.world.tutorial.isActive())
		{
			return;
		}
		int tWidth = this.main_camera.pixelWidth;
		int tHeight = this.main_camera.pixelHeight;
		if (this._last_width != (float)tWidth || this._last_height != (float)tHeight)
		{
			this._last_width = (float)tWidth;
			this._last_height = (float)tHeight;
			if (this._skip_reset_zoom)
			{
				this._skip_reset_zoom = false;
				return;
			}
			this.resetZoom();
			return;
		}
		else
		{
			if (Globals.TRAILER_MODE)
			{
				this.updateTrailerMode();
			}
			if (InputHelpers.touchCount > 0)
			{
				if (Input.GetTouch(0).phase == TouchPhase.Began && World.world.isOverUI())
				{
					this._first_touch_on_ui = true;
				}
			}
			else
			{
				this._first_touch_on_ui = false;
			}
			if (!ScrollWindow.isWindowActive() && (!World.world.isOverUI() || MoveCamera.inSpectatorMode()))
			{
				this.updateZoomControls();
			}
			if (this._target_zoom != this.main_camera.orthographicSize)
			{
				this.zoomToBounds(false);
			}
			if (this._focus_zoom > -1000000f)
			{
				this.checkFocusReached();
			}
			if (World.world.isGameplayControlsLocked() || ScrollWindow.isAnimationActive() || this._first_touch_on_ui)
			{
				this.clearTouches();
				this._old_touch_positions[0] = null;
				this._old_touch_positions[1] = null;
				return;
			}
			if (InputHelpers.touchSupported)
			{
				this.updateMobileCamera();
			}
			if (!InputHelpers.mouseSupported)
			{
				return;
			}
			if (!InputHelpers.touchSupported || InputHelpers.touchCount <= 0)
			{
				this.updateMouseCameraDrag();
				if (!ScrollWindow.isWindowActive() && !ControllableUnit.isControllingUnit())
				{
					this.updateVelocity();
				}
			}
			return;
		}
	}

	// Token: 0x06002721 RID: 10017 RVA: 0x0013D5A8 File Offset: 0x0013B7A8
	public Vector2 getVelocity()
	{
		return this._move_velocity;
	}

	// Token: 0x06002722 RID: 10018 RVA: 0x0013D5B0 File Offset: 0x0013B7B0
	private bool ignoreTouchControls()
	{
		return World.world.isOverUI() || ScrollWindow.isWindowActive() || ScrollWindow.isAnimationActive();
	}

	// Token: 0x06002723 RID: 10019 RVA: 0x0013D5CC File Offset: 0x0013B7CC
	private void updateMobileCamera()
	{
		if (InputHelpers.touchCount == 0)
		{
			this._old_touch_positions[0] = null;
			this._old_touch_positions[1] = null;
			return;
		}
		if (World.world.isAnyPowerSelected() && World.world.selected_power.hold_action && InputHelpers.touchCount == 1)
		{
			return;
		}
		if (World.world.player_control.already_used_power)
		{
			return;
		}
		if (ControllableUnit.isControllingUnit())
		{
			return;
		}
		Vector3 tPrevPosition = base.transform.position;
		if (InputHelpers.touchCount == 1)
		{
			if (this._old_touch_positions[0] == null || this._old_touch_positions[1] != null)
			{
				this._old_touch_positions[0] = new Vector2?(Input.GetTouch(0).position);
				this._old_touch_positions[1] = null;
			}
			else
			{
				Vector2 tNewTouchPosition = Input.GetTouch(0).position;
				Vector3 position = base.transform.position;
				Transform transform = base.transform;
				Vector2? vector = (this._old_touch_positions[0] - tNewTouchPosition) * this.main_camera.orthographicSize;
				float d = (float)this.main_camera.pixelHeight;
				Vector3 tMovedPosition = transform.TransformDirection(((vector != null) ? new Vector2?(vector.GetValueOrDefault() / d * 2f) : null).Value);
				Vector3 tNewPosition = position + tMovedPosition;
				base.transform.position = tNewPosition;
				this._old_touch_positions[0] = new Vector2?(tNewTouchPosition);
				this.cameraToBounds();
			}
		}
		else if (this._old_touch_positions[1] == null)
		{
			this._old_touch_positions[0] = new Vector2?(Input.GetTouch(0).position);
			this._old_touch_positions[1] = new Vector2?(Input.GetTouch(1).position);
			this._old_touch_vector = (this._old_touch_positions[0] - this._old_touch_positions[1]).Value;
			this._old_touch_distance = this._old_touch_vector.magnitude;
		}
		else
		{
			Vector2 screen = new Vector2((float)this.main_camera.pixelWidth, (float)this.main_camera.pixelHeight);
			Vector2[] newTouchPositions = new Vector2[]
			{
				Input.GetTouch(0).position,
				Input.GetTouch(1).position
			};
			Vector2 newTouchVector = newTouchPositions[0] - newTouchPositions[1];
			float newTouchDistance = newTouchVector.magnitude;
			base.transform.position += base.transform.TransformDirection(((this._old_touch_positions[0] + this._old_touch_positions[1] - screen) * this.main_camera.orthographicSize / screen.y).Value);
			if (newTouchDistance != 0f && this._old_touch_distance != newTouchDistance)
			{
				this.main_camera.orthographicSize = Mathf.Clamp(this.main_camera.orthographicSize * (this._old_touch_distance / newTouchDistance), 10f, this.orthographic_size_max);
			}
			World.world.setZoomOrthographic(this.main_camera.orthographicSize);
			base.transform.position -= base.transform.TransformDirection((newTouchPositions[0] + newTouchPositions[1] - screen) * this.main_camera.orthographicSize / screen.y);
			this.cameraToBounds();
			this._old_touch_positions[0] = new Vector2?(newTouchPositions[0]);
			this._old_touch_positions[1] = new Vector2?(newTouchPositions[1]);
			this._old_touch_vector = newTouchVector;
			this._old_touch_distance = newTouchDistance;
			World.world.player_control.already_used_zoom = true;
		}
		this.checkDistanceMoved(tPrevPosition);
	}

	// Token: 0x06002724 RID: 10020 RVA: 0x0013DB38 File Offset: 0x0013BD38
	private static float getMoveDistance(bool pFast = false)
	{
		float tDeltaTime = Time.deltaTime * 55f;
		if (pFast)
		{
			tDeltaTime *= 2.5f;
		}
		return tDeltaTime * MoveCamera.instance._target_zoom * MoveCamera.instance.camera_move_speed;
	}

	// Token: 0x06002725 RID: 10021 RVA: 0x0013DB74 File Offset: 0x0013BD74
	public static void move(HotkeyAsset pAsset)
	{
		float tMove = MoveCamera.getMoveDistance(pAsset.id.StartsWith("fast_"));
		string id = pAsset.id;
		uint num = <PrivateImplementationDetails>.ComputeStringHash(id);
		if (num <= 1849326364U)
		{
			if (num <= 1035581717U)
			{
				if (num != 306900080U)
				{
					if (num != 1035581717U)
					{
						goto IL_162;
					}
					if (!(id == "down"))
					{
						goto IL_162;
					}
				}
				else
				{
					if (!(id == "left"))
					{
						goto IL_162;
					}
					goto IL_151;
				}
			}
			else if (num != 1128467232U)
			{
				if (num != 1849326364U)
				{
					goto IL_162;
				}
				if (!(id == "fast_down"))
				{
					goto IL_162;
				}
			}
			else
			{
				if (!(id == "up"))
				{
					goto IL_162;
				}
				goto IL_11A;
			}
			MoveCamera.instance.addVelocity(0f, -tMove);
			goto IL_162;
		}
		if (num <= 2129743154U)
		{
			if (num != 2028154341U)
			{
				if (num != 2129743154U)
				{
					goto IL_162;
				}
				if (!(id == "fast_right"))
				{
					goto IL_162;
				}
			}
			else if (!(id == "right"))
			{
				goto IL_162;
			}
			MoveCamera.instance.addVelocity(tMove, 0f);
			goto IL_162;
		}
		if (num != 2401780753U)
		{
			if (num != 2963289565U)
			{
				goto IL_162;
			}
			if (!(id == "fast_up"))
			{
				goto IL_162;
			}
		}
		else
		{
			if (!(id == "fast_left"))
			{
				goto IL_162;
			}
			goto IL_151;
		}
		IL_11A:
		MoveCamera.instance.addVelocity(0f, tMove);
		goto IL_162;
		IL_151:
		MoveCamera.instance.addVelocity(-tMove, 0f);
		IL_162:
		MoveCamera.instance.clampVelocity();
		MoveCamera.instance._mouse_controls_used_last = false;
	}

	// Token: 0x06002726 RID: 10022 RVA: 0x0013DCF8 File Offset: 0x0013BEF8
	private void addVelocity(float pX, float pY)
	{
		this._move_velocity.x = this._move_velocity.x + pX;
		this._move_velocity.y = this._move_velocity.y + pY;
	}

	// Token: 0x06002727 RID: 10023 RVA: 0x0013DD1C File Offset: 0x0013BF1C
	private void clampVelocity()
	{
		float tMin = -this._target_zoom * this.camera_move_max;
		float tMax = this._target_zoom * this.camera_move_max;
		this._move_velocity.y = Mathf.Clamp(this._move_velocity.y, tMin, tMax);
		this._move_velocity.x = Mathf.Clamp(this._move_velocity.x, tMin, tMax);
	}

	// Token: 0x06002728 RID: 10024 RVA: 0x0013DD80 File Offset: 0x0013BF80
	public static void zoomIn(HotkeyAsset pAsset)
	{
		MoveCamera.instance._target_zoom -= MoveCamera.instance.main_camera.orthographicSize * 0.05f;
	}

	// Token: 0x06002729 RID: 10025 RVA: 0x0013DDA8 File Offset: 0x0013BFA8
	public static void zoomOut(HotkeyAsset pAsset)
	{
		MoveCamera.instance._target_zoom += MoveCamera.instance.main_camera.orthographicSize * 0.05f;
	}

	// Token: 0x0600272A RID: 10026 RVA: 0x0013DDD0 File Offset: 0x0013BFD0
	public static void zoomInWheel(HotkeyAsset pAsset)
	{
		MoveCamera.instance._target_zoom -= MoveCamera.instance.main_camera.orthographicSize * 0.2f;
	}

	// Token: 0x0600272B RID: 10027 RVA: 0x0013DDF8 File Offset: 0x0013BFF8
	public static void zoomOutWheel(HotkeyAsset pAsset)
	{
		MoveCamera.instance._target_zoom += MoveCamera.instance.main_camera.orthographicSize * 0.2f;
	}

	// Token: 0x0600272C RID: 10028 RVA: 0x0013DE20 File Offset: 0x0013C020
	private void updateTrailerMode()
	{
		if (Input.GetKeyUp(KeyCode.F10))
		{
			this.camera_zoom_speed -= 0.2f;
			if (this.camera_zoom_speed < 0f)
			{
				this.camera_zoom_speed = 0.2f;
			}
		}
		if (Input.GetKeyUp(KeyCode.F11))
		{
			this.camera_zoom_speed += 0.2f;
		}
		if (Input.GetKeyUp(KeyCode.O))
		{
			this.camera_move_max -= 0.1f;
			if (this.camera_move_max < 0.01f)
			{
				this.camera_move_max = 0.01f;
			}
		}
		if (Input.GetKeyUp(KeyCode.P))
		{
			this.camera_move_max += 0.1f;
		}
		if (Input.GetKeyUp(KeyCode.K))
		{
			this.camera_move_speed -= 0.01f;
			if (this.camera_move_speed < 0.01f)
			{
				this.camera_move_speed = 0.01f;
			}
		}
		if (Input.GetKeyUp(KeyCode.L))
		{
			this.camera_move_speed += 0.01f;
		}
		if (Input.GetKeyDown(KeyCode.R) && this._target_zoom != this.main_camera.orthographicSize)
		{
			if (this._target_zoom > this.main_camera.orthographicSize)
			{
				this._target_zoom = this.main_camera.orthographicSize + this._target_zoom * 0.1f;
				return;
			}
			this._target_zoom = this.main_camera.orthographicSize - this._target_zoom * 0.1f;
		}
	}

	// Token: 0x0600272D RID: 10029 RVA: 0x0013DF8C File Offset: 0x0013C18C
	public void debug(DebugTool pTool)
	{
		pTool.setText("bounds_normal:", this._visible_bounds, 0f, false, 0L, false, false, "");
		pTool.setText("bounds_wth_power_bar:", this._visible_bounds_without_power_bar, 0f, false, 0L, false, false, "");
		pTool.setText("is_no_input_detected:", this.isNoInputDetected(), 0f, false, 0L, false, false, "");
		pTool.setText("_whooshState:", this._whoosh_state, 0f, false, 0L, false, false, "");
		pTool.setText("InputHelpers.touchCount:", InputHelpers.touchCount, 0f, false, 0L, false, false, "");
		pTool.setText("world.isGameplayControlsLocked():", World.world.isGameplayControlsLocked(), 0f, false, 0L, false, false, "");
		pTool.setText("ScrollWindow.animationActive:", ScrollWindow.isAnimationActive(), 0f, false, 0L, false, false, "");
		pTool.setText("firstTouchOnUI", this._first_touch_on_ui, 0f, false, 0L, false, false, "");
		pTool.setText("world.alreadyUsedZoom", World.world.player_control.already_used_zoom, 0f, false, 0L, false, false, "");
		pTool.setText("world.alreadyUsedPower", World.world.player_control.already_used_power, 0f, false, 0L, false, false, "");
		pTool.setText("world.already_used_camera_drag", World.world.player_control.already_used_camera_drag, 0f, false, 0L, false, false, "");
		pTool.setText("_touch_dist", MoveCamera._touch_dist, 0f, false, 0L, false, false, "");
		pTool.setText("cameraDragRun", MoveCamera.camera_drag_run, 0f, false, 0L, false, false, "");
		pTool.setText("camera_drag_activated", MoveCamera.camera_drag_activated, 0f, false, 0L, false, false, "");
		if (UltimateJoystick.getJoyCount() == 2)
		{
			pTool.setText("JoyRight", UltimateJoystick.GetJoystickState("JoyRight"), 0f, false, 0L, false, false, "");
			pTool.setText("JoyLeft", UltimateJoystick.GetJoystickState("JoyLeft"), 0f, false, 0L, false, false, "");
		}
	}

	// Token: 0x0600272E RID: 10030 RVA: 0x0013E213 File Offset: 0x0013C413
	public void skipResetZoom()
	{
		this._skip_reset_zoom = true;
	}

	// Token: 0x04001D4A RID: 7498
	private Vector3 _origin;

	// Token: 0x04001D4B RID: 7499
	private bool _is_zooming;

	// Token: 0x04001D4C RID: 7500
	internal const float ORTHOGRAPHIC_SIZE_MIN = 10f;

	// Token: 0x04001D4D RID: 7501
	internal float orthographic_size_max = 130f;

	// Token: 0x04001D4E RID: 7502
	private float _target_zoom;

	// Token: 0x04001D4F RID: 7503
	private Vector3 _first_touch;

	// Token: 0x04001D50 RID: 7504
	internal Camera main_camera;

	// Token: 0x04001D51 RID: 7505
	internal static MoveCamera instance;

	// Token: 0x04001D52 RID: 7506
	private WhooshState _whoosh_state;

	// Token: 0x04001D53 RID: 7507
	private Action _focus_reached_callback;

	// Token: 0x04001D54 RID: 7508
	private Action _focus_cancel_callback;

	// Token: 0x04001D55 RID: 7509
	private float _focus_zoom = -1000000f;

	// Token: 0x04001D56 RID: 7510
	private float _focus_timer;

	// Token: 0x04001D57 RID: 7511
	private static Actor _focus_unit;

	// Token: 0x04001D58 RID: 7512
	private static bool _spectator_mode;

	// Token: 0x04001D59 RID: 7513
	private static float _touch_dist;

	// Token: 0x04001D5A RID: 7514
	public static bool camera_drag_activated;

	// Token: 0x04001D5B RID: 7515
	public static int camera_drag_activated_frame;

	// Token: 0x04001D5C RID: 7516
	public static bool camera_drag_run;

	// Token: 0x04001D5D RID: 7517
	private float _last_width;

	// Token: 0x04001D5E RID: 7518
	private float _last_height;

	// Token: 0x04001D5F RID: 7519
	private bool _first_touch_on_ui;

	// Token: 0x04001D60 RID: 7520
	internal float camera_zoom_speed = 5f;

	// Token: 0x04001D61 RID: 7521
	internal float camera_move_speed = 0.01f;

	// Token: 0x04001D62 RID: 7522
	internal float camera_move_max = 0.06f;

	// Token: 0x04001D63 RID: 7523
	private Vector2 _move_velocity;

	// Token: 0x04001D64 RID: 7524
	private readonly Vector2?[] _old_touch_positions = new Vector2?[2];

	// Token: 0x04001D65 RID: 7525
	private Vector2 _old_touch_vector;

	// Token: 0x04001D66 RID: 7526
	private float _old_touch_distance;

	// Token: 0x04001D67 RID: 7527
	private Rect _visible_bounds;

	// Token: 0x04001D68 RID: 7528
	private Rect _visible_bounds_without_power_bar;

	// Token: 0x04001D69 RID: 7529
	public float power_bar_position_y;

	// Token: 0x04001D6A RID: 7530
	private bool _skip_reset_zoom;

	// Token: 0x04001D6B RID: 7531
	private bool _mouse_controls_used_last;
}
