using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Blood.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class BloodLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Sanguine Leggings");
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.buyPrice(0, 1, 35);
		Item.rare = 2;
		Item.defense = 4;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "BloodClot", 15);
		val.AddRecipeGroup("Ultranium:Silver/Tungsten", 12);
		val.AddTile(16);
		val.Register();
	}
}
