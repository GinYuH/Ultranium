using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread;

public class DreadTooth : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Dread Tooth");
		//Tooltip.SetDefault("Increases armor penetration by 8");
	}

	public override void SetDefaults()
	{
		Item.width = 28;
		Item.height = 28;
		Item.value = Item.buyPrice(0, 10);
		Item.rare = 4;
		Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetArmorPenetration(DamageClass.Generic) += 8;
	}
}
