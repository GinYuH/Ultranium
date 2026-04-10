using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ethereal.Projectiles;

public class EtherealFireBall : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Ethereal Fireball");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.scale = 1.2f;
		((ModProjectile)this).Projectile.width = 18;
		((ModProjectile)this).Projectile.height = 18;
		((ModProjectile)this).Projectile.aiStyle = -1;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.alpha = 100;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.timeLeft = 180;
		base.CooldownSlot = 1;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/Ethereal/Projectiles/EtherealFireBallTrail").Width * 0.5f, (float)((ModProjectile)this).Projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).Projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).Projectile.gfxOffY);
			Color color = ((ModProjectile)this).Projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).Projectile.oldPos.Length - i) / (float)((ModProjectile)this).Projectile.oldPos.Length);
			spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/Ethereal/Projectiles/EtherealFireBallTrail"), position, null, color, ((ModProjectile)this).Projectile.rotation, vector, ((ModProjectile)this).Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		if (((ModProjectile)this).Projectile.localAI[0] == 0f)
		{
			((ModProjectile)this).Projectile.localAI[0] = 1f;
			SoundEngine.PlaySound(SoundID.Item20, ((ModProjectile)this).Projectile.position);
		}
		if ((((ModProjectile)this).Projectile.ai[0] -= 1f) > 0f)
		{
			float num = ((ModProjectile)this).Projectile.velocity.Length();
			num += ((ModProjectile)this).Projectile.ai[1];
			((ModProjectile)this).Projectile.velocity = Vector2.Normalize(((ModProjectile)this).Projectile.velocity) * num;
		}
		else if (((ModProjectile)this).Projectile.ai[0] == 0f)
		{
			((ModProjectile)this).Projectile.ai[1] = (int)Player.FindClosest(((ModProjectile)this).Projectile.Center, 0, 0);
			if (((ModProjectile)this).Projectile.ai[1] != -1f && ((Entity)Main.player[(int)((ModProjectile)this).Projectile.ai[1]]).active && !Main.player[(int)((ModProjectile)this).Projectile.ai[1]].dead)
			{
				((ModProjectile)this).Projectile.velocity = ((ModProjectile)this).Projectile.DirectionTo(Main.player[(int)((ModProjectile)this).Projectile.ai[1]].Center);
				((ModProjectile)this).Projectile.netUpdate = true;
			}
			else
			{
				((ModProjectile)this).Projectile.Kill();
			}
		}
		else
		{
			((ModProjectile)this).Projectile.tileCollide = true;
			float curAngle = ((ModProjectile)this).Projectile.velocity.ToRotation();
			float targetAngle = (Main.player[(int)((ModProjectile)this).Projectile.ai[1]].Center - ((ModProjectile)this).Projectile.Center).ToRotation();
			((ModProjectile)this).Projectile.velocity = new Vector2(((ModProjectile)this).Projectile.velocity.Length(), 0f).RotatedBy(curAngle.AngleLerp(targetAngle, 0.025f));
		}
		((ModProjectile)this).Projectile.rotation += 0.2f;
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 62, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModProjectile)this).Projectile.Center)
			{
				Main.dust[num].velocity = ((ModProjectile)this).Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		player.AddBuff(((ModProjectile)this).Mod.Find<ModBuff>("ShadowflameDebuff").Type, 120, fromNetPvP: true);
	}
}
