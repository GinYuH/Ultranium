using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Fishing;

public class HuskmireMaw : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Huskmire Maw");
		// ((ModItem)this).Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.value = Item.buyPrice(0, 0, 20);
		((Entity)(object)((ModItem)this).Item).width = 24;
		((Entity)(object)((ModItem)this).Item).height = 24;
		((ModItem)this).Item.maxStack = 999;
		((ModItem)this).Item.value = 100;
		((ModItem)this).Item.rare = 5;
	}
}
