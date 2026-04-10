using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Pumpkin;

public class Pumpkibomb : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Pumpki-Bomb");
		((ModItem)this).Tooltip.SetDefault("A throwable, explosive pumpkin");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 12;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 20;
		((Entity)(object)((ModItem)this).item).height = 20;
		((ModItem)this).item.useTime = 35;
		((ModItem)this).item.useAnimation = 35;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.value = Item.buyPrice(0, 0, 50);
		((ModItem)this).item.rare = 1;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("Pumpkibomb");
		((ModItem)this).item.shootSpeed = 5f;
		((ModItem)this).item.useTurn = true;
		((ModItem)this).item.noUseGraphic = true;
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
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(1725, 10);
		val.AddIngredient(9, 20);
		((Recipe)val).anyWood = true;
		val.AddTile(18);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
