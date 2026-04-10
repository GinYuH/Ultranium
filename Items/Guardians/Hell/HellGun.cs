using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Hell;

public class HellGun : ModItem
{
	private int Use;

	public override void SetStaticDefaults()
	{
		// ((ModItem)this).Tooltip.SetDefault("Turns bullets into flaming blasts\nShoots a giant flame blast every 30 shots\nThe giant blast will deal double the weapon's damage\n50% chance to not consume ammo");
		// ((ModItem)this).DisplayName.SetDefault("Nether Blaster");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 140;
		((ModItem)this).Item.DamageType = DamageClass.Ranged;
		((Entity)(object)((ModItem)this).Item).width = 58;
		((Entity)(object)((ModItem)this).Item).height = 26;
		((ModItem)this).Item.useTime = 8;
		((ModItem)this).Item.useAnimation = 8;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.knockBack = 6f;
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.UseSound = SoundID.Item40;
		((ModItem)this).Item.value = Item.buyPrice(1);
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = 242;
		((ModItem)this).Item.shootSpeed = 12f;
		((ModItem)this).Item.useAmmo = AmmoID.Bullet;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 vector = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(4f));
		speedX = vector.X;
		speedY = vector.Y;
		type = ((ModItem)this).Mod.Find<ModProjectile>("FlamingBulletBlast").Type;
		Use++;
		if (Use >= 30)
		{
			Vector2 vector2 = new Vector2(speedX, speedY).RotatedBy(Math.PI / (double)(Main.rand.Next(72, 1800) / 10));
			Projectile.NewProjectile(position.X, position.Y, vector2.X, vector2.Y, ((ModItem)this).Mod.Find<ModProjectile>("FlamingBulletBlastBig").Type, ((ModItem)this).Item.damage * 2, knockBack, player.whoAmI, 0f, 0f);
			Use = 0;
			return false;
		}
		return true;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return Main.rand.NextFloat() > 0.5f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(241, 166, 0);
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
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "HellShard", 10);
		val.AddTile(412);
		val.Register();
	}
}
