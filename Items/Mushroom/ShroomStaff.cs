using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Mushroom;

public class ShroomStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Fungus Staff");
		((ModItem)this).Tooltip.SetDefault("Fires mushroom spores");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 10;
		((ModItem)this).item.magic = true;
		((Entity)(object)((ModItem)this).item).width = 16;
		((Entity)(object)((ModItem)this).item).height = 14;
		((ModItem)this).item.useTime = 25;
		((ModItem)this).item.useAnimation = 25;
		((ModItem)this).item.useStyle = 5;
		Item.staff[((ModItem)this).item.type] = true;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.knockBack = 2f;
		((ModItem)this).item.value = Item.buyPrice(0, 0, 80);
		((ModItem)this).item.rare = 1;
		((ModItem)this).item.mana = 5;
		((ModItem)this).item.UseSound = SoundID.Item8;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("ShroomSpore");
		((ModItem)this).item.shootSpeed = 2.5f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(183, 10);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
