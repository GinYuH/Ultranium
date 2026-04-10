using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Minion.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class AuroraLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Aurora Crystal Greaves");
		// ((ModItem)this).Tooltip.SetDefault("2% increased movement speed");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 22;
		((Entity)(object)((ModItem)this).Item).height = 16;
		((ModItem)this).Item.value = 10000;
		((ModItem)this).Item.rare = 1;
		((ModItem)this).Item.defense = 2;
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
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "AuroraBar", 5);
		val.AddTile(16);
		val.Register();
	}
}
