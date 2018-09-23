rm -r -Force bin

dotnet publish --self-contained=yes -r win10-x64

dotnet publish --self-contained=yes -r win10-x86

dotnet publish --self-contained=yes -r linux-x64

dotnet publish --self-contained=yes -r centos-x64

dotnet publish --self-contained=yes -r ubuntu.18.04-x64

dotnet publish --self-contained=yes -r ubuntu.16.04-x64

dotnet publish --self-contained=yes -r osx-x64


cd bin/Debug/netcoreapp2.1

Compress-Archive win10-x64 -DestinationPath win10-x64.zip
Compress-Archive win10-x64 -DestinationPath asset_smasher.zip
Compress-Archive win10-x86 -DestinationPath win10-x86.zip
Compress-Archive linux-x64 -DestinationPath linux-x64.zip
Compress-Archive centos-x64 -DestinationPath centos-x64.zip
Compress-Archive ubuntu.18.04-x64 -DestinationPath ubuntu.18.04-x64.zip
Compress-Archive ubuntu.16.04-x64 -DestinationPath ubuntu.16.04-x64.zip
Compress-Archive osx-x64 -DestinationPath osx-x64.zip

cd $PSScriptRoot