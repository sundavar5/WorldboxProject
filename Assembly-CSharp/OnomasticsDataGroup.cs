using System;

// Token: 0x02000261 RID: 609
public class OnomasticsDataGroup
{
	// Token: 0x060016F3 RID: 5875 RVA: 0x000E5283 File Offset: 0x000E3483
	public bool isEmpty()
	{
		return this.characters == null || this.characters.Length == 0;
	}

	// Token: 0x040012D6 RID: 4822
	public string[] characters;

	// Token: 0x040012D7 RID: 4823
	public string characters_string;
}
