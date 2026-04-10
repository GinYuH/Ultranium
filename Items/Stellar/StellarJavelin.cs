using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar;

public class StellarJavelin : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Stellar Javelin");
		// ((ModItem)this).Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 45;
		((ModItem)this).Item.DamageType = DamageClass.Ranged;
		((Entity)(object)((ModItem)this).Item).width = 20;
		((Entity)(object)((ModItem)this).Item).height = 20;
		((ModItem)this).Item.useTime = 28;
		((ModItem)this).Item.useAnimation = 28;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.knockBack = 6f;
		((ModItem)this).Item.value = Item.buyPrice(0, 45);
		((ModItem)this).Item.rare = 5;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("StellarJavelin").Type;
		((ModItem)this).Item.shootSpeed = 11.5f;
		((ModItem)this).Item.useTurn = true;
		((ModItem)this).Item.maxStack = 1;
		((ModItem)this).Item.noUseGraphic = true;
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
