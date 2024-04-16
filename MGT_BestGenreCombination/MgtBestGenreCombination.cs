using HarmonyLib;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;

namespace MGT_BestGenreCombination
{
    public class MgtBestGenreCombination : MelonMod
    {
    }

    [HarmonyPatch(typeof(Menu_DevGame_Genre), "Init")]
    public static class PatchMenuGUIMainStart
    {
        static void Postfix(Menu_DevGame_Genre __instance, ref GUI_Main ___guiMain_, ref genres ___genres_, ref Menu_DevGame ___mDevGame_)
        {
            // __instance.genreArt 0 = main genre
            // __instance.genreArt 1 = sub genre
            var firstGenreId = __instance.genreArt == 1 ? ___mDevGame_.g_GameMainGenre : ___mDevGame_.g_GameSubGenre;
            var genreButtons = __instance.uiObjects[0].transform.GetComponentsInChildren<Item_DevGame_Genre>();
            foreach (var genreButton in genreButtons)
            {
                var color = Color.white;
                if (firstGenreId != -1 && ___genres_.genres_COMBINATION[firstGenreId, genreButton.myID])
                {
                    color = ___guiMain_.colors[4];
                }

                genreButton.GetComponent<Image>().color = color;
            }
        }
    }
}