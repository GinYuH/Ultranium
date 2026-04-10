using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent;

public class ShadeSpirit : ModNPC
{
	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Shade Spirit");
		NPCID.Sets.TrailCacheLength[((ModNPC)this).npc.type] = 7;
		NPCID.Sets.TrailingMode[((ModNPC)this).npc.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.width = 30;
		((ModNPC)this).npc.height = 28;
		((ModNPC)this).npc.damage = 70;
		((ModNPC)this).npc.defense = 50;
		((ModNPC)this).npc.lifeMax = 1100;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit36;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath6;
		((ModNPC)this).npc.knockBackResist = 0.9f;
		((ModNPC)this).npc.aiStyle = 91;
		((ModNPC)this).npc.buffImmune[24] = true;
		base.banner = ((ModNPC)this).npc.type;
		base.bannerItem = ((ModNPC)this).mod.ItemType("ShadeSpiritBanner");
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).npc.buffImmune[i] = true;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/ShadowEvent/ShadeSpiritTrail").Width * 0.5f, (float)((ModNPC)this).npc.height * 0.5f);
		for (int i = 0; i < ((ModNPC)this).npc.oldPos.Length; i++)
		{
			Vector2 position = ((ModNPC)this).npc.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModNPC)this).npc.gfxOffY);
			Color color = ((ModNPC)this).npc.GetAlpha(lightColor) * ((float)(((ModNPC)this).npc.oldPos.Length - i) / (float)((ModNPC)this).npc.oldPos.Length);
			spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/ShadowEvent/ShadeSpiritTrail"), position, null, color, ((ModNPC)this).npc.rotation, vector, ((ModNPC)this).npc.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		((ModNPC)this).npc.lifeMax = 2400;
		((ModNPC)this).npc.damage = 110;
		((ModNPC)this).npc.defense = 70;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/ShadeSpiritGore"));
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitPlayer(Player player, int damage, bool crit)
	{
		player.AddBuff(((ModNPC)this).mod.BuffType("DarkDebuff"), 120);
	}

	public override void AI()
	{
		((ModNPC)this).npc.rotation = ((ModNPC)this).npc.velocity.X * 0.06f;
		bool expertMode = Main.expertMode;
		if (((ModNPC)this).npc.localAI[1] == 0f)
		{
			int num = 2;
			for (int i = 0; i < num; i++)
			{
				int num2 = 360 / num;
				int num3 = (expertMode ? 45 : 50);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, 0f, 0f, ((ModNPC)this).mod.ProjectileType("SoulOrbiter"), num3, 0f, Main.myPlayer, (float)(i * num2), (float)((ModNPC)this).npc.whoAmI);
			}
			((ModNPC)this).npc.localAI[1] += 1f;
		}
	}

	public override void NPCLoot()
	{
		if (Main.rand.Next(6) == 0)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DarkMatter"), 1, false, 0, false, false);
		}
	}
}
