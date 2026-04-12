using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Cosmic;

public class CosmicStaff : ModItem
{
	private Color[] itemNameCycleColors = new Color[2]
	{
		new Color(93, 215, 195),
		new Color(72, 37, 169)
	};

	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Protostar Scepter");
		Tooltip.SetDefault("Casts protostars that explode into bolts");
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.damage = 380;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 10;
		Item.width = 40;
		Item.height = 40;
		Item.useTime = 16;
		Item.useAnimation = 16;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 5f;
		Item.value = Item.buyPrice(2);
		Item.rare = 11;
		Item.UseSound = SoundID.Item20;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("Protostar").Type;
		Item.shootSpeed = 10f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		foreach (TooltipLine tooltip in tooltips)
		{
			if (tooltip.Mod == "Terraria" && tooltip.Name == "ItemName")
			{
				float amount = (float)(Main.GameUpdateCount % 60) / 60f;
				int num = (int)(Main.GameUpdateCount / 60 % 2);
				tooltip.OverrideColor = Color.Lerp(itemNameCycleColors[num], itemNameCycleColors[(num + 1) % 2], amount);
			}
		}
	}
}
