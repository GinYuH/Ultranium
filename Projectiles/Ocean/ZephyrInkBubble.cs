using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ocean;

public class ZephyrInkBubble : ModProjectile
{
	private int Bounces = 2;

	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Ink Bubble");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 26;
		((ModProjectile)this).Projectile.height = 26;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.hostile = false;
		((ModProjectile)this).Projectile.tileCollide = true;
		((ModProjectile)this).Projectile.penetrate = 2;
		((ModProjectile)this).Projectile.timeLeft = 600;
		((ModProjectile)this).Projectile.light = 0f;
		((ModProjectile)this).Projectile.extraUpdates = 1;
		((ModProjectile)this).Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.spriteDirection = ((((ModProjectile)this).Projectile.velocity.X > 0f) ? 1 : (-1));
		if (((ModProjectile)this).Projectile.spriteDirection == 1)
		{
			((ModProjectile)this).Projectile.rotation += 0.05f;
		}
		if (((ModProjectile)this).Projectile.spriteDirection == -1)
		{
			((ModProjectile)this).Projectile.rotation += -0.05f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Bounces--;
		if (Bounces <= 0)
		{
			((ModProjectile)this).Projectile.Kill();
		}
		else
		{
			if (((ModProjectile)this).Projectile.velocity.X != oldVelocity.X)
			{
				((ModProjectile)this).Projectile.velocity.X = (0f - oldVelocity.X) * 0.8f;
			}
			if (((ModProjectile)this).Projectile.velocity.Y != oldVelocity.Y)
			{
				((ModProjectile)this).Projectile.velocity.Y = (0f - oldVelocity.Y) * 0.8f;
			}
		}
		if (((ModProjectile)this).Projectile.timeLeft < 100)
		{
			((ModProjectile)this).Projectile.scale += 0.02f;
		}
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item112, ((ModProjectile)this).Projectile.position);
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 191, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModProjectile)this).Projectile.Center)
			{
				Main.dust[num].velocity = ((ModProjectile)this).Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}
}
