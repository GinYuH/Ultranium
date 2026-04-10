using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class NightsJavelin : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Night's Javelin");
		((ModItem)this).Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 30;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 70;
		((Entity)(object)((ModItem)this).item).height = 62;
		((ModItem)this).item.useTime = 28;
		((ModItem)this).item.useAnimation = 28;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.value = Item.buyPrice(0, 35);
		((ModItem)this).item.rare = 3;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("NightsJavelin");
		((ModItem)this).item.shootSpeed = 10.5f;
		((ModItem)this).item.useTurn = true;
		((ModItem)this).item.maxStack = 1;
		((ModItem)this).item.noUseGraphic = true;
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
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddRecipeGroup("Ultranium:DemoniteJavelin/CrimtanePike", 1);
		val.AddIngredient((Mod)null, "NatureJavelin", 1);
		val.AddIngredient((Mod)null, "WaterJavelin", 1);
		val.AddIngredient((Mod)null, "InfernoJavelin", 1);
		val.AddTile(26);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
