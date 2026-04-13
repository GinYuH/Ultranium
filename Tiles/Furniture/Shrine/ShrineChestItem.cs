using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Furniture.Shrine;

public class ShrineChestItem : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Guardian Chest");
	}

	public override void SetDefaults()
	{
		Item.width = 26;
		Item.height = 22;
		Item.maxStack = Item.CommonMaxStack;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.consumable = true;
		Item.value = 500;
		Item.createTile = ModContent.TileType<ShrineChest>();
	}
}
