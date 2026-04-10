using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
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
		((ModItem)this).DisplayName.SetDefault("Devoted Katana");
		((ModItem)this).Tooltip.SetDefault("Shoots spreads of homing shadow stars\n~Dedicated Item~");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 200;
		((ModItem)this).item.melee = true;
		((Entity)(object)((ModItem)this).item).width = 60;
		((Entity)(object)((ModItem)this).item).height = 78;
		((ModItem)this).item.useTime = 16;
		((ModItem)this).item.useAnimation = 16;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.value = Item.buyPrice(2);
		((ModItem)this).item.UseSound = SoundID.Item1;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("DStar");
		((ModItem)this).item.shootSpeed = 10f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
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
			if (tooltip.mod == "Terraria" && tooltip.Name == "ItemName")
			{
				float amount = (float)(Main.GameUpdateCount % 60) / 60f;
				int num = (int)(Main.GameUpdateCount / 60 % 2);
				tooltip.overrideColor = Color.Lerp(itemNameCycleColors[num], itemNameCycleColors[(num + 1) % 2], amount);
			}
		}
	}
}
