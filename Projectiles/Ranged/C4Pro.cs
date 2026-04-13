using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ranged;

public class C4Pro : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		//DisplayName.SetDefault("Spazma C4");
	}

	public override void SetDefaults()
	{
		Projectile.width = 18;
		Projectile.height = 18;
		Projectile.aiStyle = ProjAIStyleID.Sickle;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.penetrate = 1;
		Projectile.tileCollide = true;
		Projectile.timeLeft = 2000;
		Projectile.extraUpdates = 1;
		Projectile.aiStyle = 0;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[Projectile.owner] = 10;
		target.AddBuff(BuffID.CursedInferno, 120, quiet: true);
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

	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item14, new Vector2(Projectile.position.X, Projectile.position.Y));
		Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("C4Boom").Type, Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
	}

	public override void AI()
	{
		Projectile.rotation += 0.2f * (float)Projectile.direction;
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] >= 50f)
		{
			Projectile.velocity.Y = Projectile.velocity.Y + 0.15f;
			Projectile.velocity.X = Projectile.velocity.X * 0.99f;
		}
	}
}
