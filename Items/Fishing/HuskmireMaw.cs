using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Fishing;

public class HuskmireMaw : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Huskmire Maw");
		//Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		Item.value = Item.buyPrice(0, 0, 20);
		Item.width = 24;
		Item.height = 24;
		Item.maxStack = 999;
		Item.value = 100;
		Item.rare = 5;
	}
}
