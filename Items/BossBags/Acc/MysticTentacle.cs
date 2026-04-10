using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags.Acc;

public class MysticTentacle : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Zephyr Squid's Tentacle");
		// ((ModItem)this).Tooltip.SetDefault("10% increased critical strike chance");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 32;
		((Entity)(object)((ModItem)this).Item).height = 32;
		((ModItem)this).Item.rare = 4;
		((ModItem)this).Item.value = Item.buyPrice(0, 2);
		((ModItem)this).Item.accessory = true;
		((ModItem)this).Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetCritChance(DamageClass.Magic) += 10;
		player.GetCritChance(DamageClass.Melee) += 10;
		player.GetCritChance(DamageClass.Ranged) += 10;
	}
}
