using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000809 RID: 2057
public class WorkshopInfoIcons : MonoBehaviour
{
	// Token: 0x06004081 RID: 16513 RVA: 0x001B9ED8 File Offset: 0x001B80D8
	private void OnEnable()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		WorkshopMapData tMeta = SaveManager.currentWorkshopMapData;
		if (tMeta == null)
		{
			return;
		}
		this.favorites.text = tMeta.workshop_item.NumFavorites.ToString();
		this.upvotes.text = tMeta.workshop_item.VotesUp.ToString();
		this.comments.text = tMeta.workshop_item.NumComments.ToString();
		this.subscription.text = tMeta.workshop_item.NumSubscriptions.ToString();
	}

	// Token: 0x04002EAE RID: 11950
	public Text favorites;

	// Token: 0x04002EAF RID: 11951
	public Text upvotes;

	// Token: 0x04002EB0 RID: 11952
	public Text comments;

	// Token: 0x04002EB1 RID: 11953
	public Text subscription;
}
