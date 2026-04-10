using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread;

public class DreadSword : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Blade of Terror");
		((ModItem)this).Tooltip.SetDefault("Fires dread bolts on swing");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 48;
		((ModItem)this).item.scale = 1f;
		((ModItem)this).item.melee = true;
		((Entity)(object)((ModItem)this).item).width = 80;
		((Entity)(object)((ModItem)this).item).height = 80;
		((ModItem)this).item.useTime = 26;
		((ModItem)this).item.useAnimation = 26;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.value = Item.buyPrice(0, 12);
		((ModItem)this).item.rare = 4;
		((ModItem)this).item.UseSound = SoundID.Item1;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("DreadFire");
		((ModItem)this).item.shootSpeed = 5f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "DreadFlame", 10);
		val.AddIngredient((Mod)null, "DreadScale", 5);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
