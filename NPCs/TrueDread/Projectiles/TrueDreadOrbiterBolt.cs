using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.TrueDread.Projectiles;

public class TrueDreadOrbiterBolt : ModProjectile
{
	public override string Texture => "Ultranium/NPCs/Dread/Projectiles/DreadOrbiter";

	public override void SetStaticDefaults()
	{
		Main.projFrames[Projectile.type] = 4;
		//DisplayName.SetDefault("Dread Orbital");
	}

	public override void SetDefaults()
	{
		Projectile.scale = 1f;
		Projectile.width = 14;
		Projectile.height = 38;
		Projectile.friendly = false;
		Projectile.hostile = true;
		Projectile.aiStyle = 0;
		Projectile.penetrate = 1;
		Projectile.extraUpdates = 1;
		Projectile.timeLeft = 360;
		Projectile.tileCollide = true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(Mod.Find<ModBuff>("DreadDebuff").Type, 180, quiet: false);
	}

	public override void AI()
	{
		if (++Projectile.frameCounter >= 16)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= 4)
			{
				Projectile.frame = 0;
			}
		}
		if (Utils.NextBool(Main.rand))
		{
			Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemRuby);
			dust.noGravity = true;
			dust.scale = 1.2f;
		}
		Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Projectile.rotation += 0f * (float)Projectile.direction;
	}

	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item14, new Vector2(Projectile.position.X, Projectile.position.Y));
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemRuby, 0f, -2f, 0, default(Color), 1.5f);
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
