using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dye;

public class DepthsDye : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Depths Fog Dye");
	}

	public override void SetDefaults()
	{
		Item.maxStack = Item.CommonMaxStack;
		Item.rare = ItemRarityID.Orange;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(ItemID.SilverDye, 1);
		val.AddIngredient(null, "DepthGlowstoneItem", 2);
		val.AddTile(TileID.DyeVat);
		val.Register();
	}
}
