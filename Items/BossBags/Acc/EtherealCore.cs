using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags.Acc;

public class EtherealCore : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Xenanis' Core");
		((ModItem)this).Tooltip.SetDefault("Increases all damage and crit by 15% when below half health\nPress the special ability key to unleash a circle of homing ethereal bolts\nThis ability has a 15 second cooldown when used");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 32;
		((Entity)(object)((ModItem)this).item).height = 32;
		((ModItem)this).item.rare = 4;
		((ModItem)this).item.value = Item.buyPrice(0, 45);
		((ModItem)this).item.accessory = true;
		((ModItem)this).item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<UltraniumPlayer>().XenanisCore = true;
		if (player.statLife < (int)((float)player.statLifeMax2 * 0.5f))
		{
			player.minionDamage += 0.15f;
			player.magicDamage += 0.15f;
			player.meleeDamage += 0.15f;
			player.rangedDamage += 0.15f;
		}
	}
}
