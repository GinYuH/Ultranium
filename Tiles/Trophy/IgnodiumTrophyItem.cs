using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Trophy;

public class IgnodiumTrophyItem : ModItem
{
	public static int _type;

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ignodium Trophy");
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
		Item.useStyle = 1;
		Item.consumable = true;
		Item.value = 0;
		Item.rare = 1;
		Item.createTile = Mod.Find<ModTile>("IgnodiumTrophy").Type;
		Item.placeStyle = 0;
	}
}
