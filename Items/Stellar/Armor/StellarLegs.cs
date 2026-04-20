using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar.Armor;

[AutoloadEquip(EquipType.Legs)]
public class StellarLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Stellar Stone Leggings");
		//Tooltip.SetDefault("6% increased damage and 15% increased movement speed\n10% increased melee speed\n+5 max life and mana\n+1 max minions");
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.buyPrice(1, 50);
		Item.rare = ItemRarityID.Pink;
		Item.defense = 15;
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
		player.moveSpeed += 0.15f;
		player.GetAttackSpeed(DamageClass.Melee) *= 1.1f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "StellarBar", 12);
		val.AddTile(TileID.MythrilAnvil);
		val.Register();
	}
}
