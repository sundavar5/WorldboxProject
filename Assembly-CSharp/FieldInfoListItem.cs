using System;
using System.Collections.Generic;

// Token: 0x02000538 RID: 1336
public class FieldInfoListItem
{
	// Token: 0x06002BBA RID: 11194 RVA: 0x0015A046 File Offset: 0x00158246
	public FieldInfoListItem(string pName, string pValue, Dictionary<string, string> pCollectionData = null)
	{
		this.field_name = pName;
		this.field_value = pValue;
		this.collection_data = pCollectionData;
	}

	// Token: 0x04002199 RID: 8601
	public string field_name;

	// Token: 0x0400219A RID: 8602
	public string field_value;

	// Token: 0x0400219B RID: 8603
	public Dictionary<string, string> collection_data;
}
