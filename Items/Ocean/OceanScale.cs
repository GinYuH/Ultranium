using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ocean;

public class OceanScale : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Zephyr Chromatophores");
		//Tooltip.SetDefault("The skin of the zephyr squid");
	}

	public override void SetDefaults()
	{
		Item.width = 24;
		Item.height = 24;
		Item.value = Item.buyPrice(0, 0, 5);
		Item.rare = ItemRarityID.Green;
		Item.maxStack = 999;
	}
}
