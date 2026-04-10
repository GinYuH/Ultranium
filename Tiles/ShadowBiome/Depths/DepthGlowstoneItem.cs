using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome.Depths;

public class DepthGlowstoneItem : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Depth Glowstone");
		((ModItem)this).Tooltip.SetDefault("Its glow is suprisingly bright");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 12;
		((Entity)(object)((ModItem)this).item).height = 12;
		((ModItem)this).item.useTime = 8;
		((ModItem)this).item.useAnimation = 15;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.value = 50;
		((ModItem)this).item.rare = 1;
		((ModItem)this).item.useTurn = true;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.consumable = true;
		((ModItem)this).item.createTile = ((ModItem)this).mod.TileType("DepthGlowstone");
		((ModItem)this).item.maxStack = 999;
	}
}
