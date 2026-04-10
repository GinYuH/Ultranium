using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome;

public class ShadowOre : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Tenebris Ore");
		((ModItem)this).Tooltip.SetDefault("'Pulses with abyssal energy'");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 12;
		((Entity)(object)((ModItem)this).item).height = 12;
		((ModItem)this).item.useTime = 20;
		((ModItem)this).item.useAnimation = 20;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.value = 50;
		((ModItem)this).item.rare = 1;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.consumable = true;
		((ModItem)this).item.createTile = ((ModItem)this).mod.TileType("ShadowOreTile");
		((ModItem)this).item.maxStack = 999;
	}
}
