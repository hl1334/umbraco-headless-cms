######¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤##############################################################
# Syncs Umbraco datatype prevalue id's from the DS set of master uSync config files to the set of site 
# specific uSync config files.
# Note: Umbraco changes the prevalue id's for datatypes, when we change the datatypes, so we can update the 
# site specific datatypes using this script and then use uSync import, so that our environments (dev, test, prod) 
# are in sync.
##############################################################################################################
$sourceRootPath = ".\src\umbraco\umbraco-headless-cms.web\uSync\data\DataType\ds-data-types"
$targetRootPath = ".\src\umbraco\umbraco-headless-cms.web\config\uSyncConfigurations\DataType\ds-data-types"

# FLOW: 
# 1: For each source uSync config file iterate each corresponding file in the targetDir.
# 2: If a target file exists then open/read the source file and target files as XML.
# 3: Iterate all prevalues in source file and update the Id's in target file, if prevalue IDs differ.
# 4: If prevalue IDs differ update prevalue IDs in target file and save new instance of the target file.  

function UpdatePrevalueIds ($sourcePath, $targetPath, $targetDirName) {
  Write-Host "------------------------------------------------------------------------------------------------------------------------"  
  Write-Host "SourceDir:" $sourcePath

  $targetDir = Join-Path $targetPath $targetDirName
  
  if (Test-Path $targetDir) {
    Write-Host "TargetDir:" $targetDir
    
    $targetFiles = Get-ChildItem -File $targetDir

    if ($targetFiles.Count -gt 0) {
      $sourceFiles = Get-ChildItem -File $sourcePath
      # There will only be one sourcefile, so we can use index 0. 
      Write-Host "SourceFile:" $sourceFiles[0]

      $sourceFilePath = Join-Path $sourcePath $sourceFiles[0]
      [xml]$sourceFileXml = Get-Content $sourceFilePath

      # Compare prevalue alias for a give ID (source => target), only if alias matches and 
      # prevalue IDs differ will we try to update the ID in the target file.
      foreach ($targetFile in $targetFiles) {
        Write-Host "TargetFile:" $targetFile
        $targetFilePath = Join-Path $targetDir $targetFile
        Write-Host "TargetFilePath:" $targetFilePath
        [xml]$targetFileXml = Get-Content $targetFilePath
        $targetFileUpdated = $false
        foreach ($targetAlias in $targetFileXml.DataType.PreValues.PreValue.Alias) {
          foreach ($sourceAlias in $sourceFileXml.DataType.PreValues.PreValue.Alias) {
            if ($sourceAlias -eq $targetAlias) {
              $sourceIndex = [array]::IndexOf($sourceFileXml.DataType.PreValues.PreValue.Alias, $sourceAlias)
              $targetIndex = [array]::IndexOf($targetFileXml.DataType.PreValues.PreValue.Alias, $targetAlias)
              $sourcePrevalueId = $sourceFileXml.DataType.PreValues.PreValue.Id[$sourceIndex]
              $targetPrevalueId = $targetFileXml.DataType.PreValues.PreValue.Id[$targetIndex]

              Write-Host "sourceIndex:" $sourceIndex "| sourceAlias:" $sourceAlias "| sourcePrevalueID:" $sourcePrevalueId
              Write-Host "targetIndex:" $targetIndex "| targetAlias:" $targetAlias "| targetPrevalueID:" $targetPrevalueId
              
              if ($targetPrevalueId -ne $sourcePrevalueId) {
                $targetFileXml.DataType.PreValues.PreValue[$targetIndex].Id = $sourcePrevalueId             
                $targetFileUpdated = $true
              }
            }
          }
        }
        if ($targetFileUpdated) {
          # Save a new instance of the target file with the update.
          $targetFileXml.Save($targetFilePath)
          Write-Host "- target file was updated" -ForegroundColor Green
        } else {
          Write-Host "- target file is up-to-date" -ForegroundColor Yellow
        }
      }         
    }
  } 
  
  $subFolders = Get-ChildItem -Directory $sourcePath

  foreach ($subFolder in $subFolders) {
    # Recursive call here to iterate all folders.
    UpdatePrevalueIds $subFolder.FullName $targetDir $subFolder.Name
  }
}

$sourceRootDir = Get-Item $sourceRootPath
$targetRootDir = Get-Item $targetRootPath

UpdatePrevalueIds $sourceRootDir.FullName $targetRootDir.FullName ""
