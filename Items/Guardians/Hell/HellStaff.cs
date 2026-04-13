using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Hell;

public class HellStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Purgatory Staff");
		//Tooltip.SetDefault("Conjures spreads of flaming bolts");
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.damage = 130;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 22;
		Item.width = 58;
		Item.height = 56;
		Item.useTime = 12;
		Item.useAnimation = 12;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 5f;
		Item.rare = 11;
		Item.value = Item.buyPrice(1);
		Item.UseSound = SoundID.Item20;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("HellBeam").Type;
		Item.shootSpeed = 0.5f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Projectile.NewProjectile(source, Main.MouseWorld.X + (float)Main.rand.Next(0, 0), player.Center.Y - -500f + (float)Main.rand.Next(-50, -50), 0f, (float)Main.rand.Next(-15, -15), Mod.Find<ModProjectile>("HellBeam").Type, Item.damage, knockback, player.whoAmI, 0f, 0f);
		return false;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(241, 166, 0);
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
