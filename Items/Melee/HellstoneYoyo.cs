using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Melee;

public class HellstoneYoyo : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Fiery Throw");
		// Tooltip.SetDefault("Shoots out fireballs");
	}

	public override void SetDefaults()
	{
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
		Item.shoot = Mod.Find<ModProjectile>("Inferno").Type;
		Item.shootSpeed = 16f;
		Item.knockBack = 2.5f;
		Item.damage = 25;
		Item.value = Item.buyPrice(0, 1);
		Item.rare = 3;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(175, 10);
		val.AddTile(16);
		val.Register();
	}
}
