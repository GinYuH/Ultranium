using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Lux;

[AutoloadEquip(EquipType.Body)]
public class LuxBody : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Lux's Body");
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
