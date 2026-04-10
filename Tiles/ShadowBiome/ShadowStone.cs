using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome;

public class ShadowStone : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Shadow Stone");
		((ModItem)this).Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 12;
		((Entity)(object)((ModItem)this).item).height = 12;
		((ModItem)this).item.useTime = 8;
		((ModItem)this).item.useAnimation = 15;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.value = 50;
		((ModItem)this).item.rare = 0;
		((ModItem)this).item.useTurn = true;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.consumable = true;
		((ModItem)this).item.createTile = ((ModItem)this).mod.TileType("ShadowStoneTile");
		((ModItem)this).item.maxStack = 999;
	}
}
