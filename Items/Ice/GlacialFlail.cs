using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ice;

public class GlacialFlail : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Glacial Flail");
		// Tooltip.SetDefault("The flail shoots icy bolts at nearby enemies");
	}

	public override void SetDefaults()
	{
		Item.width = 30;
		Item.height = 10;
		Item.rare = 3;
		Item.noMelee = true;
		Item.UseSound = SoundID.Item1;
		Item.useStyle = 5;
		Item.useAnimation = 40;
		Item.useTime = 40;
		Item.knockBack = 7.5f;
		Item.damage = 35;
		Item.noUseGraphic = true;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.channel = true;
		Item.value = Item.buyPrice(0, 20);
		Item.shoot = Mod.Find<ModProjectile>("GlacialFlail").Type;
		Item.shootSpeed = 15f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(664, 10);
		val.AddIngredient((Mod)null, "IcePelt", 7);
		val.AddTile(16);
		val.Register();
	}
}
