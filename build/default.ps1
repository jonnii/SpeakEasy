task default -depends Package

task Package -depends Test {
	$packagesDirectory = '..\targets\packages'
	
	if (Test-Path -path $packagesDirectory)
	{
		rm $packagesDirectory -recurse -force
	}

	mkdir $packagesDirectory
	cp ..\build\package.nuspec ..\targets\packages

	mkdir ..\targets\packages\lib\net40
	cp ..\targets\speakeasy\speakeasy.* ..\targets\packages\lib\net40

	..\src\.nuget\nuget.exe pack -outputdirectory .\targets\packages ..\targets\packages\package.nuspec 
}

task Test -depends Compile, Clean { 
  # run specs
  # integration tets
}

task Compile -depends Clean { 
  # call msbuild
}

task Clean { 
  # clear out ..\targets
}