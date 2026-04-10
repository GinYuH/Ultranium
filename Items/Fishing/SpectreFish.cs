using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Fishing;

public class SpectreFish : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Spectre Fish");
		// ((ModItem)this).Tooltip.SetDefault("It seems to be staring at you constantly");
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
