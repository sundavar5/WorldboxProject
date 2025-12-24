using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200051D RID: 1309
public class TestActorGameObject : MonoBehaviour
{
	// Token: 0x06002AF4 RID: 10996 RVA: 0x0015571F File Offset: 0x0015391F
	public void create(List<Sprite> pSprites)
	{
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		this.sprites = pSprites;
		this.randomRespawn();
		this.setRandomSprite();
	}

	// Token: 0x06002AF5 RID: 10997 RVA: 0x00155740 File Offset: 0x00153940
	public void randomRespawn()
	{
		WorldTile tTile = World.world.tiles_list.GetRandom<WorldTile>();
		this.pos_x = (float)tTile.x;
		this.pos_y = (float)tTile.y;
	}

	// Token: 0x06002AF6 RID: 10998 RVA: 0x00155777 File Offset: 0x00153977
	public void update(float pElapsed)
	{
		this.randomMove(pElapsed);
		this.applyUnity();
	}

	// Token: 0x06002AF7 RID: 10999 RVA: 0x00155786 File Offset: 0x00153986
	private void applyUnity()
	{
		this.spriteRenderer.sprite = this.sprite;
		base.transform.position = new Vector3(this.pos_x, this.pos_y, 0f);
	}

	// Token: 0x06002AF8 RID: 11000 RVA: 0x001557BC File Offset: 0x001539BC
	private void randomMove(float pElapsed)
	{
		this.pos_x += Randy.randomFloat(-1f, 1f) * pElapsed * 6f;
		this.pos_y += Randy.randomFloat(-1f, 1f) * pElapsed * 6f;
	}

	// Token: 0x06002AF9 RID: 11001 RVA: 0x00155811 File Offset: 0x00153A11
	private void setRandomSprite()
	{
		this.sprite = this.sprites.GetRandom<Sprite>();
	}

	// Token: 0x04002038 RID: 8248
	public Sprite sprite;

	// Token: 0x04002039 RID: 8249
	public float pos_x;

	// Token: 0x0400203A RID: 8250
	public float pos_y;

	// Token: 0x0400203B RID: 8251
	public float scale_x = 1f;

	// Token: 0x0400203C RID: 8252
	public float scale_y = 1f;

	// Token: 0x0400203D RID: 8253
	private List<Sprite> sprites = new List<Sprite>();

	// Token: 0x0400203E RID: 8254
	private SpriteRenderer spriteRenderer;
}
