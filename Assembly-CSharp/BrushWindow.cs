using System;
using UnityEngine;

// Token: 0x0200064D RID: 1613
public class BrushWindow : MonoBehaviour
{
	// Token: 0x0600346E RID: 13422 RVA: 0x00185C5C File Offset: 0x00183E5C
	public void Awake()
	{
		foreach (BrushData tBrushData in AssetManager.brush_library.list)
		{
			if (tBrushData.show_in_brush_window)
			{
				Transform tParent;
				switch (tBrushData.group)
				{
				case BrushGroup.Circles:
					tParent = this.circles;
					break;
				case BrushGroup.Squares:
					tParent = this.squares;
					break;
				case BrushGroup.Diamonds:
					tParent = this.diamonds;
					break;
				case BrushGroup.Special:
					tParent = this.special;
					break;
				default:
					continue;
				}
				Object.Instantiate<BrushSelectButton>(this.button_prefab, tParent).setup(tBrushData);
			}
		}
	}

	// Token: 0x0600346F RID: 13423 RVA: 0x00185D0C File Offset: 0x00183F0C
	public void selectBrush(GameObject pObject)
	{
		Config.current_brush = pObject.transform.name;
		base.GetComponent<ScrollWindow>().clickHide("right");
	}

	// Token: 0x04002786 RID: 10118
	public Transform circles;

	// Token: 0x04002787 RID: 10119
	public Transform squares;

	// Token: 0x04002788 RID: 10120
	public Transform diamonds;

	// Token: 0x04002789 RID: 10121
	public Transform special;

	// Token: 0x0400278A RID: 10122
	public BrushSelectButton button_prefab;
}
