using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags.Acc;

public class EtherealCore : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Xenanis' Core");
		Tooltip.SetDefault("Increases all damage and crit by 15% when below half health\nPress the special ability key to unleash a circle of homing ethereal bolts\nThis ability has a 15 second cooldown when used");
	}

	public override void SetDefaults()
	{
		Item.width = 32;
		Item.height = 32;
		Item.rare = 4;
		Item.value = Item.buyPrice(0, 45);
		Item.accessory = true;
		Item.expert = true;
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
