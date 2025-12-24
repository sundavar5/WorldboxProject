using System;
using System.Collections.Generic;

// Token: 0x0200055F RID: 1375
internal class GameplayReportData
{
	// Token: 0x04002245 RID: 8773
	public int usages_actors;

	// Token: 0x04002246 RID: 8774
	public int usages_biomes;

	// Token: 0x04002247 RID: 8775
	public int usages_food;

	// Token: 0x04002248 RID: 8776
	public List<string> owners = new List<string>();

	// Token: 0x04002249 RID: 8777
	public List<string> biomes = new List<string>();

	// Token: 0x0400224A RID: 8778
	public List<string> food = new List<string>();
}
