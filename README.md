# Jhipster

This application was generated using JHipster 7.8.1 and JHipster .Net Core 3.6.0, you can find documentation and help at https://jhipsternet.readthedocs.io/en/latest/index.html and [https://www.jhipster.tech/documentation-archive/v7.8.1](https://www.jhipster.tech/documentation-archive/v7.8.1).

## Development

Before you can build this project, you need to verify if [libman](https://github.com/aspnet/LibraryManager) and [webcompiler](https://github.com/excubo-ag/WebCompiler) are installed. (If the application is generated, the generator installed this tools for you)

If not, run

```bash
dotnet tool install -g Microsoft.Web.LibraryManager.Cli
dotnet tool install -g Excubo.WebCompiler
```

To start your application in the Debug configuration, simply run:

```bash
dotnet run --verbosity normal --project ./src/Jhipster/Jhipster.csproj
```

and

```bash
dotnet run --verbosity normal --project ./src/client/Jhipster.Client/Jhipster.Client.csproj
```

You can also use the **hotreload**

For the backend

```bash
dotnet watch --project ./src/Jhipster/Jhipster.csproj run --verbosity normal
```

For the frontend

```bash
dotnet watch --project ./src/client/Jhipster.Client/Jhipster.Client.csproj run --verbosity normal
```

This will concatenate and minify the client CSS and JavaScript files. It will also modify `index.html` so it references these new files.

## Code style / formatting

To format the dotnet code, run

    dotnet format

## Testing

To launch your application's tests, run:

    dotnet test --verbosity normal

### Generate entities

With cli

```bash
jhipster entity <entity-name>
```

or with jdl (https://start.jhipster.tech/jdl-studio/)

```bash
jhipster import-jdl my_file.jdl
```

### Code quality

By Script :

1. Run Sonar in container : `docker-compose -f ./docker/sonar.yml up -d`

2. Wait container was up Run `SonarAnalysis.ps1` and go to http://localhost:9001

Manually :

1. Run Sonar in container : `docker-compose -f ./docker/sonar.yml up -d`

2. Install sonar scanner for .net :

`dotnet tool install --global dotnet-sonarscanner`

3. Run `` dotnet sonarscanner begin /d:sonar.login=admin /d:sonar.password=admin /k:"Jhipster" /d:sonar.host.url="http://localhost:9001" /s:"`pwd`/SonarQube.Analysis.xml" ``

4. Build your application : `dotnet build`

5. Publish sonar results : `dotnet sonarscanner end /d:sonar.login=admin /d:sonar.password=admin`

6. Go to http://localhost:9001

### Monitoring

1. Run container (uncomment chronograf and kapacitor if you would use it): `docker-compose -f ./docker/monitoring.yml up -d`

2. Go to http://localhost:3000 (or http://localhost:8888 if you use chronograf)

3. (Only for chronograf) Change influxdb connection string by `YourApp-influxdb`

4. (Only for chronograf) Change kapacitor connection string by `YourApp-kapacitor`

5. (Only for chronograf) You can now add dashboard (like docker), see your app log in Cronograf Log viewer and send alert with kapacitor

## Build a Docker image

You can also fully dockerize your application and all the services that it depends on. To achieve this, first build a docker image of your app by running:

```bash
docker build -f ./Dockerfile-Back -t jhipster .
```

```bash
docker build -f ./Dockerfile-Front -t jhipster-front . --build-arg ServerUrl=https://localhost:8080
```

Then run:

```bash
docker run -p 8080:80 jhipster
```

```bash
docker run -p 8081:80 jhipster-front
```

Or you can simply run :

```bash
docker-compose -f .\docker\app.yml build
```

And

```bash
docker-compose -f .\docker\app.yml up
```
