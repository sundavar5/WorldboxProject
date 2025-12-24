using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020007E0 RID: 2016
public class NameInput : MonoBehaviour
{
	// Token: 0x06003F76 RID: 16246 RVA: 0x001B55B8 File Offset: 0x001B37B8
	private void Start()
	{
		this.textField.horizontalOverflow = HorizontalWrapMode.Wrap;
		if (this.is_onomastics)
		{
			this.inputField.onValidateInput = new InputField.OnValidateInput(this.validateOnomastics);
			return;
		}
		this.inputField.onValidateInput = new InputField.OnValidateInput(this.validate);
	}

	// Token: 0x06003F77 RID: 16247 RVA: 0x001B5608 File Offset: 0x001B3808
	private char validate(string pText, int pCharIndex, char pAddedChar)
	{
		if (pAddedChar == '<' || pAddedChar == '>')
		{
			return '\0';
		}
		return pAddedChar;
	}

	// Token: 0x06003F78 RID: 16248 RVA: 0x001B5618 File Offset: 0x001B3818
	private char validateOnomastics(string pText, int pCharIndex, char pAddedChar)
	{
		char tResult = pAddedChar;
		bool tIsLetter = char.IsLetter(tResult);
		bool tIsSpace = char.IsWhiteSpace(tResult);
		bool tIsApostrophe = tResult == '\'';
		bool tIsFirstLetter = pText.Length == 0;
		if (!tIsLetter && !tIsSpace && !tIsApostrophe)
		{
			return '\0';
		}
		if (tIsFirstLetter)
		{
			return char.ToUpper(tResult);
		}
		char c = pText[pText.Length - 1];
		bool tIsLetterPrevious = char.IsLetter(c);
		bool tIsSpacePrevious = char.IsWhiteSpace(c);
		bool tIsApostrophePrevious = c == '\'';
		if (tIsLetter)
		{
			if (tIsLetterPrevious)
			{
				tResult = char.ToLower(tResult);
			}
			else if (tIsSpacePrevious)
			{
				tResult = char.ToUpper(tResult);
			}
		}
		else if (tIsSpace)
		{
			if (tIsSpacePrevious)
			{
				return '\0';
			}
		}
		else if (tIsApostrophe && tIsApostrophePrevious)
		{
			return '\0';
		}
		return tResult;
	}

	// Token: 0x06003F79 RID: 16249 RVA: 0x001B56B0 File Offset: 0x001B38B0
	public void addListener(UnityAction<string> pAction)
	{
		this.inputField.onValueChanged.AddListener(pAction);
	}

	// Token: 0x06003F7A RID: 16250 RVA: 0x001B56C3 File Offset: 0x001B38C3
	private void OnEnable()
	{
		this.inputField.onEndEdit.AddListener(new UnityAction<string>(this.checkInput));
	}

	// Token: 0x06003F7B RID: 16251 RVA: 0x001B56E1 File Offset: 0x001B38E1
	private void OnDisable()
	{
		this.inputField.onEndEdit.RemoveAllListeners();
		if (this._outline != null)
		{
			this._outline.enabled = false;
		}
	}

	// Token: 0x06003F7C RID: 16252 RVA: 0x001B5710 File Offset: 0x001B3910
	public void SetOutline()
	{
		if (this._outline == null)
		{
			this._outline = this.inputField.gameObject.AddOrGetComponent<Outline>();
		}
		this._outline.enabled = true;
		Color tTextColor = this.textField.color;
		Color tGlowColor = new Color(tTextColor.r, tTextColor.g, tTextColor.b, 0.2f);
		this._outline.effectColor = tGlowColor;
	}

	// Token: 0x06003F7D RID: 16253 RVA: 0x001B5783 File Offset: 0x001B3983
	private void checkInput(string pInput)
	{
		if (string.IsNullOrWhiteSpace(pInput) && !this.can_be_empty)
		{
			this.inputField.text = this.LastValue;
			return;
		}
		this.LastValue = pInput;
	}

	// Token: 0x06003F7E RID: 16254 RVA: 0x001B57AE File Offset: 0x001B39AE
	public void setText(string pText)
	{
		this.textField.text = pText;
		this.inputField.text = pText;
		this.LastValue = pText;
	}

	// Token: 0x04002E13 RID: 11795
	public InputField inputField;

	// Token: 0x04002E14 RID: 11796
	public Text textField;

	// Token: 0x04002E15 RID: 11797
	private string LastValue;

	// Token: 0x04002E16 RID: 11798
	public bool can_be_empty;

	// Token: 0x04002E17 RID: 11799
	public bool is_onomastics;

	// Token: 0x04002E18 RID: 11800
	private Outline _outline;
}
