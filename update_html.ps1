$obj = get-content ./assets.json | convertfrom-json

$obj | ConvertTo-Html -CssUri ./output.css | out-file ./assets.html

$obj = get-content ./factions.json | ConvertFrom-Json

$obj | ConvertTo-Html -CssUri ./output.css | out-file ./factions.html