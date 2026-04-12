using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class NatureJavelin : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Jungle's Wrath");
		Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		Item.damage = 18;
		Item.DamageType = DamageClass.Ranged;
		((Entity)(object)Item).width = 54;
		((Entity)(object)Item).height = 56;
		Item.useTime = 28;
		Item.useAnimation = 28;
		Item.useStyle = 1;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 20);
		Item.rare = 2;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("NatureJavelin").Type;
		Item.shootSpeed = 9f;
		Item.useTurn = true;
		Item.maxStack = 1;
		Item.noUseGraphic = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(331, 12);
		val.AddIngredient(209, 12);
		val.AddTile(16);
		val.Register();
	}
}
