using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Magic;

public class NaniteBig : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		DisplayName.SetDefault("Big Nanite");
	}

	public override void SetDefaults()
	{
		Projectile.width = 38;
		Projectile.height = 38;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.penetrate = 1;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 170;
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
		Projectile.rotation += 0.35f * (float)Projectile.direction;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(144, 80, quiet: true);
	}
}
