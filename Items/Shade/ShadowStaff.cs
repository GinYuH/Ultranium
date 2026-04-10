using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade;

public class ShadowStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).Tooltip.SetDefault("Casts a shadow bolt");
		Item.staff[((ModItem)this).item.type] = true;
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 22;
		((ModItem)this).item.magic = true;
		((ModItem)this).item.mana = 5;
		((Entity)(object)((ModItem)this).item).width = 40;
		((Entity)(object)((ModItem)this).item).height = 40;
		((ModItem)this).item.useTime = 30;
		((ModItem)this).item.useAnimation = 30;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.knockBack = 5f;
		((ModItem)this).item.value = Item.buyPrice(0, 2, 50);
		((ModItem)this).item.rare = 1;
		((ModItem)this).item.UseSound = SoundID.Item20;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("ShadowStaffBolt");
		((ModItem)this).item.shootSpeed = 5.5f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "NightmareBar", 12);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
