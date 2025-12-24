using System;

// Token: 0x0200041F RID: 1055
public class RegionLink : IEquatable<RegionLink>
{
	// Token: 0x0600248B RID: 9355 RVA: 0x0013044E File Offset: 0x0012E64E
	public void reset()
	{
		this.regions.Clear();
		this.id = -1;
	}

	// Token: 0x0600248C RID: 9356 RVA: 0x00130462 File Offset: 0x0012E662
	public override int GetHashCode()
	{
		return this.id;
	}

	// Token: 0x0600248D RID: 9357 RVA: 0x0013046A File Offset: 0x0012E66A
	public override bool Equals(object obj)
	{
		return this.Equals(obj as RegionLink);
	}

	// Token: 0x0600248E RID: 9358 RVA: 0x00130478 File Offset: 0x0012E678
	public bool Equals(RegionLink pObject)
	{
		return this.id == pObject.id;
	}

	// Token: 0x04001A6C RID: 6764
	public int id;

	// Token: 0x04001A6D RID: 6765
	public readonly HashSetMapRegion regions = new HashSetMapRegion();
}
