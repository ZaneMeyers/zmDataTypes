class HierarchyInt : IComparable {
   [int[]] $Array

   # Constructor - only accepts int[] with all positive, non-null members
   HierarchyInt([int[]] $array) {
      $this.InitializeIntArray($array)
   }

   HierarchyInt([string] $delimitedString) {
      if ($null -eq $delimitedString -or $delimitedString.Trim() -eq "") {
         throw [System.ArgumentException]::new("String cannot be null or empty.")
      }

      $splitArray = $delimitedString -split ',' | ForEach-Object { 
         [int]($_.Trim()) 
      }
      $this.InitializeIntArray($splitArray)
   }

   [void] InitializeIntArray([int[]] $array) {
      if ($null -eq $array -or $array.Count -eq 0) {
         throw [System.ArgumentException]::new("Array cannot be null or empty.")
      }
      foreach ($element in $array) {
         if ($element -le 0) {
            throw [System.ArgumentException]::new("All array elements must be positive integers.")
         }
      }
      $this.Array = $array
   }

   # Implementing CompareTo for sorting
   [int] CompareTo([object] $other) {
      if ($null -eq $other -or -not ($other -is [HierarchyInt])) {
         throw [System.ArgumentException]::new("Object must be of type HierarchyInt.")
      }

      $otherArray = $other.Array
      $len = [math]::Min($this.Array.Length, $otherArray.Length)

      for ($i = 0; $i -lt $len; $i++) {
         if ($this.Array[$i] -ne $otherArray[$i]) {
            return $this.Array[$i] - $otherArray[$i]
         }
      }

      # If arrays are equal up to the shortest length, the longer one is "larger"
      return $this.Array.Length - $otherArray.Length
   }
}

# $sortedArray = @( 
#     [HierarchyInt]::new(@(1)), 
#     [HierarchyInt]::new(@(1,1,1,1,1)), 
#     [HierarchyInt]::new(@(2)), 
#     [HierarchyInt]::new(@(12,1))
# ) | Sort-Object

<#
# Hierarchy Type

## Description

In legalese and project specifications
(a dialect of legalese),
it is popular to format nested lists
with as many numbering schemes and formats
as can be imagined at the time of writing.

There is a disgusting amount of potential variety
in how these hierarchies can be formatted in text,
and there is rarely any consistency
to how the various number systems are used,
even throughout a single document.

* Decimal: `0-9`
* Bijective Base 26, Lowercase: `a,b,...,z,aa,ab,...,az,...`
* Bijective Base 26, Capital: `A,B,...,Z,AA,AB,...,AZ,...`
* Roman Numerals, Lowercase: `i,ii,iii,iv,v...`
* Roman Numerals, Capital: `I,II,III,IV,V...`

I personally feel that if you can have a Section 500 and a Section A in the same document,
then you should have just used integers for everything to begin with.

Luckily all these permutations normalize to an ordered list of integers.
A sensible spreadsheet program would let you store the list in a single cell
and sort them as is obvious,
but in lieu of such a program,
you can stringify the resultant list
with sufficient zero padding for the application.

Note that this sorting is more comprehensive than "Natural Sorting"

## Implementation

Split into component integers

* Regex boundaries
  * \b
  * \B

```cs
var hierarchy = new Hierarchy('250.4(A)(1)')
{250,4,1,1}
```

```pwsh
PS> [Hierarchy]::FormatString('250.4(A)(1)')
'250.4.1.1'

PS> [Hierarchy]::FormatString('250.4(A)(1)',4)
'0250.0004.0001.0001'

PS> [Hierarchy]::FormatString('250.4(A)(1)',2)
'250.04.01.01'
```

## Examples

NFPA70: "250.4(A)(1)" = @(250,4,1,1)

COE Specs: "26 12 19.10 1.5.3.1"
COE Specs: "26 12 19.10 1.6.1 (a.)"

SCAR Project Manual: "1.3.2.38"

Yakult Exhibit A: B. Section A - Codes and Standards/01 - 00 Construction/2./iv.

Current determination: hierarchies should be simple ordered[int[]] types. a join table can be provided for additional info.
#>