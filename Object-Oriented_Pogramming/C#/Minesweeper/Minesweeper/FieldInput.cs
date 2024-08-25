using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper;
public class FieldInput
{
    public int XCoordinate { get; }
    public int YCoordinate { get; }
    public Action ActionType { get; set; } 

    public enum Action
    {
        // R = Reveal, F = Set Flag, RF = Remove Flag
        Reveal,
        Flag,
        RemoveFlag,
    }

    public FieldInput(int xCoordinate, int yCoordinate, Action actionType) 
    {
        XCoordinate = xCoordinate;
        YCoordinate = yCoordinate;
        ActionType = actionType; 
    }
}
