using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Trophy;

public class DreadTrophyItem : ModItem
{
	public static int _type;

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Dread Trophy");
	}

	public override void SetDefaults()
	{
		Item.width = 30;
		Item.height = 30;
		Item.maxStack = Item.CommonMaxStack;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.consumable = true;
		Item.value = 0;
		Item.rare = ItemRarityID.Blue;
		Item.createTile = Mod.Find<ModTile>("DreadTrophy").Type;
		Item.placeStyle = 0;
	}
}
