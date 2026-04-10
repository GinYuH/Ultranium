using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Town.Shrooms;

public class SolarShroom : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Piltzintecuhtli");
		// Tooltip.SetDefault("The god of the sun that the lizhard's worshiped was a mushroom all along. how dreadful.");
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 30;
		Item.rare = -11;
		Item.maxStack = 1;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "TempleShroom", 1);
		val.AddTile(17);
		val.Register();
	}
}
