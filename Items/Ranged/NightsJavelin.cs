using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class NightsJavelin : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Night's Javelin");
		// Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		Item.damage = 30;
		Item.DamageType = DamageClass.Ranged;
		((Entity)(object)Item).width = 70;
		((Entity)(object)Item).height = 62;
		Item.useTime = 28;
		Item.useAnimation = 28;
		Item.useStyle = 1;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 35);
		Item.rare = 3;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("NightsJavelin").Type;
		Item.shootSpeed = 10.5f;
		Item.useTurn = true;
		Item.maxStack = 1;
		Item.noUseGraphic = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddRecipeGroup("Ultranium:DemoniteJavelin/CrimtanePike", 1);
		val.AddIngredient((Mod)null, "NatureJavelin", 1);
		val.AddIngredient((Mod)null, "WaterJavelin", 1);
		val.AddIngredient((Mod)null, "InfernoJavelin", 1);
		val.AddTile(26);
		val.Register();
	}
}
