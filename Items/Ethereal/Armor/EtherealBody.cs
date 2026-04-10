using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ethereal.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class EtherealBody : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Occultist Chestmail");
		((ModItem)this).Tooltip.SetDefault("12% increased damage and 6% increased critical strike chance\n+5 max life and mana\n+2 max minions");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 18;
		((Entity)(object)((ModItem)this).item).height = 18;
		((ModItem)this).item.value = Item.buyPrice(0, 30);
		((ModItem)this).item.rare = 9;
		((ModItem)this).item.defense = 17;
	}

	public override void UpdateEquip(Player player)
	{
		player.statLifeMax2 += 5;
		player.statManaMax2 += 5;
		player.maxMinions += 2;
		player.meleeDamage += 0.12f;
		player.rangedDamage += 0.12f;
		player.magicDamage += 0.12f;
		player.minionDamage += 0.12f;
		player.magicCrit += 6;
		player.meleeCrit += 6;
		player.rangedCrit += 6;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "XenanisFlesh", 10);
		val.AddIngredient((Mod)null, "ShadowFlame", 6);
		val.AddIngredient(225, 13);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
