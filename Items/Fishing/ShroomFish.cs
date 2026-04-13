using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Fishing;

public class ShroomFish : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Shroom Fish");
		//Tooltip.SetDefault("'Is it a fish or a squid?'");
	}

	public override void SetDefaults()
	{
		Item.value = Item.buyPrice(0, 0, 10);
		Item.width = 24;
		Item.height = 24;
		Item.maxStack = 999;
		Item.rare = 1;
	}
}
