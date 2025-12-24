using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200081F RID: 2079
public class WorldElement : MonoBehaviour
{
	// Token: 0x04002F5F RID: 12127
	internal Map map;

	// Token: 0x04002F60 RID: 12128
	internal ScrollWindow windowWorldList;

	// Token: 0x04002F61 RID: 12129
	internal WorldListWindow worldListWindow;

	// Token: 0x04002F62 RID: 12130
	public Text mapName;

	// Token: 0x04002F63 RID: 12131
	public Text mapId;

	// Token: 0x04002F64 RID: 12132
	public Text mapDescription;

	// Token: 0x04002F65 RID: 12133
	public GameObject iconsGroup;

	// Token: 0x04002F66 RID: 12134
	public Image raceHumans;

	// Token: 0x04002F67 RID: 12135
	public Image raceOrcs;

	// Token: 0x04002F68 RID: 12136
	public Image raceElves;

	// Token: 0x04002F69 RID: 12137
	public Image raceDwarves;

	// Token: 0x04002F6A RID: 12138
	public Text population;

	// Token: 0x04002F6B RID: 12139
	public Text cities;

	// Token: 0x04002F6C RID: 12140
	public Text houses;

	// Token: 0x04002F6D RID: 12141
	public Text zones;

	// Token: 0x04002F6E RID: 12142
	public GameObject tag1;

	// Token: 0x04002F6F RID: 12143
	public Image tag1icon;

	// Token: 0x04002F70 RID: 12144
	public GameObject tag2;

	// Token: 0x04002F71 RID: 12145
	public Image tag2icon;

	// Token: 0x04002F72 RID: 12146
	public GameObject year;

	// Token: 0x04002F73 RID: 12147
	public GameObject uploader;

	// Token: 0x04002F74 RID: 12148
	public Text tag1Text;

	// Token: 0x04002F75 RID: 12149
	public Text tag2Text;

	// Token: 0x04002F76 RID: 12150
	public Text yearText;

	// Token: 0x04002F77 RID: 12151
	public Text uploaderText;

	// Token: 0x04002F78 RID: 12152
	public Text downloads;

	// Token: 0x04002F79 RID: 12153
	public Text plays;

	// Token: 0x04002F7A RID: 12154
	public Text favs;

	// Token: 0x04002F7B RID: 12155
	public MapPreviewImage mapPreviewImage;

	// Token: 0x04002F7C RID: 12156
	public bool clickable = true;

	// Token: 0x04002F7D RID: 12157
	public bool listPreview;
}
