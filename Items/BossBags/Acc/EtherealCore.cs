using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags.Acc;

public class EtherealCore : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Xenanis' Core");
		// ((ModItem)this).Tooltip.SetDefault("Increases all damage and crit by 15% when below half health\nPress the special ability key to unleash a circle of homing ethereal bolts\nThis ability has a 15 second cooldown when used");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 32;
		((Entity)(object)((ModItem)this).Item).height = 32;
		((ModItem)this).Item.rare = 4;
		((ModItem)this).Item.value = Item.buyPrice(0, 45);
		((ModItem)this).Item.accessory = true;
		((ModItem)this).Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<UltraniumPlayer>().XenanisCore = true;
		if (player.statLife < (int)((float)player.statLifeMax2 * 0.5f))
		{
			player.GetDamage(DamageClass.Summon) += 0.15f;
			player.GetDamage(DamageClass.Magic) += 0.15f;
			player.GetDamage(DamageClass.Melee) += 0.15f;
			player.GetDamage(DamageClass.Ranged) += 0.15f;
		}
	}
}
