using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Futaba;

[AutoloadEquip(EquipType.Legs)]
public class FutabaLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Futaba Legs");
		//Tooltip.SetDefault("~Developer item~");
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.vanity = true;
		Item.rare = ItemRarityID.Cyan;
	}
}
