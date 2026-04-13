using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar;

public class StellarBar : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Stellar Bar");
		//Tooltip.SetDefault("It emnates cold, spacial energy");
	}

	public override void SetDefaults()
	{
		Item.width = 24;
		Item.height = 24;
		Item.value = Item.buyPrice(0, 0, 80);
		Item.rare = ItemRarityID.Pink;
		Item.maxStack = Item.CommonMaxStack;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 3);
		val.AddIngredient(ItemID.MeteoriteBar, 3);
		val.AddIngredient(null, "StellarDust", 6);
		val.AddTile(TileID.Furnaces);
		val.Register();
	}
}
