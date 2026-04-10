using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Guardians.Nature;

public class UltraniumKunai : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.Homing[((ModProjectile)this).projectile.type] = true;
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
		((ModProjectile)this).DisplayName.SetDefault("Ultranium Kunai");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 30;
		((ModProjectile)this).projectile.height = 30;
		((ModProjectile)this).projectile.aiStyle = -1;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ranged = true;
		((ModProjectile)this).projectile.penetrate = 2;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.timeLeft = 200;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		target.immune[((ModProjectile)this).projectile.owner] = 3;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Vector2 vector = new Vector2((float)Main.projectileTexture[((ModProjectile)this).projectile.type].Width * 0.5f, (float)((ModProjectile)this).projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).projectile.gfxOffY);
			Color color = ((ModProjectile)this).projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).projectile.oldPos.Length - i) / (float)((ModProjectile)this).projectile.oldPos.Length);
			spriteBatch.Draw(Main.projectileTexture[((ModProjectile)this).projectile.type], position, null, color, ((ModProjectile)this).projectile.rotation, vector, ((ModProjectile)this).projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		if (Utils.NextBool(Main.rand))
		{
			Dust dust = Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("UltraniumDust"));
			dust.noGravity = true;
			dust.scale = 1.6f;
		}
		((ModProjectile)this).projectile.rotation = (float)Math.Atan2(((ModProjectile)this).projectile.velocity.Y, ((ModProjectile)this).projectile.velocity.X) + 0.8f;
		((ModProjectile)this).projectile.ai[0] += 1f;
		if (((ModProjectile)this).projectile.ai[0] > 7f)
		{
			((ModProjectile)this).projectile.ai[0] = 7f;
			int num = HomeOnTarget();
			if (num != -1)
			{
				NPC nPC = Main.npc[num];
				Vector2 value = ((ModProjectile)this).projectile.DirectionTo(nPC.Center) * 25f;
				((ModProjectile)this).projectile.velocity = Vector2.Lerp(((ModProjectile)this).projectile.velocity, value, 0.05f);
			}
		}
	}

	private int HomeOnTarget()
	{
		int num = -1;
		for (int i = 0; i < 200; i++)
		{
			NPC nPC = Main.npc[i];
			if (nPC.CanBeChasedBy(((ModProjectile)this).projectile))
			{
				_ = nPC.wet;
				float num2 = ((ModProjectile)this).projectile.Distance(nPC.Center);
				if (num2 <= 400f && (num == -1 || ((ModProjectile)this).projectile.Distance(Main.npc[num].Center) > num2))
				{
					num = i;
				}
			}
		}
		return num;
	}

	public override void Kill(int timeLeft)
	{
		Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("UltraniumDust"));
		Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("UltraniumDust"));
		Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("UltraniumDust"));
		Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("UltraniumDust"));
		Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("UltraniumDust"));
		Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("UltraniumDust"));
		Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("UltraniumDust"));
		Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("UltraniumDust"));
		Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("UltraniumDust"));
		Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("UltraniumDust"));
		Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("UltraniumDust"));
		Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("UltraniumDust"));
		Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("UltraniumDust"));
		Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("UltraniumDust"));
		Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("UltraniumDust"));
	}
}
