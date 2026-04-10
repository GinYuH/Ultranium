using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Blood;

public class BloodYoyo : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("The Gout");
		// Tooltip.SetDefault("Randomly fires out lingering blood swirls");
	}

	public override void SetDefaults()
	{
		Item.damage = 13;
		Item.knockBack = 2.5f;
		Item.rare = 2;
		Item.useStyle = 5;
		Item.width = 24;
		Item.height = 22;
		Item.noUseGraphic = true;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.noMelee = true;
		Item.channel = true;
		Item.value = Item.buyPrice(0, 1, 35);
		Item.UseSound = SoundID.Item1;
		Item.useAnimation = 25;
		Item.useTime = 25;
		Item.shoot = Mod.Find<ModProjectile>("TheGout").Type;
		Item.shootSpeed = 16f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "BloodClot", 12);
		val.AddRecipeGroup("Ultranium:Silver/Tungsten", 8);
		val.AddTile(16);
		val.Register();
	}
}
