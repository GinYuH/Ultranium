using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.TrueDread;

public class FakeDreadHook : ModNPC
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Dread Hook");
		Main.npcFrameCount[NPC.type] = 2;
	}

	public override void SetDefaults()
	{
		NPC.lifeMax = 13000;
		NPC.damage = 60;
		NPC.defense = 45;
		NPC.knockBackResist = 0f;
		NPC.scale = 1.5f;
		NPC.width = 40;
		NPC.height = 38;
		NPC.lavaImmune = true;
		NPC.noGravity = true;
		NPC.noTileCollide = true;
		NPC.netAlways = true;
		NPC.HitSound = SoundID.NPCHit7;
		NPC.DeathSound = SoundID.NPCDeath1;
		NPC.value = Item.buyPrice();
		NPC.npcSlots = 1f;
		NPC.immortal = false;
		NPC.dontTakeDamage = false;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		NPC.lifeMax = 30000;
		NPC.damage = 65;
		NPC.defense = 45;
	}

	public override void FindFrame(int frameHeight)
	{
		if (NPC.velocity.X == 0f || NPC.velocity.Y == 0f)
		{
			NPC.frame.Y = frameHeight;
		}
		if (NPC.velocity.X > 0f || NPC.velocity.Y > 0f || NPC.velocity.X < 0f || NPC.velocity.Y < 0f)
		{
			NPC.frame.Y = 0;
		}
	}

	public override void AI()
	{
		bool flag = false;
		bool flag2 = false;
		NPC nPC = Main.npc[0];
		for (int i = 0; i < Main.npc.Length; i++)
		{
			if (Main.npc[i].type == Mod.Find<ModNPC>("FakeDread").Type)
			{
				nPC = Main.npc[i];
				break;
			}
		}
		if (!NPC.AnyNPCs(Mod.Find<ModNPC>("FakeDread").Type))
		{
			((Entity)NPC).active = false;
		}
		if (Main.player[nPC.target].dead)
		{
			flag2 = true;
		}
		NPC.localAI[0] -= 2f;
		if (Main.netMode == NetmodeID.MultiplayerClient)
		{
			if (NPC.ai[0] == 0f)
			{
				NPC.ai[0] = (int)(NPC.Center.X / 16f);
			}
			if (NPC.ai[1] == 0f)
			{
				NPC.ai[1] = (int)(NPC.Center.X / 16f);
			}
		}
		if (Main.netMode != NetmodeID.MultiplayerClient)
		{
			if (NPC.ai[0] == 0f || NPC.ai[1] == 0f)
			{
				NPC.localAI[0] = 0f;
			}
			NPC.localAI[0] -= 1f;
			if (!flag2 && NPC.localAI[0] <= 0f && NPC.ai[0] != 0f)
			{
				for (int j = 0; j < Main.npc.Length; j++)
				{
					if (j != NPC.whoAmI && ((Entity)Main.npc[j]).active && Main.npc[j].type == NPC.type && (Main.npc[j].velocity.X != 0f || Main.npc[j].velocity.Y != 0f))
					{
						NPC.localAI[0] = Main.rand.Next(60, 300);
					}
				}
			}
			if (NPC.localAI[0] <= 0f)
			{
				NPC.localAI[0] = Main.rand.Next(100, 300);
				bool flag3 = false;
				int num = 0;
				while (!flag3 && num <= 1000)
				{
					num++;
					int num2 = (int)(Main.player[nPC.target].Center.X / 16f);
					int num3 = (int)(Main.player[nPC.target].Center.Y / 16f);
					if (NPC.ai[0] == 0f)
					{
						num2 = (int)((Main.player[nPC.target].Center.X + nPC.Center.X) / 32f);
						num3 = (int)((Main.player[nPC.target].Center.Y + nPC.Center.Y) / 32f);
					}
					if (flag2)
					{
						num2 = (int)nPC.position.X / 16;
						num3 = (int)(nPC.position.Y + 400f) / 16;
					}
					int num4 = 20;
					num4 += (int)(100f * ((float)num / 1000f));
					int num5 = num2 + Main.rand.Next(-num4, num4 + 1);
					int num6 = num3 + Main.rand.Next(-num4, num4 + 1);
					if (nPC.life < nPC.lifeMax / 2 && Main.rand.Next(6) == 0)
					{
						NPC.TargetClosest();
						int num7 = (int)(Main.player[NPC.target].Center.X / 16f);
						int num8 = (int)(Main.player[NPC.target].Center.Y / 16f);
						if (Main.tile[num7, num8].WallType > WallID.None)
						{
							num5 = num7;
							num6 = num8;
						}
					}
					try
					{
						if (WorldGen.SolidTile(num5, num6) || Main.tileSolidTop[Main.tile[num5, num6].TileType] || (Main.tile[num5, num6].WallType > WallID.None && (num > 500 || nPC.life < nPC.lifeMax / 2)))
						{
							flag3 = true;
							NPC.ai[0] = num5;
							NPC.ai[1] = num6;
							NPC.netUpdate = true;
						}
					}
					catch
					{
					}
				}
			}
		}
		if (NPC.ai[0] > 0f && NPC.ai[1] > 0f)
		{
			float num9 = 10f;
			if (nPC.life < nPC.lifeMax / 2)
			{
				num9 = 14f;
			}
			if (nPC.life < nPC.lifeMax / 4)
			{
				num9 = 16f;
			}
			if (Main.expertMode)
			{
				num9 += 1f;
			}
			if (Main.expertMode && nPC.life < nPC.lifeMax / 2)
			{
				num9 += 1f;
			}
			if (flag)
			{
				num9 *= 2f;
			}
			if (flag2)
			{
				num9 *= 2f;
			}
			Vector2 vector = new Vector2(NPC.Center.X, NPC.Center.Y);
			float num10 = NPC.ai[0] * 16f - 8f - vector.X;
			float num11 = NPC.ai[1] * 16f - 8f - vector.Y;
			float num12 = (float)Math.Sqrt(num10 * num10 + num11 * num11);
			if (num12 < 12f + num9)
			{
				NPC.velocity.X = num10;
				NPC.velocity.Y = num11;
			}
			else
			{
				num12 = num9 / num12;
				NPC.velocity.X = num10 * num12;
				NPC.velocity.Y = num11 * num12;
			}
			Vector2 vector2 = new Vector2(NPC.Center.X, NPC.Center.Y);
			float num13 = nPC.Center.X - vector2.X;
			float num14 = nPC.Center.Y - vector2.Y;
			NPC.rotation = (float)Math.Atan2(num14, num13) - 1.57f;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		Texture2D texture = ModContent.Request<Texture2D>("Ultranium/NPCs/Dread/DreadChain").Value;
		NPC nPC = Main.npc[0];
		for (int i = 0; i < Main.npc.Length; i++)
		{
			if (Main.npc[i].type == Mod.Find<ModNPC>("FakeDread").Type)
			{
				nPC = Main.npc[i];
				break;
			}
		}
		Vector2 center = NPC.Center;
		Vector2 center2 = nPC.Center;
		Rectangle? sourceRectangle = null;
		Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
		float num = texture.Height;
		Vector2 vector = center2 - center;
		float rotation = (float)Math.Atan2(vector.Y, vector.X) - 1.57f;
		bool flag = true;
		if (float.IsNaN(center.X) && float.IsNaN(center.Y))
		{
			flag = false;
		}
		if (float.IsNaN(vector.X) && float.IsNaN(vector.Y))
		{
			flag = false;
		}
		while (flag)
		{
			if ((double)vector.Length() < (double)num + 1.0)
			{
				flag = false;
				continue;
			}
			Vector2 vector2 = vector;
			vector2.Normalize();
			center += vector2 * num;
			vector = center2 - center;
			Color color = Lighting.GetColor((int)center.X / 16, (int)((double)center.Y / 16.0));
			color = NPC.GetAlpha(color);
			Main.spriteBatch.Draw(texture, center - Main.screenPosition, sourceRectangle, color, rotation, origin, 1f, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life > 0)
		{
			return;
		}
		for (int i = 0; i < 80; i++)
		{
			int num = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GemRuby, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != NPC.Center)
			{
				Main.dust[num].velocity = NPC.DirectionTo(Main.dust[num].position) * 10f;
			}
		}
	}

	public override bool CheckActive()
	{
		return false;
	}
}
