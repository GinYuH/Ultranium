using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Melee;

public class HellstoneYoyo : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Fiery Throw");
		((ModItem)this).Tooltip.SetDefault("Shoots out fireballs");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.useStyle = 5;
		((Entity)(object)((ModItem)this).item).width = 30;
		((Entity)(object)((ModItem)this).item).height = 26;
		((ModItem)this).item.noUseGraphic = true;
		((ModItem)this).item.melee = true;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.channel = true;
		((ModItem)this).item.UseSound = SoundID.Item1;
		((ModItem)this).item.useAnimation = 25;
		((ModItem)this).item.useTime = 25;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("Inferno");
		((ModItem)this).item.shootSpeed = 16f;
		((ModItem)this).item.knockBack = 2.5f;
		((ModItem)this).item.damage = 25;
		((ModItem)this).item.value = Item.buyPrice(0, 1);
		((ModItem)this).item.rare = 3;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(175, 10);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
