cls

rmdir /s/q C:\Users\jonat\.nuget\packages\hoh.architecture
rmdir /s/q C:\Users\jonat\.nuget\packages\hoh.architecture.cqrs

cd C:\Work\Hoh-Architecture\framework\hoh.architecture.CQRS\bin\Debug
C:\Work\nugetPackages\nuget.exe add hoh.architecture.CQRS.1.0.0.nupkg -Source C:\Work\nugetPackages

cd C:\Work\Hoh-Architecture\framework\hoh.architecture.scafolding\bin\Debug
C:\Work\nugetPackages\nuget.exe add HOH.Architecture.1.0.0.nupkg -Source C:\Work\nugetPackages