using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Nature;

public class UltraniumSword : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Ultranium Blade");
		((ModItem)this).Tooltip.SetDefault("Fires a spread of nature energy orbs");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 195;
		((ModItem)this).item.melee = true;
		((Entity)(object)((ModItem)this).item).width = 48;
		((Entity)(object)((ModItem)this).item).height = 48;
		((ModItem)this).item.useTime = 18;
		((ModItem)this).item.useAnimation = 18;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.value = Item.buyPrice(1);
		((ModItem)this).item.UseSound = SoundID.Item1;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("UltraniumOrb");
		((ModItem)this).item.shootSpeed = 16f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(241, 166, 0);
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
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
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "UltrumShard", 10);
		val.AddTile(412);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
