using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians;

public class BrokenUltrumSummon : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Ancient Nature Relic");
		((ModItem)this).Tooltip.SetDefault("An ancient artifact of nature\nIncreases magic and summon damage by 7%\n+20 max mana and +1 max minions");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 24;
		((Entity)(object)((ModItem)this).item).height = 24;
		((ModItem)this).item.value = Item.buyPrice(0, 5);
		((ModItem)this).item.rare = 2;
		((ModItem)this).item.maxStack = 1;
		((ModItem)this).item.accessory = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.statManaMax2 += 20;
		player.maxMinions++;
		player.magicDamage += 0.07f;
		player.minionDamage += 0.07f;
	}
}
