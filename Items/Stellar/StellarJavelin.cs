using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar;

public class StellarJavelin : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Stellar Javelin");
		// Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		Item.damage = 45;
		Item.DamageType = DamageClass.Ranged;
		((Entity)(object)Item).width = 20;
		((Entity)(object)Item).height = 20;
		Item.useTime = 28;
		Item.useAnimation = 28;
		Item.useStyle = 1;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 45);
		Item.rare = 5;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("StellarJavelin").Type;
		Item.shootSpeed = 11.5f;
		Item.useTurn = true;
		Item.maxStack = 1;
		Item.noUseGraphic = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "StellarBar", 10);
		val.AddTile(134);
		val.Register();
	}
}
