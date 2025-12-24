using System;
using System.IO;
using Newtonsoft.Json;

// Token: 0x0200044C RID: 1100
public class CustomJsonArrayWriter : JsonTextWriter
{
	// Token: 0x06002613 RID: 9747 RVA: 0x00137F8E File Offset: 0x0013618E
	public CustomJsonArrayWriter(TextWriter writer) : base(writer)
	{
	}

	// Token: 0x06002614 RID: 9748 RVA: 0x00137F97 File Offset: 0x00136197
	protected override void WriteIndent()
	{
		if (base.WriteState != WriteState.Array)
		{
			base.WriteIndent();
			return;
		}
		this.WriteIndentSpace();
	}
}
