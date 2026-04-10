using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome.Depths;

public class DarkStoneItem : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Dark Stone");
		// Tooltip.SetDefault("'It is as dark as the night'\nCan grow various flora and glowshrooms");
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
		Item.createTile = Mod.Find<ModTile>("DarkStone").Type;
		Item.maxStack = 999;
	}
}
