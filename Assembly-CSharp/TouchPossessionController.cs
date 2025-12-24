using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200032D RID: 813
public class TouchPossessionController : MonoBehaviour
{
	// Token: 0x06001F60 RID: 8032 RVA: 0x0010FB60 File Offset: 0x0010DD60
	private void Awake()
	{
		TouchPossessionController.instance = this;
	}

	// Token: 0x06001F61 RID: 8033 RVA: 0x0010FB68 File Offset: 0x0010DD68
	private void OnEnable()
	{
		this.onResizeResolution((float)Screen.width, (float)Screen.height);
		this.checkButtonGraphics();
		this.setMode(PossessionActionMode.Attack);
	}

	// Token: 0x06001F62 RID: 8034 RVA: 0x0010FB89 File Offset: 0x0010DD89
	private void Update()
	{
		this.checkActiveButtons();
	}

	// Token: 0x06001F63 RID: 8035 RVA: 0x0010FB94 File Offset: 0x0010DD94
	private void checkActiveButtons()
	{
		if (!ControllableUnit.isControllingUnit())
		{
			return;
		}
		if (ControllableUnit.isControllingCrabzilla())
		{
			return;
		}
		Actor tActor = ControllableUnit.getControllableUnit();
		ActorAsset tAsset = tActor.asset;
		this._button_dash.gameObject.SetActive(tAsset.control_can_dash);
		this._button_jump.gameObject.SetActive(tAsset.control_can_jump);
		this._button_backstep.gameObject.SetActive(tAsset.control_can_backstep);
		this._button_attack.gameObject.SetActive(!tAsset.skip_fight_logic);
		this._button_kick.gameObject.SetActive(tAsset.control_can_kick);
		this._button_talk.gameObject.SetActive(tAsset.control_can_talk && !tActor.hasTrait("mute"));
		this._button_swear.gameObject.SetActive(tAsset.control_can_swear && !tActor.hasTrait("mute"));
		this._button_steal.gameObject.SetActive(tAsset.control_can_steal);
	}

	// Token: 0x06001F64 RID: 8036 RVA: 0x0010FC97 File Offset: 0x0010DE97
	private void onResizeResolution(float pWidth, float pHeight)
	{
		this._right_joystick.UpdateSizeAndPlacement(this._rect);
	}

	// Token: 0x06001F65 RID: 8037 RVA: 0x0010FCAA File Offset: 0x0010DEAA
	public static bool isActionPressedJump()
	{
		return TouchPossessionController._action_pressed_jump;
	}

	// Token: 0x06001F66 RID: 8038 RVA: 0x0010FCB1 File Offset: 0x0010DEB1
	public static bool isActionPressedDash()
	{
		return TouchPossessionController._action_pressed_dash;
	}

	// Token: 0x06001F67 RID: 8039 RVA: 0x0010FCB8 File Offset: 0x0010DEB8
	public static bool isActionPressedBackStep()
	{
		return TouchPossessionController._action_pressed_backstep;
	}

	// Token: 0x06001F68 RID: 8040 RVA: 0x0010FCBF File Offset: 0x0010DEBF
	public static bool isSelectedActionAttack()
	{
		return TouchPossessionController.isMode(PossessionActionMode.Attack);
	}

	// Token: 0x06001F69 RID: 8041 RVA: 0x0010FCC7 File Offset: 0x0010DEC7
	public static bool isSelectedActionTalk()
	{
		return TouchPossessionController.isMode(PossessionActionMode.Talk);
	}

	// Token: 0x06001F6A RID: 8042 RVA: 0x0010FCCF File Offset: 0x0010DECF
	public static bool isSelectedActionSwear()
	{
		return TouchPossessionController.isMode(PossessionActionMode.Swear);
	}

	// Token: 0x06001F6B RID: 8043 RVA: 0x0010FCD7 File Offset: 0x0010DED7
	public static bool isSelectedActionSteal()
	{
		return TouchPossessionController.isMode(PossessionActionMode.Steal);
	}

	// Token: 0x06001F6C RID: 8044 RVA: 0x0010FCDF File Offset: 0x0010DEDF
	public static bool isSelectedActionKick()
	{
		return TouchPossessionController.isMode(PossessionActionMode.Kick);
	}

	// Token: 0x06001F6D RID: 8045 RVA: 0x0010FCE7 File Offset: 0x0010DEE7
	public static void pressJump()
	{
		TouchPossessionController._action_pressed_jump = true;
	}

	// Token: 0x06001F6E RID: 8046 RVA: 0x0010FCEF File Offset: 0x0010DEEF
	public static void pressDash()
	{
		TouchPossessionController._action_pressed_dash = true;
	}

	// Token: 0x06001F6F RID: 8047 RVA: 0x0010FCF7 File Offset: 0x0010DEF7
	public static void pressBackStep()
	{
		TouchPossessionController._action_pressed_backstep = true;
	}

	// Token: 0x06001F70 RID: 8048 RVA: 0x0010FCFF File Offset: 0x0010DEFF
	public void selectModeAttack()
	{
		WorldTip.showNow("possession_action_mode_attack", true, "top", 3f, "#F3961F");
		this.setMode(PossessionActionMode.Attack);
	}

	// Token: 0x06001F71 RID: 8049 RVA: 0x0010FD22 File Offset: 0x0010DF22
	public void selectModeTalk()
	{
		WorldTip.showNow("possession_action_mode_talk", true, "top", 3f, "#F3961F");
		this.setMode(PossessionActionMode.Talk);
	}

	// Token: 0x06001F72 RID: 8050 RVA: 0x0010FD45 File Offset: 0x0010DF45
	public void selectModeSwear()
	{
		WorldTip.showNow("possession_action_mode_swear", true, "top", 3f, "#F3961F");
		this.setMode(PossessionActionMode.Swear);
	}

	// Token: 0x06001F73 RID: 8051 RVA: 0x0010FD68 File Offset: 0x0010DF68
	public void selectModeSteal()
	{
		WorldTip.showNow("possession_action_mode_steal", true, "top", 3f, "#F3961F");
		this.setMode(PossessionActionMode.Steal);
	}

	// Token: 0x06001F74 RID: 8052 RVA: 0x0010FD8B File Offset: 0x0010DF8B
	public void selectModeKick()
	{
		WorldTip.showNow("possession_action_mode_kick", true, "top", 3f, "#F3961F");
		this.setMode(PossessionActionMode.Kick);
	}

	// Token: 0x06001F75 RID: 8053 RVA: 0x0010FDAE File Offset: 0x0010DFAE
	private static bool isMode(PossessionActionMode pMode)
	{
		return TouchPossessionController._current_mode == pMode;
	}

	// Token: 0x06001F76 RID: 8054 RVA: 0x0010FDB8 File Offset: 0x0010DFB8
	private void setMode(PossessionActionMode pMode)
	{
		TouchPossessionController._current_mode = pMode;
		this.checkButtonGraphics();
	}

	// Token: 0x06001F77 RID: 8055 RVA: 0x0010FDC8 File Offset: 0x0010DFC8
	private void checkButtonGraphics()
	{
		foreach (PossessionModeButton possessionModeButton in this.possession_mode_buttons)
		{
			possessionModeButton.updateGraphics(TouchPossessionController._current_mode);
		}
	}

	// Token: 0x06001F78 RID: 8056 RVA: 0x0010FE20 File Offset: 0x0010E020
	private void LateUpdate()
	{
		this.clearActions();
	}

	// Token: 0x06001F79 RID: 8057 RVA: 0x0010FE28 File Offset: 0x0010E028
	private void clearActions()
	{
		TouchPossessionController._action_pressed_jump = false;
		TouchPossessionController._action_pressed_dash = false;
		TouchPossessionController._action_pressed_backstep = false;
	}

	// Token: 0x040016DB RID: 5851
	public static TouchPossessionController instance;

	// Token: 0x040016DC RID: 5852
	[SerializeField]
	private GameObject _button_dash;

	// Token: 0x040016DD RID: 5853
	[SerializeField]
	private GameObject _button_jump;

	// Token: 0x040016DE RID: 5854
	[SerializeField]
	private GameObject _button_backstep;

	// Token: 0x040016DF RID: 5855
	[SerializeField]
	private GameObject _button_attack;

	// Token: 0x040016E0 RID: 5856
	[SerializeField]
	private GameObject _button_kick;

	// Token: 0x040016E1 RID: 5857
	[SerializeField]
	private GameObject _button_talk;

	// Token: 0x040016E2 RID: 5858
	[SerializeField]
	private GameObject _button_swear;

	// Token: 0x040016E3 RID: 5859
	[SerializeField]
	private GameObject _button_steal;

	// Token: 0x040016E4 RID: 5860
	public List<PossessionModeButton> possession_mode_buttons = new List<PossessionModeButton>();

	// Token: 0x040016E5 RID: 5861
	[SerializeField]
	private RectTransform _rect;

	// Token: 0x040016E6 RID: 5862
	[SerializeField]
	private UltimateJoystick _right_joystick;

	// Token: 0x040016E7 RID: 5863
	private static bool _action_pressed_jump;

	// Token: 0x040016E8 RID: 5864
	private static bool _action_pressed_dash;

	// Token: 0x040016E9 RID: 5865
	private static bool _action_pressed_backstep;

	// Token: 0x040016EA RID: 5866
	public static PossessionActionMode _current_mode;
}
