using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome.Depths;

public class DepthGlowstoneItem : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Depth Glowstone");
		//Tooltip.SetDefault("Its glow is suprisingly bright");
	}

	public override void SetDefaults()
	{
		Item.width = 12;
		Item.height = 12;
		Item.useTime = 8;
		Item.useAnimation = 15;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.value = 50;
		Item.rare = ItemRarityID.Blue;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.consumable = true;
		Item.createTile = Mod.Find<ModTile>("DepthGlowstone").Type;
		Item.maxStack = 999;
	}
}
