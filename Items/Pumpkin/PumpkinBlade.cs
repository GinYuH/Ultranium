using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Pumpkin;

public class PumpkinBlade : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Pumpkin Buster");
		((ModItem)this).Tooltip.SetDefault("Fires pumpkin seeds on swing");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 12;
		((ModItem)this).item.melee = true;
		((Entity)(object)((ModItem)this).item).width = 40;
		((Entity)(object)((ModItem)this).item).height = 40;
		((ModItem)this).item.useTime = 30;
		((ModItem)this).item.useAnimation = 30;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.value = Item.buyPrice(0, 0, 50);
		((ModItem)this).item.rare = 1;
		((ModItem)this).item.UseSound = SoundID.Item1;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("PumpkinSeed");
		((ModItem)this).item.shootSpeed = 5f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(1725, 10);
		val.AddIngredient(9, 20);
		((Recipe)val).anyWood = true;
		val.AddTile(18);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
