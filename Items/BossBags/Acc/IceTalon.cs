using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags.Acc;

public class IceTalon : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Glacieron Talon");
		//Tooltip.SetDefault("Increases armor penetration by 5\nYour projectiles have a chance to inflict frostburn on enemies");
	}

	public override void SetDefaults()
	{
		Item.value = Item.buyPrice(0, 35);
		Item.width = 28;
		Item.height = 28;
		Item.value = 10000;
		Item.rare = 4;
		Item.accessory = true;
		Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetArmorPenetration(DamageClass.Generic) += 5;
		player.GetModPlayer<UltraniumPlayer>().IceTalon = true;
	}
}
