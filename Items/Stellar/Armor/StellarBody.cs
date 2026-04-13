using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar.Armor;

[AutoloadEquip(EquipType.Body)]
public class StellarBody : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Stellar Stoneplate");
		//Tooltip.SetDefault("12% increased damage and 6% increased critical strike chance\n+5 max life and mana\n+2 max minions");
	}

	public override void SetDefaults()
	{
		((Entity)(object)Item).width = 18;
		((Entity)(object)Item).height = 18;
		Item.value = Item.buyPrice(1, 50);
		Item.rare = 5;
		Item.defense = 19;
	}

	public override void UpdateEquip(Player player)
	{
		player.statLifeMax2 += 5;
		player.statManaMax2 += 5;
		player.maxMinions += 2;
		player.GetDamage(DamageClass.Melee) += 0.12f;
		player.GetDamage(DamageClass.Ranged) += 0.12f;
		player.GetDamage(DamageClass.Magic) += 0.12f;
		player.GetDamage(DamageClass.Summon) += 0.12f;
		player.GetCritChance(DamageClass.Magic) += 6;
		player.GetCritChance(DamageClass.Melee) += 6;
		player.GetCritChance(DamageClass.Ranged) += 6;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "StellarBar", 17);
		val.AddTile(134);
		val.Register();
	}
}
