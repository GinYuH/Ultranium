using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.BossMasks;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class SquidMask : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Zephyr Squid Mask");
		((ModItem)this).Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 28;
		((Entity)(object)((ModItem)this).item).height = 26;
		((ModItem)this).item.value = Item.sellPrice();
		((ModItem)this).item.rare = 1;
		((ModItem)this).item.vanity = true;
	}
}
