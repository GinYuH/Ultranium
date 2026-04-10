using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Guardians.Hell;

public class Ignodium : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Ignodium");
		Main.projFrames[((ModProjectile)this).Projectile.type] = 4;
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 4;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
		ProjectileID.Sets.MinionSacrificable[((ModProjectile)this).Projectile.type] = true;
		ProjectileID.Sets.CultistIsResistantTo[((ModProjectile)this).Projectile.type] = true;
		ProjectileID.Sets.MinionTargettingFeature[((ModProjectile)this).Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 32;
		((ModProjectile)this).Projectile.height = 32;
		Main.projPet[((ModProjectile)this).Projectile.type] = true;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.minion = true;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.aiStyle = -1;
		((ModProjectile)this).Projectile.timeLeft = 18000;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.netImportant = true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		((ModProjectile)this).Projectile.frameCounter++;
		if (((ModProjectile)this).Projectile.frameCounter >= 9)
		{
			((ModProjectile)this).Projectile.frame++;
			((ModProjectile)this).Projectile.frameCounter = 0;
			if (((ModProjectile)this).Projectile.frame >= 4)
			{
				((ModProjectile)this).Projectile.frame = 0;
			}
		}
		Texture2D texture2D = TextureAssets.Projectile[((ModProjectile)this).Projectile.type].Value;
		Vector2 vector = new Vector2((float)texture2D.Width * 0.5f, (float)((ModProjectile)this).Projectile.height * 0.5f);
		SpriteEffects effects = ((((ModProjectile)this).Projectile.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
		for (int i = 0; i < ((ModProjectile)this).Projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).Projectile.gfxOffY);
			Color color = ((ModProjectile)this).Projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).Projectile.oldPos.Length - i) / (float)((ModProjectile)this).Projectile.oldPos.Length);
			Rectangle value = new Rectangle(0, texture2D.Height / Main.projFrames[((ModProjectile)this).Projectile.type] * ((ModProjectile)this).Projectile.frame, texture2D.Width, texture2D.Height / Main.projFrames[((ModProjectile)this).Projectile.type]);
			sb.Draw(texture2D, position, value, color, ((ModProjectile)this).Projectile.rotation, vector, ((ModProjectile)this).Projectile.scale, effects, 0f);
		}
		return true;
	}

	public override void AI()
	{
		bool num = ((ModProjectile)this).Projectile.type == ((ModProjectile)this).Mod.Find<ModProjectile>("Ignodium").Type;
		Player player = Main.player[((ModProjectile)this).Projectile.owner];
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (num)
		{
			if (player.dead)
			{
				modPlayer.IgnodiumMinion = false;
			}
			if (modPlayer.IgnodiumMinion)
			{
				((ModProjectile)this).Projectile.timeLeft = 2;
			}
		}
		float num2 = (float)((ModProjectile)this).Projectile.width * 1.1f;
		for (int i = 0; i < 1000; i++)
		{
			Projectile projectile = Main.projectile[i];
			if (i != projectile.whoAmI && ((Entity)projectile).active && ((ModProjectile)this).Projectile.owner == projectile.owner && ((ModProjectile)this).Projectile.type == projectile.type && Math.Abs(((ModProjectile)this).Projectile.position.X - projectile.position.X) + Math.Abs(((ModProjectile)this).Projectile.position.Y - projectile.position.Y) < num2)
			{
				if (((ModProjectile)this).Projectile.position.X < Main.projectile[i].position.X)
				{
					((ModProjectile)this).Projectile.velocity.X -= 0.08f;
				}
				else
				{
					((ModProjectile)this).Projectile.velocity.X += 0.08f;
				}
				if (((ModProjectile)this).Projectile.position.Y < Main.projectile[i].position.Y)
				{
					((ModProjectile)this).Projectile.velocity.Y -= 0.08f;
				}
				else
				{
					((ModProjectile)this).Projectile.velocity.Y += 0.08f;
				}
			}
		}
		Vector2 vector = ((ModProjectile)this).Projectile.position;
		float num3 = 500f;
		bool flag = false;
		((ModProjectile)this).Projectile.tileCollide = true;
		for (int j = 0; j < 200; j++)
		{
			NPC nPC = Main.npc[j];
			if (nPC.CanBeChasedBy(this))
			{
				float num4 = Vector2.Distance(nPC.Center, ((ModProjectile)this).Projectile.Center);
				if ((num4 < num3 || !flag) && Collision.CanHitLine(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, nPC.position, nPC.width, nPC.height))
				{
					num3 = num4;
					vector = nPC.Center;
					flag = true;
				}
			}
		}
		if (Vector2.Distance(player.Center, ((ModProjectile)this).Projectile.Center) > (flag ? 1000f : 500f))
		{
			((ModProjectile)this).Projectile.ai[0] = 1f;
			((ModProjectile)this).Projectile.netUpdate = true;
		}
		if (((ModProjectile)this).Projectile.ai[0] == 1f)
		{
			((ModProjectile)this).Projectile.tileCollide = false;
		}
		if (flag && ((ModProjectile)this).Projectile.ai[0] == 0f)
		{
			Vector2 vector2 = vector - ((ModProjectile)this).Projectile.Center;
			if (vector2.Length() > 200f)
			{
				vector2.Normalize();
				((ModProjectile)this).Projectile.velocity = (((ModProjectile)this).Projectile.velocity * 20f + vector2 * 6f) / 21f;
			}
			else
			{
				((ModProjectile)this).Projectile.velocity *= (float)Math.Pow(0.97, 2.0);
			}
		}
		else
		{
			if (!Collision.CanHitLine(((ModProjectile)this).Projectile.Center, 1, 1, player.Center, 1, 1))
			{
				((ModProjectile)this).Projectile.ai[0] = 1f;
			}
			float num5 = 6f;
			if (((ModProjectile)this).Projectile.ai[0] == 1f)
			{
				num5 = 15f;
			}
			Vector2 center = ((ModProjectile)this).Projectile.Center;
			Vector2 vector3 = player.Center - center;
			((ModProjectile)this).Projectile.ai[1] = 3600f;
			((ModProjectile)this).Projectile.netUpdate = true;
			int num6 = 1;
			for (int k = 0; k < ((ModProjectile)this).Projectile.whoAmI; k++)
			{
				if (((Entity)Main.projectile[k]).active && Main.projectile[k].owner == ((ModProjectile)this).Projectile.owner && Main.projectile[k].type == ((ModProjectile)this).Projectile.type)
				{
					num6++;
				}
			}
			vector3.X += (10 + num6 * 40) * player.direction;
			vector3.Y -= 70f;
			float num7 = vector3.Length();
			if (num7 > 200f && num5 < 9f)
			{
				num5 = 9f;
			}
			if (num7 < 100f && ((ModProjectile)this).Projectile.ai[0] == 1f && !Collision.SolidCollision(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height))
			{
				((ModProjectile)this).Projectile.ai[0] = 0f;
				((ModProjectile)this).Projectile.netUpdate = true;
			}
			if (num7 > 2000f)
			{
				((ModProjectile)this).Projectile.Center = player.Center;
			}
			if (num7 > 48f)
			{
				vector3.Normalize();
				vector3 *= num5;
				float num8 = 10f;
				((ModProjectile)this).Projectile.velocity = (((ModProjectile)this).Projectile.velocity * num8 + vector3) / (num8 + 1f);
			}
			else
			{
				((ModProjectile)this).Projectile.direction = Main.player[((ModProjectile)this).Projectile.owner].direction;
				((ModProjectile)this).Projectile.velocity *= (float)Math.Pow(0.9, 2.0);
			}
		}
		((ModProjectile)this).Projectile.rotation = ((ModProjectile)this).Projectile.velocity.X * 0.05f;
		if (((ModProjectile)this).Projectile.velocity.X > 0f)
		{
			((ModProjectile)this).Projectile.spriteDirection = (((ModProjectile)this).Projectile.direction = -1);
		}
		else if (((ModProjectile)this).Projectile.velocity.X < 0f)
		{
			((ModProjectile)this).Projectile.spriteDirection = (((ModProjectile)this).Projectile.direction = 1);
		}
		if (((ModProjectile)this).Projectile.ai[1] > 0f)
		{
			((ModProjectile)this).Projectile.ai[1] += 1f;
		}
		if (((ModProjectile)this).Projectile.ai[1] > 45f)
		{
			((ModProjectile)this).Projectile.ai[1] = 0f;
			((ModProjectile)this).Projectile.netUpdate = true;
		}
		if (!(((ModProjectile)this).Projectile.ai[0] == 0f && flag))
		{
			return;
		}
		if ((vector - ((ModProjectile)this).Projectile.Center).X > 0f)
		{
			((ModProjectile)this).Projectile.spriteDirection = (((ModProjectile)this).Projectile.direction = -1);
		}
		else if ((vector - ((ModProjectile)this).Projectile.Center).X < 0f)
		{
			((ModProjectile)this).Projectile.spriteDirection = (((ModProjectile)this).Projectile.direction = 1);
		}
		if (((ModProjectile)this).Projectile.ai[1] != 0f)
		{
			return;
		}
		((ModProjectile)this).Projectile.ai[1] = 1f;
		if (Main.myPlayer == ((ModProjectile)this).Projectile.owner)
		{
			Vector2 vector4 = vector - ((ModProjectile)this).Projectile.Center;
			if (vector4 == Vector2.Zero)
			{
				vector4 = new Vector2(0f, 1f);
			}
			vector4.Normalize();
			vector4 *= 8.5f;
			int num9 = Projectile.NewProjectile(((ModProjectile)this).Projectile.Center.X, ((ModProjectile)this).Projectile.Center.Y, vector4.X, vector4.Y, ((ModProjectile)this).Mod.Find<ModProjectile>("HellBeam").Type, ((ModProjectile)this).Projectile.damage, ((ModProjectile)this).Projectile.knockBack, Main.myPlayer, 0f, 0f);
			Main.projectile[num9].timeLeft = 300;
			Main.projectile[num9].penetrate = 1;
			Main.projectile[num9].netUpdate = true;
			Main.projectile[num9].minion = true;
			((ModProjectile)this).Projectile.netUpdate = true;
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}
}
