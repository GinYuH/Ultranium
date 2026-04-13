using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dedicated;

public class PhantomClaw : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Projectile.type] = 4;
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		//DisplayName.SetDefault("Despair Claw");
	}

	public override void SetDefaults()
	{
		Projectile.width = 40;
		Projectile.height = 40;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = false;
		Projectile.alpha = 255;
		Projectile.penetrate = 5;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Projectile.frameCounter++;
		if (Projectile.frameCounter >= 5)
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
		SpriteEffects effects = ((Projectile.direction != 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 position = Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, Projectile.gfxOffY);
			Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
			Rectangle value = new Rectangle(0, texture2D.Height / Main.projFrames[Projectile.type] * Projectile.frame, texture2D.Width, texture2D.Height / Main.projFrames[Projectile.type]);
			Main.spriteBatch.Draw(texture2D, position, value, color, Projectile.rotation, vector, Projectile.scale, effects, 0f);
		}
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[Projectile.owner] = 6;
	}

	public override void AI()
	{
		Projectile.direction = (Projectile.spriteDirection = ((Projectile.velocity.X > 0f) ? 1 : (-1)));
		Projectile.rotation = Projectile.velocity.ToRotation();
		if (Projectile.velocity.Y > 16f)
		{
			Projectile.velocity.Y = 16f;
		}
		if (Projectile.spriteDirection == -1)
		{
			Projectile.rotation += (float)Math.PI;
		}
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] > 7f)
		{
			Projectile.ai[0] = 7f;
			int num = HomeOnTarget();
			if (num != -1)
			{
				NPC nPC = Main.npc[num];
				Vector2 value = Projectile.DirectionTo(nPC.Center) * 25f;
				Projectile.velocity = Vector2.Lerp(Projectile.velocity, value, 0.05f);
			}
		}
	}

	private int HomeOnTarget()
	{
		int num = -1;
		for (int i = 0; i < 200; i++)
		{
			NPC nPC = Main.npc[i];
			if (nPC.CanBeChasedBy(Projectile))
			{
				_ = nPC.wet;
				float num2 = Projectile.Distance(nPC.Center);
				if (num2 <= 400f && (num == -1 || Projectile.Distance(Main.npc[num].Center) > num2))
				{
					num = i;
				}
			}
		}
		return num;
	}

	public override void OnKill(int timeLeft)
	{
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("DevotedDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("DevotedDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("DevotedDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("DevotedDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("DevotedDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("DevotedDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("DevotedDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("DevotedDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("DevotedDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("DevotedDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("DevotedDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("DevotedDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("DevotedDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("DevotedDust").Type);
		Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("DevotedDust").Type);
	}
}
