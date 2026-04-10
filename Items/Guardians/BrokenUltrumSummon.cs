using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians;

public class BrokenUltrumSummon : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Ancient Nature Relic");
		// ((ModItem)this).Tooltip.SetDefault("An ancient artifact of nature\nIncreases magic and summon damage by 7%\n+20 max mana and +1 max minions");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 24;
		((Entity)(object)((ModItem)this).Item).height = 24;
		((ModItem)this).Item.value = Item.buyPrice(0, 5);
		((ModItem)this).Item.rare = 2;
		((ModItem)this).Item.maxStack = 1;
		((ModItem)this).Item.accessory = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.statManaMax2 += 20;
		player.maxMinions++;
		player.GetDamage(DamageClass.Magic) += 0.07f;
		player.GetDamage(DamageClass.Summon) += 0.07f;
	}
}
