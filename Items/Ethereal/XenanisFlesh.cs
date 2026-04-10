using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ethereal;

public class XenanisFlesh : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Ethereal Flesh");
		((ModItem)this).Tooltip.SetDefault("'It almost feels alive...'");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 24;
		((Entity)(object)((ModItem)this).item).height = 18;
		((ModItem)this).item.value = 50;
		((ModItem)this).item.value = Item.buyPrice(0, 10);
		((ModItem)this).item.rare = 9;
		((ModItem)this).item.maxStack = 999;
	}
}
