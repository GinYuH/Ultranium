using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Banner.DarkDepths;

public class AbyssalAnglerBanner : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Abyssal Angler Banner");
		//Tooltip.SetDefault("Nearby players get a bonus against: Abyssal Angler");
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
		Item.createTile = Mod.Find<ModTile>("DarkDepthsBanners").Type;
		Item.placeStyle = 7;
	}
}
