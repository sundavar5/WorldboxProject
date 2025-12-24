using System;
using Newtonsoft.Json;

// Token: 0x02000019 RID: 25
[Serializable]
public abstract class Asset : IEquatable<Asset>
{
	// Token: 0x06000187 RID: 391 RVA: 0x0000D1EA File Offset: 0x0000B3EA
	public virtual void create()
	{
	}

	// Token: 0x06000188 RID: 392 RVA: 0x0000D1EC File Offset: 0x0000B3EC
	public void setHash(int pHash)
	{
		this._hashcode = pHash;
	}

	// Token: 0x06000189 RID: 393 RVA: 0x0000D1F5 File Offset: 0x0000B3F5
	public void setIndexID(int pValue)
	{
		this._index = pValue;
	}

	// Token: 0x0600018A RID: 394 RVA: 0x0000D1FE File Offset: 0x0000B3FE
	public int getIndexID()
	{
		return this._index;
	}

	// Token: 0x0600018B RID: 395 RVA: 0x0000D206 File Offset: 0x0000B406
	public bool Equals(Asset pAsset)
	{
		return this._hashcode == pAsset.GetHashCode();
	}

	// Token: 0x0600018C RID: 396 RVA: 0x0000D216 File Offset: 0x0000B416
	public override int GetHashCode()
	{
		return this._hashcode;
	}

	// Token: 0x0600018D RID: 397 RVA: 0x0000D21E File Offset: 0x0000B41E
	public bool isTemplateAsset()
	{
		return this.id.StartsWith("$") || this.id.StartsWith("_");
	}

	// Token: 0x040000C0 RID: 192
	public const string DEFAULT_ASSET_ID = "ASSET_ID";

	// Token: 0x040000C1 RID: 193
	[JsonProperty(Order = -1)]
	public string id = "ASSET_ID";

	// Token: 0x040000C2 RID: 194
	private int _hashcode;

	// Token: 0x040000C3 RID: 195
	private int _index;
}
