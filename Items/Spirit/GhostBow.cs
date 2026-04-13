using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Spirit;

public class GhostBow : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Phantom Shot");
		Tooltip.SetDefault("Shoots a spread of phantom arrows that can phase through walls");
	}

	public override void SetDefaults()
	{
		Item.damage = 50;
		Item.noMelee = true;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 44;
		Item.height = 58;
		Item.useTime = 21;
		Item.useAnimation = 21;
		Item.useStyle = 5;
		Item.knockBack = 1f;
		Item.value = Item.buyPrice(0, 55, 50);
		Item.rare = 8;
		Item.UseSound = SoundID.Item5;
		Item.autoReuse = true;
		Item.shootSpeed = 15f;
		Item.shoot = 1;
		Item.useAmmo = AmmoID.Arrow;
		Item.shootSpeed = 10f;
		Item.alpha = 60;
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

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2[] array = randomSpread(velocity.X, velocity.Y, 8, 3);
		for (int i = 0; i < 3; i++)
		{
			Projectile.NewProjectile(source, position.X, position.Y, array[i].X, array[i].Y, Mod.Find<ModProjectile>("GhostArrow").Type, 50, 1f, player.whoAmI, 0f, 0f);
		}
		return false;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(3261, 12);
		val.AddTile(134);
		val.Register();
	}
}
