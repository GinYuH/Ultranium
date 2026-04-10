using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ocean.Projectiles;

public class SquidChargeTelegraph : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Squid Telegraph");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 2;
		((ModProjectile)this).projectile.height = 2;
		((ModProjectile)this).projectile.aiStyle = -1;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.alpha = 255;
		((ModProjectile)this).projectile.timeLeft = 3600;
		((ModProjectile)this).projectile.tileCollide = false;
	}

	public override bool CanHitPlayer(Player target)
	{
		return false;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.ai[0] += 1f;
		if (((ModProjectile)this).projectile.ai[0] <= 20f)
		{
			if (((ModProjectile)this).projectile.alpha <= 255)
			{
				((ModProjectile)this).projectile.alpha -= 12;
			}
		}
		else if (((ModProjectile)this).projectile.ai[0] >= 40f)
		{
			((ModProjectile)this).projectile.alpha += 5;
			if (((ModProjectile)this).projectile.alpha >= 255)
			{
				((ModProjectile)this).projectile.Kill();
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Main.spriteBatch.Draw(Main.magicPixel, ((ModProjectile)this).projectile.position - new Vector2(3f, 4000f) - Main.screenPosition, null, new Color(55, 69, 188) * (1f - (float)((ModProjectile)this).projectile.alpha / 255f), ((ModProjectile)this).projectile.rotation, Vector2.Zero, new Vector2(6f, 4f), SpriteEffects.None, 0f);
		return false;
	}
}
