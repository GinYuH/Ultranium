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
		//DisplayName.SetDefault("Ignodium");
		Main.projFrames[Projectile.type] = 4;
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
		ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		Projectile.width = 32;
		Projectile.height = 32;
		Main.projPet[Projectile.type] = true;
		Projectile.friendly = true;
		Projectile.minion = true;
		Projectile.penetrate = -1;
		Projectile.aiStyle = -1;
		Projectile.timeLeft = 18000;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.netImportant = true;
        Projectile.DamageType = DamageClass.Summon;
    }

	public override bool PreDraw(ref Color lightColor)
	{
		Projectile.frameCounter++;
		if (Projectile.frameCounter >= 9)
		{
			Projectile.frame++;
			Projectile.frameCounter = 0;
			if (Projectile.frame >= 4)
			{
				Projectile.frame = 0;
			}
		}
		Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
		Vector2 vector = new Vector2((float)texture2D.Width * 0.5f, (float)Projectile.height * 0.5f);
		SpriteEffects effects = ((Projectile.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 position = Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, Projectile.gfxOffY);
			Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
			Rectangle value = new Rectangle(0, texture2D.Height / Main.projFrames[Projectile.type] * Projectile.frame, texture2D.Width, texture2D.Height / Main.projFrames[Projectile.type]);
			Main.spriteBatch.Draw(texture2D, position, value, color, Projectile.rotation, vector, Projectile.scale, effects, 0f);
		}
		return true;
	}

	public override void AI()
	{
		bool num = Projectile.type == Mod.Find<ModProjectile>("Ignodium").Type;
		Player player = Main.player[Projectile.owner];
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (num)
		{
			if (player.dead)
			{
				modPlayer.IgnodiumMinion = false;
			}
			if (modPlayer.IgnodiumMinion)
			{
				Projectile.timeLeft = 2;
			}
		}
		float num2 = (float)Projectile.width * 1.1f;
		for (int i = 0; i < 1000; i++)
		{
			Projectile projectile = Main.projectile[i];
			if (i != projectile.whoAmI && ((Entity)projectile).active && Projectile.owner == projectile.owner && Projectile.type == projectile.type && Math.Abs(Projectile.position.X - projectile.position.X) + Math.Abs(Projectile.position.Y - projectile.position.Y) < num2)
			{
				if (Projectile.position.X < Main.projectile[i].position.X)
				{
					Projectile.velocity.X -= 0.08f;
				}
				else
				{
					Projectile.velocity.X += 0.08f;
				}
				if (Projectile.position.Y < Main.projectile[i].position.Y)
				{
					Projectile.velocity.Y -= 0.08f;
				}
				else
				{
					Projectile.velocity.Y += 0.08f;
				}
			}
		}
		Vector2 vector = Projectile.position;
		float num3 = 500f;
		bool flag = false;
		Projectile.tileCollide = true;
		for (int j = 0; j < 200; j++)
		{
			NPC nPC = Main.npc[j];
			if (nPC.CanBeChasedBy(this))
			{
				float num4 = Vector2.Distance(nPC.Center, Projectile.Center);
				if ((num4 < num3 || !flag) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, nPC.position, nPC.width, nPC.height))
				{
					num3 = num4;
					vector = nPC.Center;
					flag = true;
				}
			}
		}
		if (Vector2.Distance(player.Center, Projectile.Center) > (flag ? 1000f : 500f))
		{
			Projectile.ai[0] = 1f;
			Projectile.netUpdate = true;
		}
		if (Projectile.ai[0] == 1f)
		{
			Projectile.tileCollide = false;
		}
		if (flag && Projectile.ai[0] == 0f)
		{
			Vector2 vector2 = vector - Projectile.Center;
			if (vector2.Length() > 200f)
			{
				vector2.Normalize();
				Projectile.velocity = (Projectile.velocity * 20f + vector2 * 6f) / 21f;
			}
			else
			{
				Projectile.velocity *= (float)Math.Pow(0.97, 2.0);
			}
		}
		else
		{
			if (!Collision.CanHitLine(Projectile.Center, 1, 1, player.Center, 1, 1))
			{
				Projectile.ai[0] = 1f;
			}
			float num5 = 6f;
			if (Projectile.ai[0] == 1f)
			{
				num5 = 15f;
			}
			Vector2 center = Projectile.Center;
			Vector2 vector3 = player.Center - center;
			Projectile.ai[1] = 3600f;
			Projectile.netUpdate = true;
			int num6 = 1;
			for (int k = 0; k < Projectile.whoAmI; k++)
			{
				if (((Entity)Main.projectile[k]).active && Main.projectile[k].owner == Projectile.owner && Main.projectile[k].type == Projectile.type)
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
			if (num7 < 100f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
			{
				Projectile.ai[0] = 0f;
				Projectile.netUpdate = true;
			}
			if (num7 > 2000f)
			{
				Projectile.Center = player.Center;
			}
			if (num7 > 48f)
			{
				vector3.Normalize();
				vector3 *= num5;
				float num8 = 10f;
				Projectile.velocity = (Projectile.velocity * num8 + vector3) / (num8 + 1f);
			}
			else
			{
				Projectile.direction = Main.player[Projectile.owner].direction;
				Projectile.velocity *= (float)Math.Pow(0.9, 2.0);
			}
		}
		Projectile.rotation = Projectile.velocity.X * 0.05f;
		if (Projectile.velocity.X > 0f)
		{
			Projectile.spriteDirection = (Projectile.direction = -1);
		}
		else if (Projectile.velocity.X < 0f)
		{
			Projectile.spriteDirection = (Projectile.direction = 1);
		}
		if (Projectile.ai[1] > 0f)
		{
			Projectile.ai[1] += 1f;
		}
		if (Projectile.ai[1] > 45f)
		{
			Projectile.ai[1] = 0f;
			Projectile.netUpdate = true;
		}
		if (!(Projectile.ai[0] == 0f && flag))
		{
			return;
		}
		if ((vector - Projectile.Center).X > 0f)
		{
			Projectile.spriteDirection = (Projectile.direction = -1);
		}
		else if ((vector - Projectile.Center).X < 0f)
		{
			Projectile.spriteDirection = (Projectile.direction = 1);
		}
		if (Projectile.ai[1] != 0f)
		{
			return;
		}
		Projectile.ai[1] = 1f;
		if (Main.myPlayer == Projectile.owner)
		{
			Vector2 vector4 = vector - Projectile.Center;
			if (vector4 == Vector2.Zero)
			{
				vector4 = new Vector2(0f, 1f);
			}
			vector4.Normalize();
			vector4 *= 8.5f;
			int num9 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, vector4.X, vector4.Y, Mod.Find<ModProjectile>("HellBeam").Type, Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
			Main.projectile[num9].timeLeft = 300;
			Main.projectile[num9].penetrate = 1;
			Main.projectile[num9].netUpdate = true;
			Main.projectile[num9].minion = true;
			Projectile.netUpdate = true;
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}
}
