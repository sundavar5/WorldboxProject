using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000708 RID: 1800
public class CubeNodeConnection : MonoBehaviour
{
	// Token: 0x0600398B RID: 14731 RVA: 0x001995A4 File Offset: 0x001977A4
	public void update()
	{
		this.mod_light -= Time.deltaTime * 2f;
		this.mod_light = Mathf.Max(0f, this.mod_light);
	}

	// Token: 0x0600398C RID: 14732 RVA: 0x001995D4 File Offset: 0x001977D4
	public void setConnection(bool pInner)
	{
		this.inner_cube = pInner;
		if (pInner)
		{
			this.image.sprite = this._connection_inner;
			return;
		}
		this.image.sprite = this._connection_outer;
	}

	// Token: 0x0600398D RID: 14733 RVA: 0x00199603 File Offset: 0x00197803
	public void clear()
	{
		this.node_1 = null;
		this.node_2 = null;
		this.inner_cube = false;
	}

	// Token: 0x04002A73 RID: 10867
	[SerializeField]
	private Sprite _connection_inner;

	// Token: 0x04002A74 RID: 10868
	[SerializeField]
	private Sprite _connection_outer;

	// Token: 0x04002A75 RID: 10869
	public Image image;

	// Token: 0x04002A76 RID: 10870
	internal CubeNode node_1;

	// Token: 0x04002A77 RID: 10871
	internal CubeNode node_2;

	// Token: 0x04002A78 RID: 10872
	internal bool inner_cube;

	// Token: 0x04002A79 RID: 10873
	internal float mod_light = 1f;
}
