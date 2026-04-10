using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch;

public class Nihil : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Nihil");
		// Tooltip.SetDefault("Creates lingering abyss flames around your cursor\nThe abyss flames will slowly chase nearby enemies");
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.damage = 220;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 8;
		Item.width = 40;
		Item.height = 40;
		Item.useTime = 8;
		Item.useAnimation = 8;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 5f;
		Item.rare = 11;
		Item.value = Item.buyPrice(1, 50);
		Item.UseSound = SoundID.Item20;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("NihilFlame").Type;
		Item.shootSpeed = 1f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 vector = new Vector2(Main.mouseX, Main.mouseY) + Main.screenPosition;
		Projectile.NewProjectile(null, vector.X + (float)Main.rand.Next(-100, 100), vector.Y + (float)Main.rand.Next(-100, 100), 0f, 0f, type, damage, knockBack, player.whoAmI, 0f, 0f);
		return false;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "NightmareScale", 8);
		val.AddIngredient((Mod)null, "NightmareBar", 12);
		val.AddIngredient((Mod)null, "DarkMatter", 10);
		val.AddTile(412);
		val.Register();
	}
}
