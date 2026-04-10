using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Melee;

public class RetiYoyo : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Retina Throw");
		// ((ModItem)this).Tooltip.SetDefault("Fires Lasers at nearby enemies");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.useStyle = 5;
		((Entity)(object)((ModItem)this).Item).width = 30;
		((Entity)(object)((ModItem)this).Item).height = 26;
		((ModItem)this).Item.noUseGraphic = true;
		((ModItem)this).Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.channel = true;
		((ModItem)this).Item.UseSound = SoundID.Item1;
		((ModItem)this).Item.useAnimation = 25;
		((ModItem)this).Item.useTime = 25;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("RetiYoyo").Type;
		((ModItem)this).Item.shootSpeed = 16f;
		((ModItem)this).Item.knockBack = 2.5f;
		((ModItem)this).Item.damage = 49;
		((ModItem)this).Item.value = Item.buyPrice(0, 30);
		((ModItem)this).Item.rare = 5;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(1225, 10);
		val.AddIngredient(549, 5);
		val.AddTile(134);
		val.Register();
	}
}
