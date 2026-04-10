using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.TrueDread;

public class DreadTome : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Paranoia");
		// Tooltip.SetDefault("Casts dread scythes");
	}

	public override void SetDefaults()
	{
		Item.damage = 240;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 13;
		Item.width = 40;
		Item.height = 40;
		Item.useTime = 25;
		Item.useAnimation = 25;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 5f;
		Item.rare = 11;
		Item.value = Item.buyPrice(1);
		Item.UseSound = SoundID.Item20;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("DreadSickle").Type;
		Item.shootSpeed = 11f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(200, 0, 0);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		int num = 2 + Main.rand.Next(2);
		for (int i = 0; i < num; i++)
		{
			Vector2 vector = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(20f));
			float num2 = 1f - Main.rand.NextFloat() * 0.3f;
			vector *= num2;
			Projectile.NewProjectile(null, position.X, position.Y, vector.X, vector.Y, type, damage, knockback, player.whoAmI, 0f, 0f);
		}
		return false;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "NightmareFuel", 10);
		val.AddIngredient((Mod)null, "DreadScale", 6);
		val.AddTile(412);
		val.Register();
	}
}
