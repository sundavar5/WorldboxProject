using System;
using UnityEngine;

// Token: 0x0200034F RID: 847
public class SpriteShadow : MonoBehaviour
{
	// Token: 0x06002076 RID: 8310 RVA: 0x00116314 File Offset: 0x00114514
	private void Start()
	{
		this.baseMapObject = base.GetComponent<BaseMapObject>();
		this.transCaster = base.transform;
		this.transShadow = new GameObject().transform;
		this.transShadow.parent = this.transCaster;
		this.transShadow.gameObject.name = "Shadow";
		this.transShadow.localRotation = Quaternion.identity;
		this.transShadow.localScale = new Vector3(1f, 0.5f);
		this.sprRndCaster = base.GetComponent<SpriteRenderer>();
		this.sprRndShadow = this.transShadow.gameObject.AddComponent<SpriteRenderer>();
		this.sprRndShadow.sharedMaterial = LibraryMaterials.instance.mat_world_object;
		this.sprRndShadow.color = this.shadowColor;
		this.sprRndShadow.sortingLayerName = this.sprRndCaster.sortingLayerName;
		this.sprRndShadow.sortingOrder = this.sprRndCaster.sortingOrder - 1;
	}

	// Token: 0x06002077 RID: 8311 RVA: 0x00116410 File Offset: 0x00114610
	private void LateUpdate()
	{
		this.transShadow.position = new Vector2(this.transCaster.position.x + this.offset.x, this.transCaster.position.y + this.offset.y);
		Color tColor = this.shadowColor;
		tColor.a = this.sprRndCaster.color.a * 0.5f;
		this.sprRndShadow.color = tColor;
		this.sprRndShadow.sprite = this.sprRndCaster.sprite;
		this.sprRndShadow.flipX = this.sprRndCaster.flipX;
	}

	// Token: 0x0400179E RID: 6046
	public Vector2 offset = new Vector2(-3f, 3f);

	// Token: 0x0400179F RID: 6047
	internal int z_height;

	// Token: 0x040017A0 RID: 6048
	private SpriteRenderer sprRndCaster;

	// Token: 0x040017A1 RID: 6049
	private SpriteRenderer sprRndShadow;

	// Token: 0x040017A2 RID: 6050
	private Transform transCaster;

	// Token: 0x040017A3 RID: 6051
	private Transform transShadow;

	// Token: 0x040017A4 RID: 6052
	public Color shadowColor;

	// Token: 0x040017A5 RID: 6053
	private BaseMapObject baseMapObject;
}
