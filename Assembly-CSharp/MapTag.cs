using System;
using UnityEngine;

// Token: 0x020005C1 RID: 1473
public class MapTag : MonoBehaviour
{
	// Token: 0x06003072 RID: 12402 RVA: 0x00176DBA File Offset: 0x00174FBA
	private void Start()
	{
		this.updateSprite();
	}

	// Token: 0x06003073 RID: 12403 RVA: 0x00176DC2 File Offset: 0x00174FC2
	public void clickButton()
	{
	}

	// Token: 0x06003074 RID: 12404 RVA: 0x00176DC4 File Offset: 0x00174FC4
	public void clickListWorldsButton()
	{
	}

	// Token: 0x06003075 RID: 12405 RVA: 0x00176DC6 File Offset: 0x00174FC6
	private void updateSprite()
	{
	}

	// Token: 0x040024C3 RID: 9411
	public bool tagEnabled;

	// Token: 0x040024C4 RID: 9412
	public MapTagType tagType;

	// Token: 0x040024C5 RID: 9413
	public Sprite buttonOn;

	// Token: 0x040024C6 RID: 9414
	public Sprite buttonOff;

	// Token: 0x040024C7 RID: 9415
	public string icon;

	// Token: 0x040024C8 RID: 9416
	public CanvasGroup tagGroup;
}
