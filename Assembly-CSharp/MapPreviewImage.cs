using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200081D RID: 2077
public class MapPreviewImage : MonoBehaviour
{
	// Token: 0x04002F48 RID: 12104
	public bool premiumOnly = true;

	// Token: 0x04002F49 RID: 12105
	public Image premiumIcon;

	// Token: 0x04002F4A RID: 12106
	public Button button;

	// Token: 0x04002F4B RID: 12107
	public SlotButtonCallback slotData;

	// Token: 0x04002F4C RID: 12108
	public Map map;

	// Token: 0x04002F4D RID: 12109
	public Sprite defaultSprite;

	// Token: 0x04002F4E RID: 12110
	private ButtonAnimation buttonAnimation;
}
