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
		// ((ModProjectile)this).DisplayName.SetDefault("Squid Telegraph");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 2;
		((ModProjectile)this).Projectile.height = 2;
		((ModProjectile)this).Projectile.aiStyle = -1;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.alpha = 255;
		((ModProjectile)this).Projectile.timeLeft = 3600;
		((ModProjectile)this).Projectile.tileCollide = false;
	}

	public override bool CanHitPlayer(Player target)
	{
		return false;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.ai[0] += 1f;
		if (((ModProjectile)this).Projectile.ai[0] <= 20f)
		{
			if (((ModProjectile)this).Projectile.alpha <= 255)
			{
				((ModProjectile)this).Projectile.alpha -= 12;
			}
		}
		else if (((ModProjectile)this).Projectile.ai[0] >= 40f)
		{
			((ModProjectile)this).Projectile.alpha += 5;
			if (((ModProjectile)this).Projectile.alpha >= 255)
			{
				((ModProjectile)this).Projectile.Kill();
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, ((ModProjectile)this).Projectile.position - new Vector2(3f, 4000f) - Main.screenPosition, null, new Color(55, 69, 188) * (1f - (float)((ModProjectile)this).Projectile.alpha / 255f), ((ModProjectile)this).Projectile.rotation, Vector2.Zero, new Vector2(6f, 4f), SpriteEffects.None, 0f);
		return false;
	}
}
