using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.Materials;

public class DreadScale : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Dread Scale");
		//Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		Item item = new Item();
		Item.width = Item.width;
		Item.height = Item.height;
		Item.maxStack = 999;
		Item.value = Item.buyPrice(0, 0, 15);
		Item.rare = ItemRarityID.LightRed;
	}
}
