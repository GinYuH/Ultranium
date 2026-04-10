using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Banner.ShadowEvent;

public class Scp2521Banner : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Abyss Strider Banner");
		// ((ModItem)this).Tooltip.SetDefault("Nearby players get a bonus against: Abyss Strider");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 10;
		((Entity)(object)((ModItem)this).Item).height = 24;
		((ModItem)this).Item.maxStack = 99;
		((ModItem)this).Item.useTurn = true;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.useAnimation = 15;
		((ModItem)this).Item.useTime = 10;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.consumable = true;
		((ModItem)this).Item.rare = 1;
		((ModItem)this).Item.value = Item.buyPrice(0, 0, 10);
		((ModItem)this).Item.createTile = ((ModItem)this).Mod.Find<ModTile>("ShadowEventBanners").Type;
		((ModItem)this).Item.placeStyle = 3;
	}
}
