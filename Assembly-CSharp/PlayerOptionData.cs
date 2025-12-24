using System;
using System.ComponentModel;

// Token: 0x0200047B RID: 1147
[Serializable]
public class PlayerOptionData
{
	// Token: 0x06002766 RID: 10086 RVA: 0x0013F18F File Offset: 0x0013D38F
	public PlayerOptionData(string pName)
	{
		this.name = pName;
	}

	// Token: 0x04001D8D RID: 7565
	public string name = "OPTION";

	// Token: 0x04001D8E RID: 7566
	[DefaultValue(true)]
	public bool boolVal = true;

	// Token: 0x04001D8F RID: 7567
	[DefaultValue("")]
	public string stringVal = string.Empty;

	// Token: 0x04001D90 RID: 7568
	[DefaultValue(0)]
	public int intVal;

	// Token: 0x04001D91 RID: 7569
	[NonSerialized]
	public PlayerOptionAction on_switch;
}
