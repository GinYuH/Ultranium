using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent.Projectiles.Flayer;

public class FlayerTelegraph : ModProjectile
{
	private int Timer;

	public override string Texture => "Ultranium/NPCs/ShadowWorm/Projectiles/ShadowFlameBreath";

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 136;
		((ModProjectile)this).projectile.height = 136;
		((ModProjectile)this).projectile.hostile = false;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.tileCollide = true;
		((ModProjectile)this).projectile.penetrate = 2;
		((ModProjectile)this).projectile.timeLeft = 260;
		((ModProjectile)this).projectile.alpha = 255;
	}

	public override void AI()
	{
		_ = Main.expertMode;
		((ModProjectile)this).projectile.velocity *= 0f;
		Timer++;
		if (Timer < 60)
		{
			int num = 8;
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = (Vector2.One * new Vector2((float)((ModProjectile)this).projectile.width / 5f, ((ModProjectile)this).projectile.height) * 0.75f * 0.5f).RotatedBy((float)(i - (num / 2 - 1)) * ((float)Math.PI * 2f) / (float)num) + ((ModProjectile)this).projectile.Center;
				Vector2 vector2 = vector - ((ModProjectile)this).projectile.Center;
				Dust obj = Main.dust[Dust.NewDust(vector + vector2, 0, 0, 89, vector2.X * 2f, vector2.Y * 2f, 100, default(Color), 1.4f)];
				obj.noGravity = true;
				obj.noLight = false;
				obj.velocity = Vector2.Normalize(vector2) * 2f;
				obj.fadeIn = 1.3f;
			}
		}
		if (Timer < 60)
		{
			return;
		}
		for (int j = 0; j < 200; j++)
		{
			if (((Entity)Main.npc[j]).active && Main.npc[j].type == ModContent.NPCType<MindFlayer>())
			{
				for (int k = 0; k < 15; k++)
				{
					int num2 = Dust.NewDust(Main.npc[j].position, Main.npc[j].width, Main.npc[j].height, 89, 0f, 0f, 100);
					Main.dust[num2].noGravity = true;
					Main.dust[num2].velocity *= 1.4f;
				}
				Main.npc[j].Center = ((ModProjectile)this).projectile.Center;
			}
		}
		((Entity)((ModProjectile)this).projectile).active = false;
	}
}
