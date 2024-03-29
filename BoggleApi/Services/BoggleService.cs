﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoggleApi.Models;

namespace BoggleApi.Services
{
    public class BoggleService : IBoggleService
    {
        private static List<BoggleBox> boggleBoxes = new List<BoggleBox>();

        List<List<char>> letters = new List<List<char>>
        {
            new List<char> {'R', 'I', 'F', 'O', 'B', 'X'},
            new List<char> {'I', 'F', 'E', 'H', 'E', 'Y'},
            new List<char> {'D', 'E', 'N', 'O', 'W', 'S'},
            new List<char> {'U', 'T', 'O', 'K', 'N', 'D'},
            new List<char> {'H', 'M', 'S', 'R', 'A', 'O'},
            new List<char> {'L', 'U', 'P', 'E', 'T', 'S'},
            new List<char> {'A', 'C', 'I', 'T', 'O', 'A'},
            new List<char> {'Y', 'L', 'G', 'K', 'U', 'E'},
            new List<char> {'Q', 'B', 'M', 'J', 'O', 'A'},
            new List<char> {'E', 'H', 'I', 'S', 'P', 'N'},
            new List<char> {'V', 'E', 'T', 'I', 'G', 'N'},
            new List<char> {'B', 'A', 'L', 'I', 'Y', 'T'},
            new List<char> {'E', 'Z', 'A', 'V', 'N', 'D'},
            new List<char> {'R', 'A', 'L', 'E', 'S', 'C'},
            new List<char> {'U', 'W', 'I', 'L', 'R', 'G'},
            new List<char> {'P', 'A', 'C', 'E', 'M', 'D'}
        };

        static Random rnd = new Random();

        public BoggleBox GetBoggleBox()
        {
            List<Die> rolls = new() { };

            foreach (List<char> letterList in letters)
            {
                var die = new Die(letterList[index: rnd.Next(5)]);
                rolls.Add(die);
            }

            rolls = Shuffle(rolls);

            List<List<Die>> dice = new()

            {
                new List<Die> { rolls[0], rolls[1], rolls[2], rolls[3] },
                new List<Die> { rolls[4], rolls[5], rolls[6], rolls[7] },
                new List<Die> { rolls[8], rolls[9], rolls[10], rolls[11] },
                new List<Die> { rolls[12], rolls[13], rolls[14], rolls[15] }


            };

            var boggleBox = new BoggleBox
            {
                BoggleBoxId = Guid.NewGuid(),
                Dice = dice
            };

            boggleBoxes.Add(boggleBox);

            return boggleBox;
        }

        public BoggleBox GetBoggleBox(Guid boggleBoxId)
        {
            BoggleBox boggleBox = null;

            foreach (BoggleBox bb in boggleBoxes)
            {
                if (bb.BoggleBoxId == boggleBoxId)
                {
                    boggleBox = bb;
                    break;
                }
            }
            return boggleBox;
        }


        public static List<Die> Shuffle(List<Die> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                Die value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }

        public bool CheckWordPresent(BoggleBox boggleBox, string word)
        {

            List<List<Die>> dice = boggleBox.Dice;

            string _word = word;

            foreach (List<Die> row in dice)
            {
                foreach (Die die in row)
                {
                    if (_word.Contains(die.Value))
                    {
                        int index = _word.IndexOf(die.Value);
                        _word = (index < 0) ?
                            _word : _word.Remove(index, 1);
                    }
                }
            }


            if (_word.Length == 0)
            {
                return true;
            }

            return false;
        }
    }
}
