param([Parameter(ValueFromRemainingArguments=$true)][string[]]$ContentArguments)
. "$PSScriptRoot/common.ps1"
Invoke-ManosabaContent 'import' $ContentArguments
