using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Furniture;

public class ShadowLanternItem : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Eldritch Lantern");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 20;
		((Entity)(object)((ModItem)this).item).height = 11;
		((ModItem)this).item.maxStack = 999;
		((ModItem)this).item.rare = 0;
		((ModItem)this).item.useTurn = true;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.useAnimation = 15;
		((ModItem)this).item.useTime = 10;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.consumable = true;
		((ModItem)this).item.createTile = ((ModItem)this).mod.TileType("ShadowLantern");
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "ShadowWood", 6);
		val.AddIngredient(8, 1);
		val.AddTile(18);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
