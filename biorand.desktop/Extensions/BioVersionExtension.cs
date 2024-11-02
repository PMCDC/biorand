using IntelOrca.Biohazard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biorand.desktop.Extensions;

public static class BioVersionExtension
{
    public static string GetPlayer0(this BioVersion bioVersion)
    {
        return GetPlayerNameByIndex(bioVersion, 0);
    }

    public static string GetPlayer1(this BioVersion bioVersion)
    {
        return GetPlayerNameByIndex(bioVersion, 1);
    }

    private static string GetPlayerNameByIndex(BioVersion bioVersion, int playerIndex)
    {
        switch (bioVersion)
        {
            case BioVersion.Biohazard1:
                return playerIndex == 0 ? "Chris" : "Jill";
            case BioVersion.Biohazard2:
                return playerIndex == 0 ? "Leon" : "Claire";
            case BioVersion.Biohazard3:
                return playerIndex == 0 ? "Jill" : string.Empty;
            case BioVersion.BiohazardCv:
                return playerIndex == 0 ? "Claire" : string.Empty;
        }

        return string.Empty;
    }
}
