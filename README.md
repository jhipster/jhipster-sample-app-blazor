# Jhipster

This application was generated using JHipster 7.8.1, you can find documentation and help at [https://www.jhipster.tech/documentation-archive/v7.8.1](https://www.jhipster.tech/documentation-archive/v7.8.1).

This application was generated using JHipster 7.8.1 and JHipster .Net Core , you can find documentation and help at https://jhipsternet.readthedocs.io/en/latest/index.html and [https://www.jhipster.tech/documentation-archive/v7.8.1](https://www.jhipster.tech/documentation-archive/v7.8.1).

## Project Structure

Node is required for generation and recommended for development. `package.json` is always generated for a better development experience with prettier, commit hooks, scripts and so on.

In the project root, JHipster generates configuration files for tools like git, prettier, eslint, husky, and others that are well known and you can find references in the web.

- `.yo-rc.json` - Yeoman configuration file
  JHipster configuration is stored in this file at `generator-jhipster` key. You may find `generator-jhipster-*` for specific blueprints configuration.
- `.yo-resolve` (optional) - Yeoman conflict resolver
  Allows to use a specific action when conflicts are found skipping prompts for files that matches a pattern. Each line should match `[pattern] [action]` with pattern been a [Minimatch](https://github.com/isaacs/minimatch#minimatch) pattern and action been one of skip (default if ommited) or force. Lines starting with `#` are considered comments and are ignored.
- `.jhipster/*.json` - JHipster entity configuration files
- `docker/` - Docker configurations for the application and services that the application depends on
- `src/Jhipster/ClientApp/` - Web Application folder

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

## Building for production

### .Net Production builds

To build the artifacts and optimize the Jhipster application for production, run:

```
cd ./src/Jhipster
rm -rf ./src/Jhipster/wwwroot
dotnet publish --verbosity normal -c Release -o ./app/out ./Jhipster.csproj
```

The `./src/Jhipster/app/out` directory will contain your application dll and its depedencies.

## Testing

### .Net Backend tests

To launch your application's tests, run:

```
dotnet test --verbosity normal
```

## Others

### Code style / formatting

To format the dotnet code, run

```
dotnet format
```

### Code quality

By Script :

1. Run Sonar in container : `docker compose -f ./docker/sonar.yml up -d`

2. Wait container was up Run `SonarAnalysis.ps1` and go to http://localhost:9001

Manually :

1. Run Sonar in container : `docker compose -f ./docker/sonar.yml up -d`

2. Install sonar scanner for .net :

`dotnet tool install --global dotnet-sonarscanner`

3. Run ``dotnet sonarscanner begin /d:sonar.login=admin /d:sonar.password=admin /k:"Jhipster" /d:sonar.host.url="http://localhost:9001" /s:"`pwd`/SonarQube.Analysis.xml"``

4. Build your application : `dotnet build`

5. Publish sonar results : `dotnet sonarscanner end /d:sonar.login=admin /d:sonar.password=admin`

6. Go to http://localhost:9001

### Monitoring

1. Run container (uncomment chronograf and kapacitor if you would use it): `docker compose -f ./docker/monitoring.yml up -d`

2. Go to http://localhost:3000 (or http://localhost:8888 if you use chronograf)

3. (Only for chronograf) Change influxdb connection string by `YourApp-influxdb`

4. (Only for chronograf) Change kapacitor connection string by `YourApp-kapacitor`

5. (Only for chronograf) You can now add dashboard (like docker), see your app log in Cronograf Log viewer and send alert with kapacitor

### Build a Docker image

You can also fully dockerize your application and all the services that it depends on. To achieve this, first build a docker image of your app by running:

```bash
docker build -f ./Dockerfile-Back -t jhipster .
```

```bash
docker build -f ./Dockerfile-Front -t jhipster-front .
```

Then run:

```bash
docker run -p 8080:80 jhipster
```

```bash
docker run -p 8081:80 -e ServerUrl=https://localhost:8080 jhipster-front
```

Or you can simply run :

```bash
docker compose -f .\docker\app.yml build
```

And

```bash
docker compose -f .\docker\app.yml up
```

[JHipster Homepage and latest documentation]: https://www.jhipster.tech
[JHipster 7.8.1 archive]: https://www.jhipster.tech/documentation-archive/v7.8.1
[Using JHipster in development]: https://www.jhipster.tech/documentation-archive/v7.8.1/development/
[Using Docker and Docker-Compose]: https://www.jhipster.tech/documentation-archive/v7.8.1/docker-compose
[Using JHipster in production]: https://www.jhipster.tech/documentation-archive/v7.8.1/production/
[Running tests page]: https://www.jhipster.tech/documentation-archive/v7.8.1/running-tests/
[Code quality page]: https://www.jhipster.tech/documentation-archive/v7.8.1/code-quality/
[Setting up Continuous Integration]: https://www.jhipster.tech/documentation-archive/v7.8.1/setting-up-ci/
[Node.js]: https://nodejs.org/
[NPM]: https://www.npmjs.com/
