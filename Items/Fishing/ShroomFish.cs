using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Fishing;

public class ShroomFish : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Shroom Fish");
		// ((ModItem)this).Tooltip.SetDefault("'Is it a fish or a squid?'");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.value = Item.buyPrice(0, 0, 10);
		((Entity)(object)((ModItem)this).Item).width = 24;
		((Entity)(object)((ModItem)this).Item).height = 24;
		((ModItem)this).Item.maxStack = 999;
		((ModItem)this).Item.rare = 1;
	}
}
