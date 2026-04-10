using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class SpazBomb : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Spazma-Bomb");
		((ModItem)this).Tooltip.SetDefault("Explodes on death, and inflicts the \"Cursed Inferno\" debuff");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.useStyle = 1;
		((Entity)(object)((ModItem)this).item).width = 30;
		((Entity)(object)((ModItem)this).item).height = 26;
		((ModItem)this).item.noUseGraphic = true;
		((ModItem)this).item.ranged = true;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.UseSound = SoundID.Item1;
		((ModItem)this).item.useAnimation = 30;
		((ModItem)this).item.useTime = 30;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("C4Pro");
		((ModItem)this).item.shootSpeed = 7f;
		((ModItem)this).item.knockBack = 2.5f;
		((ModItem)this).item.damage = 43;
		((ModItem)this).item.value = Item.buyPrice(0, 30);
		((ModItem)this).item.rare = 5;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(1225, 10);
		val.AddIngredient(549, 5);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
