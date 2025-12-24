using System;
using System.Collections.Generic;
using ai.behaviours;

// Token: 0x020004DD RID: 1245
public class TesterBehSetNextLanguage : BehaviourActionTester
{
	// Token: 0x06002A0E RID: 10766 RVA: 0x0014AEB6 File Offset: 0x001490B6
	public TesterBehSetNextLanguage()
	{
		this.languages = LocalizedTextManager.getAllLanguagesWithChanges();
	}

	// Token: 0x06002A0F RID: 10767 RVA: 0x0014AED4 File Offset: 0x001490D4
	public override BehResult execute(AutoTesterBot pObject)
	{
		if (this.currentLanguage >= this.languages.Count)
		{
			this.currentLanguage = 0;
		}
		List<string> list = this.languages;
		int num = this.currentLanguage;
		this.currentLanguage = num + 1;
		string tLanguage = list[num];
		Console.WriteLine(string.Concat(new string[]
		{
			"[",
			Date.TimeNow(),
			"] Changed language to : ",
			tLanguage,
			" ",
			this.currentLanguage.ToString(),
			"/",
			this.languages.Count.ToString()
		}));
		LocalizedTextManager.instance.setLanguage(tLanguage);
		return BehResult.Continue;
	}

	// Token: 0x04001F5B RID: 8027
	private List<string> languages = new List<string>();

	// Token: 0x04001F5C RID: 8028
	private int currentLanguage;
}
