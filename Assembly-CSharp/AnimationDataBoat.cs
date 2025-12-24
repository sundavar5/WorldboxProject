using System;
using System.Collections.Generic;

// Token: 0x02000361 RID: 865
public class AnimationDataBoat
{
	// Token: 0x04001835 RID: 6197
	internal string id;

	// Token: 0x04001836 RID: 6198
	internal Dictionary<int, ActorAnimation> dict = new Dictionary<int, ActorAnimation>();

	// Token: 0x04001837 RID: 6199
	internal ActorAnimation broken;

	// Token: 0x04001838 RID: 6200
	internal ActorAnimation normal;
}
