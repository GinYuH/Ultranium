using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar;

public class StellarDust : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Stellar Dust");
		//Tooltip.SetDefault("'It Sparkles like the stars in the night'");
	}

	public override void SetDefaults()
	{
		((Entity)(object)Item).width = 24;
		((Entity)(object)Item).height = 24;
		Item.value = 50;
		Item.rare = 5;
		Item.maxStack = Item.CommonMaxStack;
	}
}
