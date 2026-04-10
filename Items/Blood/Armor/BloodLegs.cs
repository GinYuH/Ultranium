using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Blood.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class BloodLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Sanguine Leggings");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 18;
		((Entity)(object)((ModItem)this).item).height = 18;
		((ModItem)this).item.value = Item.buyPrice(0, 1, 35);
		((ModItem)this).item.rare = 2;
		((ModItem)this).item.defense = 4;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "BloodClot", 15);
		val.AddRecipeGroup("Ultranium:Silver/Tungsten", 12);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
