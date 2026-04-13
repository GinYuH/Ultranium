using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Banner.ShadowEvent;

public class Scp2521Banner : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Abyss Strider Banner");
		//Tooltip.SetDefault("Nearby players get a bonus against: Abyss Strider");
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
		Item.placeStyle = 3;
	}
}
