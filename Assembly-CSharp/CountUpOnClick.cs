using System;
using System.Globalization;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020005D6 RID: 1494
public class CountUpOnClick : MonoBehaviour
{
	// Token: 0x0600311A RID: 12570 RVA: 0x00178BD8 File Offset: 0x00176DD8
	private void Start()
	{
		Button tButton;
		if (!base.TryGetComponent<Button>(out tButton) || this._text == null)
		{
			base.enabled = false;
			return;
		}
		if (!this._value_updated && !this.checkString())
		{
			base.enabled = false;
			return;
		}
		tButton.onClick.AddListener(new UnityAction(this.countAnimation));
	}

	// Token: 0x0600311B RID: 12571 RVA: 0x00178C34 File Offset: 0x00176E34
	public void setValue(int pValue, string pEnd = "")
	{
		base.enabled = true;
		this._value = pValue;
		this._end = pEnd;
		this._value_updated = true;
		this._text.text = this._value.ToText(4) + pEnd;
	}

	// Token: 0x0600311C RID: 12572 RVA: 0x00178C70 File Offset: 0x00176E70
	private bool checkString()
	{
		string tTargetText = this._text.text;
		if (!this.checkIfStringIsLegit(tTargetText))
		{
			return false;
		}
		if (!int.TryParse(tTargetText, NumberStyles.Any, CultureInfo.CurrentCulture, out this._value))
		{
			base.enabled = false;
			return false;
		}
		return true;
	}

	// Token: 0x0600311D RID: 12573 RVA: 0x00178CB6 File Offset: 0x00176EB6
	private bool checkIfStringIsLegit(string pString)
	{
		return !string.IsNullOrEmpty(pString) && pString.All(new Func<char, bool>(char.IsDigit));
	}

	// Token: 0x0600311E RID: 12574 RVA: 0x00178CD9 File Offset: 0x00176ED9
	private void countAnimation()
	{
		if (this._value_updated)
		{
			this._value_updated = false;
		}
		this.checkDestroyTween();
		this._cur_tween = this._text.DOUpCounter(0, this._value, 0.45f, this._end, "");
	}

	// Token: 0x0600311F RID: 12575 RVA: 0x00178D18 File Offset: 0x00176F18
	public Text getText()
	{
		return this._text;
	}

	// Token: 0x06003120 RID: 12576 RVA: 0x00178D20 File Offset: 0x00176F20
	private void OnDisable()
	{
		this.checkDestroyTween();
	}

	// Token: 0x06003121 RID: 12577 RVA: 0x00178D28 File Offset: 0x00176F28
	private void checkDestroyTween()
	{
		this._cur_tween.Kill(true);
		this._cur_tween = null;
	}

	// Token: 0x04002518 RID: 9496
	private const float TWEEN_DURATION = 0.45f;

	// Token: 0x04002519 RID: 9497
	[SerializeField]
	private Text _text;

	// Token: 0x0400251A RID: 9498
	private Tweener _cur_tween;

	// Token: 0x0400251B RID: 9499
	private int _value;

	// Token: 0x0400251C RID: 9500
	private string _end = "";

	// Token: 0x0400251D RID: 9501
	private bool _value_updated;
}
