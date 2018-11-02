$obj = get-content $PSScriptRoot/assets.json | convertfrom-json

$obj | Select-Object -Property ID,Name,Description,HP,Attack,Counterattack,AttackStats,AttackDice,DefenderReroll,AttackerReroll | ConvertTo-Html -CssUri $PSScriptRoot/output.css | out-file $PSScriptRoot/assets.html

$obj = get-content $PSScriptRoot/factions.json | ConvertFrom-Json

$obj | Select-Object ID,"Faction Name",Force,Cunning,Wealth,PMax  | ConvertTo-Html -CssUri $PSScriptRoot/output.css | out-file $PSScriptRoot/factions.html
