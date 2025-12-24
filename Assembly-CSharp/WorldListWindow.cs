using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000821 RID: 2081
public class WorldListWindow : MonoBehaviour
{
	// Token: 0x04002F82 RID: 12162
	private static WorldListWindow instance;

	// Token: 0x04002F83 RID: 12163
	public WorldElement worldElementPrefab;

	// Token: 0x04002F84 RID: 12164
	public GameObject notFound;

	// Token: 0x04002F85 RID: 12165
	public ScrollWindow windowWorldList;

	// Token: 0x04002F86 RID: 12166
	private List<WorldElement> elements = new List<WorldElement>();

	// Token: 0x04002F87 RID: 12167
	public Transform transformContent;

	// Token: 0x04002F88 RID: 12168
	public Transform listContent;

	// Token: 0x04002F89 RID: 12169
	public Transform tagContent;

	// Token: 0x04002F8A RID: 12170
	public GameObject loadingSpinner;

	// Token: 0x04002F8B RID: 12171
	public GameObject textStatusBG;

	// Token: 0x04002F8C RID: 12172
	public Text textStatusMessage;

	// Token: 0x04002F8D RID: 12173
	public LocalizedText windowTitle;

	// Token: 0x04002F8E RID: 12174
	public static List<MapTagType> tagsActive = new List<MapTagType>();

	// Token: 0x04002F8F RID: 12175
	public static string authorId;

	// Token: 0x04002F90 RID: 12176
	public GameObject sectionTextBG;

	// Token: 0x04002F91 RID: 12177
	public GameObject profileImage;

	// Token: 0x04002F92 RID: 12178
	public GameObject filterButton;

	// Token: 0x04002F93 RID: 12179
	public Text sectionText;

	// Token: 0x04002F94 RID: 12180
	public Image filterTag1;

	// Token: 0x04002F95 RID: 12181
	public Image filterTag2;
}
