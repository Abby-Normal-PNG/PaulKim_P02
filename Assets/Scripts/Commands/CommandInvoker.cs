using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker
{
    private Stack<ICommand> CommandHistory = new Stack<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        CommandHistory.Push(command);
        command.Execute();
    }

    public BoardCard ExecuteReturnBoardCard(SpawnBoardCardCommand command, Card card)
    {
        CommandHistory.Push(command);
        BoardCard boardCardToReturn = command.Execute(card);
        return boardCardToReturn;
    }

    public void UndoCommand()
    {
        if(CommandHistory.Count <= 0)
        {
            return;
        }
        CommandHistory.Pop().Undo();
    }

    public void UndoAll()
    {
        if(CommandHistory.Count > 0)
        {
            foreach (ICommand command in CommandHistory)
            {
                UndoCommand();
            }
        }
    }
}
