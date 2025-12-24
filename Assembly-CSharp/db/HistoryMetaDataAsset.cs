using System;
using System.Collections.Generic;

namespace db
{
	// Token: 0x0200085E RID: 2142
	[Serializable]
	public class HistoryMetaDataAsset : Asset
	{
		// Token: 0x060042FD RID: 17149 RVA: 0x001C6D70 File Offset: 0x001C4F70
		public Type getTableType(HistoryInterval pInterval)
		{
			return this.table_types[pInterval];
		}

		// Token: 0x040030F5 RID: 12533
		[NonSerialized]
		public List<HistoryDataAsset> categories = new List<HistoryDataAsset>();

		// Token: 0x040030F6 RID: 12534
		public HistoryDataCollector collector;

		// Token: 0x040030F7 RID: 12535
		public MetaType meta_type;

		// Token: 0x040030F8 RID: 12536
		public Type table_type;

		// Token: 0x040030F9 RID: 12537
		public Dictionary<HistoryInterval, Type> table_types;
	}
}
