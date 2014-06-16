function GetAllT4RootItems($parent)
{
	$values = ($parent.ProjectItems | select *)
	$result = @()
	foreach ($value in $values)
	{
	    if ($value.Name.EndsWith(".tt"))
	    {
            $result = $result + $value
	    }
	    else
	    {
		    if ($value -ne $parent -and $value.Name -ne $null)
    		{
    			$result = $result + (GetAllT4RootItems($value))
    			$result = $result + (GetAllT4RootItems($value.SubProject))
    		}
		}
	}
	return $result
}

Export-ModuleMember GetAllT4RootItems

function GetProjects()
{
    $items = $DTE.Solution.Projects
    $projectItems = @(); 
    
    while ($items.Count -ne 0)
    {
        $newItems = @()
        foreach ($item in $items)
        {
            if ($item -ne $null)
            { 
                if ($item.FullName -ne $null -and $item.FullName.EndsWith('.csproj')) 
                { 
                    $projectItems = $projectItems + $item
                } 
                else 
                { 
                    if ($item.SubProject -ne $null)
                    {
                        $newItems = $newItems + $item.SubProject
                    }
                    if ($item.ProjectItems -ne $null)
                    {
                        $newItems = $newItems + $item.ProjectItems
                    }
                }                
            }
        }
        $items = $newItems
    }
    return $projectItems
}

Export-ModuleMember GetProjects

function RecursiveRunCustomTool($item)
{
    $fileName = $item.Properties | ?{$_.Name -eq 'LocalPath'} | select -ExpandProperty Value
    Write-Host $fileName
    try
    {
        $item.Object.RunCustomTool()
    }
    catch
    {
        Write-Host $fileName + ' failed'
    }
    foreach ($subItem in $item.ProjectItems | ?{$_.Name.EndsWith(".tt")})
    {
        RecursiveRunCustomTool $subItem
    }
}


function RecursiveGeneration($file)
{
    $ttItem = $DTE.Solution.FindProjectItem($file)
    RecursiveRunCustomTool($ttItem)
}

Register-TabExpansion 'RecursiveGeneration' @{ 
'file' = { GetProjects | foreach {(GetAllT4RootItems $_)} | foreach {$_.Properties | ?{$_.Name -eq 'LocalPath'} | select -ExpandProperty Value} | Sort-Object };
}

Export-ModuleMember RecursiveGeneration

