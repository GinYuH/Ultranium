using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar;

public class StellarStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Stellar Wand");
		//Tooltip.SetDefault("Fires a Stellar swirl bolt");
	}

	public override void SetDefaults()
	{
		Item.damage = 40;
		Item.DamageType = DamageClass.Magic;
		Item.width = 16;
		Item.height = 14;
		Item.useTime = 20;
		Item.useAnimation = 20;
		Item.useStyle = 5;
		Item.staff[Item.type] = true;
		Item.noMelee = true;
		Item.knockBack = 2f;
		Item.value = Item.buyPrice(0, 45);
		Item.rare = 5;
		Item.mana = 12;
		Item.UseSound = SoundID.DD2_BetsysWrathShot;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("StellarBolt").Type;
		Item.shootSpeed = 10f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "StellarBar", 10);
		val.AddTile(134);
		val.Register();
	}
}
