using System;

// Token: 0x02000103 RID: 259
public readonly struct GenomePart : IEquatable<GenomePart>
{
	// Token: 0x060007E9 RID: 2025 RVA: 0x0006FDB8 File Offset: 0x0006DFB8
	public GenomePart(string id, float pValue)
	{
		if (string.IsNullOrEmpty(id))
		{
			throw new ArgumentNullException("id cannot be null or empty");
		}
		this.id = id;
		this.value = pValue;
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x0006FDDC File Offset: 0x0006DFDC
	public override bool Equals(object pObject)
	{
		if (pObject is GenomePart)
		{
			GenomePart tOther = (GenomePart)pObject;
			return this.Equals(tOther);
		}
		return false;
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x0006FE01 File Offset: 0x0006E001
	public bool Equals(GenomePart pOther)
	{
		return string.Equals(this.id, pOther.id, StringComparison.OrdinalIgnoreCase);
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x0006FE15 File Offset: 0x0006E015
	public override int GetHashCode()
	{
		return StringComparer.OrdinalIgnoreCase.GetHashCode(this.id);
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x0006FE27 File Offset: 0x0006E027
	public override string ToString()
	{
		return string.Format("{0}: {1}", this.id, this.value);
	}

	// Token: 0x0400083C RID: 2108
	public readonly string id;

	// Token: 0x0400083D RID: 2109
	public readonly float value;
}
