using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Town.Shrooms;

public class SolarShroom : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Piltzintecuhtli");
		// ((ModItem)this).Tooltip.SetDefault("The god of the sun that the lizhard's worshiped was a mushroom all along. how dreadful.");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 20;
		((Entity)(object)((ModItem)this).Item).height = 30;
		((ModItem)this).Item.rare = -11;
		((ModItem)this).Item.maxStack = 1;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "TempleShroom", 1);
		val.AddTile(17);
		val.Register();
	}
}
