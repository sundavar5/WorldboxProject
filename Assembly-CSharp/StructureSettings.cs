using System;

// Token: 0x02000295 RID: 661
[Serializable]
public class StructureSettings
{
	// Token: 0x0600192C RID: 6444 RVA: 0x000EEA7D File Offset: 0x000ECC7D
	public virtual void create(LanguageStructure pStructure, int pSizeMin, int pSizeMax)
	{
	}

	// Token: 0x040013B9 RID: 5049
	public bool[] enabled = new bool[7];

	// Token: 0x040013BA RID: 5050
	public string[][] sets = new string[7][];

	// Token: 0x040013BB RID: 5051
	public string[] separator = new string[7];
}
