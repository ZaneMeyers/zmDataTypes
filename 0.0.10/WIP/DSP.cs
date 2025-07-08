using System;
using System.Collections.Generic;
using System.Linq;

namespace zmDataTypes.Probability
{
    /// <summary>
    /// A class representing a Dempster-Shafer "Evidence Structure" (or Body of Evidence)
    /// over a discrete frame of discernment of type T.
    /// 
    /// Focal elements are stored along with their basic probability assignments (masses).
    /// </summary>
    public class DempsterShaferEvidence<T>
    {
        // Dictionary: each focal element is a set of T, mapped to a mass in [0,1].
        private Dictionary<HashSet<T>, double> _massFunction;

        /// <summary>
        /// Constructor takes a dictionary of (focalElement -> mass).
        /// The sum of all masses should be 1.0 in a basic DS structure (unless you want to allow "open-world" or unnormalized).
        /// </summary>
        public DempsterShaferEvidence(Dictionary<HashSet<T>, double> massFunction)
        {
            // Ideally, you'd validate that sum of masses == 1 (or very close).
            double sum = massFunction.Values.Sum();
            if (Math.Abs(sum - 1.0) > 1e-8)
            {
                throw new ArgumentException("Sum of the provided masses must be 1.0.");
            }

            // Store a deep copy or ensure that each HashSet is "frozen" if you want immutability.
            // For simplicity, we'll just keep references.
            _massFunction = massFunction;
        }

        /// <summary>
        /// Exposes the current mass function for reading, e.g., to iterate focal elements.
        /// </summary>
        public IReadOnlyDictionary<HashSet<T>, double> MassFunction => _massFunction;

        /// <summary>
        /// Frame of discernment is the union of all elements in every focal set.
        /// </summary>
        public HashSet<T> FrameOfDiscernment
        {
            get
            {
                var frame = new HashSet<T>();
                foreach (var focalElement in _massFunction.Keys)
                {
                    frame.UnionWith(focalElement);
                }
                return frame;
            }
        }

        /// <summary>
        /// Combine this DS evidence with another DS evidence structure using Dempster's rule.
        /// Returns a new DS structure that represents the combined evidence.
        /// </summary>
        public DempsterShaferEvidence<T> CombineWith(DempsterShaferEvidence<T> other)
        {
            // We'll build up a new dictionary (focalElement -> combinedMass).
            var newMass = new Dictionary<HashSet<T>, double>(new SetComparer<T>());

            // Track the conflict mass (when intersection is empty).
            double conflict = 0.0;

            // For each focal element A in this object and B in other, combine.
            foreach (var kvA in _massFunction)
            {
                var A = kvA.Key;      // subset
                double massA = kvA.Value;

                foreach (var kvB in other._massFunction)
                {
                    var B = kvB.Key;
                    double massB = kvB.Value;

                    // Intersection
                    var C = new HashSet<T>(A);
                    C.IntersectWith(B);

                    if (C.Count == 0)
                    {
                        // This contributes to the conflict.
                        conflict += massA * massB;
                    }
                    else
                    {
                        // Accumulate mass for intersection set C.
                        if (!newMass.ContainsKey(C))
                        {
                            // Must create a new set instance to avoid mutation issues later
                            newMass.Add(new HashSet<T>(C), massA * massB);
                        }
                        else
                        {
                            newMass[C] += massA * massB;
                        }
                    }
                }
            }

            // Now normalize by the total non-conflict mass: 1 - conflict
            double normalization = 1.0 - conflict;
            if (Math.Abs(normalization) < 1e-12)
            {
                throw new InvalidOperationException("Total conflict is 1.0; cannot normalize.");
            }

            // Apply normalization factor to each focal element mass
            var finalMass = new Dictionary<HashSet<T>, double>(new SetComparer<T>());
            foreach (var kv in newMass)
            {
                finalMass.Add(kv.Key, kv.Value / normalization);
            }

            return new DempsterShaferEvidence<T>(finalMass);
        }

        /// <summary>
        /// Belief of a subset S: sum of all focal elements that are fully contained in S.
        /// </summary>
        public double Belief(HashSet<T> S)
        {
            double belief = 0.0;
            foreach (var kv in _massFunction)
            {
                var focalSet = kv.Key;
                double mass = kv.Value;

                if (focalSet.IsSubsetOf(S))
                {
                    belief += mass;
                }
            }
            return belief;
        }

        /// <summary>
        /// Plausibility of a subset S: sum of all focal elements that intersect S.
        /// plaus(S) = 1 - belief(S^c), but we do the direct sum for clarity.
        /// </summary>
        public double Plausibility(HashSet<T> S)
        {
            double plaus = 0.0;
            foreach (var kv in _massFunction)
            {
                var focalSet = kv.Key;
                double mass = kv.Value;

                // If focalSet intersects S, we add the mass.
                // Intersection is non-empty if their intersection has at least 1 element.
                var intersection = new HashSet<T>(focalSet);
                intersection.IntersectWith(S);
                if (intersection.Count > 0)
                {
                    plaus += mass;
                }
            }
            return plaus;
        }
    }

    /// <summary>
    /// A helper class to compare HashSet<T> keys for dictionary usage.
    /// Ensures we treat sets with the same elements as equal.
    /// </summary>
    public class SetComparer<T> : IEqualityComparer<HashSet<T>>
    {
        public bool Equals(HashSet<T> x, HashSet<T> y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            return x.SetEquals(y);
        }

        public int GetHashCode(HashSet<T> obj)
        {
            // A simple approach is to XOR the hash codes of elements, but
            // you might want a more robust scheme for large sets.
            int hash = 0;
            foreach (var item in obj)
            {
                hash ^= item.GetHashCode();
            }
            return hash;
        }
    }

}


// // Suppose our frame of discernment = {A, B, C}.
// // Example: outcomes for some uncertain event.

// // Evidence 1: 
// //   m({A})    = 0.2
// //   m({B, C}) = 0.5
// //   m({A,B,C})= 0.3
// var focalSets1 = new Dictionary<HashSet<string>, double>(new SetComparer<string>>
// {
//     { new HashSet<string> { "A" }, 0.2 },
//     { new HashSet<string> { "B", "C" }, 0.5 },
//     { new HashSet<string> { "A", "B", "C" }, 0.3 }
// });
// var ds1 = new DempsterShaferEvidence<string>(focalSets1);

// // Evidence 2:
// //   m({B})    = 0.6
// //   m({A,B,C})= 0.4
// var focalSets2 = new Dictionary<HashSet<string>, double>(new SetComparer<string>>
// {
//     { new HashSet<string> { "B" }, 0.6 },
//     { new HashSet<string> { "A", "B", "C" }, 0.4 }
// });
// var ds2 = new DempsterShaferEvidence<string>(focalSets2);

// // Combine them:
// var combined = ds1.CombineWith(ds2);

// // Let's compute some beliefs and plausibilities:
// var query = new HashSet<string> { "B" }; // e.g. we are interested in outcome "B".

// double beliefB = combined.Belief(query);
// double plausB = combined.Plausibility(query);

// Console.WriteLine("Combined DS structure focal sets:");
// foreach (var kv in combined.MassFunction)
// {
//     Console.WriteLine($"  Focal element {{{string.Join(",", kv.Key)}}} => mass = {kv.Value:F4}");
// }

// Console.WriteLine($"Belief(B) = {beliefB:F4}");
// Console.WriteLine($"Plausibility(B) = {plausB:F4}");