using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Spirit;

public class GhostBow : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Phantom Shot");
		((ModItem)this).Tooltip.SetDefault("Shoots a spread of phantom arrows that can phase through walls");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 50;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 44;
		((Entity)(object)((ModItem)this).item).height = 58;
		((ModItem)this).item.useTime = 21;
		((ModItem)this).item.useAnimation = 21;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.knockBack = 1f;
		((ModItem)this).item.value = Item.buyPrice(0, 55, 50);
		((ModItem)this).item.rare = 8;
		((ModItem)this).item.UseSound = SoundID.Item5;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shootSpeed = 15f;
		((ModItem)this).item.shoot = 1;
		((ModItem)this).item.useAmmo = AmmoID.Arrow;
		((ModItem)this).item.shootSpeed = 10f;
		((ModItem)this).item.alpha = 60;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-7f, 0f);
	}

	public static Vector2[] randomSpread(float speedX, float speedY, int angle, int num)
	{
		Vector2[] array = new Vector2[num];
		float num2 = (float)((double)angle * 0.0874532925);
		float num3 = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
		double num4 = Math.Atan2(speedX, speedY);
		for (int i = 0; i < num; i++)
		{
			double num5 = num4 + (double)((Main.rand.NextFloat() - 0.5f) * num2);
			array[i] = new Vector2(num3 * (float)Math.Sin(num5), num3 * (float)Math.Cos(num5));
		}
		return array;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		Vector2[] array = randomSpread(speedX, speedY, 8, 3);
		for (int i = 0; i < 3; i++)
		{
			Projectile.NewProjectile(position.X, position.Y, array[i].X, array[i].Y, ((ModItem)this).mod.ProjectileType("GhostArrow"), 50, 1f, player.whoAmI, 0f, 0f);
		}
		return false;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(3261, 12);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
