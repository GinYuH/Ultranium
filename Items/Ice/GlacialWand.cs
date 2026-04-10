using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ice;

public class GlacialWand : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Glacial Wand");
		// Tooltip.SetDefault("Casts a slow moving ice twister");
	}

	public override void SetDefaults()
	{
		Item.damage = 25;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 20;
		Item.width = 32;
		Item.height = 32;
		Item.useTime = 45;
		Item.useAnimation = 45;
		Item.useStyle = 1;
		Item.knockBack = 2f;
		Item.value = Item.buyPrice(0, 20);
		Item.rare = 3;
		Item.UseSound = SoundID.Item20;
		Item.shoot = Mod.Find<ModProjectile>("SnowTwister").Type;
		Item.shootSpeed = 6.5f;
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
