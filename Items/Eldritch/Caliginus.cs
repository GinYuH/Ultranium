using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch;

public class Caliginus : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Caliginus");
		//Tooltip.SetDefault("Summons a mini Erebus to fight with you");
	}

	public override void SetDefaults()
	{
		Item.damage = 270;
		Item.DamageType = DamageClass.Summon;
		Item.mana = 25;
		Item.width = 16;
		Item.height = 14;
		Item.useTime = 20;
		Item.useAnimation = 20;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.noMelee = true;
		Item.knockBack = 3f;
		Item.value = Item.buyPrice(1, 50);
		Item.rare = ItemRarityID.Purple;
		Item.UseSound = SoundID.Item44;
		Item.shoot = Mod.Find<ModProjectile>("SmolErebusHead").Type;
		Item.shootSpeed = 10f;
		Item.buffType = Mod.Find<ModBuff>("ErebusBuff").Type;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "NightmareScale", 8);
		val.AddIngredient(null, "NightmareBar", 12);
		val.AddIngredient(null, "DarkMatter", 10);
		val.AddTile(TileID.LunarCraftingStation);
		val.Register();
	}

	public override bool CanUseItem(Player player)
	{
		float num = 1f;
		float num2 = 0f;
		for (int i = 0; i < 1000; i++)
		{
			Projectile projectile = Main.projectile[i];
			if (((Entity)projectile).active && projectile.minion && projectile.owner == player.whoAmI)
			{
				num2 += projectile.minionSlots;
				if (num2 + num > (float)player.maxMinions)
				{
					return false;
				}
			}
		}
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		damage = Item.damage;
		int whoAmI = player.whoAmI;
		float shootSpeed = Item.shootSpeed;
		player.itemTime = Item.useTime;
		Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
		Vector2 value = Vector2.UnitX.RotatedBy(player.fullRotation);
		Vector2 value2 = Main.MouseWorld - vector;
		float num = (float)Main.mouseX + Main.screenPosition.X - vector.X;
		float num2 = (float)Main.mouseY + Main.screenPosition.Y - vector.Y;
		if (player.gravDir == -1f)
		{
			num2 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector.Y;
		}
		float num3 = (float)Math.Sqrt(num * num + num2 * num2);
		if ((float.IsNaN(num) && float.IsNaN(num2)) || (num == 0f && num2 == 0f))
		{
			num = player.direction;
			num2 = 0f;
			num3 = shootSpeed;
		}
		else
		{
			num3 = shootSpeed / num3;
		}
		num *= num3;
		num2 *= num3;
		int num4 = -1;
		int num5 = -1;
		int num6 = Mod.Find<ModProjectile>("SmolErebusHead").Type;
		int num7 = Mod.Find<ModProjectile>("SmolErebusTail").Type;
		for (int i = 0; i < 1000; i++)
		{
			if (((Entity)Main.projectile[i]).active && Main.projectile[i].owner == whoAmI)
			{
				if (num4 == -1 && Main.projectile[i].type == num6)
				{
					num4 = i;
				}
				else if (num5 == -1 && Main.projectile[i].type == num7)
				{
					num5 = i;
				}
				if (num4 != -1 && num5 != -1)
				{
					break;
				}
			}
		}
		if (num4 == -1 && num5 == -1)
		{
			if (Vector2.Dot(value, value2) > 0f)
			{
				player.ChangeDir(1);
			}
			else
			{
				player.ChangeDir(-1);
			}
			num = 0f;
			num2 = 0f;
			vector.X = (float)Main.mouseX + Main.screenPosition.X;
			vector.Y = (float)Main.mouseY + Main.screenPosition.Y;
			int num8 = Projectile.NewProjectile(source, vector.X, vector.Y, num, num2, Mod.Find<ModProjectile>("SmolErebusHead").Type, damage, knockback, whoAmI, 0f, 0f);
			int num9 = num8;
			num9 = num8;
			num8 = Projectile.NewProjectile(source, vector.X, vector.Y, num, num2, Mod.Find<ModProjectile>("SmolErebusBody").Type, damage, knockback, whoAmI, (float)num9, 0f);
			Main.projectile[num9].localAI[1] = num8;
			Main.projectile[num9].netUpdate = true;
			num9 = num8;
			num8 = Projectile.NewProjectile(source, vector.X, vector.Y, num, num2, Mod.Find<ModProjectile>("SmolErebusTail").Type, damage, knockback, whoAmI, (float)num9, 0f);
			Main.projectile[num9].localAI[1] = num8;
			Main.projectile[num9].netUpdate = true;
		}
		else if (num4 != -1 && num5 != -1)
		{
			int num10 = Projectile.NewProjectile(source, vector.X, vector.Y, num, num2, Mod.Find<ModProjectile>("SmolErebusBody").Type, damage, knockback, whoAmI, Main.projectile[num5].ai[0], 0f);
			int num11 = Projectile.NewProjectile(source, vector.X, vector.Y, num, num2, Mod.Find<ModProjectile>("SmolErebusBody").Type, damage, knockback, whoAmI, (float)num10, 0f);
			Main.projectile[num10].localAI[1] = num11;
			Main.projectile[num10].ai[1] = 1f;
			Main.projectile[num10].netUpdate = true;
			Main.projectile[num5].ai[0] = num11;
			Main.projectile[num5].netUpdate = true;
			Main.projectile[num5].ai[1] = 1f;
		}
		return false;
	}
}
