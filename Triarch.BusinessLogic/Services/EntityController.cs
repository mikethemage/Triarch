using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        parent.Children.Insert(currentPosition-1, element);
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

    public void AddElement(RPGElement parent, RPGElementDefinition definitionToAdd)
    {
        RPGElement elementToAdd = definitionToAdd.CreateNode(parent.Entity, "", false);
        parent.Children.Add(elementToAdd);

        foreach (Freebie freebieDefinition in definitionToAdd.Freebies)
        {
            if (freebieDefinition.FreebieElementDefinition is LevelableDefinition levelableFreebieDefinition)
            {
                elementToAdd.Children.Add(levelableFreebieDefinition.CreateNode(elementToAdd.Entity, "", freebieDefinition.FreeLevels + freebieDefinition.RequiredLevels, true, freebieDefinition.FreeLevels, freebieDefinition.RequiredLevels));
            }
            else
            {
                elementToAdd.Children.Add(freebieDefinition.FreebieElementDefinition.CreateNode(elementToAdd.Entity, "", true));
            }
        }
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
                if(current.Parent != null)
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
}
