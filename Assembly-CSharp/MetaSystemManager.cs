using System;
using System.Collections.Generic;
using db;
using db.tables;

// Token: 0x02000227 RID: 551
public abstract class MetaSystemManager<TObject, TData> : CoreSystemManager<TObject, TData> where TObject : MetaObject<TData>, new() where TData : MetaObjectData, new()
{
	// Token: 0x06001498 RID: 5272 RVA: 0x000DBADC File Offset: 0x000D9CDC
	protected void countMetaObject(TObject pMetaObject)
	{
		MetaObjectCounter<TObject, TData> tCounter;
		if (!this._counters_dict.TryGetValue(pMetaObject, out tCounter))
		{
			tCounter = new MetaObjectCounter<TObject, TData>(pMetaObject);
			this._counters_dict.Add(pMetaObject, tCounter);
			this._counters_list.Add(tCounter);
		}
		this._counters_dict[pMetaObject].amount++;
	}

	// Token: 0x06001499 RID: 5273 RVA: 0x000DBB34 File Offset: 0x000D9D34
	protected TObject getMostUsedMetaObject()
	{
		if (!this._counters_list.Any<MetaObjectCounter<TObject, TData>>())
		{
			return default(TObject);
		}
		this._counters_list.Sort(new Comparison<MetaObjectCounter<TObject, TData>>(this.sortByAmount));
		TObject meta_object = this._counters_list[0].meta_object;
		this.clearCounters();
		return meta_object;
	}

	// Token: 0x0600149A RID: 5274 RVA: 0x000DBB86 File Offset: 0x000D9D86
	private int sortByAmount(MetaObjectCounter<TObject, TData> pO1, MetaObjectCounter<TObject, TData> pO2)
	{
		return pO2.amount.CompareTo(pO1.amount);
	}

	// Token: 0x0600149B RID: 5275 RVA: 0x000DBB99 File Offset: 0x000D9D99
	private void clearCounters()
	{
		this._counters_dict.Clear();
		this._counters_list.Clear();
	}

	// Token: 0x0600149C RID: 5276 RVA: 0x000DBBB1 File Offset: 0x000D9DB1
	public override void parallelDirtyUnitsCheck()
	{
		this.beginChecksUnits();
	}

	// Token: 0x0600149D RID: 5277 RVA: 0x000DBBB9 File Offset: 0x000D9DB9
	public void beginChecksUnits()
	{
		if (this._dirty_units)
		{
			this.clearAllUnitLists();
			this.updateDirtyUnits();
			this.finishDirtyUnits();
		}
	}

	// Token: 0x0600149E RID: 5278 RVA: 0x000DBBD8 File Offset: 0x000D9DD8
	public override void startCollectHistoryData()
	{
		HistoryMetaDataAsset tHistoryMetaDataAsset = AssetManager.history_meta_data_library.get(this.type_id);
		if (tHistoryMetaDataAsset.collector == null)
		{
			return;
		}
		foreach (TObject tObject in this)
		{
			Delegate[] invocationList = tHistoryMetaDataAsset.collector.GetInvocationList();
			for (int i = 0; i < invocationList.Length; i++)
			{
				HistoryTable historyTable = ((HistoryDataCollector)invocationList[i])(tObject);
				historyTable.timestamp = (long)World.world.map_stats.history_current_year;
				DBInserter.insertData(historyTable, this.type_id);
			}
		}
	}

	// Token: 0x0600149F RID: 5279 RVA: 0x000DBC88 File Offset: 0x000D9E88
	public override void clearLastYearStats()
	{
		foreach (TObject tobject in this)
		{
			tobject.clearLastYearStats();
		}
	}

	// Token: 0x060014A0 RID: 5280 RVA: 0x000DBCD4 File Offset: 0x000D9ED4
	public override bool isUnitsDirty()
	{
		return this._dirty_units;
	}

	// Token: 0x060014A1 RID: 5281 RVA: 0x000DBCDC File Offset: 0x000D9EDC
	protected virtual void finishDirtyUnits()
	{
		this._dirty_units = false;
		foreach (TObject tObject in this)
		{
			if (tObject.isDirtyUnits())
			{
				tObject.unDirty();
			}
		}
	}

	// Token: 0x060014A2 RID: 5282 RVA: 0x000DBD3C File Offset: 0x000D9F3C
	public override void checkDeadObjects()
	{
		base.checkDeadObjects();
		foreach (TObject tObject in this)
		{
			if (tObject.isReadyForRemoval())
			{
				this._to_remove.Add(tObject);
			}
		}
		foreach (TObject tObject2 in this._to_remove)
		{
			tObject2.triggerOnRemoveObject();
			this.removeObject(tObject2);
		}
		this._to_remove.Clear();
	}

	// Token: 0x060014A3 RID: 5283
	protected abstract void updateDirtyUnits();

	// Token: 0x060014A4 RID: 5284 RVA: 0x000DBDF4 File Offset: 0x000D9FF4
	public virtual void unitDied(TObject pObject)
	{
		this.setDirtyUnits(pObject);
	}

	// Token: 0x060014A5 RID: 5285 RVA: 0x000DBDFD File Offset: 0x000D9FFD
	public virtual void unitAdded(TObject pObject)
	{
		this.setDirtyUnits(pObject);
	}

	// Token: 0x060014A6 RID: 5286 RVA: 0x000DBE06 File Offset: 0x000DA006
	protected override void addObject(TObject pObject)
	{
		base.addObject(pObject);
		this.setDirtyUnits(pObject);
	}

	// Token: 0x060014A7 RID: 5287 RVA: 0x000DBE16 File Offset: 0x000DA016
	public void setDirtyUnits(TObject pObject)
	{
		TObject tobject = pObject;
		if (tobject != null)
		{
			tobject.setDirty();
		}
		this._dirty_units = true;
	}

	// Token: 0x060014A8 RID: 5288 RVA: 0x000DBE30 File Offset: 0x000DA030
	private void clearAllUnitLists()
	{
		foreach (TObject tObject in this)
		{
			if (tObject.isDirtyUnits())
			{
				tObject.clearListUnits();
			}
		}
	}

	// Token: 0x040011C5 RID: 4549
	private bool _dirty_units;

	// Token: 0x040011C6 RID: 4550
	protected Dictionary<TObject, MetaObjectCounter<TObject, TData>> _counters_dict = new Dictionary<TObject, MetaObjectCounter<TObject, TData>>();

	// Token: 0x040011C7 RID: 4551
	protected List<MetaObjectCounter<TObject, TData>> _counters_list = new List<MetaObjectCounter<TObject, TData>>();

	// Token: 0x040011C8 RID: 4552
	private List<TObject> _to_remove = new List<TObject>();
}
