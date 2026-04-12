using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags.Acc;

public class MysticTentacle : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Zephyr Squid's Tentacle");
		Tooltip.SetDefault("10% increased critical strike chance");
	}

	public override void SetDefaults()
	{
		Item.width = 32;
		Item.height = 32;
		Item.rare = 4;
		Item.value = Item.buyPrice(0, 2);
		Item.accessory = true;
		Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetCritChance(DamageClass.Magic) += 10;
		player.GetCritChance(DamageClass.Melee) += 10;
		player.GetCritChance(DamageClass.Ranged) += 10;
	}
}
