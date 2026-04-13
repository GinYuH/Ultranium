using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Pois1x;

[AutoloadEquip(EquipType.Legs)]
public class PoisonLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Pois1x's Leggings");
		//Tooltip.SetDefault("~Developer item~");
		ArmorIDs.Legs.Sets.HidesBottomSkin[Item.legSlot] = true;
    }

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.vanity = true;
		Item.rare = ItemRarityID.Cyan;
	}
}
