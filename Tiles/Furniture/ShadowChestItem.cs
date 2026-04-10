using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Furniture;

public class ShadowChestItem : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Eldritch Chest");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 26;
		((Entity)(object)((ModItem)this).item).height = 22;
		((ModItem)this).item.maxStack = 99;
		((ModItem)this).item.useTurn = true;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.useAnimation = 15;
		((ModItem)this).item.useTime = 10;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.consumable = true;
		((ModItem)this).item.value = 500;
		((ModItem)this).item.createTile = ModContent.TileType<ShadowChest>();
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "ShadowWood", 8);
		val.AddRecipeGroup("Ultranium:Iron/Lead", 2);
		val.AddTile(18);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
