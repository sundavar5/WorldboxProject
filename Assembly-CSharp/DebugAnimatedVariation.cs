using System;
using UnityEngine;

// Token: 0x02000557 RID: 1367
public class DebugAnimatedVariation
{
	// Token: 0x06002C97 RID: 11415 RVA: 0x0015DFAC File Offset: 0x0015C1AC
	public DebugAnimatedVariation(Sprite[] pFrames, bool pAnimated)
	{
		this.animated = pAnimated;
		this.frames = pFrames;
	}

	// Token: 0x04002226 RID: 8742
	public bool animated;

	// Token: 0x04002227 RID: 8743
	public Sprite[] frames;
}
