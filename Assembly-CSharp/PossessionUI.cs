using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006D3 RID: 1747
public class PossessionUI : MonoBehaviour
{
	// Token: 0x06003809 RID: 14345 RVA: 0x001931DC File Offset: 0x001913DC
	private void Awake()
	{
		PossessionUI.instance = this;
		this._text_backstep = base.transform.FindRecursive("backstep").GetComponent<Text>();
		this._text_dash = base.transform.FindRecursive("dash").GetComponent<Text>();
		this._text_steal = base.transform.FindRecursive("steal").GetComponent<Text>();
		this._text_yell = base.transform.FindRecursive("yell").GetComponent<Text>();
		this._text_talk = base.transform.FindRecursive("talk").GetComponent<Text>();
		this._text_walk = base.transform.FindRecursive("walk").GetComponent<Text>();
		this._text_attack = base.transform.FindRecursive("attack").GetComponent<Text>();
		this._text_special = base.transform.FindRecursive("special").GetComponent<Text>();
		this._text_jump = base.transform.FindRecursive("jump").GetComponent<Text>();
		this._text_kick = base.transform.FindRecursive("kick").GetComponent<Text>();
		this.setText(this._text_backstep, "possession_tip_backstep", "CONTROL");
		this.setText(this._text_dash, "possession_tip_dash", "SHIFT");
		this.setText(this._text_steal, "possession_tip_steal", "Q");
		this.setText(this._text_yell, "possession_tip_yell", "F");
		this.setText(this._text_talk, "possession_tip_talk", "T");
		this.setText(this._text_walk, "possession_tip_walk", "WASD/ARROWS");
		this.setText(this._text_attack, "possession_tip_attack", "LEFT CLICK");
		this.setText(this._text_special, "possession_tip_special", "MIDDLE CLICK");
		this.setText(this._text_jump, "possession_tip_jump", "SPACE");
		this.setText(this._text_kick, "possession_tip_kick", "RIGHT CLICK");
		this.toggleInstance(false);
	}

	// Token: 0x0600380A RID: 14346 RVA: 0x001933E0 File Offset: 0x001915E0
	private void Update()
	{
		if (!this._state_change_planned)
		{
			return;
		}
		this._state_change_planned = false;
		if (this._mover.visible == this._planned_state)
		{
			return;
		}
		this.toggleInstance(this._planned_state);
	}

	// Token: 0x0600380B RID: 14347 RVA: 0x00193414 File Offset: 0x00191614
	private void setText(Text pTextField, string pLocaleID, string pKey)
	{
		string tText = LocalizedTextManager.getText(pLocaleID, null, false);
		tText = Toolbox.coloredString(tText, "white");
		pKey = Toolbox.coloredString(pKey, "#F3961F");
		pTextField.text = tText + " --> [ " + pKey + " ]";
	}

	// Token: 0x0600380C RID: 14348 RVA: 0x0019345A File Offset: 0x0019165A
	public static void toggle(bool pState)
	{
		PossessionUI.instance._state_change_planned = true;
		PossessionUI.instance._planned_state = pState;
	}

	// Token: 0x0600380D RID: 14349 RVA: 0x00193474 File Offset: 0x00191674
	private void toggleInstance(bool pState)
	{
		if (pState)
		{
			PossessionUI.instance._inner.SetActive(true);
		}
		PossessionUI.instance._mover.setVisible(pState, false, delegate
		{
			if (!pState && !PossessionUI.instance._mover.visible)
			{
				PossessionUI.instance._inner.SetActive(false);
			}
		});
	}

	// Token: 0x0400298A RID: 10634
	[SerializeField]
	private UiMover _mover;

	// Token: 0x0400298B RID: 10635
	[SerializeField]
	private GameObject _inner;

	// Token: 0x0400298C RID: 10636
	public static PossessionUI instance;

	// Token: 0x0400298D RID: 10637
	private Text _text_backstep;

	// Token: 0x0400298E RID: 10638
	private Text _text_dash;

	// Token: 0x0400298F RID: 10639
	private Text _text_steal;

	// Token: 0x04002990 RID: 10640
	private Text _text_yell;

	// Token: 0x04002991 RID: 10641
	private Text _text_talk;

	// Token: 0x04002992 RID: 10642
	private Text _text_walk;

	// Token: 0x04002993 RID: 10643
	private Text _text_attack;

	// Token: 0x04002994 RID: 10644
	private Text _text_special;

	// Token: 0x04002995 RID: 10645
	private Text _text_jump;

	// Token: 0x04002996 RID: 10646
	private Text _text_kick;

	// Token: 0x04002997 RID: 10647
	private bool _planned_state;

	// Token: 0x04002998 RID: 10648
	private bool _state_change_planned;
}
