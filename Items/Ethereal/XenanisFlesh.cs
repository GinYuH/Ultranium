using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ethereal;

public class XenanisFlesh : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Ethereal Flesh");
		// ((ModItem)this).Tooltip.SetDefault("'It almost feels alive...'");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 24;
		((Entity)(object)((ModItem)this).Item).height = 18;
		((ModItem)this).Item.value = 50;
		((ModItem)this).Item.value = Item.buyPrice(0, 10);
		((ModItem)this).Item.rare = 9;
		((ModItem)this).Item.maxStack = 999;
	}
}
