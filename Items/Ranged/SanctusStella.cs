using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class SanctusStella : ModItem
{
	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 53;
		((ModItem)this).Item.DamageType = DamageClass.Ranged;
		((Entity)(object)((ModItem)this).Item).width = 28;
		((Entity)(object)((ModItem)this).Item).height = 28;
		((ModItem)this).Item.useTime = 23;
		((ModItem)this).Item.useAnimation = 23;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.knockBack = 6f;
		((ModItem)this).Item.value = Item.buyPrice(0, 35, 45);
		((ModItem)this).Item.rare = 5;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.UseSound = SoundID.Item1;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("SkyStar").Type;
		((ModItem)this).Item.shootSpeed = 16f;
		((ModItem)this).Item.useTurn = true;
		((ModItem)this).Item.maxStack = 1;
		((ModItem)this).Item.noUseGraphic = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(575, 5);
		val.AddIngredient(1225, 6);
		val.AddTile(134);
		val.Register();
	}
}
