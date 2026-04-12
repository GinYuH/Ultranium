using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Pumpkin;

public class Pumpkibomb : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Pumpki-Bomb");
		Tooltip.SetDefault("A throwable, explosive pumpkin");
	}

	public override void SetDefaults()
	{
		Item.damage = 12;
		Item.DamageType = DamageClass.Ranged;
		((Entity)(object)Item).width = 20;
		((Entity)(object)Item).height = 20;
		Item.useTime = 35;
		Item.useAnimation = 35;
		Item.useStyle = 1;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 0, 50);
		Item.rare = 1;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("Pumpkibomb").Type;
		Item.shootSpeed = 5f;
		Item.useTurn = true;
		Item.noUseGraphic = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(1725, 10);
		val.AddRecipeGroup(RecipeGroupID.Wood, 20);
		val.AddTile(18);
		val.Register();
	}
}
