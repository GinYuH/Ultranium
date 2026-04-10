using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ice;

public class IcePelt : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Glacial Pelt");
		((ModItem)this).Tooltip.SetDefault("Its really soft and cold");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 24;
		((Entity)(object)((ModItem)this).item).height = 18;
		((ModItem)this).item.value = 50;
		((ModItem)this).item.value = Item.buyPrice(0, 1);
		((ModItem)this).item.rare = 3;
		((ModItem)this).item.maxStack = 999;
	}
}
