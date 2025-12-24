using System;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class CircleIconShaderMod : MonoBehaviour
{
	// Token: 0x0600001B RID: 27 RVA: 0x000034BA File Offset: 0x000016BA
	private void Awake()
	{
		this._instance_material = new Material(this.prefab_radial_fill);
		this.sprite_renderer_with_mat.material = this._instance_material;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x000034E0 File Offset: 0x000016E0
	public void setShaderVal(float pVal)
	{
		if (this.sprite_renderer_with_mat == null)
		{
			return;
		}
		float fillAmount = Mathf.PingPong(pVal, 1f);
		this._instance_material.SetFloat("_FillAmount", fillAmount);
	}

	// Token: 0x04000015 RID: 21
	public Material prefab_radial_fill;

	// Token: 0x04000016 RID: 22
	private Material _instance_material;

	// Token: 0x04000017 RID: 23
	public SpriteRenderer sprite_renderer_with_mat;
}
