using Terraria.ModLoader;

namespace Ultranium.Items.Dye;

public class IceDragonDye : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Glacial Dye");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.maxStack = 99;
		((ModItem)this).item.rare = 3;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(1037, 1);
		val.AddIngredient((Mod)null, "IcePelt", 1);
		val.AddTile(228);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
