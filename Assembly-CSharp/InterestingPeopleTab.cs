using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

// Token: 0x020007A0 RID: 1952
public class InterestingPeopleTab : WindowMetaElementBase
{
	// Token: 0x06003DC8 RID: 15816 RVA: 0x001AF93C File Offset: 0x001ADB3C
	protected override void Awake()
	{
		this._interesting_people_window = base.GetComponentInParent<IInterestingPeopleWindow>();
		this._all_elements = new InterestingPeopleElement[]
		{
			this.biggest_level,
			this.fastest,
			this.fullest,
			this.happiest,
			this.hungriest,
			this.most_births,
			this.most_children,
			this.most_kills,
			this.most_known,
			this.oldest,
			this.richest,
			this.saddest,
			this.smartest,
			this.dumbest,
			this.strongest,
			this.weakest,
			this.youngest,
			this.most_health,
			this.lowest_health
		};
		this._all_unit_lists = new List<Actor>[]
		{
			this._unit_biggest_level,
			this._unit_fastest,
			this._unit_fullest,
			this._unit_happiest,
			this._unit_hungriest,
			this._unit_most_births,
			this._unit_most_children,
			this._unit_most_kills,
			this._unit_most_known,
			this._unit_oldest,
			this._unit_richest,
			this._unit_saddest,
			this._unit_smartest,
			this._unit_dumbest,
			this._unit_strongest,
			this._unit_weakest,
			this._unit_youngest,
			this._unit_most_health,
			this._unit_lowest_health
		};
		base.Awake();
	}

	// Token: 0x06003DC9 RID: 15817 RVA: 0x001AFAE0 File Offset: 0x001ADCE0
	protected override IEnumerator showContent()
	{
		IEnumerable<Actor> tActors = this._interesting_people_window.getInterestingUnitsList();
		return this.renderElements(tActors);
	}

	// Token: 0x06003DCA RID: 15818 RVA: 0x001AFB00 File Offset: 0x001ADD00
	private IEnumerator renderElements(IEnumerable<Actor> pList)
	{
		InterestingPeopleTab.<renderElements>d__45 <renderElements>d__ = new InterestingPeopleTab.<renderElements>d__45(0);
		<renderElements>d__.<>4__this = this;
		<renderElements>d__.pList = pList;
		return <renderElements>d__;
	}

	// Token: 0x06003DCB RID: 15819 RVA: 0x001AFB16 File Offset: 0x001ADD16
	private IEnumerator render(List<Actor> pActor, InterestingPeopleElement pElement, int pValue, int pMinValue = 2)
	{
		if (pValue < pMinValue || pActor.Count == 0)
		{
			pElement.gameObject.SetActive(false);
			yield break;
		}
		pElement.gameObject.SetActive(true);
		foreach (Actor tActor in pActor)
		{
			if (tActor.isAlive())
			{
				pElement.show(tActor, pValue);
				yield return new WaitForSecondsRealtime(0.025f);
			}
		}
		List<Actor>.Enumerator enumerator = default(List<Actor>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06003DCC RID: 15820 RVA: 0x001AFB3C File Offset: 0x001ADD3C
	private void finishTweens()
	{
		foreach (Tweener t in this._tweeners)
		{
			t.Kill(true);
		}
		this._tweeners.Clear();
	}

	// Token: 0x06003DCD RID: 15821 RVA: 0x001AFB98 File Offset: 0x001ADD98
	protected override void clear()
	{
		base.clear();
		this.finishTweens();
		InterestingPeopleElement[] all_elements = this._all_elements;
		for (int i = 0; i < all_elements.Length; i++)
		{
			all_elements[i].gameObject.SetActive(false);
		}
		List<Actor>[] all_unit_lists = this._all_unit_lists;
		for (int i = 0; i < all_unit_lists.Length; i++)
		{
			all_unit_lists[i].Clear();
		}
	}

	// Token: 0x04002CBE RID: 11454
	private const float TWEEN_DURATION = 0.15f;

	// Token: 0x04002CBF RID: 11455
	public InterestingPeopleElement most_kills;

	// Token: 0x04002CC0 RID: 11456
	public InterestingPeopleElement most_children;

	// Token: 0x04002CC1 RID: 11457
	public InterestingPeopleElement most_births;

	// Token: 0x04002CC2 RID: 11458
	public InterestingPeopleElement oldest;

	// Token: 0x04002CC3 RID: 11459
	public InterestingPeopleElement fastest;

	// Token: 0x04002CC4 RID: 11460
	public InterestingPeopleElement strongest;

	// Token: 0x04002CC5 RID: 11461
	public InterestingPeopleElement weakest;

	// Token: 0x04002CC6 RID: 11462
	public InterestingPeopleElement smartest;

	// Token: 0x04002CC7 RID: 11463
	public InterestingPeopleElement dumbest;

	// Token: 0x04002CC8 RID: 11464
	public InterestingPeopleElement richest;

	// Token: 0x04002CC9 RID: 11465
	public InterestingPeopleElement most_known;

	// Token: 0x04002CCA RID: 11466
	public InterestingPeopleElement biggest_level;

	// Token: 0x04002CCB RID: 11467
	public InterestingPeopleElement happiest;

	// Token: 0x04002CCC RID: 11468
	public InterestingPeopleElement saddest;

	// Token: 0x04002CCD RID: 11469
	public InterestingPeopleElement hungriest;

	// Token: 0x04002CCE RID: 11470
	public InterestingPeopleElement fullest;

	// Token: 0x04002CCF RID: 11471
	public InterestingPeopleElement youngest;

	// Token: 0x04002CD0 RID: 11472
	public InterestingPeopleElement most_health;

	// Token: 0x04002CD1 RID: 11473
	public InterestingPeopleElement lowest_health;

	// Token: 0x04002CD2 RID: 11474
	private readonly List<Actor> _unit_most_kills = new List<Actor>();

	// Token: 0x04002CD3 RID: 11475
	private readonly List<Actor> _unit_most_children = new List<Actor>();

	// Token: 0x04002CD4 RID: 11476
	private readonly List<Actor> _unit_most_births = new List<Actor>();

	// Token: 0x04002CD5 RID: 11477
	private readonly List<Actor> _unit_oldest = new List<Actor>();

	// Token: 0x04002CD6 RID: 11478
	private readonly List<Actor> _unit_fastest = new List<Actor>();

	// Token: 0x04002CD7 RID: 11479
	private readonly List<Actor> _unit_strongest = new List<Actor>();

	// Token: 0x04002CD8 RID: 11480
	private readonly List<Actor> _unit_weakest = new List<Actor>();

	// Token: 0x04002CD9 RID: 11481
	private readonly List<Actor> _unit_smartest = new List<Actor>();

	// Token: 0x04002CDA RID: 11482
	private readonly List<Actor> _unit_dumbest = new List<Actor>();

	// Token: 0x04002CDB RID: 11483
	private readonly List<Actor> _unit_richest = new List<Actor>();

	// Token: 0x04002CDC RID: 11484
	private readonly List<Actor> _unit_most_known = new List<Actor>();

	// Token: 0x04002CDD RID: 11485
	private readonly List<Actor> _unit_biggest_level = new List<Actor>();

	// Token: 0x04002CDE RID: 11486
	private readonly List<Actor> _unit_saddest = new List<Actor>();

	// Token: 0x04002CDF RID: 11487
	private readonly List<Actor> _unit_happiest = new List<Actor>();

	// Token: 0x04002CE0 RID: 11488
	private readonly List<Actor> _unit_hungriest = new List<Actor>();

	// Token: 0x04002CE1 RID: 11489
	private readonly List<Actor> _unit_fullest = new List<Actor>();

	// Token: 0x04002CE2 RID: 11490
	private readonly List<Actor> _unit_youngest = new List<Actor>();

	// Token: 0x04002CE3 RID: 11491
	private readonly List<Actor> _unit_most_health = new List<Actor>();

	// Token: 0x04002CE4 RID: 11492
	private readonly List<Actor> _unit_lowest_health = new List<Actor>();

	// Token: 0x04002CE5 RID: 11493
	private List<Actor>[] _all_unit_lists;

	// Token: 0x04002CE6 RID: 11494
	private InterestingPeopleElement[] _all_elements;

	// Token: 0x04002CE7 RID: 11495
	private IInterestingPeopleWindow _interesting_people_window;

	// Token: 0x04002CE8 RID: 11496
	private List<Tweener> _tweeners = new List<Tweener>();
}
