using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch;

public class Noctis : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Noctis");
		// ((ModItem)this).Tooltip.SetDefault("Fires a spread of eldritch knives");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.scale = 1f;
		((ModItem)this).Item.damage = 210;
		((ModItem)this).Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		((Entity)(object)((ModItem)this).Item).width = 80;
		((Entity)(object)((ModItem)this).Item).height = 80;
		((ModItem)this).Item.useTime = 25;
		((ModItem)this).Item.useAnimation = 25;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.knockBack = 6f;
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.value = Item.buyPrice(1, 50);
		((ModItem)this).Item.UseSound = SoundID.Item1;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.noUseGraphic = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("EldritchKnife").Type;
		((ModItem)this).Item.shootSpeed = 22f;
		((ModItem)this).Item.value = Item.buyPrice(1, 50);
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		float num = 5f;
		float num2 = MathHelper.ToRadians(25f);
		position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
		for (int i = 0; (float)i < num; i++)
		{
			Vector2 vector = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(0f - num2, num2, (float)i / (num - 1f))) * 0.2f;
			Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
		}
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
