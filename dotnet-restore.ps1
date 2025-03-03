Get-ChildItem -Path . -Recurse -Filter *.sln | ForEach-Object {
  dotnet restore $_.FullName
}
