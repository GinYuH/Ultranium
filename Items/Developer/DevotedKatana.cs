using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Developer;

public class DevotedKatana : ModItem
{
	private Color[] itemNameCycleColors = new Color[2]
	{
		new Color(255, 65, 176),
		new Color(132, 22, 199)
	};

	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Devoted Katana");
		// ((ModItem)this).Tooltip.SetDefault("Shoots spreads of homing shadow stars\n~Dedicated Item~");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 200;
		((ModItem)this).Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		((Entity)(object)((ModItem)this).Item).width = 60;
		((Entity)(object)((ModItem)this).Item).height = 78;
		((ModItem)this).Item.useTime = 16;
		((ModItem)this).Item.useAnimation = 16;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.knockBack = 6f;
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.value = Item.buyPrice(2);
		((ModItem)this).Item.UseSound = SoundID.Item1;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("DStar").Type;
		((ModItem)this).Item.shootSpeed = 10f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 vector = Vector2.Normalize(new Vector2(speedX, speedY)) * 100f;
		if (Collision.CanHit(position, 0, 0, position + vector, 0, 0))
		{
			position += vector;
		}
		int num = Main.rand.Next(3, 4);
		for (int i = 0; i < num; i++)
		{
			Projectile.NewProjectile(position, new Vector2(speedX, speedY).RotatedByRandom(0.19634954631328583), type, damage, knockBack, player.whoAmI, 0f, 0f);
		}
		return false;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		foreach (TooltipLine tooltip in tooltips)
		{
			if (tooltip.Mod == "Terraria" && tooltip.Name == "ItemName")
			{
				float amount = (float)(Main.GameUpdateCount % 60) / 60f;
				int num = (int)(Main.GameUpdateCount / 60 % 2);
				tooltip.OverrideColor = Color.Lerp(itemNameCycleColors[num], itemNameCycleColors[(num + 1) % 2], amount);
			}
		}
	}
}
