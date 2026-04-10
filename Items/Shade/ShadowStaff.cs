using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade;

public class ShadowStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).Tooltip.SetDefault("Casts a shadow bolt");
		Item.staff[((ModItem)this).Item.type] = true;
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 22;
		((ModItem)this).Item.DamageType = DamageClass.Magic;
		((ModItem)this).Item.mana = 5;
		((Entity)(object)((ModItem)this).Item).width = 40;
		((Entity)(object)((ModItem)this).Item).height = 40;
		((ModItem)this).Item.useTime = 30;
		((ModItem)this).Item.useAnimation = 30;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.knockBack = 5f;
		((ModItem)this).Item.value = Item.buyPrice(0, 2, 50);
		((ModItem)this).Item.rare = 1;
		((ModItem)this).Item.UseSound = SoundID.Item20;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("ShadowStaffBolt").Type;
		((ModItem)this).Item.shootSpeed = 5.5f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "NightmareBar", 12);
		val.AddTile(16);
		val.Register();
	}
}
