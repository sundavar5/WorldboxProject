using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200060B RID: 1547
public class UiInputCounter : MonoBehaviour
{
	// Token: 0x060032BC RID: 12988 RVA: 0x001804D4 File Offset: 0x0017E6D4
	private void Start()
	{
		this.nameText.onValueChanged.AddListener(delegate(string <p0>)
		{
			this.textChanged();
		});
	}

	// Token: 0x060032BD RID: 12989 RVA: 0x001804F2 File Offset: 0x0017E6F2
	private void OnEnable()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		this.textChanged();
	}

	// Token: 0x060032BE RID: 12990 RVA: 0x00180504 File Offset: 0x0017E704
	public void textChanged()
	{
		base.GetComponent<Text>().text = this.nameText.text.Length.ToString() + " / " + this.nameText.characterLimit.ToString();
	}

	// Token: 0x0400265A RID: 9818
	public InputField nameText;
}
