using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ocean;

public class LingerBubble : ModProjectile
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Water Bubble");
	}

	public override void SetDefaults()
	{
		Projectile.width = 26;
		Projectile.height = 26;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.tileCollide = true;
		Projectile.penetrate = 5;
		Projectile.timeLeft = 600;
		Projectile.light = 0f;
		Projectile.extraUpdates = 1;
		Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		Projectile.spriteDirection = ((Projectile.velocity.X > 0f) ? 1 : (-1));
		if (Projectile.spriteDirection == 1)
		{
			Projectile.rotation += 0.05f;
		}
		if (Projectile.spriteDirection == -1)
		{
			Projectile.rotation += -0.05f;
		}
		Projectile.velocity *= 0f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item85, Projectile.position);
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 33, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != Projectile.Center)
			{
				Main.dust[num].velocity = Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}
}
