using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar;

public class StellarHammer : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).Tooltip.SetDefault("Throws Stellar Hammers that fall after a while");
		((ModItem)this).DisplayName.SetDefault("Stellar Hammer");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 60;
		((ModItem)this).item.melee = true;
		((Entity)(object)((ModItem)this).item).width = 42;
		((Entity)(object)((ModItem)this).item).height = 42;
		((ModItem)this).item.useTime = 45;
		((ModItem)this).item.useAnimation = 50;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 8f;
		((ModItem)this).item.noUseGraphic = true;
		((ModItem)this).item.value = Item.buyPrice(0, 35, 45);
		((ModItem)this).item.rare = 5;
		((ModItem)this).item.UseSound = SoundID.Item1;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("StellarHammer");
		((ModItem)this).item.shootSpeed = 9f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "StellarBar", 10);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
