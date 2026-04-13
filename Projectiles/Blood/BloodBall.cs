using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Blood;

public class BloodBall : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		//DisplayName.SetDefault("Blood Bolt");
	}

	public override void SetDefaults()
	{
		Projectile.width = 14;
		Projectile.height = 14;
		Projectile.aiStyle = ProjAIStyleID.Sickle;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.penetrate = 3;
		Projectile.tileCollide = true;
		Projectile.timeLeft = 200;
		Projectile.extraUpdates = 1;
		Projectile.aiStyle = 0;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, (float)Projectile.height * 0.5f);
		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 position = Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, Projectile.gfxOffY);
			Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
			Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, position, null, color, Projectile.rotation, vector, Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] >= 30f)
		{
			Projectile.velocity.Y = Projectile.velocity.Y + 0.15f;
			Projectile.velocity.X = Projectile.velocity.X * 0.99f;
		}
		Projectile.rotation += 0.02f;
	}

	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item86, new Vector2(Projectile.position.X, Projectile.position.Y));
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Blood, 0f, -2f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 0.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 0.5f;
			if (Main.dust[num].position != Projectile.Center)
			{
				Main.dust[num].velocity = Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}
}
