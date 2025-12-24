using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200079F RID: 1951
public class InterestingPeopleElement : MonoBehaviour
{
	// Token: 0x06003DC3 RID: 15811 RVA: 0x001AF888 File Offset: 0x001ADA88
	private void Awake()
	{
		this._pool_elements = new ObjectPoolGenericMono<PrefabUnitElement>(this._element, this._grid);
		for (int i = 0; i < this._grid.childCount; i++)
		{
			Object.DestroyImmediate(this._grid.GetChild(i).gameObject);
		}
	}

	// Token: 0x06003DC4 RID: 15812 RVA: 0x001AF8D8 File Offset: 0x001ADAD8
	public void show(Actor pActor, int pValue)
	{
		this.showMember(pActor);
		this._counter.text = pValue.ToString();
	}

	// Token: 0x06003DC5 RID: 15813 RVA: 0x001AF8F3 File Offset: 0x001ADAF3
	private void showMember(Actor pActor)
	{
		PrefabUnitElement next = this._pool_elements.getNext();
		next.transform.localScale = new Vector3(0.9f, 0.9f, 1f);
		next.show(pActor);
	}

	// Token: 0x06003DC6 RID: 15814 RVA: 0x001AF925 File Offset: 0x001ADB25
	private void OnDisable()
	{
		this._pool_elements.clear(true);
	}

	// Token: 0x04002CBA RID: 11450
	private ObjectPoolGenericMono<PrefabUnitElement> _pool_elements;

	// Token: 0x04002CBB RID: 11451
	[SerializeField]
	private PrefabUnitElement _element;

	// Token: 0x04002CBC RID: 11452
	[SerializeField]
	private Text _counter;

	// Token: 0x04002CBD RID: 11453
	[SerializeField]
	private Transform _grid;
}
