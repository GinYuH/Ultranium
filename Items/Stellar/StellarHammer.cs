using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar;

public class StellarHammer : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).Tooltip.SetDefault("Throws Stellar Hammers that fall after a while");
		// ((ModItem)this).DisplayName.SetDefault("Stellar Hammer");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 60;
		((ModItem)this).Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		((Entity)(object)((ModItem)this).Item).width = 42;
		((Entity)(object)((ModItem)this).Item).height = 42;
		((ModItem)this).Item.useTime = 45;
		((ModItem)this).Item.useAnimation = 50;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.knockBack = 8f;
		((ModItem)this).Item.noUseGraphic = true;
		((ModItem)this).Item.value = Item.buyPrice(0, 35, 45);
		((ModItem)this).Item.rare = 5;
		((ModItem)this).Item.UseSound = SoundID.Item1;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("StellarHammer").Type;
		((ModItem)this).Item.shootSpeed = 9f;
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
