using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

// Token: 0x0200045B RID: 1115
[Preserve]
[Serializable]
public class GameProgressData
{
	// Token: 0x06002645 RID: 9797 RVA: 0x00138EE4 File Offset: 0x001370E4
	public GameProgressData()
	{
		this.setDefaultValues();
	}

	// Token: 0x06002646 RID: 9798 RVA: 0x00138F04 File Offset: 0x00137104
	public void setDefaultValues()
	{
		if (this.achievements == null)
		{
			this.achievements = new HashSet<string>();
		}
		if (this.unlocked_traits_actor == null)
		{
			this.unlocked_traits_actor = new HashSet<string>();
		}
		if (this.unlocked_traits_culture == null)
		{
			this.unlocked_traits_culture = new HashSet<string>();
		}
		if (this.unlocked_traits_language == null)
		{
			this.unlocked_traits_language = new HashSet<string>();
		}
		if (this.unlocked_traits_subspecies == null)
		{
			this.unlocked_traits_subspecies = new HashSet<string>();
		}
		if (this.unlocked_traits_clan == null)
		{
			this.unlocked_traits_clan = new HashSet<string>();
		}
		if (this.unlocked_traits_kingdom == null)
		{
			this.unlocked_traits_kingdom = new HashSet<string>();
		}
		if (this.unlocked_traits_religion == null)
		{
			this.unlocked_traits_religion = new HashSet<string>();
		}
		if (this.unlocked_equipment == null)
		{
			this.unlocked_equipment = new HashSet<string>();
		}
		if (this.unlocked_genes == null)
		{
			this.unlocked_genes = new HashSet<string>();
		}
		if (this.unlocked_actors == null)
		{
			this.unlocked_actors = new HashSet<string>();
		}
		if (this.unlocked_plots == null)
		{
			this.unlocked_plots = new HashSet<string>();
		}
	}

	// Token: 0x06002647 RID: 9799 RVA: 0x00138FF8 File Offset: 0x001371F8
	public void prepare()
	{
		this.all_hashsets.Clear();
		this.all_hashsets.Add(this.unlocked_traits_actor);
		this.all_hashsets.Add(this.unlocked_traits_culture);
		this.all_hashsets.Add(this.unlocked_traits_language);
		this.all_hashsets.Add(this.unlocked_traits_subspecies);
		this.all_hashsets.Add(this.unlocked_traits_clan);
		this.all_hashsets.Add(this.unlocked_traits_kingdom);
		this.all_hashsets.Add(this.unlocked_traits_religion);
		this.all_hashsets.Add(this.unlocked_equipment);
		this.all_hashsets.Add(this.unlocked_genes);
		this.all_hashsets.Add(this.unlocked_actors);
		this.all_hashsets.Add(this.unlocked_plots);
	}

	// Token: 0x1700020C RID: 524
	// (set) Token: 0x06002648 RID: 9800 RVA: 0x001390CB File Offset: 0x001372CB
	[Preserve]
	[Obsolete("use .unlocked_traits_actor instead", true)]
	public List<string> unlocked_traits
	{
		set
		{
			if (value == null)
			{
				return;
			}
			if (this.unlocked_traits_actor == null)
			{
				this.unlocked_traits_actor = new HashSet<string>(value);
				return;
			}
			this.unlocked_traits_actor.UnionWith(value);
		}
	}

	// Token: 0x04001CE1 RID: 7393
	[NonSerialized]
	internal List<HashSet<string>> all_hashsets = new List<HashSet<string>>();

	// Token: 0x04001CE2 RID: 7394
	public HashSet<string> achievements;

	// Token: 0x04001CE3 RID: 7395
	public HashSet<string> unlocked_traits_actor;

	// Token: 0x04001CE4 RID: 7396
	public HashSet<string> unlocked_traits_culture;

	// Token: 0x04001CE5 RID: 7397
	public HashSet<string> unlocked_traits_language;

	// Token: 0x04001CE6 RID: 7398
	public HashSet<string> unlocked_traits_subspecies;

	// Token: 0x04001CE7 RID: 7399
	public HashSet<string> unlocked_traits_clan;

	// Token: 0x04001CE8 RID: 7400
	public HashSet<string> unlocked_traits_kingdom;

	// Token: 0x04001CE9 RID: 7401
	public HashSet<string> unlocked_traits_religion;

	// Token: 0x04001CEA RID: 7402
	public HashSet<string> unlocked_equipment;

	// Token: 0x04001CEB RID: 7403
	public HashSet<string> unlocked_genes;

	// Token: 0x04001CEC RID: 7404
	public HashSet<string> unlocked_actors;

	// Token: 0x04001CED RID: 7405
	public HashSet<string> unlocked_plots;

	// Token: 0x04001CEE RID: 7406
	public int saveVersion = 2;
}
