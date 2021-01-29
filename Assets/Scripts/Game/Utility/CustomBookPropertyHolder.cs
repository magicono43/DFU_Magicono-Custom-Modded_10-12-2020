// Project:         Daggerfall Unity Re-imagined for Daggerfall Unity (http://www.dfworkshop.net)
// Copyright:       Copyright (C) 2020 Kirk.O
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Author:          Kirk.O
// Created On: 	    1/28/2021, 5:30 PM
// Last Edit:		1/28/2021, 5:30 PM
// Version:			1.00
// Special Thanks:  
// Modifier:		

using DaggerfallWorkshop;
using DaggerfallConnect.Arena2;
using DaggerfallWorkshop.Game;
using System.Collections.Generic;
using DaggerfallWorkshop.Game.Items;

public class CustomBookPropertyHolder
{
    public static float[] GetBookProperties(int BookID = -1)
    { // Index Meanings: 0 = Price/Value, 1 = Rarity, 2 = WeightInKG, 3 = TextureRecord Index of File 900 currently 0-36 
        float[] bookProps = { 1, 1, 0.75f, 0 };

        switch (BookID)
        {
            case (int)Religion_Prophecy_Books.The_First_Scroll_of_Baan_Dar: return new float[] { 188, 11, 0.7875f, 22 };
            case (int)Fiction_Books.A_Tale_of_Kieran: return new float[] { 156, 6, 0.825f, 28 };
            case (int)History_Lore_Books.Fragment_On_Artaeum: return new float[] { 200, 14, 0.7725f, 21 };
            case (int)History_Lore_Books.The_Wild_Elves: return new float[] { 115, 8, 0.765f, 4 };
            case (int)History_Lore_Books.Redguards_History_and_Heroes: return new float[] { 62, 2, 0.795f, 19 };
            case (int)Religion_Prophecy_Books.Arkay_The_God: return new float[] { 62, 3, 0.7725f, 7 };
            case (int)Instruction_Research_Books.The_Faerie: return new float[] { 69, 4, 0.765f, 6 };
            case (int)Fiction_Books.Oelanders_Hammer: return new float[] { 135, 8, 0.78f, 15 };
            case (int)History_Lore_Books.The_Pig_Children: return new float[] { 69, 4, 0.765f, 9 };
            case (int)Religion_Prophecy_Books.The_Old_Ways: return new float[] { 212, 15, 0.7725f, 13 };
            case (int)Instruction_Research_Books.Ghraewaj: return new float[] { 84, 5, 0.765f, 5 };
            case (int)Instruction_Research_Books.A_Scholars_Guide_to_Nymphs: return new float[] { 189, 12, 0.78f, 2 };
            case (int)Journals_Logs_Books.The_MemoryStone: return new float[] { 279, 16, 0.795f, 29 };
            case (int)Politics_Law_Books.Legal_Basics: return new float[] { 54, 2, 0.78f, 8 };
            case (int)Faction_Related_Books.The_Brothers_of_Darkness: return new float[] { 162, 11, 0.7725f, 20 };
            case (int)Instruction_Research_Books.Special_Flora_of_Tamriel: return new float[] { 60, 3, 0.765f, 10 };
            case (int)History_Lore_Books.Brief_History_of_the_Empire_Part_I: return new float[] { 37, 1, 0.7725f, 16 };
            case (int)History_Lore_Books.Brief_History_of_the_Empire_Part_II: return new float[] { 65, 3, 0.7725f, 16 };
            case (int)History_Lore_Books.Brief_History_of_the_Empire_Part_III: return new float[] { 100, 6, 0.7725f, 16 };
            case (int)History_Lore_Books.Brief_History_of_the_Empire_Part_IV: return new float[] { 137, 9, 0.7725f, 16 };
            case (int)Fiction_Books.The_Epic_of_the_Grey_Falcon: return new float[] { 65, 3, 0.7725f, 5 };
            case (int)Instruction_Research_Books.Holidays_of_the_Iliac_Bay: return new float[] { 37, 1, 0.7725f, 14 };
            case (int)Fiction_Books.Broken_Diamonds: return new float[] { 62, 3, 0.7725f, 13 };
            case (int)Fiction_Books.The_Legend_of_Lovers_Lament: return new float[] { 84, 5, 0.765f, 11 };
            case (int)Instruction_Research_Books.On_Lycanthropy: return new float[] { 121, 7, 0.78f, 5 };
            case (int)Fiction_Books.The_Story_of_Lyrisius: return new float[] { 192, 14, 0.765f, 1 };
            case (int)Faction_Related_Books.Origin_of_the_Mages_Guild: return new float[] { 37, 1, 0.7725f, 17 };
            case (int)Fiction_Books.The_Asylum_Ball: return new float[] { 156, 10, 0.7725f, 3 };
            case (int)Instruction_Research_Books.Mysticism: return new float[] { 96, 6, 0.765f, 23 };
            case (int)History_Lore_Books.The_Fall_of_the_Usurper: return new float[] { 91, 5, 0.7725f, 15 };
            case (int)Biography_Books.The_Madness_of_Pelagius: return new float[] { 188, 11, 0.7875f, 19 };
            case (int)Biography_Books.The_Real_Barenziah: return new float[] { 135, 7, 0.7875f, 25 };
            case (int)Biography_Books.The_Real_Barenziah_Part_II: return new float[] { 165, 8, 0.8025f, 25 };
            case (int)Biography_Books.The_Real_Barenziah_Part_III: return new float[] { 154, 9, 0.78f, 25 };
            case (int)Biography_Books.The_Real_Barenziah_Part_IV: return new float[] { 222, 10, 0.8175f, 25 };
            case (int)Biography_Books.The_Real_Barenziah_Part_V: return new float[] { 208, 11, 0.795f, 25 };
            case (int)Biography_Books.The_Real_Barenziah_Part_VI: return new float[] { 189, 12, 0.78f, 25 };
            case (int)Biography_Books.The_Real_Barenziah_Part_VII: return new float[] { 232, 14, 0.7875f, 25 };
            case (int)Biography_Books.The_Real_Barenziah_Part_VIII: return new float[] { 306, 16, 0.8025f, 25 };
            case (int)Biography_Books.The_Real_Barenziah_Part_IX: return new float[] { 340, 18, 0.8025f, 25 };
            case (int)Biography_Books.The_Real_Barenziah_Part_X: return new float[] { 385, 20, 0.81f, 25 };
            case (int)Instruction_Research_Books.On_Oblivion: return new float[] { 212, 15, 0.7725f, 19 };
            case (int)Fiction_Books.The_Sage: return new float[] { 210, 12, 0.7875f, 23 };
            case (int)Fiction_Books.The_Light_and_The_Dark: return new float[] { 159, 7, 0.7875f, 16 };
            case (int)Faction_Related_Books.Confessions_of_a_Thief: return new float[] { 96, 6, 0.765f, 5 };
            case (int)Instruction_Research_Books.Vampires_of_the_Iliac_Bay_Part_I: return new float[] { 125, 8, 0.7725f, 11 };
            case (int)Instruction_Research_Books.Vampires_of_the_Iliac_Bay_Part_II: return new float[] { 162, 11, 0.7725f, 11 };
            case (int)Biography_Books.Biography_of_Queen_Barenziah_Vol_I: return new float[] { 36, 1, 0.765f, 7 };
            case (int)Biography_Books.Biography_of_Queen_Barenziah_Vol_II: return new float[] { 72, 4, 0.765f, 7 };
            case (int)History_Lore_Books.A_History_of_Daggerfall: return new float[] { 40, 1, 0.78f, 9 };
            case (int)Fiction_Books.A_Dubious_Tale_of_the_Crystal_Tower: return new float[] { 100, 6, 0.7725f, 2 };
            case (int)Religion_Prophecy_Books.An_Overview_of_Gods_and_Worship: return new float[] { 37, 1, 0.7725f, 10 };
            case (int)Travel_Books.Wayrest_Jewel_of_the_Bay: return new float[] { 75, 4, 0.7725f, 13 };
            case (int)Journals_Logs_Books.Wabbajack: return new float[] { 264, 20, 0.765f, 12 };
            case (int)Biography_Books.Galerion_the_Mystic: return new float[] { 132, 9, 0.765f, 5 };
            case (int)Religion_Prophecy_Books.The_Ebon_Arm: return new float[] { 84, 5, 0.765f, 3 };
            case (int)Plays_Poetry_Riddles_Books.Rude_Song: return new float[] { 34, 1, 0.765f, 6 };
            case (int)History_Lore_Books.Maras_Tear: return new float[] { 81, 4, 0.78f, 10 };
            case (int)Humor_Books.Jokes: return new float[] { 40, 1, 0.78f, 14 };
            case (int)Religion_Prophecy_Books.Ius_Animal_God: return new float[] { 125, 8, 0.7725f, 8 };
            case (int)Fiction_Books.The_Healers_Tale: return new float[] { 108, 7, 0.765f, 14 };
            case (int)Religion_Prophecy_Books.Of_Jephre: return new float[] { 92, 6, 0.765f, 12 };
            case (int)Plays_Poetry_Riddles_Books.Fools_Ebony_Part_the_Oneth: return new float[] { 87, 3, 0.81f, 34 };
            case (int)Plays_Poetry_Riddles_Books.Fools_Ebony_Part_the_Twoth: return new float[] { 154, 5, 0.84f, 34 };
            case (int)Plays_Poetry_Riddles_Books.Fools_Ebony_Part_the_Threeth: return new float[] { 165, 8, 0.81f, 34 };
            case (int)Plays_Poetry_Riddles_Books.Fools_Ebony_Part_the_Fourth: return new float[] { 210, 10, 0.81f, 34 };
            case (int)Plays_Poetry_Riddles_Books.Fools_Ebony_Part_the_Fiveth: return new float[] { 308, 12, 0.84f, 34 };
            case (int)Instruction_Research_Books.Etiquette_With_Rulers: return new float[] { 78, 4, 0.7725f, 15 };
            case (int)Religion_Prophecy_Books.Invocation_of_Azura: return new float[] { 137, 9, 0.7725f, 10 };
            case (int)Fiction_Books.The_Arrowshot_Woman: return new float[] { 69, 4, 0.765f, 3 };
            case (int)Fiction_Books.Bankers_Bet: return new float[] { 42, 1, 0.765f, 6 };
            case (int)History_Lore_Books.The_War_Of_Betony_by_Favte: return new float[] { 130, 8, 0.7725f, 14 };
            case (int)History_Lore_Books.The_War_Of_Betony_by_Newgate: return new float[] { 94, 5, 0.78f, 22 };
            case (int)Travel_Books.The_Alikr: return new float[] { 117, 7, 0.7725f, 4 };
            case (int)Fiction_Books.Divad_the_Singer: return new float[] { 104, 6, 0.7725f, 12 };
            case (int)Notes_Letters_Books.Notes_For_Redguard_History: return new float[] { 202, 13, 0.78f, 0 };
            case (int)Plays_Poetry_Riddles_Books.Fools_Ebony_Part_the_Sixth: return new float[] { 464, 14, 0.8925f, 34 };
            case (int)Biography_Books.Biography_of_Queen_Barenziah_Vol_III: return new float[] { 100, 6, 0.7725f, 7 };
            case (int)Biography_Books.King_Edward_Part_I: return new float[] { 49, 1, 0.8025f, 33 };
            case (int)Biography_Books.King_Edward_Part_II: return new float[] { 112, 3, 0.8475f, 33 };
            case (int)Biography_Books.King_Edward_Part_III: return new float[] { 129, 5, 0.8175f, 33 };
            case (int)Biography_Books.King_Edward_Part_IV: return new float[] { 166, 7, 0.8175f, 33 };
            case (int)Biography_Books.King_Edward_Part_V: return new float[] { 187, 9, 0.8025f, 33 };
            case (int)Biography_Books.King_Edward_Part_VI: return new float[] { 240, 10, 0.825f, 33 };
            case (int)Biography_Books.King_Edward_Part_VII: return new float[] { 259, 12, 0.8175f, 33 };
            case (int)Biography_Books.King_Edward_Part_VIII: return new float[] { 360, 14, 0.8475f, 33 };
            case (int)Biography_Books.King_Edward_Part_IX: return new float[] { 378, 16, 0.8325f, 33 };
            case (int)Biography_Books.King_Edward_Part_X: return new float[] { 340, 18, 0.795f, 33 };
            case (int)Biography_Books.King_Edward_Part_XI: return new float[] { 440, 20, 0.825f, 33 };
            case (int)Biography_Books.King_Edward_Part_XII: return new float[] { 530, 20, 0.8325f, 33 };
            default: return bookProps;
        }
    }
}