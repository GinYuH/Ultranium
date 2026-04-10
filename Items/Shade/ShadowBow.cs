using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade;

public class ShadowBow : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Shadow Bow");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 20;
		((ModItem)this).Item.DamageType = DamageClass.Ranged;
		((Entity)(object)((ModItem)this).Item).width = 40;
		((Entity)(object)((ModItem)this).Item).height = 40;
		((ModItem)this).Item.useTime = 20;
		((ModItem)this).Item.useAnimation = 20;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.knockBack = 6f;
		((ModItem)this).Item.rare = 1;
		((ModItem)this).Item.value = Item.buyPrice(0, 8);
		((ModItem)this).Item.UseSound = SoundID.Item5;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = 10;
		((ModItem)this).Item.shootSpeed = 6.5f;
		((ModItem)this).Item.useAmmo = AmmoID.Arrow;
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
