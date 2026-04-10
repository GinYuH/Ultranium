using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Dye;

public class IgnodiumDye : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Hell Dye");
	}

	public override void SetDefaults()
	{
		Item.maxStack = 99;
		Item.rare = 3;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(1037, 1);
		val.AddIngredient((Mod)null, "HellShard", 1);
		val.AddTile(228);
		val.Register();
	}
}
