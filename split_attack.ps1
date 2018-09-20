$obj = get-content $PSScriptRoot\assets.json | ConvertFrom-Json

foreach($item in $obj)
{
    $atk = $item.Attack

    if($atk -eq "None")
    {
        add-member -InputObject $item -MemberType NoteProperty -Name AttackStats -Value None

        add-member -InputObject $item -MemberType NoteProperty -Name AttackDice -Value None
    }
    else
    {
        $stats = $atk.Split(",")[0].Trim()

        $dice = $atk.Split(",")[1].Trim()

        add-member -InputObject $item -MemberType NoteProperty -Name AttackStats -Value $stats

        add-member -InputObject $item -MemberType NoteProperty -Name AttackDice -Value $dice
    }
}

$obj | ConvertTo-Json | out-file $PSScriptRoot\assets_modified.json