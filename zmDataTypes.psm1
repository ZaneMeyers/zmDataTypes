
# $PSInstallPath = "~\AppData\Local\Microsoft\powershell"

Add-Type -Path "$PSScriptRoot\Classes\MixedNumber.cs",
    "$PSScriptRoot\Classes\WireGauge.cs",
    "$PSScriptRoot\Classes\Tube.cs",
    "$PSScriptRoot\Classes\Conduit.cs",
    "$PSScriptRoot\Classes\Starter.cs"

# Add-Type -Path "$PSScriptRoot\Classes\Rebar.cs"

# Get-ChildItem -Path "$PSScriptRoot\Classes" -Filter *.cs | ForEach-Object {
#     # . $_.FullName
#     Add-Type -Path $_.FullName -ErrorAction Stop
# }