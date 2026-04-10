using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome;

public class ShadowGrassItem : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Eldritch Moss");
		// Tooltip.SetDefault("will grow various shadow floras");
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
		Item.createTile = Mod.Find<ModTile>("ShadowGrass").Type;
		Item.maxStack = 999;
	}
}
