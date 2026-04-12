using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Blood;

public class BloodStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Aortic Staff");
		Tooltip.SetDefault("Casts balls of blood");
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.damage = 15;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 6;
		Item.width = 40;
		Item.height = 40;
		Item.useTime = 35;
		Item.useAnimation = 35;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 5f;
		Item.value = Item.buyPrice(0, 1, 35);
		Item.rare = 2;
		Item.autoReuse = true;
		Item.UseSound = SoundID.Item21;
		Item.shoot = Mod.Find<ModProjectile>("BloodBall").Type;
		Item.shootSpeed = 9f;
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
