using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch;

public class Inanis : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).Tooltip.SetDefault("Turns bullets into Void Bolts\nHas a 20% chance to fire out an eldritch blast\n35% chance not to consume ammo");
		((ModItem)this).DisplayName.SetDefault("Inanis");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 260;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 20;
		((Entity)(object)((ModItem)this).item).height = 40;
		((ModItem)this).item.useTime = 8;
		((ModItem)this).item.useAnimation = 8;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.value = Item.buyPrice(1, 50);
		((ModItem)this).item.UseSound = SoundID.Item11;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("VoidBolt");
		((ModItem)this).item.shootSpeed = 16f;
		((ModItem)this).item.useAmmo = AmmoID.Bullet;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(34, 166, 118);
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(0f, 0f);
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		for (int i = 0; i < 1; i++)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ((ModItem)this).mod.ProjectileType("VoidBolt"), ((ModItem)this).item.damage, knockBack, ((ModItem)this).item.owner, 0f, 0f);
		}
		if (Main.rand.Next(5) == 0)
		{
			Vector2 vector = new Vector2(speedX, speedY);
			Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, ((ModItem)this).mod.ProjectileType("NoctisBlast"), ((ModItem)this).item.damage, knockBack, player.whoAmI, 0f, 0f);
			return false;
		}
		return false;
	}

	public override bool ConsumeAmmo(Player player)
	{
		return Main.rand.NextFloat() > 0.35f;
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
