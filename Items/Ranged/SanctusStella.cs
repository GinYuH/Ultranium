using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class SanctusStella : ModItem
{
	public override void SetDefaults()
	{
		Item.damage = 53;
		Item.DamageType = DamageClass.Ranged;
		((Entity)(object)Item).width = 28;
		((Entity)(object)Item).height = 28;
		Item.useTime = 23;
		Item.useAnimation = 23;
		Item.useStyle = 1;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 35, 45);
		Item.rare = 5;
		Item.autoReuse = true;
		Item.UseSound = SoundID.Item1;
		Item.shoot = Mod.Find<ModProjectile>("SkyStar").Type;
		Item.shootSpeed = 16f;
		Item.useTurn = true;
		Item.maxStack = 1;
		Item.noUseGraphic = true;
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
