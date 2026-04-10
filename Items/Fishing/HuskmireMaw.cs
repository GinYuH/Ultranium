using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Fishing;

public class HuskmireMaw : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Huskmire Maw");
		((ModItem)this).Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.value = Item.buyPrice(0, 0, 20);
		((Entity)(object)((ModItem)this).item).width = 24;
		((Entity)(object)((ModItem)this).item).height = 24;
		((ModItem)this).item.maxStack = 999;
		((ModItem)this).item.value = 100;
		((ModItem)this).item.rare = 5;
	}
}
