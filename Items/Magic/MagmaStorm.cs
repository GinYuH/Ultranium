using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Magic;

public class MagmaStorm : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).Tooltip.SetDefault("Shoots magma bolts that burn enemies");
		// ((ModItem)this).DisplayName.SetDefault("Magma Storm");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 38;
		((ModItem)this).Item.DamageType = DamageClass.Magic;
		((ModItem)this).Item.mana = 7;
		((Entity)(object)((ModItem)this).Item).width = 40;
		((Entity)(object)((ModItem)this).Item).height = 40;
		((ModItem)this).Item.useTime = 22;
		((ModItem)this).Item.useAnimation = 22;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.knockBack = 5f;
		((ModItem)this).Item.value = Item.buyPrice(0, 10);
		((ModItem)this).Item.rare = 4;
		((ModItem)this).Item.UseSound = SoundID.Item20;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.scale = 0.8f;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("MagmaBolt").Type;
		((ModItem)this).Item.shootSpeed = 15f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(531, 1);
		val.AddIngredient(2701, 5);
		val.AddTile(134);
		val.Register();
	}
}
