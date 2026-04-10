using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Spirit;

public class GhostScythe : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Phantom Slicer");
		((ModItem)this).Tooltip.SetDefault("Throws ectoplasmic scythes images");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 60;
		((Entity)(object)((ModItem)this).item).width = 62;
		((Entity)(object)((ModItem)this).item).height = 62;
		((ModItem)this).item.useTime = 20;
		((ModItem)this).item.useAnimation = 20;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.value = Item.buyPrice(0, 55, 50);
		((ModItem)this).item.rare = 8;
		((ModItem)this).item.UseSound = SoundID.Item1;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.melee = true;
		((ModItem)this).item.noUseGraphic = true;
		((ModItem)this).item.alpha = 60;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("SoulScytheProjectile");
		((ModItem)this).item.shootSpeed = 12f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(3261, 12);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
