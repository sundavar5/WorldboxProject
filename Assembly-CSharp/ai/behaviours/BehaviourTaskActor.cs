using System;
using UnityEngine;

namespace ai.behaviours
{
	// Token: 0x0200093B RID: 2363
	[Serializable]
	public class BehaviourTaskActor : BehaviourTaskBase<BehaviourActionActor>
	{
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06004618 RID: 17944 RVA: 0x001D4A14 File Offset: 0x001D2C14
		protected override string locale_key_prefix
		{
			get
			{
				return "task_unit";
			}
		}

		// Token: 0x06004619 RID: 17945 RVA: 0x001D4A1C File Offset: 0x001D2C1C
		public Sprite getSprite()
		{
			if (this._cached_sprite == null)
			{
				this._cached_sprite = SpriteTextureLoader.getSprite(this.path_icon);
				if (this._cached_sprite == null)
				{
					Debug.LogError("No sprite found for " + this.path_icon);
				}
			}
			return this._cached_sprite;
		}

		// Token: 0x0600461A RID: 17946 RVA: 0x001D4A71 File Offset: 0x001D2C71
		public void setIcon(string pPath)
		{
			this.path_icon = pPath;
			this.show_icon = true;
		}

		// Token: 0x040031D1 RID: 12753
		public bool move_from_block;

		// Token: 0x040031D2 RID: 12754
		public bool ignore_fight_check;

		// Token: 0x040031D3 RID: 12755
		public bool in_combat;

		// Token: 0x040031D4 RID: 12756
		public string force_hand_tool = string.Empty;

		// Token: 0x040031D5 RID: 12757
		public bool flag_boat_related;

		// Token: 0x040031D6 RID: 12758
		public bool diet;

		// Token: 0x040031D7 RID: 12759
		public bool cancellable_by_reproduction;

		// Token: 0x040031D8 RID: 12760
		public bool cancellable_by_socialize;

		// Token: 0x040031D9 RID: 12761
		public bool is_fireman;

		// Token: 0x040031DA RID: 12762
		public string path_icon = "ui/Icons/iconWarning";

		// Token: 0x040031DB RID: 12763
		public bool show_icon;

		// Token: 0x040031DC RID: 12764
		public float speed_multiplier = 1f;

		// Token: 0x040031DD RID: 12765
		[NonSerialized]
		public UnitHandToolAsset cached_hand_tool_asset;

		// Token: 0x040031DE RID: 12766
		private Sprite _cached_sprite;
	}
}
