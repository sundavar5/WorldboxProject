using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000649 RID: 1609
public class CultureBookButton : MonoBehaviour
{
	// Token: 0x0600345E RID: 13406 RVA: 0x001859FF File Offset: 0x00183BFF
	private void Start()
	{
		this.create();
	}

	// Token: 0x0600345F RID: 13407 RVA: 0x00185A07 File Offset: 0x00183C07
	private void create()
	{
		if (this._created)
		{
			return;
		}
		this._created = true;
		this.setupTooltip();
	}

	// Token: 0x06003460 RID: 13408 RVA: 0x00185A20 File Offset: 0x00183C20
	public void setupTooltip()
	{
		TipButton tTipButton;
		if (!base.TryGetComponent<TipButton>(out tTipButton))
		{
			return;
		}
		tTipButton.setHoverAction(new TooltipAction(this.showTooltip), true);
	}

	// Token: 0x06003461 RID: 13409 RVA: 0x00185A4C File Offset: 0x00183C4C
	internal void load(long pBookID)
	{
		Book tBook = World.world.books.get(pBookID);
		this.load(tBook);
	}

	// Token: 0x06003462 RID: 13410 RVA: 0x00185A74 File Offset: 0x00183C74
	internal void load(Book pBook)
	{
		this._book = pBook;
		BookTypeAsset tTypeAsset = this._book.getAsset();
		string tIconPath = "books/book_icons/" + tTypeAsset.path_icons + this._book.data.path_icon;
		string pPath = "books/book_covers/" + this._book.data.path_cover;
		Sprite tSpriteIcon = SpriteTextureLoader.getSprite(tIconPath);
		Sprite tSpriteCover = SpriteTextureLoader.getSprite(pPath);
		this.icon.sprite = tSpriteIcon;
		this.cover.sprite = tSpriteCover;
		base.gameObject.name = this._book.getAsset().id;
	}

	// Token: 0x06003463 RID: 13411 RVA: 0x00185B0F File Offset: 0x00183D0F
	private void showTooltip()
	{
		Tooltip.show(base.gameObject, "book", new TooltipData
		{
			book = this._book
		});
	}

	// Token: 0x0400277E RID: 10110
	private Book _book;

	// Token: 0x0400277F RID: 10111
	public Image cover;

	// Token: 0x04002780 RID: 10112
	public Image icon;

	// Token: 0x04002781 RID: 10113
	private bool _created;
}
