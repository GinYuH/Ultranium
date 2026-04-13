using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Fishing;

public class SpectreFish : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Spectre Fish");
		//Tooltip.SetDefault("It seems to be staring at you constantly");
	}

	public override void SetDefaults()
	{
		Item.value = Item.buyPrice(0, 0, 20);
		Item.width = 24;
		Item.height = 24;
		Item.maxStack = 999;
		Item.value = 100;
		Item.rare = ItemRarityID.Pink;
	}
}
