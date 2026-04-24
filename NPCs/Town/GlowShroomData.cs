using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Localization;

namespace Ultranium.NPCs.Town;

public class GlowShroomData : CustomCurrencySingleCoin
{
	public Color GlowShroomDataTextColor = Color.Purple;

	public GlowShroomData(int coinItemID, long currencyCap)
		: base(coinItemID, currencyCap)
	{
	}

    public override void GetPriceText(string[] lines, ref int currentLine, long price)
    {
		Color color = GlowShroomDataTextColor * ((float)(int)Main.mouseTextColor / 255f);
		lines[currentLine++] = string.Format("[c/{0:X2}{1:X2}{2:X2}:{3} {4} {5}]", color.R, color.G, color.B, Language.GetTextValue("LegacyTooltip.50"), price, Ultranium.GetTextValue("NPCs.Keeper.Currency"));
	}
}
