using System.Windows.Forms;
using System.Drawing;

namespace Digger
{
    class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    class Player : ICreature
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
            return "Digger.png";
        }
    }

    class Sack : ICreature
    {
        private int fallDistance = 0;
        CreatureCommand ICreature.Act(int x, int y)
        {
            if (MayFalling(x, y))
                return Fall();
            return Idle();
        }

        public bool MayFalling(int x, int y)
        {
            if (y + 1 < Game.MapHeight)
            {
                var bottomObject = Game.Map[x, y + 1];
                return (bottomObject is Player || bottomObject is Monster) && fallDistance > 0 || bottomObject == null;
            }
            return false;
        }

        public CreatureCommand Fall()
        {
            fallDistance++;
            return new CreatureCommand() { DeltaY = 1 };
        }

        public CreatureCommand Idle()
        {
            var action = new CreatureCommand();
            if (fallDistance > 1)
                action.TransformTo = new Gold();
            fallDistance = 0;
            return action;
        }

        bool ICreature.DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        int ICreature.GetDrawingPriority()
        {
            return 2;
        }

        string ICreature.GetImageFileName()
        {
            return "Sack.png";
        }
    }

    class Gold : ICreature
    {
        CreatureCommand ICreature.Act(int x, int y)
        {
            return new CreatureCommand();
        }

        bool ICreature.DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Player)
            {
                Game.Scores += 10;
                return true;
            }
            return conflictedObject is Monster;
        }

        int ICreature.GetDrawingPriority()
        {
            return 2;
        }

        string ICreature.GetImageFileName()
        {
            return "Gold.png";
        }
    }

    class Monster : ICreature
    {
        private int x = 0;
        private int y = 0;

        CreatureCommand ICreature.Act(int x, int y)
        {
            var playerPosition = GetPlayerLocation();

            if (playerPosition.X != int.MinValue)
            {
                this.x = playerPosition.X - x;
                this.y = playerPosition.Y - y;
            }

            if (playerPosition.X != int.MinValue && CanMoveTo(x, y))
                return Walk();
            return Idle();
        }

        private CreatureCommand Idle()
        {
            return new CreatureCommand();
        }

        private CreatureCommand Walk()
        {
            if (x != 0)
                return new CreatureCommand() { DeltaX = x };
            else if (y != 0)
                return new CreatureCommand() { DeltaY = y };
            return new CreatureCommand();
        }

        private bool CanMoveTo(int x, int y)
        {
            return CanMoveUp(x, y) || CanMoveDown(x, y)
                || CanMoveLeft(x, y) || CanMoveRight(x, y);
        }

        private bool CheckNextCell(int x, int y, int dx, int dy)
        {
            var nextCell = Game.Map[x, y];

            if (nextCell is Player || nextCell is Gold || nextCell == null)
            {
                this.x = dx;
                this.y = dy;
                return true;
            }
            return false;
        }

        private bool CanMoveUp(int x, int y)
        {
            return y - 1 >= 0 && this.y != 0
                && CheckNextCell(x, y - 1, 0, -1);
        }

        private bool CanMoveDown(int x, int y)
        {
            return y + 1 < Game.MapHeight && this.y != 0
                && CheckNextCell(x, y + 1, 0, 1);
        }

        private bool CanMoveLeft(int x, int y)
        {
            return x - 1 >= 0 && this.x != 0
                && CheckNextCell(x - 1, y, -1, 0);
        }

        private bool CanMoveRight(int x, int y)
        {
            return x + 1 < Game.MapWidth && this.x != 0
                && CheckNextCell(x + 1, y, 1, 0);
        }

        public Point GetPlayerLocation()
        {
            for (int x = 0; x < Game.MapWidth; x++)
            {
                for (int y = 0; y < Game.MapHeight; y++)
                {
                    if (Game.Map[x, y] is Player)
                    {
                        return new Point(x, y);
                    }
                }
            }
            return new Point(int.MinValue, int.MinValue);
        }

        bool ICreature.DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Sack
                || conflictedObject is Monster ? true : false;
        }

        int ICreature.GetDrawingPriority()
        {
            return 1;
        }

        string ICreature.GetImageFileName()
        {
            return "Monster.png";
        }
    }
}
