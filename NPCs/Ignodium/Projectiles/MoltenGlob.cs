using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ignodium.Projectiles;

public class MoltenGlob : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Molten Glob");
	}

	public override void SetDefaults()
	{
		Main.projFrames[Projectile.type] = 4;
		Projectile.CloneDefaults(467);
		base.AIType = ProjectileID.CultistBossFireBall;
	}

	public override bool PreKill(int timeLeft)
	{
		Projectile.type = ProjectileID.CultistBossFireBall;
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}
}
