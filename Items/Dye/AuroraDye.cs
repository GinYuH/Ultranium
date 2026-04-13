using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Dye;

public class AuroraDye : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Aurora Crystal Dye");
	}

	public override void SetDefaults()
	{
		Item.maxStack = Item.CommonMaxStack;
		Item.rare = 3;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(1037, 1);
		val.AddIngredient((Mod)null, "AuroraOreItem", 2);
		val.AddTile(228);
		val.Register();
	}
}
