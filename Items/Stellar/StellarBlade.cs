using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar;

public class StellarBlade : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Stellar Blade");
		//Tooltip.SetDefault("Shoots Stellar Stars");
	}

	public override void SetDefaults()
	{
		Item.damage = 52;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		((Entity)(object)Item).width = 48;
		((Entity)(object)Item).height = 48;
		Item.useTime = 22;
		Item.useAnimation = 22;
		Item.useStyle = 1;
		Item.knockBack = 8f;
		Item.value = Item.buyPrice(0, 35, 45);
		Item.rare = 5;
		Item.UseSound = SoundID.Item71;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("StellarStar").Type;
		Item.shootSpeed = 15f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "StellarBar", 10);
		val.AddTile(134);
		val.Register();
	}
}
