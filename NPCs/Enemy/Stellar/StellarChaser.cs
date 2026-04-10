using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Stellar;

public class StellarChaser : ModNPC
{
	private int moveSpeed;

	private int moveSpeedY;

	private float HomeY = 150f;

	private int ShootTimer;

	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Stellar Chaser");
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.width = 38;
		((ModNPC)this).npc.height = 38;
		((ModNPC)this).npc.damage = 22;
		((ModNPC)this).npc.defense = 11;
		((ModNPC)this).npc.noTileCollide = true;
		((ModNPC)this).npc.lifeMax = 230;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit3;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath43;
		((ModNPC)this).npc.value = 360f;
		((ModNPC)this).npc.knockBackResist = 0.16f;
		((ModNPC)this).npc.noGravity = true;
		base.banner = ((ModNPC)this).npc.type;
		base.bannerItem = ((ModNPC)this).mod.ItemType("StellarChaserBanner");
	}

	public override void AI()
	{
		((ModNPC)this).npc.spriteDirection = ((ModNPC)this).npc.direction;
		Player player = Main.player[((ModNPC)this).npc.target];
		if (((ModNPC)this).npc.Center.X >= player.Center.X && moveSpeed >= -45)
		{
			moveSpeed--;
		}
		if (((ModNPC)this).npc.Center.X <= player.Center.X && moveSpeed <= 45)
		{
			moveSpeed++;
		}
		((ModNPC)this).npc.velocity.X = (float)moveSpeed * 0.1f;
		if (((ModNPC)this).npc.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -27)
		{
			moveSpeedY--;
			HomeY = 150f;
		}
		if (((ModNPC)this).npc.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 27)
		{
			moveSpeedY++;
		}
		((ModNPC)this).npc.velocity.Y = (float)moveSpeedY * 0.12f;
		if (Main.rand.Next(220) == 6)
		{
			HomeY = -35f;
		}
		((ModNPC)this).npc.rotation += 0.1f;
		ShootTimer++;
		if (ShootTimer == 300)
		{
			Vector2 vector = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
			vector.Normalize();
			vector.X *= 6f;
			vector.Y *= 6f;
			Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector.X, vector.Y, ((ModNPC)this).mod.ProjectileType("EyeBolt"), 18, 1f, Main.myPlayer, 0f, 0f);
		}
	}

	public override void HitEffect(int hitDirection, double damage)
	{
		if (((ModNPC)this).npc.life > 0)
		{
			return;
		}
		((ModNPC)this).npc.position.X = ((ModNPC)this).npc.position.X + (float)(((ModNPC)this).npc.width / 2);
		((ModNPC)this).npc.position.Y = ((ModNPC)this).npc.position.Y + (float)(((ModNPC)this).npc.height / 2);
		((ModNPC)this).npc.width = 30;
		((ModNPC)this).npc.height = 30;
		((ModNPC)this).npc.position.X = ((ModNPC)this).npc.position.X - (float)(((ModNPC)this).npc.width / 2);
		((ModNPC)this).npc.position.Y = ((ModNPC)this).npc.position.Y - (float)(((ModNPC)this).npc.height / 2);
		for (int i = 0; i < 20; i++)
		{
			int num = Dust.NewDust(new Vector2(((ModNPC)this).npc.position.X, ((ModNPC)this).npc.position.Y), ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.DustType("StellarDust"), 0f, 0f, 100, default(Color), 2f);
			Main.dust[num].velocity *= 3f;
			if (Main.rand.Next(2) == 0)
			{
				Main.dust[num].scale = 0.5f;
				Main.dust[num].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/StellarChaserGore1"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/StellarChaserGore2"));
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		Player player = spawnInfo.player;
		if (!player.ZoneTowerSolar && !player.ZoneTowerVortex && !player.ZoneTowerNebula && !player.ZoneTowerStardust && ((!Main.pumpkinMoon && !Main.snowMoon) || (double)spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || (double)spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime) && SpawnCondition.GoblinArmy.Chance == 0f)
		{
			if (!spawnInfo.player.ZoneSkyHeight || !Main.hardMode)
			{
				return 0f;
			}
			return 10f;
		}
		return 0f;
	}

	public override void NPCLoot()
	{
		if (Main.rand.Next(2) == 0)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("StellarDust"), Main.rand.Next(1, 3), false, 0, false, false);
		}
	}
}
