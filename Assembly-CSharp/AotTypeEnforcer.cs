using System;
using Newtonsoft.Json.Utilities;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class AotTypeEnforcer : MonoBehaviour
{
	// Token: 0x06000024 RID: 36 RVA: 0x00003832 File Offset: 0x00001A32
	public void Awake()
	{
		AotHelper.EnsureType<CustomDataContainer<int>>();
		AotHelper.EnsureType<CustomDataContainer<float>>();
		AotHelper.EnsureType<CustomDataContainer<bool>>();
		AotHelper.EnsureType<CustomDataContainer<string>>();
		AotHelper.EnsureList<int>();
		AotHelper.EnsureList<float>();
		AotHelper.EnsureList<bool>();
		AotHelper.EnsureList<string>();
	}
}
