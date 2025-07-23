using System;
using System.Collections.Generic;

namespace zmDataTypes.Misc
{
   /**
   * <summary>
   * Generic collection that provides T[] </summary>*/

   public class Hierarchy<T> : IComparable<Hierarchy<T>> where T : IComparable<T>
   {
      public T[] Array { get; private set; }

      // Constructor that takes an array of type T
      public Hierarchy(T[] array)
      {
         if (array == null || array.Length == 0)
         {
            throw new ArgumentException("Array cannot be null or empty.");
         }

         foreach (T element in array)
         {
            if (element == null)
            {
               throw new ArgumentException("Array elements cannot be null.");
            }
         }

         this.Array = array;
      }

      // Implementing CompareTo for sorting
      public int CompareTo(Hierarchy<T> other)
      {
         if (other == null)
         {
            throw new ArgumentException("Object cannot be null.");
         }

         // get minimum # members between arrays: max # of necessary comparisons
         int len = Math.Min(this.Array.Length, other.Array.Length);

         for (int i = 0; i < len; i++)
         {
            // compare on each Array[i] 
            int comparison = this.Array[i].CompareTo(other.Array[i]);
            if (comparison != 0)
            {
               return comparison;
            }
         }

         // If arrays are equal up to the shortest length, the longer one is "larger"
         // i.e. [1,2],[1,2,1],[1,2,2]
         return this.Array.Length - other.Array.Length;
      }
   }

   // [Hierarchy[string]][string[]]@("Electrical","Power")
   // [Hierarchy[int]][int[]]@(1,2)
}
