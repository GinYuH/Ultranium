using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent;

public class MindFlayerClone : ModNPC
{
	private int timer;

	private int moveSpeed;

	private int moveSpeedY;

	private float HomeY = 130f;

	public int roarTimer = 120;

	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Mind Flayer");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 6;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.scale = 1f;
		((ModNPC)this).npc.width = 100;
		((ModNPC)this).npc.height = 150;
		((ModNPC)this).npc.damage = 85;
		((ModNPC)this).npc.lifeMax = 10000;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.noTileCollide = true;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit55;
		((ModNPC)this).npc.DeathSound = ((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/MindFlayerRoar")?.WithVolume(1.2f)?.WithPitchVariance(0.5f);
		((ModNPC)this).npc.defense = 35;
		((ModNPC)this).npc.npcSlots = 1f;
		((ModNPC)this).npc.lavaImmune = true;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.aiStyle = 0;
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).npc.buffImmune[i] = true;
		}
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		((ModNPC)this).npc.lifeMax = 12000;
		((ModNPC)this).npc.damage = 120;
		((ModNPC)this).npc.defense = 50;
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
			int num = Dust.NewDust(new Vector2(((ModNPC)this).npc.position.X, ((ModNPC)this).npc.position.Y), ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.DustType("ShadowDustBlack"), 0f, 0f, 100, default(Color), 2f);
			Main.dust[num].velocity *= 3f;
			if (Main.rand.Next(2) == 0)
			{
				Main.dust[num].scale = 0.5f;
				Main.dust[num].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitPlayer(Player player, int damage, bool crit)
	{
		player.AddBuff(((ModNPC)this).mod.BuffType("DarkDebuff"), 300);
	}

	public override bool PreAI()
	{
		if (NPC.CountNPCS(((ModNPC)this).mod.NPCType("MindFlayer")) == 0)
		{
			((Entity)((ModNPC)this).npc).active = false;
		}
		((ModNPC)this).npc.rotation = ((ModNPC)this).npc.velocity.X * 0.02f;
		Player player = Main.player[((ModNPC)this).npc.target];
		bool expertMode = Main.expertMode;
		((ModNPC)this).npc.netUpdate = true;
		((ModNPC)this).npc.TargetClosest();
		((ModNPC)this).npc.TargetClosest(faceTarget: false);
		((ModNPC)this).npc.velocity.Y = -100f;
		if (((ModNPC)this).npc.ai[0] == 0f)
		{
			if (((ModNPC)this).npc.Center.X >= player.Center.X && moveSpeed >= -50)
			{
				moveSpeed--;
			}
			else if (((ModNPC)this).npc.Center.X <= player.Center.X && moveSpeed <= 50)
			{
				moveSpeed++;
			}
			((ModNPC)this).npc.velocity.X = (float)moveSpeed * 0.1f;
			if (((ModNPC)this).npc.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -50)
			{
				moveSpeedY--;
				HomeY = 150f;
			}
			else if (((ModNPC)this).npc.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 50)
			{
				moveSpeedY++;
			}
			((ModNPC)this).npc.velocity.Y = (float)moveSpeedY * 0.12f;
			if (Main.rand.Next(220) == 6)
			{
				HomeY = -33f;
			}
		}
		((ModNPC)this).npc.velocity.Y = (float)moveSpeedY * 0.12f;
		timer++;
		if (timer == 200)
		{
			Vector2 vector = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
			vector.Normalize();
			vector.X *= 7f;
			vector.Y *= 7f;
			int num = (expertMode ? 40 : 45);
			Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector.X, vector.Y, ((ModNPC)this).mod.ProjectileType("DarkMatter"), num, 1f, Main.myPlayer, 0f, 0f);
			timer = 0;
		}
		return true;
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).npc.frameCounter += 1.0;
		if (((ModNPC)this).npc.frameCounter >= 11.0)
		{
			((ModNPC)this).npc.frame.Y = (((ModNPC)this).npc.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).npc.type] * frameHeight);
			((ModNPC)this).npc.frameCounter = 1.0;
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override bool CheckDead()
	{
		return true;
	}
}
