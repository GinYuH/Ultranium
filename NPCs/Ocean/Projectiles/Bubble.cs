using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ocean.Projectiles;

public class Bubble : ModProjectile
{
	private int Bounces = 2;

	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Water Bubble");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 26;
		((ModProjectile)this).projectile.height = 26;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.tileCollide = true;
		((ModProjectile)this).projectile.penetrate = 5;
		((ModProjectile)this).projectile.timeLeft = 600;
		((ModProjectile)this).projectile.light = 0f;
		((ModProjectile)this).projectile.extraUpdates = 1;
		((ModProjectile)this).projectile.ignoreWater = true;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.spriteDirection = ((((ModProjectile)this).projectile.velocity.X > 0f) ? 1 : (-1));
		if (((ModProjectile)this).projectile.spriteDirection == 1)
		{
			((ModProjectile)this).projectile.rotation += 0.05f;
		}
		if (((ModProjectile)this).projectile.spriteDirection == -1)
		{
			((ModProjectile)this).projectile.rotation += -0.05f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Bounces--;
		if (Bounces <= 0)
		{
			((ModProjectile)this).projectile.Kill();
		}
		else
		{
			if (((ModProjectile)this).projectile.velocity.X != oldVelocity.X)
			{
				((ModProjectile)this).projectile.velocity.X = (0f - oldVelocity.X) * 0.8f;
			}
			if (((ModProjectile)this).projectile.velocity.Y != oldVelocity.Y)
			{
				((ModProjectile)this).projectile.velocity.Y = (0f - oldVelocity.Y) * 0.8f;
			}
		}
		if (((ModProjectile)this).projectile.timeLeft < 100)
		{
			((ModProjectile)this).projectile.scale += 0.02f;
		}
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void Kill(int timeLeft)
	{
		Main.PlaySound(SoundID.Item85, ((ModProjectile)this).projectile.position);
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 33, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModProjectile)this).projectile.Center)
			{
				Main.dust[num].velocity = ((ModProjectile)this).projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}
}
