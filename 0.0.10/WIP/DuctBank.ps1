class DuctBank {
    [float]$DuctOuterDiameterInches
    [float]$DuctSpacingInches = 3.0
    [float]$SurroundThicknessInches = 3.0
    [int]$NumberOfDuctsWide
    [int]$NumberOfDuctsHigh
    [int]$CoverThicknessInches = 36

    DuctBank() {
    }

    [float] GetWidth() {
        return ($this.NumberOfDuctsWide * $this.DuctOuterDiameterInches) + (($this.NumberOfDuctsWide - 1) * $this.DuctSpacingInches)
    }

    [float] GetHeight() {
        return ($this.NumberOfDuctsHigh * $this.DuctOuterDiameterInches) + (($this.NumberOfDuctsHigh - 1) * $this.DuctSpacingInches)
    }

    [float] GetTrenchDepth() {
        return $this.GetHeight() + $this.CoverThicknessInches
    }

    [float] GetArea() {
        return $this.GetWidth() * $this.GetHeight()
    }

    [float] GetDuctArea() {
        $NumberOfDucts = $this.NumberOfDuctsWide * $this.NumberOfDuctsHigh
        $ductArea = [math]::PI * [math]::Pow(($this.DuctOuterDiameterInches / 2), 2)
        return $NumberOfDucts * $ductArea
    }

    [float] GetSurroundArea() {
        return $this.GetArea() - $this.GetDuctArea()
    }
}