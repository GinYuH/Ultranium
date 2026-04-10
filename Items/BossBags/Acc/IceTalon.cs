using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags.Acc;

public class IceTalon : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Glacieron Talon");
		((ModItem)this).Tooltip.SetDefault("Increases armor penetration by 5\nYour projectiles have a chance to inflict frostburn on enemies");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.value = Item.buyPrice(0, 35);
		((Entity)(object)((ModItem)this).item).width = 28;
		((Entity)(object)((ModItem)this).item).height = 28;
		((ModItem)this).item.value = 10000;
		((ModItem)this).item.rare = 4;
		((ModItem)this).item.accessory = true;
		((ModItem)this).item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.armorPenetration += 5;
		player.GetModPlayer<UltraniumPlayer>().IceTalon = true;
	}
}
