using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Cosmic;

public class CosmicBlade : ModItem
{
	private int Use;

	private Color[] itemNameCycleColors = new Color[2]
	{
		new Color(93, 215, 195),
		new Color(72, 37, 169)
	};

	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Wrath of the Cosmos");
		Tooltip.SetDefault("Fires down a stream of cosmic stars\nEvery 30 swings will send down a bigger cosmic star that explodes into smaller stars");
	}

	public override void SetDefaults()
	{
		Item.scale = 1.2f;
		Item.damage = 360;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.width = 28;
		Item.height = 32;
		Item.useTime = 17;
		Item.useAnimation = 17;
		Item.useStyle = 1;
		Item.knockBack = 5f;
		Item.value = Item.buyPrice(2);
		Item.rare = 11;
		Item.UseSound = SoundID.Item9;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("CosmicSwordStar").Type;
		Item.shootSpeed = 30f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		foreach (TooltipLine tooltip in tooltips)
		{
			if (tooltip.Mod == "Terraria" && tooltip.Name == "ItemName")
			{
				float amount = (float)(Main.GameUpdateCount % 60) / 60f;
				int num = (int)(Main.GameUpdateCount / 60 % 2);
				tooltip.OverrideColor = Color.Lerp(itemNameCycleColors[num], itemNameCycleColors[(num + 1) % 2], amount);
			}
		}
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Use++;
		if (Use < 30)
		{
			int num = Main.rand.Next(3, 5);
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
				float num5 = Item.shootSpeed / num4;
				float num6 = num2 * num5;
				float num7 = num3 * num5;
				float num8 = num6 + (float)Main.rand.Next(-40, 41) * 0.02f;
				float num9 = num7 + (float)Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(null, vector.X, vector.Y, num8, num9, type, damage, knockback, Main.myPlayer, 0f, (float)Main.rand.Next(5));
			}
		}
		if (Use >= 30)
		{
			int num10 = 1;
			for (int j = 0; j < num10; j++)
			{
				Vector2 vector2 = new Vector2((float)((double)player.position.X + (double)player.width * 0.5 + (double)(Main.rand.Next(201) * -player.direction) + ((double)Main.mouseX + (double)Main.screenPosition.X - (double)player.position.X)), (float)((double)player.position.Y + (double)player.height * 0.5 - 600.0));
				vector2.X = (float)(((double)vector2.X + (double)player.Center.X) / 2.0) + (float)Main.rand.Next(-200, 201);
				vector2.Y -= 100 * j;
				float num11 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
				float num12 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
				if ((double)num12 < 0.0)
				{
					num12 *= -1f;
				}
				if ((double)num12 < 20.0)
				{
					num12 = 20f;
				}
				float num13 = (float)Math.Sqrt((double)num11 * (double)num11 + (double)num12 * (double)num12);
				float num14 = Item.shootSpeed / num13;
				float num15 = num11 * num14;
				float num16 = num12 * num14;
				float num17 = num15 + (float)Main.rand.Next(-40, 41) * 0.02f;
				float num18 = num16 + (float)Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(null, vector2.X, vector2.Y, num17, num18, Mod.Find<ModProjectile>("GiantSwordStar").Type, damage, knockback, Main.myPlayer, 0f, (float)Main.rand.Next(5));
			}
			Use = 0;
		}
		return false;
	}
}
