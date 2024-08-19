using Triarch.BusinessLogic.Models.Definitions;
using Triarch.BusinessLogic.Models.Entities;

namespace Triarch.BusinessLogic.Services;

public class EntityController
{
    public bool MoveUpElement(RPGElement element)
    {
        if (element.Parent == null)
        {
            return false;
        }
        RPGElement parent = element.Parent;
        if (parent.Children.First() == element)
        {
            return false;
        }
        int currentPosition = parent.Children.IndexOf(element);
        parent.Children.Remove(element);
        parent.Children.Insert(currentPosition - 1, element);
        return currentPosition != parent.Children.IndexOf(element);
    }

    public bool MoveDownElement(RPGElement element)
    {
        if (element.Parent == null)
        {
            return false;
        }
        RPGElement parent = element.Parent;
        if (parent.Children.Last() == element)
        {
            return false;
        }
        int currentPosition = parent.Children.IndexOf(element);
        parent.Children.Remove(element);
        parent.Children.Insert(currentPosition + 1, element);
        return currentPosition != parent.Children.IndexOf(element);
    }

    public RPGElement AddElement(RPGElement parent, RPGElementDefinition definitionToAdd)
    {
        RPGElement elementToAdd = definitionToAdd.CreateNode(parent.Entity, "", false);
        parent.Children.Add(elementToAdd);
        elementToAdd.Parent = parent;
        elementToAdd.AddFreebies();

        return elementToAdd;
    }

    public Stack<RPGElement> DeleteElement(RPGElement elementToDelete)
    {
        Stack<RPGElement> deletedStack = new Stack<RPGElement>();
        if (elementToDelete.Parent != null)
        {
            Queue<RPGElement> tempQueue = new Queue<RPGElement>([elementToDelete]);
            Stack<RPGElement> deletionStack = new Stack<RPGElement>();
            while (tempQueue.Count > 0)
            {
                RPGElement current = tempQueue.Dequeue();
                foreach (RPGElement child in current.Children)
                {
                    tempQueue.Enqueue(child);
                }
                deletionStack.Push(current);
                deletedStack.Push(current);
            }

            while (deletionStack.Count > 0)
            {
                RPGElement current = deletionStack.Pop();
                if (current.Parent != null)
                {
                    current.Parent.Children.Remove(current);
                    current.Parent = null;
                }
            }
        }
        return deletedStack;
    }

    public void UpdateLevel(Levelable levelable, int newLevel)
    {
        if (levelable.Level != newLevel)
        {
            levelable.Level = newLevel;
        }
    }

    public Stack<RPGElement> UpdateGenre(Genre genre, RPGEntity entity)
    {
        Stack<RPGElement> updatedStack = new Stack<RPGElement>();

        entity.Genre = genre;
        Queue<RPGElement> tempQueue = new Queue<RPGElement>([entity.RootElement]);
        while (tempQueue.Count > 0)
        {
            RPGElement current = tempQueue.Dequeue();
            foreach (RPGElement child in current.Children)
            {
                tempQueue.Enqueue(child);
            }
            if (current.AssociatedDefinition is MultiGenreDefinition)
            {
                updatedStack.Push(current);
            }            
        }

        return updatedStack;
    }

}
