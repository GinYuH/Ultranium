using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch;

public class Inanis : ModItem
{
	public override void SetStaticDefaults()
	{
		Tooltip.SetDefault("Turns bullets into Void Bolts\nHas a 20% chance to fire out an eldritch blast\n35% chance not to consume ammo");
		DisplayName.SetDefault("Inanis");
	}

	public override void SetDefaults()
	{
		Item.damage = 260;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 20;
		Item.height = 40;
		Item.useTime = 8;
		Item.useAnimation = 8;
		Item.useStyle = 5;
		Item.knockBack = 6f;
		Item.rare = 11;
		Item.value = Item.buyPrice(1, 50);
		Item.UseSound = SoundID.Item11;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("VoidBolt").Type;
		Item.shootSpeed = 16f;
		Item.useAmmo = AmmoID.Bullet;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(0f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		for (int i = 0; i < 1; i++)
		{
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("VoidBolt").Type, Item.damage, knockback, Item.playerIndexTheItemIsReservedFor, 0f, 0f);
		}
		if (Main.rand.Next(5) == 0)
		{
			Vector2 vector = new Vector2(velocity.X, velocity.Y);
			Projectile.NewProjectile(source, position.X, position.Y, vector.X, vector.Y, Mod.Find<ModProjectile>("NoctisBlast").Type, Item.damage, knockback, player.whoAmI, 0f, 0f);
			return false;
		}
		return false;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return Main.rand.NextFloat() > 0.35f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "NightmareScale", 8);
		val.AddIngredient((Mod)null, "NightmareBar", 12);
		val.AddIngredient((Mod)null, "DarkMatter", 10);
		val.AddTile(412);
		val.Register();
	}
}
