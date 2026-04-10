using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Magic;

public class FeatherTome : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Featherfall");
		((ModItem)this).Tooltip.SetDefault("Casts magical feathers down from the sky");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 14;
		((ModItem)this).item.magic = true;
		((ModItem)this).item.mana = 8;
		((Entity)(object)((ModItem)this).item).width = 28;
		((Entity)(object)((ModItem)this).item).height = 32;
		((ModItem)this).item.useTime = 25;
		((ModItem)this).item.useAnimation = 25;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.knockBack = 5f;
		((ModItem)this).item.rare = 2;
		((ModItem)this).item.UseSound = SoundID.Item9;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.scale = 0.8f;
		((ModItem)this).item.value = Item.buyPrice(0, 2);
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("Feather");
		((ModItem)this).item.shootSpeed = 10f;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		int num = 2;
		for (int i = 0; i < num; i++)
		{
			Vector2 vector = new Vector2((float)((double)player.position.X + (double)player.width * 0.5 + (double)(Main.rand.Next(201) * -player.direction) + ((double)Main.mouseX + (double)Main.screenPosition.X - (double)player.position.X)), (float)((double)player.position.Y + (double)player.height * 0.5 - 600.0));
			vector.X = (float)(((double)vector.X + (double)player.Center.X) / 2.0) + (float)Main.rand.Next(-200, 201);
			vector.Y -= 100 * i;
			float num2 = (float)Main.mouseX + Main.screenPosition.X - vector.X;
			float num3 = (float)Main.mouseY + Main.screenPosition.Y - vector.Y;
			if ((double)num3 < 0.0)
			{
				num3 *= -1f;
			}
			if ((double)num3 < 20.0)
			{
				num3 = 20f;
			}
			float num4 = (float)Math.Sqrt((double)num2 * (double)num2 + (double)num3 * (double)num3);
			float num5 = ((ModItem)this).item.shootSpeed / num4;
			float num6 = num2 * num5;
			float num7 = num3 * num5;
			float num8 = num6 + (float)Main.rand.Next(-40, 41) * 0.02f;
			float num9 = num7 + (float)Main.rand.Next(-40, 41) * 0.02f;
			Projectile.NewProjectile(vector.X, vector.Y, num8, num9, type, damage, knockBack, Main.myPlayer, 0f, (float)Main.rand.Next(5));
		}
		return false;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(320, 12);
		val.AddIngredient(149, 1);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
