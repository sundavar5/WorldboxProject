using System;
using UnityEngine;

// Token: 0x02000119 RID: 281
[Serializable]
public class ContainerItemColor
{
	// Token: 0x060008AC RID: 2220 RVA: 0x0007D5C3 File Offset: 0x0007B7C3
	public ContainerItemColor(string pID, string pMaterialPath)
	{
		this.color = Toolbox.makeColor(pID);
		this.color_id = pID;
		this.path_material = pMaterialPath;
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x0007D5E8 File Offset: 0x0007B7E8
	public Material getMaterial()
	{
		if (string.IsNullOrEmpty(this.path_material))
		{
			return null;
		}
		Material tMat = Resources.Load<Material>(this.path_material);
		this.material = tMat;
		return this.material;
	}

	// Token: 0x040008F7 RID: 2295
	public string color_id;

	// Token: 0x040008F8 RID: 2296
	public Color color;

	// Token: 0x040008F9 RID: 2297
	private Material material;

	// Token: 0x040008FA RID: 2298
	private string path_material;
}
