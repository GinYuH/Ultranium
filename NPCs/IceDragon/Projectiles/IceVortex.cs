using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.IceDragon.Projectiles;

public class IceVortex : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 11;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		DisplayName.SetDefault("Ice Vortex");
	}

	public override void SetDefaults()
	{
		Projectile.width = 58;
		Projectile.height = 58;
		Projectile.aiStyle = 0;
		Projectile.friendly = false;
		Projectile.hostile = true;
		Projectile.tileCollide = true;
		Projectile.penetrate = 1;
		Projectile.timeLeft = 180;
		Projectile.extraUpdates = 1;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
		Vector2 vector = new Vector2((float)texture2D.Width * 0.5f, (float)Projectile.height * 0.5f);
		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 position = Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, Projectile.gfxOffY);
			Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
			Rectangle value = new Rectangle(0, texture2D.Height / Main.projFrames[Projectile.type] * Projectile.frame, texture2D.Width, texture2D.Height / Main.projFrames[Projectile.type]);
			Main.spriteBatch.Draw(texture2D, position, value, color, Projectile.rotation, vector, Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		Projectile.rotation += 0.2f;
	}

	public override void OnKill(int timeLeft)
	{
		Projectile.NewProjectile(null, Projectile.Center.X, Projectile.Center.Y - 100f, 0f, 0f, Mod.Find<ModProjectile>("IceTwisterLarge").Type, 30, 1f, Main.myPlayer, 0f, 0f);
	}
}
