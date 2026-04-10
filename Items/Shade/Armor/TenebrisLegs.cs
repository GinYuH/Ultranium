using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class TenebrisLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Tenebris Greaves");
		((ModItem)this).Tooltip.SetDefault("2% increased damage\n5% increased movement speed");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 22;
		((Entity)(object)((ModItem)this).item).height = 16;
		((ModItem)this).item.value = Item.buyPrice(0, 2, 50);
		((ModItem)this).item.rare = 1;
		((ModItem)this).item.defense = 4;
	}

	public override void UpdateEquip(Player player)
	{
		player.meleeDamage += 0.02f;
		player.rangedDamage += 0.02f;
		player.minionDamage += 0.02f;
		player.magicDamage += 0.02f;
		player.moveSpeed += 0.5f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "NightmareBar", 7);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
