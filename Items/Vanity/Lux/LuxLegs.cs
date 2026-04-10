using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Lux;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class LuxLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).SetStaticDefaults();
		// ((ModItem)this).DisplayName.SetDefault("Lux's Legs");
		// ((ModItem)this).Tooltip.SetDefault("~Developer item~");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 18;
		((Entity)(object)((ModItem)this).Item).height = 18;
		((ModItem)this).Item.vanity = true;
		((ModItem)this).Item.rare = 9;
	}
}
