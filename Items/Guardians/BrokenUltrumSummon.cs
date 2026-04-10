using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians;

public class BrokenUltrumSummon : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Ancient Nature Relic");
		// Tooltip.SetDefault("An ancient artifact of nature\nIncreases magic and summon damage by 7%\n+20 max mana and +1 max minions");
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
		player.statManaMax2 += 20;
		player.maxMinions++;
		player.GetDamage(DamageClass.Magic) += 0.07f;
		player.GetDamage(DamageClass.Summon) += 0.07f;
	}
}
