using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Lux;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class LuxHead : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).SetStaticDefaults();
		((ModItem)this).DisplayName.SetDefault("Lux's Head");
		((ModItem)this).Tooltip.SetDefault("~Developer item~");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 18;
		((Entity)(object)((ModItem)this).item).height = 18;
		((ModItem)this).item.vanity = true;
		((ModItem)this).item.rare = 9;
	}
}
