using System;
using ai.behaviours;

// Token: 0x020004DE RID: 1246
public class TesterBehSetResolution : BehaviourActionTester
{
	// Token: 0x06002A10 RID: 10768 RVA: 0x0014AF84 File Offset: 0x00149184
	public TesterBehSetResolution(int pWidth, int pHeight, string pName = null)
	{
		this.width = pWidth;
		this.height = pHeight;
		this.name = pName;
	}

	// Token: 0x06002A11 RID: 10769 RVA: 0x0014AFA1 File Offset: 0x001491A1
	public override BehResult execute(AutoTesterBot pObject)
	{
		return BehResult.Continue;
	}

	// Token: 0x04001F5D RID: 8029
	private int width;

	// Token: 0x04001F5E RID: 8030
	private int height;

	// Token: 0x04001F5F RID: 8031
	private string name;
}
