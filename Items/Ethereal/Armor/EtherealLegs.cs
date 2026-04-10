using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ethereal.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class EtherealLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Occultist Leggings");
		// Tooltip.SetDefault("6% increased damage and 15% increased movement speed\n10% increased melee speed\n+5 max life and mana\n+1 max minions");
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.buyPrice(0, 30);
		Item.rare = 9;
		Item.defense = 14;
	}

	public override void UpdateEquip(Player player)
	{
		player.statLifeMax2 += 5;
		player.statManaMax2 += 5;
		player.maxMinions++;
		player.GetDamage(DamageClass.Melee) += 0.06f;
		player.GetDamage(DamageClass.Ranged) += 0.06f;
		player.GetDamage(DamageClass.Magic) += 0.06f;
		player.GetDamage(DamageClass.Summon) += 0.06f;
		player.moveSpeed += 15f;
		player.GetAttackSpeed(DamageClass.Melee) *= 1.1f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "XenanisFlesh", 6);
		val.AddIngredient((Mod)null, "ShadowFlame", 4);
		val.AddIngredient(225, 8);
		val.AddTile(134);
		val.Register();
	}
}
