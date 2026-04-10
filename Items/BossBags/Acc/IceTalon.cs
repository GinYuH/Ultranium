using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags.Acc;

public class IceTalon : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Glacieron Talon");
		// ((ModItem)this).Tooltip.SetDefault("Increases armor penetration by 5\nYour projectiles have a chance to inflict frostburn on enemies");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.value = Item.buyPrice(0, 35);
		((Entity)(object)((ModItem)this).Item).width = 28;
		((Entity)(object)((ModItem)this).Item).height = 28;
		((ModItem)this).Item.value = 10000;
		((ModItem)this).Item.rare = 4;
		((ModItem)this).Item.accessory = true;
		((ModItem)this).Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetArmorPenetration(DamageClass.Generic) += 5;
		player.GetModPlayer<UltraniumPlayer>().IceTalon = true;
	}
}
