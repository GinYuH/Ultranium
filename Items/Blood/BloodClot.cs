using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Blood;

public class BloodClot : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Blood Clot");
		//Tooltip.SetDefault("\"A clot of blood obtained from a bloody monster\"");
	}

	public override void SetDefaults()
	{
		Item.width = 22;
		Item.height = 18;
		Item.value = Item.buyPrice(0, 0, 50);
		Item.rare = ItemRarityID.Green;
		Item.maxStack = 999;
	}
}
