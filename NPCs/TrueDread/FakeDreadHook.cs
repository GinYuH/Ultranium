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
		((ModNPC)this).DisplayName.SetDefault("Dread Hook");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 2;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.lifeMax = 13000;
		((ModNPC)this).npc.damage = 60;
		((ModNPC)this).npc.defense = 45;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.scale = 1.5f;
		((ModNPC)this).npc.width = 40;
		((ModNPC)this).npc.height = 38;
		((ModNPC)this).npc.lavaImmune = true;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.noTileCollide = true;
		((ModNPC)this).npc.netAlways = true;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit7;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath1;
		((ModNPC)this).npc.value = Item.buyPrice();
		((ModNPC)this).npc.npcSlots = 1f;
		((ModNPC)this).npc.immortal = false;
		((ModNPC)this).npc.dontTakeDamage = false;
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		((ModNPC)this).npc.lifeMax = 30000;
		((ModNPC)this).npc.damage = 65;
		((ModNPC)this).npc.defense = 45;
	}

	public override void FindFrame(int frameHeight)
	{
		if (((ModNPC)this).npc.velocity.X == 0f || ((ModNPC)this).npc.velocity.Y == 0f)
		{
			((ModNPC)this).npc.frame.Y = frameHeight;
		}
		if (((ModNPC)this).npc.velocity.X > 0f || ((ModNPC)this).npc.velocity.Y > 0f || ((ModNPC)this).npc.velocity.X < 0f || ((ModNPC)this).npc.velocity.Y < 0f)
		{
			((ModNPC)this).npc.frame.Y = 0;
		}
	}

	public override void AI()
	{
		bool flag = false;
		bool flag2 = false;
		NPC nPC = Main.npc[0];
		for (int i = 0; i < Main.npc.Length; i++)
		{
			if (Main.npc[i].type == ((ModNPC)this).mod.NPCType("FakeDread"))
			{
				nPC = Main.npc[i];
				break;
			}
		}
		if (!NPC.AnyNPCs(((ModNPC)this).mod.NPCType("FakeDread")))
		{
			((Entity)((ModNPC)this).npc).active = false;
		}
		if (Main.player[nPC.target].dead)
		{
			flag2 = true;
		}
		((ModNPC)this).npc.localAI[0] -= 2f;
		if (Main.netMode == 1)
		{
			if (((ModNPC)this).npc.ai[0] == 0f)
			{
				((ModNPC)this).npc.ai[0] = (int)(((ModNPC)this).npc.Center.X / 16f);
			}
			if (((ModNPC)this).npc.ai[1] == 0f)
			{
				((ModNPC)this).npc.ai[1] = (int)(((ModNPC)this).npc.Center.X / 16f);
			}
		}
		if (Main.netMode != 1)
		{
			if (((ModNPC)this).npc.ai[0] == 0f || ((ModNPC)this).npc.ai[1] == 0f)
			{
				((ModNPC)this).npc.localAI[0] = 0f;
			}
			((ModNPC)this).npc.localAI[0] -= 1f;
			if (!flag2 && ((ModNPC)this).npc.localAI[0] <= 0f && ((ModNPC)this).npc.ai[0] != 0f)
			{
				for (int j = 0; j < Main.npc.Length; j++)
				{
					if (j != ((ModNPC)this).npc.whoAmI && ((Entity)Main.npc[j]).active && Main.npc[j].type == ((ModNPC)this).npc.type && (Main.npc[j].velocity.X != 0f || Main.npc[j].velocity.Y != 0f))
					{
						((ModNPC)this).npc.localAI[0] = Main.rand.Next(60, 300);
					}
				}
			}
			if (((ModNPC)this).npc.localAI[0] <= 0f)
			{
				((ModNPC)this).npc.localAI[0] = Main.rand.Next(100, 300);
				bool flag3 = false;
				int num = 0;
				while (!flag3 && num <= 1000)
				{
					num++;
					int num2 = (int)(Main.player[nPC.target].Center.X / 16f);
					int num3 = (int)(Main.player[nPC.target].Center.Y / 16f);
					if (((ModNPC)this).npc.ai[0] == 0f)
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
						((ModNPC)this).npc.TargetClosest();
						int num7 = (int)(Main.player[((ModNPC)this).npc.target].Center.X / 16f);
						int num8 = (int)(Main.player[((ModNPC)this).npc.target].Center.Y / 16f);
						if (Main.tile[num7, num8].wall > 0)
						{
							num5 = num7;
							num6 = num8;
						}
					}
					try
					{
						if (WorldGen.SolidTile(num5, num6) || Main.tileSolidTop[Main.tile[num5, num6].type] || (Main.tile[num5, num6].wall > 0 && (num > 500 || nPC.life < nPC.lifeMax / 2)))
						{
							flag3 = true;
							((ModNPC)this).npc.ai[0] = num5;
							((ModNPC)this).npc.ai[1] = num6;
							((ModNPC)this).npc.netUpdate = true;
						}
					}
					catch
					{
					}
				}
			}
		}
		if (((ModNPC)this).npc.ai[0] > 0f && ((ModNPC)this).npc.ai[1] > 0f)
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
			Vector2 vector = new Vector2(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y);
			float num10 = ((ModNPC)this).npc.ai[0] * 16f - 8f - vector.X;
			float num11 = ((ModNPC)this).npc.ai[1] * 16f - 8f - vector.Y;
			float num12 = (float)Math.Sqrt(num10 * num10 + num11 * num11);
			if (num12 < 12f + num9)
			{
				((ModNPC)this).npc.velocity.X = num10;
				((ModNPC)this).npc.velocity.Y = num11;
			}
			else
			{
				num12 = num9 / num12;
				((ModNPC)this).npc.velocity.X = num10 * num12;
				((ModNPC)this).npc.velocity.Y = num11 * num12;
			}
			Vector2 vector2 = new Vector2(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y);
			float num13 = nPC.Center.X - vector2.X;
			float num14 = nPC.Center.Y - vector2.Y;
			((ModNPC)this).npc.rotation = (float)Math.Atan2(num14, num13) - 1.57f;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Texture2D texture = ModContent.GetTexture("Ultranium/NPCs/Dread/DreadChain");
		NPC nPC = Main.npc[0];
		for (int i = 0; i < Main.npc.Length; i++)
		{
			if (Main.npc[i].type == ((ModNPC)this).mod.NPCType("FakeDread"))
			{
				nPC = Main.npc[i];
				break;
			}
		}
		Vector2 center = ((ModNPC)this).npc.Center;
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
			color = ((ModNPC)this).npc.GetAlpha(color);
			Main.spriteBatch.Draw(texture, center - Main.screenPosition, sourceRectangle, color, rotation, origin, 1f, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void HitEffect(int hitDirection, double damage)
	{
		if (((ModNPC)this).npc.life > 0)
		{
			return;
		}
		for (int i = 0; i < 80; i++)
		{
			int num = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 90, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModNPC)this).npc.Center)
			{
				Main.dust[num].velocity = ((ModNPC)this).npc.DirectionTo(Main.dust[num].position) * 10f;
			}
		}
	}

	public override bool CheckActive()
	{
		return false;
	}
}
