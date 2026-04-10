using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class HallowedJavelin : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Holy Javelin");
		((ModItem)this).Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 50;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 62;
		((Entity)(object)((ModItem)this).item).height = 60;
		((ModItem)this).item.useTime = 28;
		((ModItem)this).item.useAnimation = 28;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.value = Item.buyPrice(0, 45);
		((ModItem)this).item.rare = 5;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("HallowedJavelin");
		((ModItem)this).item.shootSpeed = 11.5f;
		((ModItem)this).item.useTurn = true;
		((ModItem)this).item.maxStack = 1;
		((ModItem)this).item.noUseGraphic = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(1225, 12);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
