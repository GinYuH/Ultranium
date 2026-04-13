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
		//DisplayName.SetDefault("Mind Flayer");
		Main.npcFrameCount[NPC.type] = 6;
	}

	public override void SetDefaults()
	{
		NPC.scale = 1f;
		NPC.width = 100;
		NPC.height = 150;
		NPC.damage = 85;
		NPC.lifeMax = 10000;
		NPC.knockBackResist = 0f;
		NPC.noGravity = true;
		NPC.noTileCollide = true;
		NPC.HitSound = SoundID.NPCHit55;
		NPC.DeathSound = new Terraria.Audio.SoundStyle("Ultranium/Sounds/MindFlayerRoar") with { PitchVariance = 0.5f };
		NPC.defense = 35;
		NPC.npcSlots = 1f;
		NPC.lavaImmune = true;
		NPC.noGravity = true;
		NPC.aiStyle = 0;
		for (int i = 0; i < 206; i++)
		{
			NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		NPC.lifeMax = 12000;
		NPC.damage = 120;
		NPC.defense = 50;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life > 0)
		{
			return;
		}
		NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
		NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
		NPC.width = 30;
		NPC.height = 30;
		NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
		NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
		for (int i = 0; i < 20; i++)
		{
			int num = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, Mod.Find<ModDust>("ShadowDustBlack").Type, 0f, 0f, 100, default(Color), 2f);
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

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(Mod.Find<ModBuff>("DarkDebuff").Type, 300);
	}

	public override bool PreAI()
	{
		if (NPC.CountNPCS(Mod.Find<ModNPC>("MindFlayer").Type) == 0)
		{
			((Entity)NPC).active = false;
		}
		NPC.rotation = NPC.velocity.X * 0.02f;
		Player player = Main.player[NPC.target];
		bool expertMode = Main.expertMode;
		NPC.netUpdate = true;
		NPC.TargetClosest();
		NPC.TargetClosest(faceTarget: false);
		NPC.velocity.Y = -100f;
		if (NPC.ai[0] == 0f)
		{
			if (NPC.Center.X >= player.Center.X && moveSpeed >= -50)
			{
				moveSpeed--;
			}
			else if (NPC.Center.X <= player.Center.X && moveSpeed <= 50)
			{
				moveSpeed++;
			}
			NPC.velocity.X = (float)moveSpeed * 0.1f;
			if (NPC.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -50)
			{
				moveSpeedY--;
				HomeY = 150f;
			}
			else if (NPC.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 50)
			{
				moveSpeedY++;
			}
			NPC.velocity.Y = (float)moveSpeedY * 0.12f;
			if (Main.rand.Next(220) == 6)
			{
				HomeY = -33f;
			}
		}
		NPC.velocity.Y = (float)moveSpeedY * 0.12f;
		timer++;
		if (timer == 200)
		{
			Vector2 vector = Main.player[NPC.target].Center - NPC.Center;
			vector.Normalize();
			vector.X *= 7f;
			vector.Y *= 7f;
			int num = (expertMode ? 40 : 45);
			Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector.X, vector.Y, Mod.Find<ModProjectile>("DarkMatter").Type, num, 1f, Main.myPlayer, 0f, 0f);
			timer = 0;
		}
		return true;
	}

	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter += 1.0;
		if (NPC.frameCounter >= 11.0)
		{
			NPC.frame.Y = (NPC.frame.Y + frameHeight) % (Main.npcFrameCount[NPC.type] * frameHeight);
			NPC.frameCounter = 1.0;
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
