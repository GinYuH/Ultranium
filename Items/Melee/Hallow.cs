using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Melee;

public class Hallow : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Chaos Blade");
		// Tooltip.SetDefault("Shoots out a chaos star");
	}

	public override void SetDefaults()
	{
		Item.damage = 42;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.width = 54;
		Item.height = 54;
		Item.useTime = 24;
		Item.useAnimation = 24;
		Item.useStyle = 1;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 30);
		Item.rare = 5;
		Item.UseSound = SoundID.Item60;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("BlueStar").Type;
		Item.shootSpeed = 8f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(65, 1);
		val.AddIngredient(520, 20);
		val.AddIngredient(502, 15);
		val.AddTile(134);
		val.Register();
	}
}
