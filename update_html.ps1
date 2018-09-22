$obj = get-content $PSScriptRoot/assets.json | convertfrom-json

$obj | ConvertTo-Html -CssUri $PSScriptRoot/output.css | out-file $PSScriptRoot/assets.html

$obj = get-content $PSScriptRoot/factions.json | ConvertFrom-Json

$obj | ConvertTo-Html -CssUri $PSScriptRoot/output.css | out-file $PSScriptRoot/factions.html