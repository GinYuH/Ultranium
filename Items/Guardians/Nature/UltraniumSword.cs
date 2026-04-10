using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Nature;

public class UltraniumSword : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Ultranium Blade");
		// ((ModItem)this).Tooltip.SetDefault("Fires a spread of nature energy orbs");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 195;
		((ModItem)this).Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		((Entity)(object)((ModItem)this).Item).width = 48;
		((Entity)(object)((ModItem)this).Item).height = 48;
		((ModItem)this).Item.useTime = 18;
		((ModItem)this).Item.useAnimation = 18;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.knockBack = 6f;
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.value = Item.buyPrice(1);
		((ModItem)this).Item.UseSound = SoundID.Item1;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("UltraniumOrb").Type;
		((ModItem)this).Item.shootSpeed = 16f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(241, 166, 0);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 vector = Vector2.Normalize(new Vector2(speedX, speedY)) * 100f;
		if (Collision.CanHit(position, 0, 0, position + vector, 0, 0))
		{
			position += vector;
		}
		int num = Main.rand.Next(3, 4);
		for (int i = 0; i < num; i++)
		{
			Projectile.NewProjectile(position, new Vector2(speedX, speedY).RotatedByRandom(0.19634954631328583), type, damage, knockBack, player.whoAmI, 0f, 0f);
		}
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
