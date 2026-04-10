using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians;

public class BrokenIgnodiumSummon : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Ancient Hell Relic");
		// ((ModItem)this).Tooltip.SetDefault("An ancient artifact of hell\nIncreases melee and ranged damage by 7%\n+10 max life");
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
		player.statLifeMax2 += 10;
		player.GetDamage(DamageClass.Melee) += 0.07f;
		player.GetDamage(DamageClass.Ranged) += 0.07f;
	}
}
