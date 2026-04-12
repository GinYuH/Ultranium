using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ignodium.Projectiles;

public class IgnodiumBeamTelegraph : ModProjectile
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Eruption Telegraph");
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
		else if (Projectile.ai[0] == 22f)
		{
			Projectile obj = Main.projectile[Projectile.NewProjectile(null, Projectile.Center.X, Projectile.Center.Y, 0f, -4f, Mod.Find<ModProjectile>("IgnodiumBeam").Type, 55, 0f, Main.myPlayer, 0f, 40f)];
			obj.localAI[1] = 125f;
			obj.Center = Projectile.Center;
		}
		else if (Projectile.ai[0] >= 22f)
		{
			Projectile.alpha += 10;
			if (Projectile.alpha >= 255)
			{
				Projectile.Kill();
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, Projectile.position - new Vector2(3f, 4000f) - Main.screenPosition, null, new Color(246, 131, 43) * (1f - (float)Projectile.alpha / 255f), Projectile.rotation, Vector2.Zero, new Vector2(6f, 4f), SpriteEffects.None, 0f);
		return false;
	}
}
