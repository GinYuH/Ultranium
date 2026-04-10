using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread;

public class DreadStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Fear's Flame");
		((ModItem)this).Tooltip.SetDefault("Casts dread fire balls");
		Item.staff[((ModItem)this).item.type] = true;
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 50;
		((ModItem)this).item.magic = true;
		((ModItem)this).item.mana = 12;
		((Entity)(object)((ModItem)this).item).width = 58;
		((Entity)(object)((ModItem)this).item).height = 56;
		((ModItem)this).item.useTime = 16;
		((ModItem)this).item.useAnimation = 16;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.knockBack = 5f;
		((ModItem)this).item.rare = 4;
		((ModItem)this).item.value = Item.buyPrice(0, 12);
		((ModItem)this).item.UseSound = SoundID.Item20;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("DreadFlameBall");
		((ModItem)this).item.shootSpeed = 6f;
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
