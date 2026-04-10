using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class StellarLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Stellar Stone Leggings");
		((ModItem)this).Tooltip.SetDefault("6% increased damage and 15% increased movement speed\n10% increased melee speed\n+5 max life and mana\n+1 max minions");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 18;
		((Entity)(object)((ModItem)this).item).height = 18;
		((ModItem)this).item.value = Item.buyPrice(1, 50);
		((ModItem)this).item.rare = 5;
		((ModItem)this).item.defense = 15;
	}

	public override void UpdateEquip(Player player)
	{
		player.statLifeMax2 += 5;
		player.statManaMax2 += 5;
		player.maxMinions++;
		player.meleeDamage += 0.06f;
		player.rangedDamage += 0.06f;
		player.magicDamage += 0.06f;
		player.minionDamage += 0.06f;
		player.moveSpeed += 15f;
		player.meleeSpeed *= 1.1f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "StellarBar", 12);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
