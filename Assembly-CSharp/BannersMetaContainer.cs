using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007CD RID: 1997
public class BannersMetaContainer<TMetaBanner, TMetaObject, TMetaData> : WindowMetaElementBase where TMetaBanner : BannerGeneric<TMetaObject, TMetaData> where TMetaObject : CoreSystemObject<TMetaData> where TMetaData : BaseSystemData
{
	// Token: 0x06003EFB RID: 16123 RVA: 0x001B456F File Offset: 0x001B276F
	protected override void Awake()
	{
		base.Awake();
		this._pool_elements = new ObjectPoolGenericMono<TMetaBanner>(this._prefab, this._container);
	}

	// Token: 0x06003EFC RID: 16124 RVA: 0x001B458E File Offset: 0x001B278E
	protected override void OnEnable()
	{
	}

	// Token: 0x06003EFD RID: 16125 RVA: 0x001B4590 File Offset: 0x001B2790
	public void update(NanoObject pNano)
	{
		this.clear();
		this._pool_elements.clear(true);
		this.showContent(pNano);
	}

	// Token: 0x06003EFE RID: 16126 RVA: 0x001B45AC File Offset: 0x001B27AC
	private void showContent(NanoObject pNano)
	{
		using (ListPool<TMetaObject> tListObjects = new ListPool<TMetaObject>(this.getMetaList(pNano as IMetaObject)))
		{
			for (int i = 0; i < tListObjects.Count; i++)
			{
				TMetaObject tObject = tListObjects[i];
				this.track_objects.Add(tObject);
				this.showElement(tObject);
			}
		}
	}

	// Token: 0x06003EFF RID: 16127 RVA: 0x001B4618 File Offset: 0x001B2818
	private void showElement(TMetaObject pMeta)
	{
		TMetaBanner tElement = this._pool_elements.getNext();
		tElement.enable_tab_show_click = true;
		tElement.enable_default_click = false;
		if (!tElement.HasComponent<DraggableLayoutElement>())
		{
			tElement.AddComponent<DraggableLayoutElement>();
		}
		tElement.load(pMeta);
	}

	// Token: 0x06003F00 RID: 16128 RVA: 0x001B4673 File Offset: 0x001B2873
	protected virtual IEnumerable<TMetaObject> getMetaList(IMetaObject pMeta)
	{
		throw new NotImplementedException();
	}

	// Token: 0x04002DD9 RID: 11737
	[SerializeField]
	private TMetaBanner _prefab;

	// Token: 0x04002DDA RID: 11738
	[SerializeField]
	private Transform _container;

	// Token: 0x04002DDB RID: 11739
	private StatsWindow _window;

	// Token: 0x04002DDC RID: 11740
	private ObjectPoolGenericMono<TMetaBanner> _pool_elements;
}
