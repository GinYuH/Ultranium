using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Furniture;

internal class DepthSnailCageItem : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Depth Snail Cage");
	}

	public override void SetDefaults()
	{
		Item.CloneDefaults(2175);
		Item.createTile = Mod.Find<ModTile>("DepthSnailCage").Type;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(2208, 1);
		val.AddIngredient((Mod)null, "DepthSnailItem", 1);
		val.Register();
	}
}
