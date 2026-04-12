using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade;

public class ShadowStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		Tooltip.SetDefault("Casts a shadow bolt");
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.damage = 22;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 5;
		((Entity)(object)Item).width = 40;
		((Entity)(object)Item).height = 40;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 5f;
		Item.value = Item.buyPrice(0, 2, 50);
		Item.rare = 1;
		Item.UseSound = SoundID.Item20;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("ShadowStaffBolt").Type;
		Item.shootSpeed = 5.5f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "NightmareBar", 12);
		val.AddTile(16);
		val.Register();
	}
}
