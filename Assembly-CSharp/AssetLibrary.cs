using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Beebyte.Obfuscator;
using UnityEngine;

// Token: 0x0200001F RID: 31
[ObfuscateLiterals]
[Serializable]
public abstract class AssetLibrary<T> : BaseAssetLibrary where T : Asset
{
	// Token: 0x06000193 RID: 403 RVA: 0x0000D25C File Offset: 0x0000B45C
	public virtual T get(string pID)
	{
		T tResult;
		if (this.dict.TryGetValue(pID, out tResult))
		{
			return tResult;
		}
		this._not_found.Add(pID);
		return default(T);
	}

	// Token: 0x06000194 RID: 404 RVA: 0x0000D294 File Offset: 0x0000B494
	public T getSimple(string pID)
	{
		if (!this.has(pID))
		{
			return default(T);
		}
		T tResult;
		if (this.dict.TryGetValue(pID, out tResult))
		{
			return tResult;
		}
		return default(T);
	}

	// Token: 0x06000195 RID: 405 RVA: 0x0000D2CF File Offset: 0x0000B4CF
	public virtual bool has(string pID)
	{
		return this.dict.ContainsKey(pID);
	}

	// Token: 0x06000196 RID: 406 RVA: 0x0000D2E0 File Offset: 0x0000B4E0
	public virtual T add(T pAsset)
	{
		string tNewID = pAsset.id;
		if (this.dict.ContainsKey(tNewID))
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				if (!(this.list[i].id != tNewID))
				{
					this.list.RemoveAt(i);
					break;
				}
			}
			this.dict.Remove(tNewID);
			BaseAssetLibrary.logAssetError("<e>AssetLibrary<" + typeof(T).Name + "></e>: duplicate asset - overwriting...", tNewID);
		}
		this.t = pAsset;
		this.t.create();
		this.t.setHash(BaseAssetLibrary._latest_hash++);
		if (!pAsset.isTemplateAsset())
		{
			this.list.Add(pAsset);
		}
		this.t.setIndexID(this.list.Count);
		this.dict.Add(tNewID, pAsset);
		return pAsset;
	}

	// Token: 0x06000197 RID: 407 RVA: 0x0000D3F0 File Offset: 0x0000B5F0
	public virtual T clone(string pNew, string pFrom)
	{
		T tCloned;
		this.clone(out tCloned, this.dict[pFrom]);
		this.t = tCloned;
		this.t.id = pNew;
		this.add(this.t);
		return this.t;
	}

	// Token: 0x06000198 RID: 408 RVA: 0x0000D43C File Offset: 0x0000B63C
	public virtual void clone(out T pNew, T pFrom)
	{
		pNew = Activator.CreateInstance<T>();
		foreach (FieldInfo tField in typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public))
		{
			if (!tField.IsNotSerialized)
			{
				object tValue = tField.GetValue(pFrom);
				if (tValue == null || tField.isString())
				{
					tField.SetValue(pNew, tValue);
				}
				else if (tField.isCloneable())
				{
					ICloneable tCloneable = tValue as ICloneable;
					tField.SetValue(pNew, tCloneable.Clone());
				}
				else if (tField.isCollection())
				{
					ICollection tCollection = tValue as ICollection;
					tField.SetValue(pNew, Activator.CreateInstance(tField.FieldType, new object[]
					{
						tCollection
					}));
				}
				else if (tField.isEnumerable())
				{
					IEnumerable tEnumerable = tValue as IEnumerable;
					tField.SetValue(pNew, Activator.CreateInstance(tField.FieldType, new object[]
					{
						tEnumerable
					}));
				}
				else
				{
					tField.SetValue(pNew, tValue);
				}
			}
		}
	}

	// Token: 0x06000199 RID: 409 RVA: 0x0000D56C File Offset: 0x0000B76C
	internal void loadFromFile<TAssetLib>() where TAssetLib : AssetLibrary<T>
	{
		foreach (T tAsset in JsonUtility.FromJson<TAssetLib>(Resources.Load<TextAsset>(this.file_path).text).list)
		{
			this.add(tAsset);
		}
	}

	// Token: 0x0600019A RID: 410 RVA: 0x0000D5DC File Offset: 0x0000B7DC
	public T[] getArray()
	{
		if (this._array == null)
		{
			this._array = this.list.ToArray();
		}
		return this._array;
	}

	// Token: 0x0600019B RID: 411 RVA: 0x0000D600 File Offset: 0x0000B800
	public override void editorDiagnostic()
	{
		Type tCurrentType = typeof(T);
		while (tCurrentType != null)
		{
			if (!tCurrentType.IsSerializable)
			{
				BaseAssetLibrary.logAssetError("<e>AssetLibrary<" + typeof(T).Name + "></e>: Asset not marked serializable", tCurrentType.Name);
			}
			tCurrentType = tCurrentType.BaseType;
		}
		foreach (FieldInfo tField in typeof(T).GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
		{
			if (tField.IsAssembly && !tField.IsNotSerialized)
			{
				BaseAssetLibrary.logAssetError("<e>AssetLibrary<" + typeof(T).Name + "></e>: Asset field is marked <e>internal</e> - set it to <e>public</e> and/or <e>[NonSerialized]</e> instead. Currently it's not being cloned.", tField.Name);
			}
			if (tField.IsFamily && !tField.IsNotSerialized)
			{
				BaseAssetLibrary.logAssetError("<e>AssetLibrary<" + typeof(T).Name + "></e>: Asset field is marked <e>protected</e> - set it to <e>public</e> and/or <e>[NonSerialized]</e> instead. Currently it's not being cloned.", tField.Name);
			}
			if (tField.IsPrivate && tField.GetCustomAttribute<SerializeField>() != null)
			{
				BaseAssetLibrary.logAssetError("<e>AssetLibrary<" + typeof(T).Name + "></e>: Asset field is marked <e>private</e> and has <e>[SerializeField]</e> attribute - it won't be cloned. Set it to <e>public</e> instead", tField.Name);
			}
		}
		base.editorDiagnostic();
	}

	// Token: 0x0600019C RID: 412 RVA: 0x0000D738 File Offset: 0x0000B938
	public override void checkLocale(Asset pAsset, string pLocaleID)
	{
		string tLocaleID = (pLocaleID != null) ? pLocaleID.Underscore() : null;
		if (tLocaleID != pLocaleID)
		{
			BaseAssetLibrary.logAssetError(string.Concat(new string[]
			{
				"<e>AssetLibrary<",
				typeof(T).Name,
				"></e>: Translation key is not in lowercase - <e>",
				pLocaleID,
				"</e> should be <e>",
				tLocaleID,
				"</e>"
			}), pAsset.id);
		}
		if (!(pAsset is ILocalizedAsset) && !(pAsset is IMultiLocalesAsset))
		{
			BaseAssetLibrary.logAssetError(string.Concat(new string[]
			{
				"<e>AssetLibrary<",
				typeof(T).Name,
				"></e>: Interface missing for <e>",
				tLocaleID,
				"</e>"
			}), pAsset.id);
		}
		if (!string.IsNullOrEmpty(tLocaleID) && !LocalizedTextManager.stringExists(tLocaleID))
		{
			BaseAssetLibrary.logAssetError(string.Concat(new string[]
			{
				"<e>AssetLibrary<",
				typeof(T).Name,
				"></e>: Missing translation key <e>",
				tLocaleID,
				"</e>"
			}), pAsset.id);
			AssetManager.missing_locale_keys.Add(tLocaleID);
		}
	}

	// Token: 0x0600019D RID: 413 RVA: 0x0000D85F File Offset: 0x0000BA5F
	public string getEditorPathForSave()
	{
		return Application.dataPath + "/Resources/" + this.file_path + ".json";
	}

	// Token: 0x0600019E RID: 414 RVA: 0x0000D87B File Offset: 0x0000BA7B
	public void saveToFile(string pPath = "units.json")
	{
		Application.streamingAssetsPath + "/modules/core/" + pPath;
	}

	// Token: 0x0600019F RID: 415 RVA: 0x0000D890 File Offset: 0x0000BA90
	protected bool checkSpriteExists(string pVariableID, string pPath, Asset pAsset)
	{
		if (!string.IsNullOrEmpty(pPath) && !base.hasSpriteInResourcesDebug(pPath))
		{
			BaseAssetLibrary.logAssetError(string.Concat(new string[]
			{
				this.id,
				": <e>",
				pVariableID,
				"</e> doesn't exist for <e>",
				pAsset.id,
				"</e> at "
			}), pPath);
			return false;
		}
		return true;
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x0000D8F0 File Offset: 0x0000BAF0
	protected static TA[] a<TA>(params TA[] pArgs)
	{
		return Toolbox.a<TA>(pArgs);
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x0000D8F8 File Offset: 0x0000BAF8
	protected static List<TL> l<TL>(params TL[] pArgs)
	{
		return Toolbox.l<TL>(pArgs);
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x0000D900 File Offset: 0x0000BB00
	protected static HashSet<TH> h<TH>(params TH[] pArgs)
	{
		return Toolbox.h<TH>(pArgs);
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x0000D908 File Offset: 0x0000BB08
	public override IEnumerable<Asset> getList()
	{
		return this.list;
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000D910 File Offset: 0x0000BB10
	public override int total_items
	{
		get
		{
			return this.list.Count;
		}
	}

	// Token: 0x040000C4 RID: 196
	public List<T> list = new List<T>();

	// Token: 0x040000C5 RID: 197
	[NonSerialized]
	public Dictionary<string, T> dict = new Dictionary<string, T>();

	// Token: 0x040000C6 RID: 198
	protected T t;

	// Token: 0x040000C7 RID: 199
	private T[] _array;

	// Token: 0x040000C8 RID: 200
	public string file_path;

	// Token: 0x040000C9 RID: 201
	private HashSet<string> _not_found = new HashSet<string>();
}
