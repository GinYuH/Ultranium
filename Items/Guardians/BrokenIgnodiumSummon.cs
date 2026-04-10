using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians;

public class BrokenIgnodiumSummon : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Ancient Hell Relic");
		((ModItem)this).Tooltip.SetDefault("An ancient artifact of hell\nIncreases melee and ranged damage by 7%\n+10 max life");
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
		player.statLifeMax2 += 10;
		player.meleeDamage += 0.07f;
		player.rangedDamage += 0.07f;
	}
}
