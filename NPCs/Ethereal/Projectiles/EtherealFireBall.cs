using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ethereal.Projectiles;

public class EtherealFireBall : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Ethereal Fireball");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.scale = 1.2f;
		((ModProjectile)this).projectile.width = 18;
		((ModProjectile)this).projectile.height = 18;
		((ModProjectile)this).projectile.aiStyle = -1;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.alpha = 100;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.timeLeft = 180;
		base.cooldownSlot = 1;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/Ethereal/Projectiles/EtherealFireBallTrail").Width * 0.5f, (float)((ModProjectile)this).projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).projectile.gfxOffY);
			Color color = ((ModProjectile)this).projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).projectile.oldPos.Length - i) / (float)((ModProjectile)this).projectile.oldPos.Length);
			spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/Ethereal/Projectiles/EtherealFireBallTrail"), position, null, color, ((ModProjectile)this).projectile.rotation, vector, ((ModProjectile)this).projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		if (((ModProjectile)this).projectile.localAI[0] == 0f)
		{
			((ModProjectile)this).projectile.localAI[0] = 1f;
			Main.PlaySound(SoundID.Item20, ((ModProjectile)this).projectile.position);
		}
		if ((((ModProjectile)this).projectile.ai[0] -= 1f) > 0f)
		{
			float num = ((ModProjectile)this).projectile.velocity.Length();
			num += ((ModProjectile)this).projectile.ai[1];
			((ModProjectile)this).projectile.velocity = Vector2.Normalize(((ModProjectile)this).projectile.velocity) * num;
		}
		else if (((ModProjectile)this).projectile.ai[0] == 0f)
		{
			((ModProjectile)this).projectile.ai[1] = (int)Player.FindClosest(((ModProjectile)this).projectile.Center, 0, 0);
			if (((ModProjectile)this).projectile.ai[1] != -1f && ((Entity)Main.player[(int)((ModProjectile)this).projectile.ai[1]]).active && !Main.player[(int)((ModProjectile)this).projectile.ai[1]].dead)
			{
				((ModProjectile)this).projectile.velocity = ((ModProjectile)this).projectile.DirectionTo(Main.player[(int)((ModProjectile)this).projectile.ai[1]].Center);
				((ModProjectile)this).projectile.netUpdate = true;
			}
			else
			{
				((ModProjectile)this).projectile.Kill();
			}
		}
		else
		{
			((ModProjectile)this).projectile.tileCollide = true;
			float curAngle = ((ModProjectile)this).projectile.velocity.ToRotation();
			float targetAngle = (Main.player[(int)((ModProjectile)this).projectile.ai[1]].Center - ((ModProjectile)this).projectile.Center).ToRotation();
			((ModProjectile)this).projectile.velocity = new Vector2(((ModProjectile)this).projectile.velocity.Length(), 0f).RotatedBy(curAngle.AngleLerp(targetAngle, 0.025f));
		}
		((ModProjectile)this).projectile.rotation += 0.2f;
	}

	public override void Kill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 62, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModProjectile)this).projectile.Center)
			{
				Main.dust[num].velocity = ((ModProjectile)this).projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}

	public override void OnHitPlayer(Player player, int damage, bool crit)
	{
		player.AddBuff(((ModProjectile)this).mod.BuffType("ShadowflameDebuff"), 120, fromNetPvP: true);
	}
}
