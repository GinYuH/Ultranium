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
		//Tooltip.SetDefault("Turns bullets into flaming blasts\nShoots a giant flame blast every 30 shots\nThe giant blast will deal double the weapon's damage\n50% chance to not consume ammo");
		//DisplayName.SetDefault("Nether Blaster");
	}

	public override void SetDefaults()
	{
		Item.damage = 140;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 58;
		Item.height = 26;
		Item.useTime = 8;
		Item.useAnimation = 8;
		Item.useStyle = 5;
		Item.knockBack = 6f;
		Item.rare = 11;
		Item.UseSound = SoundID.Item40;
		Item.value = Item.buyPrice(1);
		Item.autoReuse = true;
		Item.shoot = 242;
		Item.shootSpeed = 12f;
		Item.useAmmo = AmmoID.Bullet;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 vector = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(4f));
		velocity.X = vector.X;
		velocity.Y = vector.Y;
		type = Mod.Find<ModProjectile>("FlamingBulletBlast").Type;
		Use++;
		if (Use >= 30)
		{
			Vector2 vector2 = new Vector2(velocity.X, velocity.Y).RotatedBy(Math.PI / (double)(Main.rand.Next(72, 1800) / 10));
			Projectile.NewProjectile(source, position.X, position.Y, vector2.X, vector2.Y, Mod.Find<ModProjectile>("FlamingBulletBlastBig").Type, Item.damage * 2, knockback, player.whoAmI, 0f, 0f);
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
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "HellShard", 10);
		val.AddTile(412);
		val.Register();
	}
}
