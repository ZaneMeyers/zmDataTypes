class BijectiveBase26 {
   [int]$Value

   BijectiveBase26([int]$number) {
      if ($number -le 0) {
         throw "Number must be greater than zero."
      }
      $this.Value = $number
   }

   BijectiveBase26([string]$string) {
      $string = $string.ToUpper()
      if ($string -match '^[A-Z]+$') {
         $number = [BijectiveBase26]::StringToInt($string)
         $this.Value = $number
      }
      else {
         throw "Input must be a valid bijective base-26 string (only A-Z allowed)."
      }
   }

   [string] ToString() {
      $number = $this.Value
      return [BijectiveBase26]::IntToString($number)
   }

   static [string]IntToString([int]$number) {
      $string = ""
      while ($number -gt 0) {
         $number--  # Adjust for 1-based index
         $remainder = $number % 26
         $string = [string][char]([int][char]'A' + $remainder) + $string
         $number = [math]::Floor($number / 26)
      }
      return $string
   }

   static [string]StringToInt([string]$string) {
      $number = 0
      $length = $string.Length

      for ($i = 0; $i -lt $length; $i++) {
         $charValue = [int][char]$string[$i] - [int][char]'A' + 1
         $number += $charValue * [math]::Pow(26, $length - $i - 1)
      }
      return $number
   }
}