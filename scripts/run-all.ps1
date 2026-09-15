# Smoke-run every sample project (presentability / CI-local check).
$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
Set-Location $root

Write-Host "Building solution..." -ForegroundColor Cyan
dotnet build AL_NET_Library.sln --nologo -v q
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

$projects = @(
    "samples/Dependency",
    "samples/Threads",
    "samples/DesignPatterns",
    "samples/SOLID",
    "samples/Exceptions",
    "samples/AdvancedFeatures",
    "samples/LINQ",
    "samples/AuditEntry",
    "samples/EntityFramework",
    "samples/DataManagement",
    "samples/Logging"
)

foreach ($project in $projects) {
    Write-Host ""
    Write-Host "======== $project ========" -ForegroundColor Green
    dotnet run --project $project --no-build
    if ($LASTEXITCODE -ne 0) {
        Write-Host "FAILED: $project" -ForegroundColor Red
        exit $LASTEXITCODE
    }
}

Write-Host ""
Write-Host "All samples completed." -ForegroundColor Green
