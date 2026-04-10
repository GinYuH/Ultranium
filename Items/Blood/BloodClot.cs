using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Blood;

public class BloodClot : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Blood Clot");
		// ((ModItem)this).Tooltip.SetDefault("\"A clot of blood obtained from a bloody monster\"");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 22;
		((Entity)(object)((ModItem)this).Item).height = 18;
		((ModItem)this).Item.value = Item.buyPrice(0, 0, 50);
		((ModItem)this).Item.rare = 2;
		((ModItem)this).Item.maxStack = 999;
	}
}
