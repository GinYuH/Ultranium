using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ice;

public class GlacialFlail : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Glacial Flail");
		// ((ModItem)this).Tooltip.SetDefault("The flail shoots icy bolts at nearby enemies");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 30;
		((Entity)(object)((ModItem)this).Item).height = 10;
		((ModItem)this).Item.rare = 3;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.UseSound = SoundID.Item1;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.useAnimation = 40;
		((ModItem)this).Item.useTime = 40;
		((ModItem)this).Item.knockBack = 7.5f;
		((ModItem)this).Item.damage = 35;
		((ModItem)this).Item.noUseGraphic = true;
		((ModItem)this).Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		((ModItem)this).Item.channel = true;
		((ModItem)this).Item.value = Item.buyPrice(0, 20);
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("GlacialFlail").Type;
		((ModItem)this).Item.shootSpeed = 15f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(664, 10);
		val.AddIngredient((Mod)null, "IcePelt", 7);
		val.AddTile(16);
		val.Register();
	}
}
