using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ethereal;

public class XenanisFlesh : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ethereal Flesh");
		//Tooltip.SetDefault("'It almost feels alive...'");
	}

	public override void SetDefaults()
	{
		Item.width = 24;
		Item.height = 18;
		Item.value = 50;
		Item.value = Item.buyPrice(0, 10);
		Item.rare = ItemRarityID.Cyan;
		Item.maxStack = 999;
	}
}
