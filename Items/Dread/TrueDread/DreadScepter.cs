using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.TrueDread;

public class DreadScepter : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Dread Energy Scepter");
		((ModItem)this).Tooltip.SetDefault("Summons stationary dread orbs to shoot bolts at nearby enemies\nEach orb will last for 30 seconds, and will then explode into a giant circle of bolts upon death\nEach existing orb takes up 1 minion slot");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 26;
		((Entity)(object)((ModItem)this).item).height = 28;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.mana = 30;
		((ModItem)this).item.damage = 200;
		((ModItem)this).item.knockBack = 1f;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.useTime = 30;
		((ModItem)this).item.useAnimation = 30;
		((ModItem)this).item.summon = true;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.UseSound = SoundID.Item117;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("DreadSummonOrb");
		((ModItem)this).item.shootSpeed = 10f;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		Vector2 vector = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
		position = vector;
		return true;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(200, 0, 0);
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
