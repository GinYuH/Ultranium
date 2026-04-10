using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class NatureJavelin : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Jungle's Wrath");
		((ModItem)this).Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 18;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 54;
		((Entity)(object)((ModItem)this).item).height = 56;
		((ModItem)this).item.useTime = 28;
		((ModItem)this).item.useAnimation = 28;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.value = Item.buyPrice(0, 20);
		((ModItem)this).item.rare = 2;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("NatureJavelin");
		((ModItem)this).item.shootSpeed = 9f;
		((ModItem)this).item.useTurn = true;
		((ModItem)this).item.maxStack = 1;
		((ModItem)this).item.noUseGraphic = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(331, 12);
		val.AddIngredient(209, 12);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
