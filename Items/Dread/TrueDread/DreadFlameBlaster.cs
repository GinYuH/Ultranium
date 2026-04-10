using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.TrueDread;

public class DreadFlameBlaster : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).Tooltip.SetDefault("'Why melt enemies when you can scorch them with fear?'\nSpews out a stream of dread fire\nWill also randomly fire out dread fire balls\n80% chance to not consume gel");
		// ((ModItem)this).DisplayName.SetDefault("Foreboding Flame");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 170;
		((ModItem)this).Item.DamageType = DamageClass.Ranged;
		((Entity)(object)((ModItem)this).Item).width = 58;
		((Entity)(object)((ModItem)this).Item).height = 26;
		((ModItem)this).Item.useTime = 5;
		((ModItem)this).Item.useAnimation = 5;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.knockBack = 6f;
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.UseSound = SoundID.Item34;
		((ModItem)this).Item.value = Item.buyPrice(1);
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("DreadParticleBolt").Type;
		((ModItem)this).Item.shootSpeed = 8f;
		((ModItem)this).Item.useAmmo = AmmoID.Gel;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return Main.rand.NextFloat() > 0.8f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(200, 0, 0);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (Main.rand.Next(5) == 0)
		{
			Vector2 vector = new Vector2(speedX, speedY).RotatedBy(Math.PI / (double)(Main.rand.Next(72, 1800) / 10));
			Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, ((ModItem)this).Mod.Find<ModProjectile>("DreadFlameBall").Type, ((ModItem)this).Item.damage, knockBack, player.whoAmI, 0f, 0f);
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
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "NightmareFuel", 10);
		val.AddIngredient((Mod)null, "DreadScale", 6);
		val.AddTile(412);
		val.Register();
	}
}
