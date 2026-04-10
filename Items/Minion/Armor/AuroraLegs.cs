using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Minion.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class AuroraLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Aurora Crystal Greaves");
		((ModItem)this).Tooltip.SetDefault("2% increased movement speed");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 22;
		((Entity)(object)((ModItem)this).item).height = 16;
		((ModItem)this).item.value = 10000;
		((ModItem)this).item.rare = 1;
		((ModItem)this).item.defense = 2;
	}

	public override void UpdateEquip(Player player)
	{
		player.moveSpeed += 2f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "AuroraBar", 5);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
