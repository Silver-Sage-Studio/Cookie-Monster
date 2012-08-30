using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sage_Editor
{
    public enum Commands
    {
        SetTileCommand,
    }

   public static class CommandFactory
    {

     static List<Command> AvailableCommands = new List<Command>();

       public static void Initilise(Form1 form)
       {
           AvailableCommands.Add(new SetTileCommand(form));
       }

       public static Command Execute(Commands commandType)
       {
           return AvailableCommands[(int)commandType].Clone();
       }

    }
}
