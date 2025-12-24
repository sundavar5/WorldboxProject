using System;
using Newtonsoft.Json;

// Token: 0x02000521 RID: 1313
[JsonConverter(typeof(BrushPixelDataConverter))]
[Serializable]
public readonly struct BrushPixelData : IEquatable<BrushPixelData>
{
	// Token: 0x06002B0A RID: 11018 RVA: 0x00155B1B File Offset: 0x00153D1B
	public BrushPixelData(int pX, int pY, int pDist)
	{
		this.x = pX;
		this.y = pY;
		this.dist = pDist;
	}

	// Token: 0x06002B0B RID: 11019 RVA: 0x00155B32 File Offset: 0x00153D32
	public bool Equals(BrushPixelData pOther)
	{
		return this.x == pOther.x && this.y == pOther.y;
	}

	// Token: 0x06002B0C RID: 11020 RVA: 0x00155B52 File Offset: 0x00153D52
	public override int GetHashCode()
	{
		return this.x * 100000 + this.y;
	}

	// Token: 0x04002045 RID: 8261
	public readonly int x;

	// Token: 0x04002046 RID: 8262
	public readonly int y;

	// Token: 0x04002047 RID: 8263
	public readonly int dist;
}
