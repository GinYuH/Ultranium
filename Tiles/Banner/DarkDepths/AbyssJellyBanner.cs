using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Banner.DarkDepths;

public class AbyssJellyBanner : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Abyss Jelly Banner");
		//Tooltip.SetDefault("Nearby players get a bonus against: Abyssal Jelly");
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
		Item.placeStyle = 6;
	}
}
