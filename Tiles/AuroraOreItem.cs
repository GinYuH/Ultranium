using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles;

public class AuroraOreItem : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Aurora Crystal Shard");
		//Tooltip.SetDefault("It glows with a neon energy");
	}

	public override void SetDefaults()
	{
		Item.width = 12;
		Item.height = 12;
		Item.useTime = 8;
		Item.useAnimation = 15;
		Item.useStyle = 1;
		Item.value = 50;
		Item.rare = 2;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.consumable = true;
		Item.createTile = Mod.Find<ModTile>("AuroraOre").Type;
		Item.maxStack = 999;
	}
}
