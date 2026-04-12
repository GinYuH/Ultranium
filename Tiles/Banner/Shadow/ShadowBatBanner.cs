using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Banner.Shadow;

public class ShadowBatBanner : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Shadow Bat Banner");
		Tooltip.SetDefault("Nearby players get a bonus against: Shadow Bat");
	}

	public override void SetDefaults()
	{
		Item.width = 10;
		Item.height = 24;
		Item.maxStack = 99;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = 1;
		Item.consumable = true;
		Item.rare = 1;
		Item.value = Item.buyPrice(0, 0, 10);
		Item.createTile = Mod.Find<ModTile>("ShadowBanners").Type;
		Item.placeStyle = 4;
	}
}
