using System;
using UnityEngine;

// Token: 0x020002EA RID: 746
public class QuantumSpriteAnimated : QuantumSpriteArrows
{
	// Token: 0x06001C22 RID: 7202 RVA: 0x001004AD File Offset: 0x000FE6AD
	private void Update()
	{
		this._animation.update(World.world.delta_time);
	}

	// Token: 0x0400157A RID: 5498
	[SerializeField]
	private SpriteAnimation _animation;
}
