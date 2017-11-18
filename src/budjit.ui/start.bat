dotnet publish -r win7-x64 --output "obj\Host\bin"
obj\Host\node_modules\.bin\electron.cmd "obj\Host\main.js"