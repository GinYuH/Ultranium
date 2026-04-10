using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Melee;

public class HellstoneYoyo : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Fiery Throw");
		// ((ModItem)this).Tooltip.SetDefault("Shoots out fireballs");
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
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("Inferno").Type;
		((ModItem)this).Item.shootSpeed = 16f;
		((ModItem)this).Item.knockBack = 2.5f;
		((ModItem)this).Item.damage = 25;
		((ModItem)this).Item.value = Item.buyPrice(0, 1);
		((ModItem)this).Item.rare = 3;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(175, 10);
		val.AddTile(16);
		val.Register();
	}
}
