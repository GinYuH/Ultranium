using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Blood;

public class BloodClot : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Blood Clot");
		((ModItem)this).Tooltip.SetDefault("\"A clot of blood obtained from a bloody monster\"");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 22;
		((Entity)(object)((ModItem)this).item).height = 18;
		((ModItem)this).item.value = Item.buyPrice(0, 0, 50);
		((ModItem)this).item.rare = 2;
		((ModItem)this).item.maxStack = 999;
	}
}
