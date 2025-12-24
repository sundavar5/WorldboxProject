using System;
using System.Runtime.CompilerServices;

// Token: 0x02000327 RID: 807
public readonly struct ZoneConnection : IEquatable<ZoneConnection>
{
	// Token: 0x06001F12 RID: 7954 RVA: 0x0010EC3F File Offset: 0x0010CE3F
	public ZoneConnection(TileZone pZone, MapRegion pRegion)
	{
		this.zone = pZone;
		this.region = pRegion;
	}

	// Token: 0x06001F13 RID: 7955 RVA: 0x0010EC4F File Offset: 0x0010CE4F
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(ZoneConnection pObject)
	{
		return this.zone.Equals(pObject.zone) && this.region.Equals(pObject.region);
	}

	// Token: 0x06001F14 RID: 7956 RVA: 0x0010EC77 File Offset: 0x0010CE77
	public override int GetHashCode()
	{
		return this.zone.GetHashCode() + this.region.GetHashCode() * 100000;
	}

	// Token: 0x040016B9 RID: 5817
	public readonly TileZone zone;

	// Token: 0x040016BA RID: 5818
	public readonly MapRegion region;
}
