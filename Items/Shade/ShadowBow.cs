using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade;

public class ShadowBow : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Shadow Bow");
	}

	public override void SetDefaults()
	{
		Item.damage = 20;
		Item.DamageType = DamageClass.Ranged;
		((Entity)(object)Item).width = 40;
		((Entity)(object)Item).height = 40;
		Item.useTime = 20;
		Item.useAnimation = 20;
		Item.useStyle = 5;
		Item.knockBack = 6f;
		Item.rare = 1;
		Item.value = Item.buyPrice(0, 8);
		Item.UseSound = SoundID.Item5;
		Item.autoReuse = true;
		Item.shoot = 10;
		Item.shootSpeed = 6.5f;
		Item.useAmmo = AmmoID.Arrow;
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
