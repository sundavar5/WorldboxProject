using System;
using UnityEngine;

// Token: 0x020002EF RID: 751
public class QuantumSpriteWithText : QuantumSprite
{
	// Token: 0x06001C36 RID: 7222 RVA: 0x00100C40 File Offset: 0x000FEE40
	public void initText()
	{
		Transform transform = base.transform.Find("Text");
		this.text = ((transform != null) ? transform.GetComponent<TextMesh>() : null);
	}

	// Token: 0x04001590 RID: 5520
	public TextMesh text;
}
