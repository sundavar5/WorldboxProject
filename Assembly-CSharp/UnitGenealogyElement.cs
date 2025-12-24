using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006E0 RID: 1760
public class UnitGenealogyElement : UnitElement
{
	// Token: 0x0600386C RID: 14444 RVA: 0x001952F4 File Offset: 0x001934F4
	protected override void Awake()
	{
		this._pool_siblings = new ObjectPoolGenericMono<UiUnitAvatarElement>(this.prefab_avatar, this.transform_siblings);
		this._pool_children = new ObjectPoolGenericMono<UiUnitAvatarElement>(this.prefab_avatar, this.transform_children);
		this._pool_grandparents = new ObjectPoolGenericMono<UiUnitAvatarElement>(this.prefab_avatar, this.transform_grandparents);
		this._pool_parents = new ObjectPoolGenericMono<UiUnitAvatarElement>(this.prefab_avatar, this.transform_parents);
		this._siblings_unfolder = Object.Instantiate<UnfoldButton>(this._prefab_unfolder, this.transform_siblings);
		this._siblings_unfolder.setCallback(delegate(bool _)
		{
			base.StartCoroutine(this.loadSiblings(true));
		});
		this._children_unfolder = Object.Instantiate<UnfoldButton>(this._prefab_unfolder, this.transform_children);
		this._children_unfolder.setCallback(delegate(bool _)
		{
			base.StartCoroutine(this.loadChildren(true));
		});
		base.Awake();
	}

	// Token: 0x0600386D RID: 14445 RVA: 0x001953BF File Offset: 0x001935BF
	protected override IEnumerator showContent()
	{
		if (this.actor.asset.inspect_sex)
		{
			if (this.actor.isSexMale())
			{
				this._sex_icon.sprite = SpriteTextureLoader.getSprite("ui/icons/IconMale");
			}
			else
			{
				this._sex_icon.sprite = SpriteTextureLoader.getSprite("ui/icons/IconFemale");
			}
		}
		else
		{
			this._sex_icon.sprite = this._default_genealogy_icon;
		}
		yield return this.loadParents();
		yield return this.loadChildren(false);
		yield return this.loadSiblings(false);
		yield return this.loadGrandParents();
		yield break;
	}

	// Token: 0x0600386E RID: 14446 RVA: 0x001953D0 File Offset: 0x001935D0
	protected override void clear()
	{
		this._pool_siblings.clear(true);
		this._pool_children.clear(true);
		this._pool_grandparents.clear(true);
		this._pool_parents.clear(true);
		this.prefab_avatar.gameObject.SetActive(false);
		this._siblings_unfolder.gameObject.SetActive(false);
		this._siblings_unfolder.clear();
		this._children_unfolder.gameObject.SetActive(false);
		this._children_unfolder.clear();
		base.clear();
	}

	// Token: 0x0600386F RID: 14447 RVA: 0x0019545C File Offset: 0x0019365C
	private IEnumerator loadParents()
	{
		UnitGenealogyElement.<loadParents>d__22 <loadParents>d__ = new UnitGenealogyElement.<loadParents>d__22(0);
		<loadParents>d__.<>4__this = this;
		return <loadParents>d__;
	}

	// Token: 0x06003870 RID: 14448 RVA: 0x0019546B File Offset: 0x0019366B
	private IEnumerator loadGrandParents()
	{
		UnitGenealogyElement.<loadGrandParents>d__23 <loadGrandParents>d__ = new UnitGenealogyElement.<loadGrandParents>d__23(0);
		<loadGrandParents>d__.<>4__this = this;
		return <loadGrandParents>d__;
	}

	// Token: 0x06003871 RID: 14449 RVA: 0x0019547A File Offset: 0x0019367A
	private IEnumerator loadChildren(bool pUnfold = false)
	{
		yield return this.unfold(new UnfoldCheck(this.childrenCheck), this._pool_children, this._children_unfolder, this.transform_children, pUnfold);
		yield break;
	}

	// Token: 0x06003872 RID: 14450 RVA: 0x00195490 File Offset: 0x00193690
	private IEnumerator loadSiblings(bool pUnfold = false)
	{
		yield return this.unfold(new UnfoldCheck(this.siblingsCheck), this._pool_siblings, this._siblings_unfolder, this.transform_siblings, pUnfold);
		yield break;
	}

	// Token: 0x06003873 RID: 14451 RVA: 0x001954A6 File Offset: 0x001936A6
	private IEnumerator unfold(UnfoldCheck pCheckAction, ObjectPoolGenericMono<UiUnitAvatarElement> pPool, UnfoldButton pFoldButton, Transform pParent, bool pUnfold = false)
	{
		UnitGenealogyElement.<unfold>d__26 <unfold>d__ = new UnitGenealogyElement.<unfold>d__26(0);
		<unfold>d__.<>4__this = this;
		<unfold>d__.pCheckAction = pCheckAction;
		<unfold>d__.pPool = pPool;
		<unfold>d__.pFoldButton = pFoldButton;
		<unfold>d__.pParent = pParent;
		<unfold>d__.pUnfold = pUnfold;
		return <unfold>d__;
	}

	// Token: 0x06003874 RID: 14452 RVA: 0x001954DA File Offset: 0x001936DA
	private IEnumerator counter(int pLeft, UnfoldButton pButton)
	{
		float tPerStep = (float)pLeft / 20f;
		for (float i = 0f; i < (float)(pLeft + 1); i += tPerStep)
		{
			string tText = "+" + Mathf.Floor(i).ToString();
			pButton.setText(tText);
			yield return new WaitForSecondsRealtime(0.025f);
		}
		yield break;
	}

	// Token: 0x06003875 RID: 14453 RVA: 0x001954F0 File Offset: 0x001936F0
	private bool childrenCheck(Actor pActor)
	{
		return pActor.isChildOf(this.actor);
	}

	// Token: 0x06003876 RID: 14454 RVA: 0x001954FE File Offset: 0x001936FE
	private bool siblingsCheck(Actor pActor)
	{
		return this.hasSameParent(pActor, this.actor);
	}

	// Token: 0x06003877 RID: 14455 RVA: 0x00195510 File Offset: 0x00193710
	private bool hasSameParent(Actor pActor, Actor pOther)
	{
		long tParent = pOther.data.parent_id_1;
		long tParent2 = pOther.data.parent_id_2;
		long tID = pActor.data.parent_id_1;
		if (tID.hasValue())
		{
			if (tID == tParent)
			{
				return true;
			}
			if (tID == tParent2)
			{
				return true;
			}
		}
		tID = pActor.data.parent_id_2;
		if (tID.hasValue())
		{
			if (tID == tParent)
			{
				return true;
			}
			if (tID == tParent2)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003878 RID: 14456 RVA: 0x00195576 File Offset: 0x00193776
	private IEnumerator showAvatar(Actor pActor, ObjectPoolGenericMono<UiUnitAvatarElement> pPool)
	{
		if (pActor.isRekt())
		{
			yield break;
		}
		yield return new WaitForSecondsRealtime(0.025f);
		if (pActor.isRekt())
		{
			yield break;
		}
		pPool.getNext().show(pActor);
		yield break;
	}

	// Token: 0x040029DF RID: 10719
	private const int AVATARS_LIMIT_PER_UNFOLD = 128;

	// Token: 0x040029E0 RID: 10720
	private const int AVATARS_LIMIT_INITIAL = 16;

	// Token: 0x040029E1 RID: 10721
	public const float COUNT_ANIMATION_STEP_TIME = 0.025f;

	// Token: 0x040029E2 RID: 10722
	private const float COUNT_ANIMATION_LENGTH = 0.5f;

	// Token: 0x040029E3 RID: 10723
	public const float COUNT_ANIMATION_STEPS = 20f;

	// Token: 0x040029E4 RID: 10724
	public UiUnitAvatarElement prefab_avatar;

	// Token: 0x040029E5 RID: 10725
	[SerializeField]
	private UnfoldButton _prefab_unfolder;

	// Token: 0x040029E6 RID: 10726
	[SerializeField]
	private Image _sex_icon;

	// Token: 0x040029E7 RID: 10727
	[SerializeField]
	private Sprite _default_genealogy_icon;

	// Token: 0x040029E8 RID: 10728
	private UnfoldButton _siblings_unfolder;

	// Token: 0x040029E9 RID: 10729
	private UnfoldButton _children_unfolder;

	// Token: 0x040029EA RID: 10730
	private ObjectPoolGenericMono<UiUnitAvatarElement> _pool_grandparents;

	// Token: 0x040029EB RID: 10731
	private ObjectPoolGenericMono<UiUnitAvatarElement> _pool_parents;

	// Token: 0x040029EC RID: 10732
	private ObjectPoolGenericMono<UiUnitAvatarElement> _pool_siblings;

	// Token: 0x040029ED RID: 10733
	private ObjectPoolGenericMono<UiUnitAvatarElement> _pool_children;

	// Token: 0x040029EE RID: 10734
	public Transform transform_siblings;

	// Token: 0x040029EF RID: 10735
	public Transform transform_children;

	// Token: 0x040029F0 RID: 10736
	public Transform transform_parents;

	// Token: 0x040029F1 RID: 10737
	public Transform transform_grandparents;
}
