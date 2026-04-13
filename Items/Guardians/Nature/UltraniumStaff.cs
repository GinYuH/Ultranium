using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Nature;

public class UltraniumStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Ultranium Energy Scepter");
		Tooltip.SetDefault("Fires a barrage of Ultranium blasts");
	}

	public override void SetDefaults()
	{
		Item.damage = 140;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 12;
		Item.width = 16;
		Item.height = 14;
		Item.useTime = 12;
		Item.useAnimation = 12;
		Item.useStyle = 5;
		Item.staff[Item.type] = true;
		Item.noMelee = true;
		Item.knockBack = 2f;
		Item.rare = 11;
		Item.value = Item.buyPrice(1);
		Item.UseSound = SoundID.DD2_BetsysWrathShot;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("UltraniumEnergyBolt").Type;
		Item.shootSpeed = 10f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(241, 166, 0);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		for (int i = 0; i < 3; i++)
		{
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			int myPlayer = Main.myPlayer;
			float shootSpeed = Item.shootSpeed;
			int num = damage;
			float num2 = knockback;
			float x = (float)Main.mouseX + Main.screenPosition.X - vector.X;
			float y = (float)Main.mouseY + Main.screenPosition.Y - vector.Y;
			float f = Main.rand.NextFloat() * ((float)Math.PI * 2f);
			float value = 20f;
			float value2 = 60f;
			Vector2 vector2 = vector + f.ToRotationVector2() * MathHelper.Lerp(value, value2, Main.rand.NextFloat());
			for (int j = 0; j < 50; j++)
			{
				vector2 = vector + f.ToRotationVector2() * MathHelper.Lerp(value, value2, Main.rand.NextFloat());
				if (Collision.CanHit(vector, 0, 0, vector2 + (vector2 - vector).SafeNormalize(Vector2.UnitX) * 8f, 0, 0))
				{
					break;
				}
				f = Main.rand.NextFloat() * ((float)Math.PI * 2f);
			}
			Vector2 v = Main.MouseWorld - vector2;
			Vector2 vector3 = new Vector2(x, y).SafeNormalize(Vector2.UnitY) * shootSpeed;
			v = v.SafeNormalize(vector3) * shootSpeed;
			v = Vector2.Lerp(v, vector3, 0.25f);
			Projectile.NewProjectile(source, vector2, v, Mod.Find<ModProjectile>("UltraniumEnergyBolt").Type, num, num2, myPlayer, 0f, 0f);
		}
		return false;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "UltrumShard", 10);
		val.AddTile(412);
		val.Register();
	}
}
