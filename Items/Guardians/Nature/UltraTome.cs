using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Nature;

public class UltraTome : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Ultranium Grimoire");
		// ((ModItem)this).Tooltip.SetDefault("Creates a circle of nature blasts that close in on the cursor");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 210;
		((ModItem)this).Item.DamageType = DamageClass.Magic;
		((ModItem)this).Item.mana = 18;
		((Entity)(object)((ModItem)this).Item).width = 28;
		((Entity)(object)((ModItem)this).Item).height = 32;
		((ModItem)this).Item.useTime = 60;
		((ModItem)this).Item.useAnimation = 60;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.knockBack = 5f;
		((ModItem)this).Item.value = 10000;
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.value = Item.buyPrice(1);
		((ModItem)this).Item.UseSound = SoundID.Item20;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.scale = 0.8f;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("NatureBlastBase").Type;
		((ModItem)this).Item.shootSpeed = 0f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(241, 166, 0);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 vector = new Vector2(Main.mouseX, Main.mouseY) + Main.screenPosition;
		Projectile.NewProjectile(vector.X, vector.Y, 0f, 0f, type, damage, knockBack, player.whoAmI, 0f, 0f);
		return false;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "UltrumShard", 10);
		val.AddTile(412);
		val.Register();
	}
}
