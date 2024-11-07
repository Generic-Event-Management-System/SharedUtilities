# Define the output directory
$outputDir = "C:\Git\libs"

# Check if the output directory exists; create if not
if (!(Test-Path -Path $outputDir)) {
    New-Item -ItemType Directory -Path $outputDir
    Write-Output "Created directory: $outputDir"
} else {
    Write-Output "Directory already exists: $outputDir"
}

# Build the project to the default output location
dotnet build ../SharedUtilities.csproj -c Release

# Define the path to the DLL in the default output folder
$dllPath = "../bin/Release/net8.0/SharedUtilities.dll"

# Check if the DLL exists and copy it to the target directory only
if (Test-Path -Path $dllPath) {
    Copy-Item -Path $dllPath -Destination "$outputDir\SharedUtilities.dll" -Force
    Write-Output "Copied SharedUtilities.dll to $outputDir"
} else {
    Write-Output "DLL file not found: $dllPath"
}