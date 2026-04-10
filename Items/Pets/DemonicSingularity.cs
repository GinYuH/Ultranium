using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Buffs.Pet;
using Ultranium.Projectiles.Pets;

namespace Ultranium.Items.Pets;

public class DemonicSingularity : ModItem
{
	private Color[] itemNameCycleColors = new Color[2]
	{
		new Color(210, 0, 0),
		new Color(230, 160, 0)
	};

	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Demonic Singularity");
		((ModItem)this).Tooltip.SetDefault("Summons a pet cacodemon\n~Developer Item~");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 16;
		((Entity)(object)((ModItem)this).item).height = 30;
		((ModItem)this).item.damage = 0;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.useAnimation = 20;
		((ModItem)this).item.useTime = 20;
		((ModItem)this).item.UseSound = SoundID.Item2;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.value = Item.sellPrice(0, 20);
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.shoot = ModContent.ProjectileType<Cacodemon>();
		((ModItem)this).item.buffType = ModContent.BuffType<CacodemonBuff>();
	}

	public override void UseStyle(Player player)
	{
		if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
		{
			player.AddBuff(((ModItem)this).item.buffType, 3600, fromNetPvP: true);
		}
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
