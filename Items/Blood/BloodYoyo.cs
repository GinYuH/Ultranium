using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Blood;

public class BloodYoyo : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("The Gout");
		// ((ModItem)this).Tooltip.SetDefault("Randomly fires out lingering blood swirls");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 13;
		((ModItem)this).Item.knockBack = 2.5f;
		((ModItem)this).Item.rare = 2;
		((ModItem)this).Item.useStyle = 5;
		((Entity)(object)((ModItem)this).Item).width = 24;
		((Entity)(object)((ModItem)this).Item).height = 22;
		((ModItem)this).Item.noUseGraphic = true;
		((ModItem)this).Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.channel = true;
		((ModItem)this).Item.value = Item.buyPrice(0, 1, 35);
		((ModItem)this).Item.UseSound = SoundID.Item1;
		((ModItem)this).Item.useAnimation = 25;
		((ModItem)this).Item.useTime = 25;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("TheGout").Type;
		((ModItem)this).Item.shootSpeed = 16f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "BloodClot", 12);
		val.AddRecipeGroup("Ultranium:Silver/Tungsten", 8);
		val.AddTile(16);
		val.Register();
	}
}
