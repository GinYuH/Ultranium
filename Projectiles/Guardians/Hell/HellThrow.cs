using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Guardians.Hell;

public class HellThrow : ModProjectile
{
	public bool hasRing;

	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Hell Throw");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 3;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.scale = 1f;
		((ModProjectile)this).projectile.width = 18;
		((ModProjectile)this).projectile.height = 18;
		((ModProjectile)this).projectile.aiStyle = 99;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.melee = true;
		((ModProjectile)this).projectile.scale = 1f;
		ProjectileID.Sets.YoyosLifeTimeMultiplier[((ModProjectile)this).projectile.type] = -1f;
		ProjectileID.Sets.YoyosMaximumRange[((ModProjectile)this).projectile.type] = 370f;
		ProjectileID.Sets.YoyosTopSpeed[((ModProjectile)this).projectile.type] = 18f;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Vector2 vector = new Vector2((float)Main.projectileTexture[((ModProjectile)this).projectile.type].Width * 0.5f, (float)((ModProjectile)this).projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).projectile.gfxOffY);
			Color color = ((ModProjectile)this).projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).projectile.oldPos.Length - i) / (float)((ModProjectile)this).projectile.oldPos.Length);
			spriteBatch.Draw(Main.projectileTexture[((ModProjectile)this).projectile.type], position, null, color, ((ModProjectile)this).projectile.rotation, vector, ((ModProjectile)this).projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void PostAI()
	{
		((ModProjectile)this).projectile.rotation -= 20f;
	}

	public override void AI()
	{
		if (!hasRing)
		{
			Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, 0f, 0f, ((ModProjectile)this).mod.ProjectileType("HellThrowAura"), ((ModProjectile)this).projectile.damage, 0.5f, 0, 0f, (float)((ModProjectile)this).projectile.whoAmI);
			hasRing = true;
		}
	}
}
