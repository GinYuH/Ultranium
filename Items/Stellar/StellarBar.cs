using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar;

public class StellarBar : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Stellar Bar");
		// ((ModItem)this).Tooltip.SetDefault("It emnates cold, spacial energy");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 24;
		((Entity)(object)((ModItem)this).Item).height = 24;
		((ModItem)this).Item.value = Item.buyPrice(0, 0, 80);
		((ModItem)this).Item.rare = 5;
		((ModItem)this).Item.maxStack = 99;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 3);
		val.AddIngredient(117, 3);
		val.AddIngredient((Mod)null, "StellarDust", 6);
		val.AddTile(17);
		val.Register();
	}
}
