using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200081E RID: 2078
public class MapShowMeta : MonoBehaviour
{
	// Token: 0x06004112 RID: 16658 RVA: 0x001BC268 File Offset: 0x001BA468
	private void Update()
	{
		if (this.startSpinning)
		{
			this.angle -= Time.deltaTime * 180f;
			this.iconFavorite.transform.localEulerAngles = new Vector3(0f, 0f, this.angle);
			return;
		}
		if (this.angle != 0f)
		{
			this.angle -= Time.deltaTime * 720f;
			if (this.angle < -360f)
			{
				this.angle = 0f;
			}
			this.iconFavorite.transform.localEulerAngles = new Vector3(0f, 0f, this.angle);
		}
	}

	// Token: 0x06004113 RID: 16659 RVA: 0x001BC31D File Offset: 0x001BA51D
	public void pressFavorite()
	{
	}

	// Token: 0x06004114 RID: 16660 RVA: 0x001BC31F File Offset: 0x001BA51F
	public void copyToClipboard()
	{
	}

	// Token: 0x04002F4F RID: 12111
	public WorldElement worldElementPrefab;

	// Token: 0x04002F50 RID: 12112
	public WorldElement element;

	// Token: 0x04002F51 RID: 12113
	public Transform transformContent;

	// Token: 0x04002F52 RID: 12114
	public GameObject loadingSpinner;

	// Token: 0x04002F53 RID: 12115
	public GameObject errorImage;

	// Token: 0x04002F54 RID: 12116
	public GameObject textStatusBG;

	// Token: 0x04002F55 RID: 12117
	public Text textStatusMessage;

	// Token: 0x04002F56 RID: 12118
	public GameObject playButton;

	// Token: 0x04002F57 RID: 12119
	public GameObject favButton;

	// Token: 0x04002F58 RID: 12120
	public Text playButtonText;

	// Token: 0x04002F59 RID: 12121
	public GameObject deleteButton;

	// Token: 0x04002F5A RID: 12122
	public GameObject reportButton;

	// Token: 0x04002F5B RID: 12123
	public GameObject bottomButtons;

	// Token: 0x04002F5C RID: 12124
	public Image iconFavorite;

	// Token: 0x04002F5D RID: 12125
	private bool startSpinning;

	// Token: 0x04002F5E RID: 12126
	private float angle;
}
