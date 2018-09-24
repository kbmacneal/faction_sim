$obj = get-content $PSScriptRoot/assets.json | convertfrom-json

$obj | Select-Object -Property ID,Name,HP,Attack,Counterattack,AttackStats,AttackDice,DefenderReroll,AttackerReroll | ConvertTo-Html -CssUri $PSScriptRoot/output.css | out-file $PSScriptRoot/assets.html

$obj = get-content $PSScriptRoot/factions.json | ConvertFrom-Json

$obj | Select-Object ID,"Faction Name",Force,Cunning,Wealth,AttackerReroll,AttackerRerollStat,DefenderReroll,DefenderRerollStat,AttackerRerolled,DefenderRerolled,NumAttackerRerolls,NumDefenderRerolls,AlwaysRerollAtk,AlwaysRerollDef,PMax  | ConvertTo-Html -CssUri $PSScriptRoot/output.css | out-file $PSScriptRoot/factions.html
