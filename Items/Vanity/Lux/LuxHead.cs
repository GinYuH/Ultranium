using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Lux;

[AutoloadEquip(EquipType.Head)]
public class LuxHead : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Lux's Head");
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
