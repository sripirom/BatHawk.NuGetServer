
echo "SONARSCANNER_MSBUILD: $SONARSCANNER_MSBUILD"
echo "NUGET_EXE: $NUGET_EXE"

sonarScannerMsbuild=$SONARSCANNER_MSBUILD
#sonarScannerMsbuild=/Volumes/MyDrive/SripiromDev/Tools/sonar-scanner-msbuild/SonarScanner.MSBuild.dll 
if [ ! -z $1 ]; then
  if [ $1 -lt 0 ] || [ $1 -gt 100 ]; then
    echo "Threshold should be between 0 and 100"
    threshold=80
  fi
  threshold=$1
else
  threshold=80
fi
 

dot="$(cd "$(dirname "$0")"; pwd)"
#path="$dot/some/path"

dotnet $sonarScannerMsbuild begin \
    /k:"BatHawk.NuGetServer" \
    /n:"BatHawk.NuGetServer" \
    /s:"$dot/SonarQube.Analysis.xml"
      #/d:sonar.cs.xunit.it.reportsPaths="$dot/TestResult.xml" \

mono $NUGET_EXE restore ./BatHawk/BatHawk.sln
msbuild ./BatHawk/BatHawk.sln

cd tools
#dotnet restore

# Instrument assemblies inside 'test' folder to detect hits for source files inside 'src' folder
#dotnet minicover instrument --workdir ../ --assemblies test/**/bin/**/*.dll --sources src/**/*.cs  

# Reset hits count in case minicover was run for this project
#dotnet minicover reset

 
#for project in $dot/test/**/*.csproj; do dotnet test  --no-build $project \
 #--logger:"trx" --results-directory:"$dot/TestResults"; done

#dotnet minicover uninstrument --workdir ../

# Print console report
# This command returns failure if the coverage is lower than the threshold
#dotnet minicover report --workdir ../ --threshold $threshold
 
# Create Opencover report
#dotnet minicover opencoverreport --workdir ../ --threshold $threshold

cd ..

dotnet $sonarScannerMsbuild end



