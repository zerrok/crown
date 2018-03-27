using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexturePackerMonoGameDefinitions;

namespace crown.Terrain {
    public class Tile {
        String type;
        bool isClear;

        public bool IsClear {
            get {
                return isClear;
            }

            set {
                isClear = value;
            }
        }

        public String Type {
            get {
                return type;
            }

            set {
                type = value;
            }
        }
    }
}
