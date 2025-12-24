using System;
using UnityEngine;

// Token: 0x020001AB RID: 427
[Serializable]
public class ResourceAsset : Asset, ILocalizedAsset, IHandRenderer
{
	// Token: 0x06000C67 RID: 3175 RVA: 0x000B45BF File Offset: 0x000B27BF
	public Sprite getSpriteIcon()
	{
		if (this._cached_sprite_icon == null)
		{
			this._cached_sprite_icon = SpriteTextureLoader.getSprite("ui/Icons/" + this.path_icon);
		}
		return this._cached_sprite_icon;
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x000B45F0 File Offset: 0x000B27F0
	public Sprite getGameplaySprite()
	{
		if (this._cached_gameplay_sprite == null)
		{
			this._cached_gameplay_sprite = this.gameplay_sprites[0];
		}
		return this._cached_gameplay_sprite;
	}

	// Token: 0x06000C69 RID: 3177 RVA: 0x000B4614 File Offset: 0x000B2814
	public string getLocaleID()
	{
		return this.id;
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x000B461C File Offset: 0x000B281C
	public string getTranslatedName()
	{
		return this.getLocaleID().Localize();
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x000B4629 File Offset: 0x000B2829
	public Sprite[] getSprites()
	{
		return this.gameplay_sprites;
	}

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x06000C6C RID: 3180 RVA: 0x000B4631 File Offset: 0x000B2831
	public bool is_colored
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x06000C6D RID: 3181 RVA: 0x000B4634 File Offset: 0x000B2834
	public bool is_animated
	{
		get
		{
			return false;
		}
	}

	// Token: 0x04000B93 RID: 2963
	public string path_icon;

	// Token: 0x04000B94 RID: 2964
	public string path_gameplay_sprite = "bag_default";

	// Token: 0x04000B95 RID: 2965
	[NonSerialized]
	public Sprite[] gameplay_sprites;

	// Token: 0x04000B96 RID: 2966
	public int drop_max = 30;

	// Token: 0x04000B97 RID: 2967
	public int drop_per_mass = 50;

	// Token: 0x04000B98 RID: 2968
	private Sprite _cached_sprite_icon;

	// Token: 0x04000B99 RID: 2969
	private Sprite _cached_gameplay_sprite;

	// Token: 0x04000B9A RID: 2970
	public ResType type = ResType.Ingredient;

	// Token: 0x04000B9B RID: 2971
	public int mine_rate;

	// Token: 0x04000B9C RID: 2972
	public int maximum = 999;

	// Token: 0x04000B9D RID: 2973
	public bool wood;

	// Token: 0x04000B9E RID: 2974
	public bool mineral;

	// Token: 0x04000B9F RID: 2975
	public ResourceEatAction eat_action;

	// Token: 0x04000BA0 RID: 2976
	public int restore_nutrition;

	// Token: 0x04000BA1 RID: 2977
	public int restore_happiness;

	// Token: 0x04000BA2 RID: 2978
	public int restore_mana;

	// Token: 0x04000BA3 RID: 2979
	public int restore_stamina;

	// Token: 0x04000BA4 RID: 2980
	public int produce_min = 10;

	// Token: 0x04000BA5 RID: 2981
	public int stack_size = 15;

	// Token: 0x04000BA6 RID: 2982
	public int loot_value = 1;

	// Token: 0x04000BA7 RID: 2983
	public int money_cost = 2;

	// Token: 0x04000BA8 RID: 2984
	public float restore_health;

	// Token: 0x04000BA9 RID: 2985
	public int give_experience;

	// Token: 0x04000BAA RID: 2986
	public int ingredients_amount = 1;

	// Token: 0x04000BAB RID: 2987
	public string[] ingredients;

	// Token: 0x04000BAC RID: 2988
	public string[] diet;

	// Token: 0x04000BAD RID: 2989
	public bool food;

	// Token: 0x04000BAE RID: 2990
	public int supply_bound_give = 30;

	// Token: 0x04000BAF RID: 2991
	public int supply_bound_take = 10;

	// Token: 0x04000BB0 RID: 2992
	public float favorite_food_chance = 0.5f;

	// Token: 0x04000BB1 RID: 2993
	public float tastiness = 1f;

	// Token: 0x04000BB2 RID: 2994
	public int supply_give = 10;

	// Token: 0x04000BB3 RID: 2995
	public int trade_bound = 40;

	// Token: 0x04000BB4 RID: 2996
	public int trade_give = 10;

	// Token: 0x04000BB5 RID: 2997
	public int trade_cost = 1;

	// Token: 0x04000BB6 RID: 2998
	public int storage_max = 50;

	// Token: 0x04000BB7 RID: 2999
	public float give_chance;

	// Token: 0x04000BB8 RID: 3000
	public string[] give_trait_id;

	// Token: 0x04000BB9 RID: 3001
	[NonSerialized]
	public ActorTrait[] give_trait;

	// Token: 0x04000BBA RID: 3002
	public string[] give_status_id;

	// Token: 0x04000BBB RID: 3003
	[NonSerialized]
	public StatusAsset[] give_status;

	// Token: 0x04000BBC RID: 3004
	public ResourceEatAction give_action;

	// Token: 0x04000BBD RID: 3005
	[NonSerialized]
	public int order = -1;

	// Token: 0x04000BBE RID: 3006
	public string tooltip = "city_resource";

	// Token: 0x04000BBF RID: 3007
	public string full_sprite_path;
}
