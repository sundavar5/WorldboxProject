using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007B4 RID: 1972
public class MapUploadingWindow : MonoBehaviour
{
	// Token: 0x04002D7E RID: 11646
	public Button doneButton;

	// Token: 0x04002D7F RID: 11647
	public Image loadingImage;

	// Token: 0x04002D80 RID: 11648
	public Image doneImage;

	// Token: 0x04002D81 RID: 11649
	public GameObject mapIDGroup;

	// Token: 0x04002D82 RID: 11650
	public Text mapIDText;

	// Token: 0x04002D83 RID: 11651
	public Text statusMessage;

	// Token: 0x04002D84 RID: 11652
	public Text percents;

	// Token: 0x04002D85 RID: 11653
	public Image bar;

	// Token: 0x04002D86 RID: 11654
	public Image mask;

	// Token: 0x04002D87 RID: 11655
	public static bool uploading;
}
