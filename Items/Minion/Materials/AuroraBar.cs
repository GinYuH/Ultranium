using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Minion.Materials;

public class AuroraBar : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Aurora Crystal Bar");
		((ModItem)this).Tooltip.SetDefault("Its glow is almost blinding to look at");
	}

	public override void SetDefaults()
	{
		Item item = new Item();
		((Entity)(object)((ModItem)this).item).width = ((Entity)(object)item).width;
		((Entity)(object)((ModItem)this).item).height = ((Entity)(object)item).height;
		((ModItem)this).item.maxStack = 999;
		((ModItem)this).item.value = 1000;
		((ModItem)this).item.rare = 2;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "AuroraOreItem", 4);
		val.AddTile(17);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
