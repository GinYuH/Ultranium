using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class SanctusStella : ModItem
{
	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 53;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 28;
		((Entity)(object)((ModItem)this).item).height = 28;
		((ModItem)this).item.useTime = 23;
		((ModItem)this).item.useAnimation = 23;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.value = Item.buyPrice(0, 35, 45);
		((ModItem)this).item.rare = 5;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.UseSound = SoundID.Item1;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("SkyStar");
		((ModItem)this).item.shootSpeed = 16f;
		((ModItem)this).item.useTurn = true;
		((ModItem)this).item.maxStack = 1;
		((ModItem)this).item.noUseGraphic = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(575, 5);
		val.AddIngredient(1225, 6);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
