using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Humanizer;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005EF RID: 1519
public class PatchLogLoader : MonoBehaviour
{
	// Token: 0x060031AB RID: 12715 RVA: 0x0017B5E1 File Offset: 0x001797E1
	public void OnEnable()
	{
		this.loadEntries();
		base.StartCoroutine(this.createElements());
	}

	// Token: 0x060031AC RID: 12716 RVA: 0x0017B5F8 File Offset: 0x001797F8
	private void loadEntries()
	{
		if (this._entries_list.Count > 0)
		{
			return;
		}
		TextAsset[] tRawPatchNotes = Resources.LoadAll<TextAsset>("texts/patch_notes");
		if (tRawPatchNotes.Length == 0)
		{
			return;
		}
		TextAsset[] array = tRawPatchNotes;
		for (int i = 0; i < array.Length; i++)
		{
			string[] tMainParts = array[i].text.Split(new string[]
			{
				"##########"
			}, StringSplitOptions.None);
			string[] array2 = tMainParts[0].Split(new string[]
			{
				"\r\n",
				"\r",
				"\n"
			}, StringSplitOptions.None);
			string tVersion = array2[0];
			string tName = array2[1];
			string tDate = array2[2];
			string tIconPath = array2[3];
			string tText = tMainParts[1];
			if (tText.StartsWith("\r\n"))
			{
				tText = tText.Substring(2);
			}
			else if (tText.StartsWith("\n"))
			{
				tText = tText.Substring(1);
			}
			PatchLogEntry tNewEntry = new PatchLogEntry
			{
				version = tVersion,
				name = tName,
				date = tDate,
				icon_path = tIconPath,
				text = tText
			};
			this._entries_dict[tNewEntry.version] = tNewEntry;
			this._entries_list.Add(tNewEntry);
		}
		this._entries_list.Sort(delegate(PatchLogEntry pA, PatchLogEntry pB)
		{
			Version tVA;
			Version tVB;
			if (Version.TryParse(pA.version, out tVA) && Version.TryParse(pB.version, out tVB))
			{
				return tVB.CompareTo(tVA);
			}
			return string.Compare(pB.version, pA.version, StringComparison.Ordinal);
		});
	}

	// Token: 0x060031AD RID: 12717 RVA: 0x0017B742 File Offset: 0x00179942
	private IEnumerator createElements()
	{
		if (this._entries_dict.Count == 0)
		{
			yield break;
		}
		int num;
		for (int i = 0; i < this._entries_list.Count; i = num + 1)
		{
			PatchLogEntry tEntry = this._entries_list[i];
			PatchLogElement tElement = this.showEntry(tEntry);
			if (!(tElement == null))
			{
				if (i < 5)
				{
					tElement.unfold();
				}
				else
				{
					tElement.fold();
				}
				yield return new WaitForSeconds(0.05f);
			}
			num = i;
		}
		yield break;
	}

	// Token: 0x060031AE RID: 12718 RVA: 0x0017B754 File Offset: 0x00179954
	private PatchLogElement showEntry(PatchLogEntry pEntry)
	{
		if (!CursedSacrifice.isAllSacrificesDone() && !WorldLawLibrary.world_law_cursed_world.isEnabled() && pEntry.name == "VoidBox")
		{
			return null;
		}
		PatchLogElement result;
		using (ListPool<string> tItems = new ListPool<string>(10))
		{
			string[] tContent = pEntry.text.Split(new string[]
			{
				"\r\n",
				"\r",
				"\n"
			}, StringSplitOptions.None);
			string tStringDaysAgo;
			try
			{
				tStringDaysAgo = this.prettyDaysAgo(pEntry.date);
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				throw;
			}
			bool flag = this.isValidDate(pEntry.date);
			string tStringDate = flag ? pEntry.date : "???";
			string tColorVersion = flag ? "#23F3FF" : "#96adb3";
			string tColorTitle = flag ? "#FFFF51" : "#96adb3";
			string tStringTitle = pEntry.version.ColorHex(tColorVersion, false) + " - " + pEntry.name.ToUpper().ColorHex(tColorTitle, false);
			PatchLogElement tElement = Object.Instantiate<PatchLogElement>(this._prefab_element, base.gameObject.transform);
			tElement.name = "PatchLog " + pEntry.version;
			this._visual_elements.Add(tElement.gameObject);
			PatchLogTitle title = tElement.title;
			title.title.text = tStringTitle;
			tElement.date.text = tStringDate;
			tElement.date_ago.text = tStringDaysAgo;
			Sprite tIconSprite = SpriteTextureLoader.getSprite(pEntry.icon_path);
			if (tIconSprite == null)
			{
				Debug.LogError("Failed to load icon in " + pEntry.version);
			}
			title.icon_left.sprite = tIconSprite;
			title.icon_right.sprite = tIconSprite;
			for (int i = 0; i < tContent.Length; i += 10)
			{
				tItems.Clear();
				int j = i;
				while (j < i + 10 && j < tContent.Length)
				{
					tItems.Add(tContent[j]);
					j++;
				}
				string tTextString = string.Join("\n", tItems);
				tTextString = this.colorElements(tTextString);
				this.createTextField(tTextString, tElement.texts.gameObject).color = Toolbox.color_text_default_bright;
			}
			result = tElement;
		}
		return result;
	}

	// Token: 0x060031AF RID: 12719 RVA: 0x0017B9AC File Offset: 0x00179BAC
	private string prettyDaysAgo(string pDateString)
	{
		if (!this.isValidDate(pDateString))
		{
			return pDateString;
		}
		DateTime tDate = DateTime.ParseExact(pDateString, "yyyy-MM-dd", null);
		int tDaysAgo = (DateTime.UtcNow - tDate).Days;
		CultureInfo tCulture = LocalizedTextManager.getCulture(null);
		DateTime input = tDate;
		bool utcDate = true;
		CultureInfo culture = tCulture;
		string tResult = input.Humanize(utcDate, null, culture);
		if (!string.IsNullOrEmpty(tResult))
		{
			return tResult;
		}
		return tDaysAgo.ToString() + " days ago";
	}

	// Token: 0x060031B0 RID: 12720 RVA: 0x0017BA20 File Offset: 0x00179C20
	private bool isValidDate(string pInput)
	{
		DateTime dateTime;
		return DateTime.TryParseExact(pInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
	}

	// Token: 0x060031B1 RID: 12721 RVA: 0x0017BA40 File Offset: 0x00179C40
	private Text createTextField(string pText, GameObject pEntryBackground)
	{
		Text tTextField = Object.Instantiate<GameObject>(this._prefab_text, pEntryBackground.transform).GetComponent<Text>();
		tTextField.text = pText;
		this._visual_elements.Add(tTextField.gameObject);
		return tTextField;
	}

	// Token: 0x060031B2 RID: 12722 RVA: 0x0017BA80 File Offset: 0x00179C80
	private string colorElements(string pText)
	{
		pText = pText.Replace("added:", "<color=#4CCFFF>added:</color>");
		pText = pText.Replace("fixed:", "<color=#D95032>fixed:</color>");
		pText = pText.Replace("fixes:", "<color=#D95032>fixed:</color>");
		pText = pText.Replace("fxed:", "<color=#D95032>fixed:</color>");
		pText = pText.Replace("changes:", "<color=#F3961F>changed:</color>");
		pText = pText.Replace("changed:", "<color=#F3961F>changed:</color>");
		pText = pText.Replace("ongoing:", "<color=#4CCFFF>ongoing:</color>");
		pText = pText.Replace("modding:", "<color=#43FF43>modding:</color>");
		pText = pText.Replace("known issues:", "<color=#D95032>known issues:</color>");
		pText = pText.Replace("translations:", "<color=#d6abff>translation:</color>");
		return pText;
	}

	// Token: 0x060031B3 RID: 12723 RVA: 0x0017BB44 File Offset: 0x00179D44
	public void OnDisable()
	{
		foreach (GameObject gameObject in this._visual_elements)
		{
			Object.Destroy(gameObject.gameObject);
		}
		this._visual_elements.Clear();
	}

	// Token: 0x0400258C RID: 9612
	private const int FIRST_UNFOLDED_ELEMS_AMOUNT = 5;

	// Token: 0x0400258D RID: 9613
	[SerializeField]
	private GameObject _prefab_text;

	// Token: 0x0400258E RID: 9614
	[SerializeField]
	private GameObject _prefab_entry_bg;

	// Token: 0x0400258F RID: 9615
	[SerializeField]
	private PatchLogElement _prefab_element;

	// Token: 0x04002590 RID: 9616
	private readonly List<GameObject> _visual_elements = new List<GameObject>();

	// Token: 0x04002591 RID: 9617
	private readonly Dictionary<string, PatchLogEntry> _entries_dict = new Dictionary<string, PatchLogEntry>();

	// Token: 0x04002592 RID: 9618
	private readonly List<PatchLogEntry> _entries_list = new List<PatchLogEntry>();
}
