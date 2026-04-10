using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar;

public class StellarDust : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Stellar Dust");
		// ((ModItem)this).Tooltip.SetDefault("'It Sparkles like the stars in the night'");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 24;
		((Entity)(object)((ModItem)this).Item).height = 24;
		((ModItem)this).Item.value = 50;
		((ModItem)this).Item.rare = 5;
		((ModItem)this).Item.maxStack = 99;
	}
}
