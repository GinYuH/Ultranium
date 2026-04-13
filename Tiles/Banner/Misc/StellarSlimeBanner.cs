using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Banner.Misc;

public class StellarSlimeBanner : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Stellar Slime Banner");
		//Tooltip.SetDefault("Nearby players get a bonus against: Stellar Slime");
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
		Item.useStyle = 1;
		Item.consumable = true;
		Item.rare = 1;
		Item.value = Item.buyPrice(0, 0, 10);
		Item.createTile = Mod.Find<ModTile>("MiscBanners").Type;
		Item.placeStyle = 3;
	}
}
