using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Banner.Shadow;

public class ShadeGhoulBanner : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Shade Ghoul Banner");
		//Tooltip.SetDefault("Nearby players get a bonus against: Shade Ghoul");
	}

	public override void SetDefaults()
	{
		Item.width = 10;
		Item.height = 24;
		Item.maxStack = Item.CommonMaxStack;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.consumable = true;
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.buyPrice(0, 0, 10);
		Item.createTile = Mod.Find<ModTile>("ShadowBanners").Type;
		Item.placeStyle = 3;
	}
}
