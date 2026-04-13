using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Banner.ShadowEvent;

public class AbyssalWraithBanner : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Abyssal Wraith Banner");
		//Tooltip.SetDefault("Nearby players get a bonus against: Abyssal Wraith");
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
		Item.createTile = Mod.Find<ModTile>("ShadowEventBanners").Type;
		Item.placeStyle = 0;
	}
}
