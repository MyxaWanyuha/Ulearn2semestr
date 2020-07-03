using System.Windows.Forms;
using System.Drawing;

namespace Digger.Objects.Characters
{
    class Player2 : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            var move = new CreatureCommand();
            var key = Game.KeyPressed;

            switch (key)
            {
                case Keys.Up:
                    if (y - 1 >= 0 && CanMoveTo(x, y - 1)) move.DeltaY = -1;
                    break;
                case Keys.Down:
                    if (y + 1 < Game.MapHeight && CanMoveTo(x, y + 1)) move.DeltaY = 1;
                    break;
                case Keys.Left:
                    if (x - 1 >= 0 && CanMoveTo(x - 1, y)) move.DeltaX = -1;
                    break;
                case Keys.Right:
                    if (x + 1 < Game.MapWidth && CanMoveTo(x + 1, y)) move.DeltaX = 1;
                    break;
            }
            return move;
        }

        public bool CanMoveTo(int x, int y)
        {
            return !(Game.Map[x, y] is Sack);
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Sack || conflictedObject is Monster ? true : false;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "P_Idle.png";
        }
    }

}
