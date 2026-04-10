using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Fishing;

public class ShroomFish : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Shroom Fish");
		((ModItem)this).Tooltip.SetDefault("'Is it a fish or a squid?'");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.value = Item.buyPrice(0, 0, 10);
		((Entity)(object)((ModItem)this).item).width = 24;
		((Entity)(object)((ModItem)this).item).height = 24;
		((ModItem)this).item.maxStack = 999;
		((ModItem)this).item.rare = 1;
	}
}
