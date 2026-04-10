using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class DemoniteJavelin : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Demonite Javelin");
		((ModItem)this).Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 18;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 58;
		((Entity)(object)((ModItem)this).item).height = 56;
		((ModItem)this).item.useTime = 23;
		((ModItem)this).item.useAnimation = 23;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.value = Item.buyPrice(0, 12);
		((ModItem)this).item.rare = 1;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("DemoniteJavelin");
		((ModItem)this).item.shootSpeed = 8f;
		((ModItem)this).item.useTurn = true;
		((ModItem)this).item.maxStack = 1;
		((ModItem)this).item.noUseGraphic = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(57, 10);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
