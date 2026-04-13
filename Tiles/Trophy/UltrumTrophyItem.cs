using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Trophy;

public class UltrumTrophyItem : ModItem
{
	public static int _type;

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ultrum Trophy");
	}

	public override void SetDefaults()
	{
		Item.width = 30;
		Item.height = 30;
		Item.maxStack = 99;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = 1;
		Item.consumable = true;
		Item.value = 0;
		Item.rare = 1;
		Item.createTile = Mod.Find<ModTile>("UltrumTrophy").Type;
		Item.placeStyle = 0;
	}
}
