using System;
using System.Collections.Generic;

// Token: 0x02000506 RID: 1286
public class BenchmarkGroup
{
	// Token: 0x06002A8F RID: 10895 RVA: 0x00151AF0 File Offset: 0x0014FCF0
	public void flatten()
	{
		foreach (ToolBenchmarkData toolBenchmarkData in this.dict_data.Values)
		{
			toolBenchmarkData.end(0.0);
		}
	}

	// Token: 0x04001F9A RID: 8090
	public string id;

	// Token: 0x04001F9B RID: 8091
	public Dictionary<string, ToolBenchmarkData> dict_data = new Dictionary<string, ToolBenchmarkData>();
}
