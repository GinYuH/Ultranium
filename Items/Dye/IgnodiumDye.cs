using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dye;

public class IgnodiumDye : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Hell Dye");
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
		val.AddIngredient(null, "HellShard", 1);
		val.AddTile(TileID.DyeVat);
		val.Register();
	}
}
