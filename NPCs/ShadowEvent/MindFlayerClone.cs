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
		// ((ModNPC)this).DisplayName.SetDefault("Mind Flayer");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 6;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.scale = 1f;
		((ModNPC)this).NPC.width = 100;
		((ModNPC)this).NPC.height = 150;
		((ModNPC)this).NPC.damage = 85;
		((ModNPC)this).NPC.lifeMax = 10000;
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.noTileCollide = true;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit55;
		((ModNPC)this).NPC.DeathSound = ((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/MindFlayerRoar")?.WithVolume(1.2f)?.WithPitchVariance(0.5f);
		((ModNPC)this).NPC.defense = 35;
		((ModNPC)this).NPC.npcSlots = 1f;
		((ModNPC)this).NPC.lavaImmune = true;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.aiStyle = 0;
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		((ModNPC)this).NPC.lifeMax = 12000;
		((ModNPC)this).NPC.damage = 120;
		((ModNPC)this).NPC.defense = 50;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (((ModNPC)this).NPC.life > 0)
		{
			return;
		}
		((ModNPC)this).NPC.position.X = ((ModNPC)this).NPC.position.X + (float)(((ModNPC)this).NPC.width / 2);
		((ModNPC)this).NPC.position.Y = ((ModNPC)this).NPC.position.Y + (float)(((ModNPC)this).NPC.height / 2);
		((ModNPC)this).NPC.width = 30;
		((ModNPC)this).NPC.height = 30;
		((ModNPC)this).NPC.position.X = ((ModNPC)this).NPC.position.X - (float)(((ModNPC)this).NPC.width / 2);
		((ModNPC)this).NPC.position.Y = ((ModNPC)this).NPC.position.Y - (float)(((ModNPC)this).NPC.height / 2);
		for (int i = 0; i < 20; i++)
		{
			int num = Dust.NewDust(new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y), ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModDust>("ShadowDustBlack").Type, 0f, 0f, 100, default(Color), 2f);
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
		player.AddBuff(((ModNPC)this).Mod.Find<ModBuff>("DarkDebuff").Type, 300);
	}

	public override bool PreAI()
	{
		if (NPC.CountNPCS(((ModNPC)this).Mod.Find<ModNPC>("MindFlayer").Type) == 0)
		{
			((Entity)((ModNPC)this).NPC).active = false;
		}
		((ModNPC)this).NPC.rotation = ((ModNPC)this).NPC.velocity.X * 0.02f;
		Player player = Main.player[((ModNPC)this).NPC.target];
		bool expertMode = Main.expertMode;
		((ModNPC)this).NPC.netUpdate = true;
		((ModNPC)this).NPC.TargetClosest();
		((ModNPC)this).NPC.TargetClosest(faceTarget: false);
		((ModNPC)this).NPC.velocity.Y = -100f;
		if (((ModNPC)this).NPC.ai[0] == 0f)
		{
			if (((ModNPC)this).NPC.Center.X >= player.Center.X && moveSpeed >= -50)
			{
				moveSpeed--;
			}
			else if (((ModNPC)this).NPC.Center.X <= player.Center.X && moveSpeed <= 50)
			{
				moveSpeed++;
			}
			((ModNPC)this).NPC.velocity.X = (float)moveSpeed * 0.1f;
			if (((ModNPC)this).NPC.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -50)
			{
				moveSpeedY--;
				HomeY = 150f;
			}
			else if (((ModNPC)this).NPC.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 50)
			{
				moveSpeedY++;
			}
			((ModNPC)this).NPC.velocity.Y = (float)moveSpeedY * 0.12f;
			if (Main.rand.Next(220) == 6)
			{
				HomeY = -33f;
			}
		}
		((ModNPC)this).NPC.velocity.Y = (float)moveSpeedY * 0.12f;
		timer++;
		if (timer == 200)
		{
			Vector2 vector = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
			vector.Normalize();
			vector.X *= 7f;
			vector.Y *= 7f;
			int num = (expertMode ? 40 : 45);
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector.X, vector.Y, ((ModNPC)this).Mod.Find<ModProjectile>("DarkMatter").Type, num, 1f, Main.myPlayer, 0f, 0f);
			timer = 0;
		}
		return true;
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).NPC.frameCounter += 1.0;
		if (((ModNPC)this).NPC.frameCounter >= 11.0)
		{
			((ModNPC)this).NPC.frame.Y = (((ModNPC)this).NPC.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).NPC.type] * frameHeight);
			((ModNPC)this).NPC.frameCounter = 1.0;
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
