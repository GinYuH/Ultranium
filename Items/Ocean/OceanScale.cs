using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ocean;

public class OceanScale : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Zephyr Chromatophores");
		((ModItem)this).Tooltip.SetDefault("The skin of the zephyr squid");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 24;
		((Entity)(object)((ModItem)this).item).height = 24;
		((ModItem)this).item.value = Item.buyPrice(0, 0, 5);
		((ModItem)this).item.rare = 2;
		((ModItem)this).item.maxStack = 999;
	}
}
