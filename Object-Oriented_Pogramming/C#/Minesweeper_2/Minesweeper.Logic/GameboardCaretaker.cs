using System.Collections.Generic;

namespace Minesweeper
{
    public class GameboardCaretaker
    {
        private Stack<Gameboard> mementos = new Stack<Gameboard>();

        public void SaveState(Gameboard gameboard)
        {
            mementos.Push(gameboard.Clone());
        }

        public Gameboard RestoreState()
        {
            if (mementos.Count > 0)
            {
                mementos.Pop();
                return mementos.Pop();
            }
            return null;
        }
    }
}
