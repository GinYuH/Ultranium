using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Nature;

public class UltraniumBow : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Ultranium Pulse Bow");
		((ModItem)this).Tooltip.SetDefault("Fires a spread of nature arrows\n50% chance to not consume ammo");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 230;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 48;
		((Entity)(object)((ModItem)this).item).height = 74;
		((ModItem)this).item.useTime = 32;
		((ModItem)this).item.useAnimation = 32;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.knockBack = 1f;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.value = Item.buyPrice(1);
		((ModItem)this).item.UseSound = SoundID.Item5;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.useAmmo = AmmoID.Arrow;
		((ModItem)this).item.shoot = 11;
		((ModItem)this).item.shootSpeed = 65f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(241, 166, 0);
	}

	public override bool ConsumeAmmo(Player player)
	{
		return Main.rand.NextFloat() > 0.5f;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		float num = 5f;
		float num2 = MathHelper.ToRadians(10f);
		position += Vector2.Normalize(new Vector2(speedX, speedY)) * 10f;
		for (int i = 0; (float)i < num; i++)
		{
			Vector2 vector = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(0f - num2, num2, (float)i / (num - 1f))) * 0.2f;
			Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, ((ModItem)this).mod.ProjectileType("UltraniumArrow"), damage, knockBack, player.whoAmI, 0f, 0f);
		}
		return false;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-10f, 0f);
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
