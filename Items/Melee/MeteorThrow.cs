using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Melee;

public class MeteorThrow : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("The Asteroid");
		// Tooltip.SetDefault("Set enemies ablaze on contact");
	}

	public override void SetDefaults()
	{
		Item.damage = 24;
		Item.useStyle = 5;
		Item.width = 30;
		Item.height = 26;
		Item.noUseGraphic = true;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.noMelee = true;
		Item.channel = true;
		Item.UseSound = SoundID.Item1;
		Item.useAnimation = 25;
		Item.useTime = 25;
		Item.shoot = Mod.Find<ModProjectile>("MeteorThrow").Type;
		Item.shootSpeed = 16f;
		Item.knockBack = 2.5f;
		Item.value = Item.buyPrice(0, 2);
		Item.rare = 2;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(117, 8);
		val.AddTile(16);
		val.Register();
	}
}
