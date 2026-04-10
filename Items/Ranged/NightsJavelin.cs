using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class NightsJavelin : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Night's Javelin");
		// ((ModItem)this).Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 30;
		((ModItem)this).Item.DamageType = DamageClass.Ranged;
		((Entity)(object)((ModItem)this).Item).width = 70;
		((Entity)(object)((ModItem)this).Item).height = 62;
		((ModItem)this).Item.useTime = 28;
		((ModItem)this).Item.useAnimation = 28;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.knockBack = 6f;
		((ModItem)this).Item.value = Item.buyPrice(0, 35);
		((ModItem)this).Item.rare = 3;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("NightsJavelin").Type;
		((ModItem)this).Item.shootSpeed = 10.5f;
		((ModItem)this).Item.useTurn = true;
		((ModItem)this).Item.maxStack = 1;
		((ModItem)this).Item.noUseGraphic = true;
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
