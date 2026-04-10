using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Pumpkin.Eldritch;

public class EldritchPumpkibomb : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Eldritch Pumpki-Bomb");
		((ModItem)this).Tooltip.SetDefault("Throws an eldritch pumpkin bomb that explodes into lingering fire\nThe pumpkin will also explode into tentacles when it hits an enemy");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 45;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 20;
		((Entity)(object)((ModItem)this).item).height = 20;
		((ModItem)this).item.useTime = 38;
		((ModItem)this).item.useAnimation = 38;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.value = Item.buyPrice(0, 10, 50);
		((ModItem)this).item.rare = 4;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("EldritchPumpkibomb");
		((ModItem)this).item.shootSpeed = 6.5f;
		((ModItem)this).item.useTurn = true;
		((ModItem)this).item.noUseGraphic = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "Pumpkibomb", 1);
		val.AddIngredient((Mod)null, "ShadowEssence", 20);
		val.AddIngredient(521, 10);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
