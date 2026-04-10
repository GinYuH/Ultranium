using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent;

public class AbyssalWraith : ModNPC
{
	private int MoveSpeed;

	private int MoveSpeedY;

	private float HomeY = 150f;

	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Abyssal Wraith");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 5;
		NPCID.Sets.TrailCacheLength[((ModNPC)this).npc.type] = 10;
		NPCID.Sets.TrailingMode[((ModNPC)this).npc.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.width = 64;
		((ModNPC)this).npc.height = 84;
		((ModNPC)this).npc.damage = 80;
		((ModNPC)this).npc.defense = 65;
		((ModNPC)this).npc.lifeMax = 1500;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit54;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath6;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.aiStyle = 0;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.noTileCollide = true;
		((ModNPC)this).npc.buffImmune[24] = true;
		base.banner = ((ModNPC)this).npc.type;
		base.bannerItem = ((ModNPC)this).mod.ItemType("AbyssalWraithBanner");
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).npc.buffImmune[i] = true;
		}
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		((ModNPC)this).npc.lifeMax = 3200;
		((ModNPC)this).npc.damage = 110;
		((ModNPC)this).npc.defense = 70;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
	{
		if (((ModNPC)this).npc.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/ShadowEvent/AbyssalWraithTrail").Width * 0.5f, (float)((ModNPC)this).npc.height * 0.5f);
			for (int i = 0; i < ((ModNPC)this).npc.oldPos.Length; i++)
			{
				SpriteEffects effects = ((((ModNPC)this).npc.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				Vector2 position = ((ModNPC)this).npc.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModNPC)this).npc.gfxOffY);
				Color color = ((ModNPC)this).npc.GetAlpha(drawColor) * ((float)(((ModNPC)this).npc.oldPos.Length - i) / (float)((ModNPC)this).npc.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/ShadowEvent/AbyssalWraithTrail"), position, ((ModNPC)this).npc.frame, color, ((ModNPC)this).npc.rotation, vector, ((ModNPC)this).npc.scale, effects, 0f);
			}
		}
		return true;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/AbyssalWraithGore1"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/AbyssalWraithGore2"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/AbyssalWraithGore3"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/AbyssalWraithGore4"));
		return true;
	}

	public override void OnHitPlayer(Player player, int damage, bool crit)
	{
		player.AddBuff(((ModNPC)this).mod.BuffType("DarkDebuff"), 120);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		((ModNPC)this).npc.rotation = ((ModNPC)this).npc.velocity.X * 0.05f;
		((ModNPC)this).npc.spriteDirection = ((ModNPC)this).npc.direction;
		Player player = Main.player[((ModNPC)this).npc.target];
		((ModNPC)this).npc.netUpdate = true;
		((ModNPC)this).npc.TargetClosest();
		((ModNPC)this).npc.velocity.Y = -100f;
		if (((ModNPC)this).npc.ai[0] == 0f)
		{
			if (((ModNPC)this).npc.Center.X >= player.Center.X && MoveSpeed >= -53)
			{
				MoveSpeed--;
			}
			else if (((ModNPC)this).npc.Center.X <= player.Center.X && MoveSpeed <= 53)
			{
				MoveSpeed++;
			}
			((ModNPC)this).npc.velocity.X = (float)MoveSpeed * 0.07f;
			if (((ModNPC)this).npc.Center.Y >= player.Center.Y - HomeY && MoveSpeedY >= -30)
			{
				MoveSpeedY--;
				HomeY = 100f;
			}
			else if (((ModNPC)this).npc.Center.Y <= player.Center.Y - HomeY && MoveSpeedY <= 30)
			{
				MoveSpeedY++;
			}
			((ModNPC)this).npc.velocity.Y = (float)MoveSpeedY * 0.07f;
			if (Main.rand.Next(220) == 6)
			{
				HomeY = -34f;
			}
		}
		((ModNPC)this).npc.velocity.Y = (float)MoveSpeedY * 0.07f;
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).npc.frameCounter += 1.0;
		if (((ModNPC)this).npc.frameCounter >= 10.0)
		{
			((ModNPC)this).npc.frame.Y = (((ModNPC)this).npc.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).npc.type] * frameHeight);
			((ModNPC)this).npc.frameCounter = 1.0;
		}
	}

	public override void NPCLoot()
	{
		if (Main.rand.Next(5) == 0)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DarkMatter"), Main.rand.Next(1, 1), false, 0, false, false);
		}
	}
}
