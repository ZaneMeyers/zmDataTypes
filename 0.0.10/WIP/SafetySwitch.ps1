class SafetySwitch : IComparable {

    hidden [hashtable] $RatingPatterns = @{
        Voltage = @{
            Pattern = "(\d{1,4}) ?V"
            Suffix = "V"
        }
        SwitchAmps = @{
            Pattern = "(\d{1,4}) ?A"
            Suffix = "A"
        }
        FuseAmps = @{
            Pattern = "(\d{1,4}) ?AF"
            Suffix = "AF"
        }
        PoleCount = @{
            Pattern = "(\d) ?P"
            Suffix = "P"
        }
        Enclosure = @{
            Pattern = "N(?:EMA)? ?(\d\d?[KPRS]?X?)"
            Prefix = "N"
        }
    } 

    [System.Collections.Generic.Dictionary[string,string]] $Ratings

    static [SafetySwitch] Parse([string] $string) {
        $switch = [SafetySwitch]::New()
        $switch.Ratings = [System.Collections.Generic.Dictionary[string,string]]::New()

        foreach ($key in $switch.RatingPatterns.Keys) {
            $pattern = $switch.RatingPatterns[$key].Pattern
            $match = $string -match $pattern
            if ($match) {
                $switch.Ratings.Add($key,$Matches[1])
                $string = $string -replace $pattern, ""
            }
        }

        return $switch

    }

    [string] ToString() {
        $dict = [System.Collections.Generic.Dictionary[string,string]]::New()
        foreach ($key in $this.Ratings.Keys) {
            $formattedValue = ($this.RatingPatterns[$key].Prefix, $this.Ratings[$key], $this.RatingPatterns[$key].Suffix) -join ""
            $dict.Add($key,$formattedValue)
        }
        $string = ($dict.SwitchAmps,$dict.FuseAmps,$dict.PoleCount) -join "/"
        $string = ($dict.Voltage,$string,$dict.Enclosure) -join " "
        return $string
    }

    [int] CompareTo([object] $other) {
        return 1
    }
}