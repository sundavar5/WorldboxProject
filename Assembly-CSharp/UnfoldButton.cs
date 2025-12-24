using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000611 RID: 1553
public class UnfoldButton : MonoBehaviour
{
	// Token: 0x060032E5 RID: 13029 RVA: 0x0018114E File Offset: 0x0017F34E
	private void Awake()
	{
		this._button.onClick.AddListener(delegate()
		{
			UnfoldAction action = this._action;
			if (action == null)
			{
				return;
			}
			action(false);
		});
	}

	// Token: 0x060032E6 RID: 13030 RVA: 0x0018116C File Offset: 0x0017F36C
	public void setData(int pCount, int pOffset)
	{
		this.offset = pOffset;
		this.setText(pCount.ToString());
	}

	// Token: 0x060032E7 RID: 13031 RVA: 0x00181182 File Offset: 0x0017F382
	public void setCallback(UnfoldAction pCallback)
	{
		this._action = pCallback;
	}

	// Token: 0x060032E8 RID: 13032 RVA: 0x0018118B File Offset: 0x0017F38B
	public void setText(string pText)
	{
		this._text.text = pText;
	}

	// Token: 0x060032E9 RID: 13033 RVA: 0x00181199 File Offset: 0x0017F399
	public void clear()
	{
		this.offset = 0;
	}

	// Token: 0x060032EA RID: 13034 RVA: 0x001811A2 File Offset: 0x0017F3A2
	public Button getButton()
	{
		return this._button;
	}

	// Token: 0x04002695 RID: 9877
	[SerializeField]
	private Button _button;

	// Token: 0x04002696 RID: 9878
	[SerializeField]
	private Text _text;

	// Token: 0x04002697 RID: 9879
	private UnfoldAction _action;

	// Token: 0x04002698 RID: 9880
	public int offset;
}
