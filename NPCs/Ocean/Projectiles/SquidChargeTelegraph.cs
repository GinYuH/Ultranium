using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ocean.Projectiles;

public class SquidChargeTelegraph : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Squid Telegraph");
	}

	public override void SetDefaults()
	{
		Projectile.width = 2;
		Projectile.height = 2;
		Projectile.aiStyle = -1;
		Projectile.penetrate = -1;
		Projectile.alpha = 255;
		Projectile.timeLeft = 3600;
		Projectile.tileCollide = false;
	}

	public override bool CanHitPlayer(Player target)
	{
		return false;
	}

	public override void AI()
	{
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] <= 20f)
		{
			if (Projectile.alpha <= 255)
			{
				Projectile.alpha -= 12;
			}
		}
		else if (Projectile.ai[0] >= 40f)
		{
			Projectile.alpha += 5;
			if (Projectile.alpha >= 255)
			{
				Projectile.Kill();
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, Projectile.position - new Vector2(3f, 4000f) - Main.screenPosition, null, new Color(55, 69, 188) * (1f - (float)Projectile.alpha / 255f), Projectile.rotation, Vector2.Zero, new Vector2(6f, 4f), SpriteEffects.None, 0f);
		return false;
	}
}
