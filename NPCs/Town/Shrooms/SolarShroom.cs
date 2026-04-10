using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Town.Shrooms;

public class SolarShroom : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Piltzintecuhtli");
		((ModItem)this).Tooltip.SetDefault("The god of the sun that the lizhard's worshiped was a mushroom all along. how dreadful.");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 20;
		((Entity)(object)((ModItem)this).item).height = 30;
		((ModItem)this).item.rare = -11;
		((ModItem)this).item.maxStack = 1;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "TempleShroom", 1);
		val.AddTile(17);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
