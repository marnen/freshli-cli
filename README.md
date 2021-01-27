# freshli CLI

## Getting started with `freshli` CLI

### Prerequisites

Install [docker](https://docs.docker.com/get-docker/)

### Building `freshli-cli`

First, run :
```
git submodule update --init --recursive
```

Freshli-Cli can be built locally using `dotnet build`, or built and run from a docker container.

To build the docker container, run the following:
```
➜  freshli-cli git:(main) ✗ docker build -t freshli-cli .
[+] Building 13.7s (13/13) FINISHED
 => [internal] load build definition from Dockerfile                                                                                                                                                                               0.0s
 => => transferring dockerfile: 32B                                                                                                                                                                                                0.0s
 => [internal] load .dockerignore                                                                                                                                                                                                  0.0s
 => => transferring context: 34B                                                                                                                                                                                                   0.0s
 => [internal] load metadata for mcr.microsoft.com/dotnet/aspnet:5.0                                                                                                                                                               0.3s
 => [internal] load metadata for mcr.microsoft.com/dotnet/sdk:5.0-focal                                                                                                                                                            0.3s
 => [stage-1 1/3] FROM mcr.microsoft.com/dotnet/aspnet:5.0@sha256:8758e3b970476add678a29ec0117cd0acd88cc9627558647338a2244d793d190                                                                                                 0.0s
 => [internal] load build context                                                                                                                                                                                                  0.3s
 => => transferring context: 884.15kB                                                                                                                                                                                              0.1s
 => [build-env 1/4] FROM mcr.microsoft.com/dotnet/sdk:5.0-focal@sha256:77429a46d7d5e113524faf3e7ec2090ce15de90b74a50ce5fc6294453e3cf30d                                                                                            0.0s
 => CACHED [build-env 2/4] WORKDIR /app                                                                                                                                                                                            0.0s
 => [build-env 3/4] COPY . ./                                                                                                                                                                                                      0.1s
 => [build-env 4/4] RUN dotnet publish Freshli.Cli -c Release -o out                                                                                                                                                              12.4s
 => CACHED [stage-1 2/3] WORKDIR /app                                                                                                                                                                                              0.0s
 => [stage-1 3/3] COPY --from=build-env /app/out .                                                                                                                                                                                 0.2s
 => exporting to image                                                                                                                                                                                                             0.2s
 => => exporting layers                                                                                                                                                                                                            0.1s
 => => writing image sha256:9c382d3a2fab0279a57613c108d2f8e00420fb8716c7922d1329585a538ab290                                                                                                                                       0.0s
 => => naming to docker.io/library/freshli-cli 
```

### Running `freshli-cli`

Freshli-Cli can be run locally via dotnet or docker. If you choose to run from docker, nothing else is required, but if you'd like to run local, a `dotnet` installation is required.

#### Using dotnet
```
dotnet run --project Freshli.Cli/Freshli.Cli.csproj -- <url>
```

#### Using docker (preferred method)
```
docker run --rm --interactive --tty freshli-cli <url>
```

Example:
```
➜  freshli-cli git:(main) ✗ docker run --rm --interactive --tty freshli-cli https://github.com/corgibytes/freshli-fixture-ruby-nokotest.git
2021/01/27 19:50:11.881| INFO|Freshli.Cli.Program:10|Main(https://github.com/corgibytes/freshli-fixture-ruby-nokotest.git)
2021/01/27 19:50:12.006| INFO|Freshli.ManifestFinder:49|Registering IManifestFinder: Freshli.Languages.Ruby.RubyBundlerManifestFinder
2021/01/27 19:50:12.006| INFO|Freshli.ManifestFinder:49|Registering IManifestFinder: Freshli.Languages.Python.PipRequirementsTxtManifestFinder
2021/01/27 19:50:12.006| INFO|Freshli.ManifestFinder:49|Registering IManifestFinder: Freshli.Languages.Php.PhpComposerManifestFinder
2021/01/27 19:50:12.006| INFO|Freshli.ManifestFinder:49|Registering IManifestFinder: Freshli.Languages.Perl.CpanfileManifestFinder
2021/01/27 19:50:12.006| INFO|Freshli.Cli.Program:14|Collecting data for https://github.com/corgibytes/freshli-fixture-ruby-nokotest.git
2021/01/27 19:50:12.010| INFO|Freshli.Runner:20|Run(https://github.com/corgibytes/freshli-fixture-ruby-nokotest.git, 01/27/2021)
Date    LibYear UpgradesAvailable       Skipped
2017/01/01      0.0000  0       0
2017/02/01      0.0219  1       0
2017/03/01      0.0219  1       0
2017/04/01      0.2274  1       0
2017/05/01      0.2274  1       0
2017/06/01      0.3644  1       0
2017/07/01      1.8521  2       0
2017/08/01      1.8521  2       0
2017/09/01      1.8521  2       0
2017/10/01      2.4164  2       0
2017/11/01      2.4164  2       0
2017/12/01      2.4164  2       0
2018/01/01      0.0000  0       0
2018/02/01      0.3616  1       0
2018/03/01      0.3616  1       0
2018/04/01      0.3616  1       0
2018/05/01      0.3616  1       0
2018/06/01      0.3616  1       0
2018/07/01      0.7397  1       0
2018/08/01      0.7890  1       0
2018/09/01      0.7890  1       0
2018/10/01      0.7890  1       0
2018/11/01      1.0438  1       0
2018/12/01      1.0438  1       0
2019/01/01      0.0000  0       0
2019/02/01      0.0712  1       0
2019/03/01      0.0712  1       0
2019/04/01      0.2658  1       0
2019/05/01      0.3425  1       0
2019/06/01      0.3425  1       0
2019/07/01      0.3425  1       0
2019/08/01      0.3425  1       0
2019/09/01      0.6466  1       0
2019/10/01      0.6466  1       0
2019/11/01      0.8685  1       0
2019/12/01      0.8685  1       0
2020/01/01      0.9616  1       0
2020/02/01      0.9616  1       0
2020/03/01      2.4329  2       0
2020/04/01      2.4329  2       0
2020/05/01      2.4329  2       0
2020/06/01      2.4329  2       0
2020/07/01      2.4329  2       0
2020/08/01      2.7808  2       0
2020/09/01      2.7808  2       0
2020/10/01      2.7808  2       0
2020/11/01      2.7808  2       0
2020/12/01      2.7808  2       0
2021/01/01      2.7808  2       0
```

`freshli-cli` and `freshli` should build and run on any platform that's supported by the .NET Core SDK. It is heavily tested on both macOS and Windows. If you run into problems, please open an issue. The output from above was captured from running in `zsh` on macOS Catalina (10.15.5).

### Running the test suite

Simply running `dotnet test` will kick off the test runner. If you're using an IDE to build `freshli-cli`, such as JetBrains Rider or Visual Studio 2019, then you can use the test runner that's built into the IDE.

Here's an example of a successful test run:

```
➜  freshli git:(main) ✗ dotnet test
  Determining projects to restore...
  Restored /home/dan/RiderProjects/freshli-cli/Freshli.Cli.Test/Freshli.Cli.Test.csproj (in 723 ms).
  Restored /home/dan/RiderProjects/freshli-cli/freshli/Freshli/Freshli.csproj (in 723 ms).
  Restored /home/dan/RiderProjects/freshli-cli/Freshli.Cli/Freshli.Cli.csproj (in 722 ms).
  Restored /home/dan/RiderProjects/freshli-cli/freshli/Freshli.Test/Freshli.Test.csproj (in 736 ms).
  Freshli -> /home/dan/RiderProjects/freshli-cli/freshli/Freshli/bin/Debug/net5.0/Freshli.dll
  Freshli.Cli -> /home/dan/RiderProjects/freshli-cli/Freshli.Cli/bin/Debug/net5.0/Freshli.Cli.dll
  Freshli.Test -> /home/dan/RiderProjects/freshli-cli/freshli/Freshli.Test/bin/Debug/net5.0/Freshli.Test.dll
  Archive:  nokotest.zip
    inflating: nokotest/Gemfile        
    inflating: nokotest/Gemfile.lock   
    inflating: nokotest/.git/config    
   extracting: nokotest/.git/objects/0d/8f4f864a22eac5f72153cf1d77fc9791796e6d  
   extracting: nokotest/.git/objects/93/e24fec7e2d55e1f2649989a131b1a044008e60  
   extracting: nokotest/.git/objects/bb/e94adc863a728d5c63b1293a7d1d81ac437f30  
   extracting: nokotest/.git/objects/6e/dae1c2dc746439f567894cf77effc7a8abf97b  
   extracting: nokotest/.git/objects/01/7031627f36deb582d69cddd381718be0044b02  
   extracting: nokotest/.git/objects/90/2a3082740f83776eec419c59a56e54424fdec5  
   extracting: nokotest/.git/objects/b9/803963c64c5c8794334bb667d98c969add6fd0  
   extracting: nokotest/.git/objects/b9/d397bcc26e2a820a2e077298f35521b154febd  
   extracting: nokotest/.git/objects/c4/a0ab82b5bf0d03d646348bce24527d84d8bfe4  
   extracting: nokotest/.git/objects/e1/be34540508cfb94fea222ecdc61a95652068ee  
   extracting: nokotest/.git/objects/76/06873e8c521ba79d093029969c2da124ed03d3  
   extracting: nokotest/.git/objects/13/963f09081c175c66d20f7dd15d23fedc789ce4  
   extracting: nokotest/.git/HEAD      
    inflating: nokotest/.git/info/exclude  
    inflating: nokotest/.git/logs/HEAD  
    inflating: nokotest/.git/logs/refs/heads/master  
    inflating: nokotest/.git/description  
    inflating: nokotest/.git/hooks/commit-msg.sample  
    inflating: nokotest/.git/hooks/pre-rebase.sample  
    inflating: nokotest/.git/hooks/pre-commit.sample  
    inflating: nokotest/.git/hooks/applypatch-msg.sample  
    inflating: nokotest/.git/hooks/prepare-commit-msg.sample  
    inflating: nokotest/.git/hooks/post-update.sample  
    inflating: nokotest/.git/hooks/pre-applypatch.sample  
    inflating: nokotest/.git/hooks/pre-push.sample  
    inflating: nokotest/.git/hooks/update.sample  
   extracting: nokotest/.git/refs/heads/master  
    inflating: nokotest/.git/index     
   extracting: nokotest/.git/COMMIT_EDITMSG  
Test run for /home/dan/RiderProjects/freshli-cli/freshli/Freshli.Test/bin/Debug/net5.0/Freshli.Test.dll (.NETCoreApp,Version=v5.0)
Microsoft (R) Test Execution Command Line Tool Version 16.8.3
Copyright (c) Microsoft Corporation.  All rights reserved.

  Freshli.Cli.Test -> /home/dan/RiderProjects/freshli-cli/Freshli.Cli.Test/bin/Debug/net5.0/Freshli.Cli.Test.dll
Test run for /home/dan/RiderProjects/freshli-cli/Freshli.Cli.Test/bin/Debug/net5.0/Freshli.Cli.Test.dll (.NETCoreApp,Version=v5.0)
Microsoft (R) Test Execution Command Line Tool Version 16.8.3
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
Starting test execution, please wait...
A total of 1 test files matched the specified pattern.
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:     1, Skipped:     0, Total:     1, Duration: 8 ms - /home/dan/RiderProjects/freshli-cli/Freshli.Cli.Test/bin/Debug/net5.0/Freshli.Cli.Test.dll (net5.0)
2021/01/25 12:56:30.242| INFO|Freshli.ManifestFinder:57|Registering IManifestFinder: Freshli.Languages.Ruby.RubyBundlerManifestFinder 
2021/01/25 12:56:30.299| INFO|Freshli.ManifestFinder:57|Registering IManifestFinder: Freshli.Languages.Python.PipRequirementsTxtManifestFinder 
...
2021/01/25 13:01:34.341| WARN|Freshli.LibYearCalculator:76|Negative value (-0.036) computed for tzinfo as of 3/1/2014; setting value to 0: { Name: "tzinfo", RepoVersion: "0.3.38", RepoVersionPublishedAt: 2013-10-08T00:00:00, LatestVersion: "1.1.0", LatestPublishedAt: 2013-09-25T00:00:00, UpgradeAvailable: True, Value: 0 } 
2021/01/25 13:01:40.380| WARN|Freshli.LibYearCalculator:76|Negative value (-0.003) computed for json as of 4/1/2017; setting value to 0: { Name: "json", RepoVersion: "1.8.6", RepoVersionPublishedAt: 2017-01-13T00:00:00, LatestVersion: "2.0.3", LatestPublishedAt: 2017-01-12T00:00:00, UpgradeAvailable: True, Value: 0 } 
2021/01/25 13:01:40.383| WARN|Freshli.LibYearCalculator:76|Negative value (-0.364) computed for rack as of 4/1/2017; setting value to 0: { Name: "rack", RepoVersion: "1.6.5", RepoVersionPublishedAt: 2016-11-10T00:00:00, LatestVersion: "2.0.1", LatestPublishedAt: 2016-06-30T00:00:00, UpgradeAvailable: True, Value: 0 } 
2021/01/25 13:01:40.397| WARN|Freshli.LibYearCalculator:76|Negative value (-0.364) computed for rack as of 5/1/2017; setting value to 0: { Name: "rack", RepoVersion: "1.6.5", RepoVersionPublishedAt: 2016-11-10T00:00:00, LatestVersion: "2.0.1", LatestPublishedAt: 2016-06-30T00:00:00, UpgradeAvailable: True, Value: 0 } 

Passed!  - Failed:     0, Passed:   813, Skipped:     0, Total:   813, Duration: 6 m 7 s - /home/dan/RiderProjects/freshli-cli/freshli/Freshli.Test/bin/Debug/net5.0/Freshli.Test.dll (net5.0)
```

The tests currently take longer to run than we would like. We're exploring ways to speed that up. You can run a subset of tests by including the `--filter` flag, e.g. `dotnet test --filter ComputeAsOf`.

## Next Steps
Once the cli-breakout changes are merged into freshli, the freshli submodule will need to be updated to the `main` branch and the output for this `README` will need to be re-generated.