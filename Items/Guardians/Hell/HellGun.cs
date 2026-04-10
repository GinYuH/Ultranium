using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Hell;

public class HellGun : ModItem
{
	private int Use;

	public override void SetStaticDefaults()
	{
		((ModItem)this).Tooltip.SetDefault("Turns bullets into flaming blasts\nShoots a giant flame blast every 30 shots\nThe giant blast will deal double the weapon's damage\n50% chance to not consume ammo");
		((ModItem)this).DisplayName.SetDefault("Nether Blaster");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 140;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 58;
		((Entity)(object)((ModItem)this).item).height = 26;
		((ModItem)this).item.useTime = 8;
		((ModItem)this).item.useAnimation = 8;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.UseSound = SoundID.Item40;
		((ModItem)this).item.value = Item.buyPrice(1);
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = 242;
		((ModItem)this).item.shootSpeed = 12f;
		((ModItem)this).item.useAmmo = AmmoID.Bullet;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		Vector2 vector = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(4f));
		speedX = vector.X;
		speedY = vector.Y;
		type = ((ModItem)this).mod.ProjectileType("FlamingBulletBlast");
		Use++;
		if (Use >= 30)
		{
			Vector2 vector2 = new Vector2(speedX, speedY).RotatedBy(Math.PI / (double)(Main.rand.Next(72, 1800) / 10));
			Projectile.NewProjectile(position.X, position.Y, vector2.X, vector2.Y, ((ModItem)this).mod.ProjectileType("FlamingBulletBlastBig"), ((ModItem)this).item.damage * 2, knockBack, player.whoAmI, 0f, 0f);
			Use = 0;
			return false;
		}
		return true;
	}

	public override bool ConsumeAmmo(Player player)
	{
		return Main.rand.NextFloat() > 0.5f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(241, 166, 0);
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-6f, 0f);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "HellShard", 10);
		val.AddTile(412);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
