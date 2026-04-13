using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Furniture.Shrine;

public class IgnodiumShrineItem : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ignodium Shrine");
	}

	public override void SetDefaults()
	{
		Item.rare = ItemRarityID.White;
		Item.width = 12;
		Item.height = 30;
		Item.maxStack = Item.CommonMaxStack;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.consumable = true;
		Item.value = 150;
		Item.createTile = Mod.Find<ModTile>("IgnodiumShrine").Type;
	}
}
