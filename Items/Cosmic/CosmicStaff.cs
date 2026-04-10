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
		((ModItem)this).DisplayName.SetDefault("Protostar Scepter");
		((ModItem)this).Tooltip.SetDefault("Casts protostars that explode into bolts");
		Item.staff[((ModItem)this).item.type] = true;
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 380;
		((ModItem)this).item.magic = true;
		((ModItem)this).item.mana = 10;
		((Entity)(object)((ModItem)this).item).width = 40;
		((Entity)(object)((ModItem)this).item).height = 40;
		((ModItem)this).item.useTime = 16;
		((ModItem)this).item.useAnimation = 16;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.knockBack = 5f;
		((ModItem)this).item.value = Item.buyPrice(2);
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.UseSound = SoundID.Item20;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("Protostar");
		((ModItem)this).item.shootSpeed = 10f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		foreach (TooltipLine tooltip in tooltips)
		{
			if (tooltip.mod == "Terraria" && tooltip.Name == "ItemName")
			{
				float amount = (float)(Main.GameUpdateCount % 60) / 60f;
				int num = (int)(Main.GameUpdateCount / 60 % 2);
				tooltip.overrideColor = Color.Lerp(itemNameCycleColors[num], itemNameCycleColors[(num + 1) % 2], amount);
			}
		}
	}
}
