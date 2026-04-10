using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent;

public class FlayerWraith : ModNPC
{
	private int timer;

	private int MoveSpeed;

	private int MoveSpeedY;

	private float HomeY = 150f;

	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Flayer Wraith");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 5;
		NPCID.Sets.TrailCacheLength[((ModNPC)this).npc.type] = 10;
		NPCID.Sets.TrailingMode[((ModNPC)this).npc.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.scale = 1.2f;
		((ModNPC)this).npc.width = 78;
		((ModNPC)this).npc.height = 112;
		((ModNPC)this).npc.damage = 60;
		((ModNPC)this).npc.lifeMax = 4000;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.noTileCollide = true;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit55;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath52;
		((ModNPC)this).npc.defense = 50;
		((ModNPC)this).npc.npcSlots = 1f;
		((ModNPC)this).npc.lavaImmune = true;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.noTileCollide = true;
		((ModNPC)this).npc.buffImmune[24] = true;
		((ModNPC)this).npc.netAlways = true;
		((ModNPC)this).npc.aiStyle = 0;
		base.banner = ((ModNPC)this).npc.type;
		base.bannerItem = ((ModNPC)this).mod.ItemType("FlayerWraithBanner");
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).npc.buffImmune[i] = true;
		}
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		((ModNPC)this).npc.lifeMax = 5000;
		((ModNPC)this).npc.damage = 110;
		((ModNPC)this).npc.defense = 65;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
	{
		if (((ModNPC)this).npc.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/ShadowEvent/FlayerWraithTrail").Width * 0.5f, (float)((ModNPC)this).npc.height * 0.5f);
			for (int i = 0; i < ((ModNPC)this).npc.oldPos.Length; i++)
			{
				SpriteEffects effects = ((((ModNPC)this).npc.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				Vector2 position = ((ModNPC)this).npc.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModNPC)this).npc.gfxOffY);
				Color color = ((ModNPC)this).npc.GetAlpha(drawColor) * ((float)(((ModNPC)this).npc.oldPos.Length - i) / (float)((ModNPC)this).npc.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/ShadowEvent/FlayerWraithTrail"), position, ((ModNPC)this).npc.frame, color, ((ModNPC)this).npc.rotation, vector, ((ModNPC)this).npc.scale, effects, 0f);
			}
		}
		return true;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/FlayerWraithGore1"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/FlayerWraithGore2"));
		return true;
	}

	public override void OnHitPlayer(Player player, int damage, bool crit)
	{
		player.AddBuff(((ModNPC)this).mod.BuffType("DarkDebuff"), 180);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		((ModNPC)this).npc.rotation = ((ModNPC)this).npc.velocity.X * 0.01f;
		Player player = Main.player[((ModNPC)this).npc.target];
		bool expertMode = Main.expertMode;
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
			((ModNPC)this).npc.velocity.X = (float)MoveSpeed * 0.1f;
			if (((ModNPC)this).npc.Center.Y >= player.Center.Y - HomeY && MoveSpeedY >= -30)
			{
				MoveSpeedY--;
				HomeY = 150f;
			}
			else if (((ModNPC)this).npc.Center.Y <= player.Center.Y - HomeY && MoveSpeedY <= 30)
			{
				MoveSpeedY++;
			}
			((ModNPC)this).npc.velocity.Y = (float)MoveSpeedY * 0.1f;
			if (Main.rand.Next(220) == 6)
			{
				HomeY = -36f;
			}
		}
		timer++;
		if (timer == 360)
		{
			Vector2 vector = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
			vector.Normalize();
			vector.X *= 5.5f;
			vector.Y *= 5.5f;
			int num = (expertMode ? 40 : 45);
			Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector.X, vector.Y, ((ModNPC)this).mod.ProjectileType("DarkMatterBoltGreen"), num, 1f, ((ModNPC)this).npc.target, 0f, 0f);
			timer = 0;
		}
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

	public override void NPCLoot()
	{
		if (Main.rand.Next(3) == 0)
		{
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("DarkMatter"), Main.rand.Next(1, 2), false, 0, false, false);
		}
	}
}
