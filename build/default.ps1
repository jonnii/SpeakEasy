properties {
	$buildName = "dev"
}

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

	# update nuspec package
	$version = get-content ..\VERSION
	$when = (get-date).ToString("yyyyMMddHHmmss")
	$packageVersion = "$version-$buildName-$when"

	get-content ..\targets\packages\package.nuspec | 
        %{$_ -replace '0.0.0.1', $packageVersion } > ..\targets\packages\package.nuspec.tmp
	
	mv ..\targets\packages\package.nuspec.tmp ..\targets\packages\package.nuspec -force


	..\src\.nuget\nuget.exe pack "..\targets\packages\package.nuspec" -outputdirectory ".\..\targets\packages"
}

task Publish -depends Package {
	$package = gci .\..\targets\packages\*.nupkg | select -first 1
	
	..\src\.nuget\nuget.exe Push $package.fullname
}

task CopyTools {
	
}

task Test -depends Clean,CopyTools,Compile { 
  # run specs
  ..\src\packages\Machine.Specifications.0.5.2.0\tools\mspec-clr4.exe .\..\targets\specifications\speakeasy.specifications.dll

  # run integration tests somehow (need a local nunit-console.exe, or switch to xunit?)
}

task UpdateAssemblyInfo {
	$version = get-content ..\VERSION

	$assemblyVersion = 'AssemblyVersion("' + $version + '")';
	$assemblyFileVersion = 'AssemblyFileVersion("' + $version + '")';

	get-content ..\src\CommonAssemblyInfo.cs | 
        %{$_ -replace 'AssemblyVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)', $assemblyVersion } |
        %{$_ -replace 'AssemblyFileVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)', $assemblyFileVersion }  > ..\src\CommonAssemblyInfo.cs.tmp
	
	mv ..\src\CommonAssemblyInfo.cs.tmp ..\src\CommonAssemblyInfo.cs -force
}

task Compile -depends Clean,UpdateAssemblyInfo { 
  # call msbuild
  $options = "/p:Configuration=Release"
  $msbuild = "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe"
  
  Push-Location ..\src
  $build = $msbuild + ' "SpeakEasy.sln" ' + $options + " /t:Build"

  iex $build
  Pop-Location
}

task Clean { 
  if (Test-Path -path '..\targets') {
	rm -recurse -force ..\targets
  }
}