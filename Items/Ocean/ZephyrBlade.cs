using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ocean;

public class ZephyrBlade : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).Tooltip.SetDefault("Fires zephyr bubbles on swing\nHas a chance to shoot an ink bubble that inflicts slowness on enemies");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 20;
		((ModItem)this).item.melee = true;
		((Entity)(object)((ModItem)this).item).width = 54;
		((Entity)(object)((ModItem)this).item).height = 54;
		((ModItem)this).item.useTime = 35;
		((ModItem)this).item.useAnimation = 35;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.value = Item.buyPrice(0, 35, 45);
		((ModItem)this).item.rare = 2;
		((ModItem)this).item.UseSound = SoundID.Item1;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("ZephyrBubble");
		((ModItem)this).item.shootSpeed = 3.5f;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		if (Main.rand.Next(5) == 0)
		{
			Vector2 vector = new Vector2(speedX, speedY);
			Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, ((ModItem)this).mod.ProjectileType("ZephyrInkBubble"), damage, knockBack, player.whoAmI, 0f, 0f);
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "OceanScale", 8);
		val.AddIngredient(275, 5);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
