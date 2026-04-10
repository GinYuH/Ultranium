using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Shrine;

public class UltrumRockItem : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Ultrum Rock");
		// Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		Item.width = 12;
		Item.height = 12;
		Item.useTime = 8;
		Item.useAnimation = 15;
		Item.useStyle = 1;
		Item.value = 50;
		Item.rare = 1;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.consumable = true;
		Item.createTile = Mod.Find<ModTile>("UltrumRock").Type;
		Item.maxStack = 999;
	}
}
