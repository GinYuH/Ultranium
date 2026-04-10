using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch;

public class Nihil : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Nihil");
		((ModItem)this).Tooltip.SetDefault("Creates lingering abyss flames around your cursor\nThe abyss flames will slowly chase nearby enemies");
		Item.staff[((ModItem)this).item.type] = true;
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 220;
		((ModItem)this).item.magic = true;
		((ModItem)this).item.mana = 8;
		((Entity)(object)((ModItem)this).item).width = 40;
		((Entity)(object)((ModItem)this).item).height = 40;
		((ModItem)this).item.useTime = 8;
		((ModItem)this).item.useAnimation = 8;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.knockBack = 5f;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.value = Item.buyPrice(1, 50);
		((ModItem)this).item.UseSound = SoundID.Item20;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("NihilFlame");
		((ModItem)this).item.shootSpeed = 1f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(34, 166, 118);
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		Vector2 vector = new Vector2(Main.mouseX, Main.mouseY) + Main.screenPosition;
		Projectile.NewProjectile(vector.X + (float)Main.rand.Next(-100, 100), vector.Y + (float)Main.rand.Next(-100, 100), 0f, 0f, type, damage, knockBack, player.whoAmI, 0f, 0f);
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
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "NightmareScale", 8);
		val.AddIngredient((Mod)null, "NightmareBar", 12);
		val.AddIngredient((Mod)null, "DarkMatter", 10);
		val.AddTile(412);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
