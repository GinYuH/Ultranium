using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread;

public class DreadSword : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Blade of Terror");
		// ((ModItem)this).Tooltip.SetDefault("Fires dread bolts on swing");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 48;
		((ModItem)this).Item.scale = 1f;
		((ModItem)this).Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		((Entity)(object)((ModItem)this).Item).width = 80;
		((Entity)(object)((ModItem)this).Item).height = 80;
		((ModItem)this).Item.useTime = 26;
		((ModItem)this).Item.useAnimation = 26;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.knockBack = 6f;
		((ModItem)this).Item.value = Item.buyPrice(0, 12);
		((ModItem)this).Item.rare = 4;
		((ModItem)this).Item.UseSound = SoundID.Item1;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("DreadFire").Type;
		((ModItem)this).Item.shootSpeed = 5f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "DreadFlame", 10);
		val.AddIngredient((Mod)null, "DreadScale", 5);
		val.AddTile(134);
		val.Register();
	}
}
