using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class DemoniteJavelin : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Demonite Javelin");
		Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		Item.damage = 18;
		Item.DamageType = DamageClass.Ranged;
		((Entity)(object)Item).width = 58;
		((Entity)(object)Item).height = 56;
		Item.useTime = 23;
		Item.useAnimation = 23;
		Item.useStyle = 1;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 12);
		Item.rare = 1;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("DemoniteJavelin").Type;
		Item.shootSpeed = 8f;
		Item.useTurn = true;
		Item.maxStack = 1;
		Item.noUseGraphic = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(57, 10);
		val.AddTile(16);
		val.Register();
	}
}
