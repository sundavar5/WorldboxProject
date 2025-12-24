using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200051E RID: 1310
public class TestActorSimpleObject
{
	// Token: 0x06002AFB RID: 11003 RVA: 0x0015584D File Offset: 0x00153A4D
	public void create(List<Sprite> pSprites)
	{
		this.sprites = pSprites;
		this.randomRespawn();
		this.setRandomSprite();
	}

	// Token: 0x06002AFC RID: 11004 RVA: 0x00155864 File Offset: 0x00153A64
	public void randomRespawn()
	{
		WorldTile tTile = World.world.tiles_list.GetRandom<WorldTile>();
		this.pos_x = (float)tTile.x;
		this.pos_y = (float)tTile.y;
	}

	// Token: 0x06002AFD RID: 11005 RVA: 0x0015589B File Offset: 0x00153A9B
	public void update(float pElapsed)
	{
		this.randomMove(pElapsed);
	}

	// Token: 0x06002AFE RID: 11006 RVA: 0x001558A4 File Offset: 0x00153AA4
	private void randomMove(float pElapsed)
	{
		this.pos_x += Randy.randomFloat(-1f, 1f) * pElapsed * 6f;
		this.pos_y += Randy.randomFloat(-1f, 1f) * pElapsed * 6f;
	}

	// Token: 0x06002AFF RID: 11007 RVA: 0x001558F9 File Offset: 0x00153AF9
	private void setRandomSprite()
	{
		this.sprite = this.sprites.GetRandom<Sprite>();
	}

	// Token: 0x0400203F RID: 8255
	public Sprite sprite;

	// Token: 0x04002040 RID: 8256
	public float pos_x;

	// Token: 0x04002041 RID: 8257
	public float pos_y;

	// Token: 0x04002042 RID: 8258
	public float scale_x = 1f;

	// Token: 0x04002043 RID: 8259
	public float scale_y = 1f;

	// Token: 0x04002044 RID: 8260
	private List<Sprite> sprites = new List<Sprite>();
}
