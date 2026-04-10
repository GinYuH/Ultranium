using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians;

public class BrokenIgnodiumSummon : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Ancient Hell Relic");
		// Tooltip.SetDefault("An ancient artifact of hell\nIncreases melee and ranged damage by 7%\n+10 max life");
	}

	public override void SetDefaults()
	{
		Item.width = 24;
		Item.height = 24;
		Item.value = Item.buyPrice(0, 5);
		Item.rare = 2;
		Item.maxStack = 1;
		Item.accessory = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.statLifeMax2 += 10;
		player.GetDamage(DamageClass.Melee) += 0.07f;
		player.GetDamage(DamageClass.Ranged) += 0.07f;
	}
}
