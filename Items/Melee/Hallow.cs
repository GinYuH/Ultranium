using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Melee;

public class Hallow : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Chaos Blade");
		((ModItem)this).Tooltip.SetDefault("Shoots out a chaos star");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 42;
		((ModItem)this).item.melee = true;
		((Entity)(object)((ModItem)this).item).width = 54;
		((Entity)(object)((ModItem)this).item).height = 54;
		((ModItem)this).item.useTime = 24;
		((ModItem)this).item.useAnimation = 24;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.value = Item.buyPrice(0, 30);
		((ModItem)this).item.rare = 5;
		((ModItem)this).item.UseSound = SoundID.Item60;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("BlueStar");
		((ModItem)this).item.shootSpeed = 8f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(65, 1);
		val.AddIngredient(520, 20);
		val.AddIngredient(502, 15);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
