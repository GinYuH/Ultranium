using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Guardians.Hell;

public class HellThrow : ModProjectile
{
	public bool hasRing;

	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Hell Throw");
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		Projectile.scale = 1f;
		Projectile.width = 18;
		Projectile.height = 18;
		Projectile.aiStyle = 99;
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.scale = 1f;
		ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1f;
		ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 370f;
		ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 18f;
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

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void PostAI()
	{
		Projectile.rotation -= 20f;
	}

	public override void AI()
	{
		if (!hasRing)
		{
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("HellThrowAura").Type, Projectile.damage, 0.5f, 0, 0f, (float)Projectile.whoAmI);
			hasRing = true;
		}
	}
}
