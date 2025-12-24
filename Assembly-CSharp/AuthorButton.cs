using System;
using UnityEngine;

// Token: 0x020007B6 RID: 1974
public class AuthorButton : MonoBehaviour
{
	// Token: 0x06003E6D RID: 15981 RVA: 0x001B2781 File Offset: 0x001B0981
	private void Awake()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06003E6E RID: 15982 RVA: 0x001B278E File Offset: 0x001B098E
	public void showWorldNetAuthorListWindow()
	{
	}

	// Token: 0x04002D8A RID: 11658
	public string authorId;
}
