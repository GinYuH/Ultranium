using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags.Acc;

public class GuardianCore : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Guardian's Insignia");
		// ((ModItem)this).Tooltip.SetDefault("15% increased damage and 10% increased critical strike chance\nincreases max life and max mana by 40\n+3 max minions\n20% reduced mana usage");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 32;
		((Entity)(object)((ModItem)this).Item).height = 32;
		((ModItem)this).Item.rare = 12;
		((ModItem)this).Item.value = Item.buyPrice(1);
		((ModItem)this).Item.accessory = true;
		((ModItem)this).Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.statManaMax2 += 40;
		player.statLifeMax2 += 40;
		player.maxMinions += 3;
		player.manaCost += -0.2f;
		player.GetDamage(DamageClass.Summon) += 0.15f;
		player.GetDamage(DamageClass.Throwing) += 0.15f;
		player.GetDamage(DamageClass.Magic) += 0.15f;
		player.GetDamage(DamageClass.Melee) += 0.15f;
		player.GetDamage(DamageClass.Ranged) += 0.15f;
		player.GetCritChance(DamageClass.Throwing) += 10;
		player.GetCritChance(DamageClass.Magic) += 10;
		player.GetCritChance(DamageClass.Melee) += 10;
		player.GetCritChance(DamageClass.Ranged) += 10;
		player.GetAttackSpeed(DamageClass.Melee) += 0.08f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "UltrumRelic", 1);
		val.AddIngredient((Mod)null, "IgnodiumRelic", 1);
		val.AddTile(412);
		val.Register();
	}
}
