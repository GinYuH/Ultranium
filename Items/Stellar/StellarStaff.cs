using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar;

public class StellarStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Stellar Wand");
		// ((ModItem)this).Tooltip.SetDefault("Fires a Stellar swirl bolt");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 40;
		((ModItem)this).Item.DamageType = DamageClass.Magic;
		((Entity)(object)((ModItem)this).Item).width = 16;
		((Entity)(object)((ModItem)this).Item).height = 14;
		((ModItem)this).Item.useTime = 20;
		((ModItem)this).Item.useAnimation = 20;
		((ModItem)this).Item.useStyle = 5;
		Item.staff[((ModItem)this).Item.type] = true;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.knockBack = 2f;
		((ModItem)this).Item.value = Item.buyPrice(0, 45);
		((ModItem)this).Item.rare = 5;
		((ModItem)this).Item.mana = 12;
		((ModItem)this).Item.UseSound = SoundID.DD2_BetsysWrathShot;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("StellarBolt").Type;
		((ModItem)this).Item.shootSpeed = 10f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "StellarBar", 10);
		val.AddTile(134);
		val.Register();
	}
}
