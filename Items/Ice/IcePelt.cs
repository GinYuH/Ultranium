using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ice;

public class IcePelt : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Glacial Pelt");
		//Tooltip.SetDefault("Its really soft and cold");
	}

	public override void SetDefaults()
	{
		Item.width = 24;
		Item.height = 18;
		Item.value = 50;
		Item.value = Item.buyPrice(0, 1);
		Item.rare = ItemRarityID.Orange;
		Item.maxStack = 999;
	}
}
