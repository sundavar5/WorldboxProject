using System;
using System.Collections;
using UnityEngine;

// Token: 0x020007C5 RID: 1989
public class WarBannersContainer : WarElement
{
	// Token: 0x06003ECC RID: 16076 RVA: 0x001B3793 File Offset: 0x001B1993
	protected override void Awake()
	{
		this.pool_elements = new ObjectPoolGenericMono<KingdomBanner>(this._prefab, this._container);
		base.Awake();
		this._prefab.gameObject.SetActive(false);
	}

	// Token: 0x06003ECD RID: 16077 RVA: 0x001B37C3 File Offset: 0x001B19C3
	protected override void clear()
	{
		this.pool_elements.clear(true);
		base.clear();
	}

	// Token: 0x06003ECE RID: 16078 RVA: 0x001B37D7 File Offset: 0x001B19D7
	protected IEnumerator showBanner(Kingdom pKingdom, bool pLeft = false, bool pWinner = false, bool pLoser = false)
	{
		if (pKingdom.isRekt())
		{
			yield break;
		}
		yield return new WaitForSecondsRealtime(0.025f);
		if (pKingdom.isRekt())
		{
			yield break;
		}
		this.track_objects.Add(pKingdom);
		KingdomBanner tBanner = this.pool_elements.getNext();
		if (!tBanner.HasComponent<DraggableLayoutElement>())
		{
			tBanner.AddComponent<DraggableLayoutElement>();
		}
		tBanner.load(pKingdom);
		if (pLeft)
		{
			tBanner.hasLeftWar();
		}
		if (pWinner)
		{
			tBanner.hasWon();
		}
		if (pLoser)
		{
			tBanner.hasLost();
		}
		yield break;
	}

	// Token: 0x06003ECF RID: 16079 RVA: 0x001B3804 File Offset: 0x001B1A04
	protected override void clearInitial()
	{
		for (int i = 0; i < this._container.childCount; i++)
		{
			Object.Destroy(this._container.GetChild(i).gameObject);
		}
		base.clearInitial();
	}

	// Token: 0x04002DC3 RID: 11715
	private ObjectPoolGenericMono<KingdomBanner> pool_elements;

	// Token: 0x04002DC4 RID: 11716
	[SerializeField]
	private KingdomBanner _prefab;

	// Token: 0x04002DC5 RID: 11717
	[SerializeField]
	private Transform _container;
}
