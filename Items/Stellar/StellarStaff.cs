using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar;

public class StellarStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Stellar Wand");
		((ModItem)this).Tooltip.SetDefault("Fires a Stellar swirl bolt");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 40;
		((ModItem)this).item.magic = true;
		((Entity)(object)((ModItem)this).item).width = 16;
		((Entity)(object)((ModItem)this).item).height = 14;
		((ModItem)this).item.useTime = 20;
		((ModItem)this).item.useAnimation = 20;
		((ModItem)this).item.useStyle = 5;
		Item.staff[((ModItem)this).item.type] = true;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.knockBack = 2f;
		((ModItem)this).item.value = Item.buyPrice(0, 45);
		((ModItem)this).item.rare = 5;
		((ModItem)this).item.mana = 12;
		((ModItem)this).item.UseSound = SoundID.DD2_BetsysWrathShot;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("StellarBolt");
		((ModItem)this).item.shootSpeed = 10f;
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
