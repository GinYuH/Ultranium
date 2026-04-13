using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome;

public class ShadowOre : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Tenebris Ore");
		//Tooltip.SetDefault("'Pulses with abyssal energy'");
	}

	public override void SetDefaults()
	{
		Item.width = 12;
		Item.height = 12;
		Item.useTime = 20;
		Item.useAnimation = 20;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.value = 50;
		Item.rare = ItemRarityID.Blue;
		Item.autoReuse = true;
		Item.consumable = true;
		Item.createTile = Mod.Find<ModTile>("ShadowOreTile").Type;
		Item.maxStack = 999;
	}
}
