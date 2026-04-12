using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Town.Projectiles;

public class EldritchSpore : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Projectile.type] = 3;
		DisplayName.SetDefault("Eldritch Spore");
	}

	public override void SetDefaults()
	{
		Projectile.scale = 1f;
		Projectile.width = 14;
		Projectile.height = 38;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.aiStyle = 0;
		Projectile.penetrate = 1;
		Projectile.extraUpdates = 1;
		Projectile.timeLeft = 300;
		Projectile.tileCollide = true;
	}

	public override void AI()
	{
		if (++Projectile.frameCounter >= 16)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= 3)
			{
				Projectile.frame = 0;
			}
		}
		Projectile.velocity *= 0f;
	}

	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item14, new Vector2(Projectile.position.X, Projectile.position.Y));
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("ShadowDustPurple").Type, 0f, -2f, 0, default(Color), 1.5f);
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
