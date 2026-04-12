using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Lux;

[AutoloadEquip(EquipType.Legs)]
public class LuxLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Lux's Legs");
		Tooltip.SetDefault("~Developer item~");
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.vanity = true;
		Item.rare = 9;
	}
}
