using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding
{
    internal class CustomQueue
    {
        public int Count
        {
            get
            {
                return queue.Count;
            }
        }
        //queue ordonné en ordre decroissant de cout
        List<(int index,float cout,int origine)> queue = new();

        public void Ajouter((int index, float cout, int origine) element)
        {
            int indexNouveau = -1;
            int indexAncien = -1;

            for (int i = 0; i < queue.Count; i++) // regarder si l'index existe deja et trouver la position d'insertion
            {
                if (indexNouveau == -1 && element.cout > queue[i].cout) // regarder si il est plus grand que l'element courant
                {
                    indexNouveau = i;
                }

                if (element.index == queue[i].index) // s'il existe deja
                {
                    indexAncien = i;
                }
            }

            if(indexNouveau == -1) // si l'element est le plus petit
            {
                indexNouveau = queue.Count;
            }
            if(indexNouveau > indexAncien)
            {
                queue.Insert(indexNouveau, element);

                if(indexAncien != -1) // retirer l'ancien element
                    queue.RemoveAt(indexAncien);
            }
        }

        public (int index, float cout,int origine) Retirer()
        {
            (int, float, int) element = queue.Last();
            queue.RemoveAt(queue.Count - 1);
            return element;
        }
    }
}
