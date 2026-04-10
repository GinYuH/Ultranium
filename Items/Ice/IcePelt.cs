using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ice;

public class IcePelt : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Glacial Pelt");
		// ((ModItem)this).Tooltip.SetDefault("Its really soft and cold");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 24;
		((Entity)(object)((ModItem)this).Item).height = 18;
		((ModItem)this).Item.value = 50;
		((ModItem)this).Item.value = Item.buyPrice(0, 1);
		((ModItem)this).Item.rare = 3;
		((ModItem)this).Item.maxStack = 999;
	}
}
