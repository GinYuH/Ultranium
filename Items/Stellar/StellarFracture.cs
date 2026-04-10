using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar;

public class StellarFracture : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Stellar Fracture");
		((ModItem)this).Tooltip.SetDefault("Fires a spread of stellar beams");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 40;
		((ModItem)this).item.magic = true;
		((Entity)(object)((ModItem)this).item).width = 16;
		((Entity)(object)((ModItem)this).item).height = 14;
		((ModItem)this).item.useTime = 30;
		((ModItem)this).item.useAnimation = 30;
		((ModItem)this).item.useStyle = 5;
		Item.staff[((ModItem)this).item.type] = true;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.knockBack = 2f;
		((ModItem)this).item.value = Item.buyPrice(0, 35, 45);
		((ModItem)this).item.rare = 5;
		((ModItem)this).item.mana = 12;
		((ModItem)this).item.UseSound = SoundID.DD2_BetsysWrathShot;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("StellarFracture");
		((ModItem)this).item.shootSpeed = 10f;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		for (int i = 0; i < 4; i++)
		{
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			int myPlayer = Main.myPlayer;
			float shootSpeed = ((ModItem)this).item.shootSpeed;
			int num = damage;
			float num2 = knockBack;
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
			Projectile.NewProjectile(vector2, v, ((ModItem)this).mod.ProjectileType("StellarFracture"), num, num2, myPlayer, 0f, 0f);
		}
		return false;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "StellarBar", 10);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
