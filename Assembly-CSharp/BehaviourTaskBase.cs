using System;
using System.Collections.Generic;
using System.ComponentModel;

// Token: 0x02000385 RID: 901
[Serializable]
public class BehaviourTaskBase<T> : BehaviourElementAI, ILocalizedAsset where T : BehaviourElementAI
{
	// Token: 0x170001EF RID: 495
	// (get) Token: 0x0600218A RID: 8586 RVA: 0x0011CCC0 File Offset: 0x0011AEC0
	[DefaultValue(true)]
	protected virtual bool has_locales
	{
		get
		{
			return true;
		}
	}

	// Token: 0x170001F0 RID: 496
	// (get) Token: 0x0600218B RID: 8587 RVA: 0x0011CCC3 File Offset: 0x0011AEC3
	protected virtual string locale_key_prefix
	{
		get
		{
			throw new NotImplementedException(base.GetType().Name);
		}
	}

	// Token: 0x0600218C RID: 8588 RVA: 0x0011CCD5 File Offset: 0x0011AED5
	public BehaviourTaskBase()
	{
		this.create();
	}

	// Token: 0x0600218D RID: 8589 RVA: 0x0011CD04 File Offset: 0x0011AF04
	public T get(int pIndex)
	{
		return this.list[pIndex];
	}

	// Token: 0x0600218E RID: 8590 RVA: 0x0011CD12 File Offset: 0x0011AF12
	public string getLocaleID()
	{
		if (!this.has_locales)
		{
			return null;
		}
		if (string.IsNullOrEmpty(this.locale_key))
		{
			return this.locale_key_prefix + "_" + this.id;
		}
		return this.locale_key;
	}

	// Token: 0x0600218F RID: 8591 RVA: 0x0011CD48 File Offset: 0x0011AF48
	public string getLocalizedText()
	{
		if (!this.has_locales)
		{
			return "???";
		}
		return this.getLocaleID().Localize();
	}

	// Token: 0x06002190 RID: 8592 RVA: 0x0011CD64 File Offset: 0x0011AF64
	public void addRepeatActions(int pIndexAmount, int pHowManyTimes)
	{
		List<T> tList = new List<T>();
		int tOriginalListAmount = this.list.Count;
		for (int i = 0; i < pHowManyTimes; i++)
		{
			for (int tActionIndex = 0; tActionIndex < pIndexAmount; tActionIndex++)
			{
				int tActualIndex = tOriginalListAmount - (pIndexAmount - tActionIndex);
				T tAction = this.list[tActualIndex];
				tList.Add(tAction);
			}
		}
		this.list.AddRange(tList);
	}

	// Token: 0x06002191 RID: 8593 RVA: 0x0011CDC8 File Offset: 0x0011AFC8
	public void addBeh(T pAction)
	{
		pAction.id = pAction.GetType().ToString();
		pAction.id = pAction.id.Replace("ai.behaviours.", "");
		this.list.Add(pAction);
		pAction.create();
	}

	// Token: 0x06002192 RID: 8594 RVA: 0x0011CE2C File Offset: 0x0011B02C
	public void addTaskVerifier(T pAction)
	{
		this.task_verifier = pAction;
		this.has_verifier = true;
	}

	// Token: 0x040018D1 RID: 6353
	[DefaultValue(1f)]
	public float single_interval = 1f;

	// Token: 0x040018D2 RID: 6354
	public float single_interval_random;

	// Token: 0x040018D3 RID: 6355
	public List<T> list = new List<T>();

	// Token: 0x040018D4 RID: 6356
	public T task_verifier;

	// Token: 0x040018D5 RID: 6357
	public bool has_verifier;

	// Token: 0x040018D6 RID: 6358
	[DefaultValue("")]
	public string locale_key = string.Empty;

	// Token: 0x040018D7 RID: 6359
	public bool debug_flag;
}
