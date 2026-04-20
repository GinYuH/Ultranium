using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Minion;

public class ShadowApparition : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Shadowflame Apparition");
		Main.projFrames[Projectile.type] = 8;
		ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
		ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		Projectile.CloneDefaults(317);
		Projectile.width = 30;
		Projectile.height = 30;
		Projectile.minion = true;
		Projectile.friendly = true;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = true;
		Projectile.netImportant = true;
		AIType = ProjectileID.Raven;
		Projectile.penetrate = -1;
		Projectile.timeLeft = 18000;
		Projectile.minionSlots = 1f;
        Projectile.DamageType = DamageClass.Summon;
    }

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (Projectile.penetrate == 0)
		{
			Projectile.Kill();
		}
		return false;
	}

	public override void AI()
	{
		bool num = Projectile.type == Mod.Find<ModProjectile>("ShadowApparition").Type;
		Player player = Main.player[Projectile.owner];
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (num)
		{
			if (player.dead)
			{
				modPlayer.ShadowApparition = false;
			}
			if (modPlayer.ShadowApparition)
			{
				Projectile.timeLeft = 2;
			}
		}
		if (++Projectile.frameCounter >= 3)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= 8)
			{
				Projectile.frame = 0;
			}
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}
}
