using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.TrueDread;

public class DreadFlameBlaster : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).Tooltip.SetDefault("'Why melt enemies when you can scorch them with fear?'\nSpews out a stream of dread fire\nWill also randomly fire out dread fire balls\n80% chance to not consume gel");
		((ModItem)this).DisplayName.SetDefault("Foreboding Flame");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 170;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 58;
		((Entity)(object)((ModItem)this).item).height = 26;
		((ModItem)this).item.useTime = 5;
		((ModItem)this).item.useAnimation = 5;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.UseSound = SoundID.Item34;
		((ModItem)this).item.value = Item.buyPrice(1);
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("DreadParticleBolt");
		((ModItem)this).item.shootSpeed = 8f;
		((ModItem)this).item.useAmmo = AmmoID.Gel;
	}

	public override bool ConsumeAmmo(Player player)
	{
		return Main.rand.NextFloat() > 0.8f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(200, 0, 0);
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		if (Main.rand.Next(5) == 0)
		{
			Vector2 vector = new Vector2(speedX, speedY).RotatedBy(Math.PI / (double)(Main.rand.Next(72, 1800) / 10));
			Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, ((ModItem)this).mod.ProjectileType("DreadFlameBall"), ((ModItem)this).item.damage, knockBack, player.whoAmI, 0f, 0f);
			return false;
		}
		return true;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-6f, 0f);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "NightmareFuel", 10);
		val.AddIngredient((Mod)null, "DreadScale", 6);
		val.AddTile(412);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
