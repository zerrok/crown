using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crown.Terrain
{
    class GeneratorParameters
    {

        int size;
        int growChance;
        int minSourceAmount;
        int maxSourceAmount;

        public GeneratorParameters(int size, int growChance, int minSourceAmount, int maxSourceAmount)
        {
            this.size = size;
            this.growChance = growChance;
            this.minSourceAmount = minSourceAmount;
            this.maxSourceAmount = maxSourceAmount;
        }

        public int Size {
            get {
                return size;
            }

            set {
                size = value;
            }
        }

        public int GrowChance {
            get {
                return growChance;
            }

            set {
                growChance = value;
            }
        }

        public int MinSourceAmount {
            get {
                return minSourceAmount;
            }

            set {
                minSourceAmount = value;
            }
        }

        public int MaxSourceAmount {
            get {
                return maxSourceAmount;
            }

            set {
                maxSourceAmount = value;
            }
        }
    }
}
