using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ice;

public class GlacialWand : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Glacial Wand");
		// ((ModItem)this).Tooltip.SetDefault("Casts a slow moving ice twister");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 25;
		((ModItem)this).Item.DamageType = DamageClass.Magic;
		((ModItem)this).Item.mana = 20;
		((Entity)(object)((ModItem)this).Item).width = 32;
		((Entity)(object)((ModItem)this).Item).height = 32;
		((ModItem)this).Item.useTime = 45;
		((ModItem)this).Item.useAnimation = 45;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.knockBack = 2f;
		((ModItem)this).Item.value = Item.buyPrice(0, 20);
		((ModItem)this).Item.rare = 3;
		((ModItem)this).Item.UseSound = SoundID.Item20;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("SnowTwister").Type;
		((ModItem)this).Item.shootSpeed = 6.5f;
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
