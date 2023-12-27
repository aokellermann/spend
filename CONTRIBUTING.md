# Contributing

## Infrastructure

Spend uses terraform for IaC.

You'll need the following cloud accounts + credentials:
- AWS
- MongoDB Atlas

Bootstrap the s3 backend:

```sh
export TF_VAR_account_alias=myaccountname

terraform -chdir=terraform/meta/bootstrap apply

cp terraform/meta/config.s3.tfbackend.example erraform/meta/config.s3.tfbackend
sed -i 's/<account_alias>/'"$TF_VAR_account_alias"'/g' terraform/meta/config.s3.tfbackend
```

Create a Google cloud app and oauth credentials to allow using Google as
a Cognito identity provider. Put them into parameter store:
- `/google_idp/client_id`
- `/google_idp/client_secret`

Now you can create the actual infrastructure:

```sh
cd terraform/mesa

./tfinit.sh secrets
terraform -chdir=secrets apply

./tfinit.sh ciam
terraform -chdir=ciam apply

./tfinit.sh data
terraform -chdir=data apply
```

Once the database is created, put that in parameter store: `/db/mongo/password`.
(Todo: how to get password when created through terraform?)

## Running Locally

### DB

Spend uses mongodb as document storage.

You can spin up a local docker container with:

```sh
docker run -e MONGO_INITDB_ROOT_USERNAME=root -e MONGO_INITDB_ROOT_PASSWORD=password -e MONGO_GRAPH_PASSWORD=password --rm -p 27017:27017 -v "./Migrations/mongo_init.js:/docker-entrypoint-initdb.d/mongo_init.js" -it mongo
```

Then, to run database migrations:
```sh
mongosh mongodb://graph:password@localhost/Graph?authSource=test < Migrations/mongo_indexes.js
```

### API

The recommended way to run the .NET API is to use a `launchSettings.json` file.

The repository provides a template file for you to add your own settings:

```sh
cp Graph/src/Spend/Properties/launchSettingsExample.json Graph/src/Spend/Properties/launchSettings.json
```

After adding these variables, you can start the server:

```sh
dotnet run --project Graph/src/Spend --profile Local
```

### App

#### Android

If you are using a physical Android device, to enable network access to a local API, run:

```sh
adb reverse tcp:5000 tcp:5000
```

The app uses AWS Amplify plugin to configure auth through Cognito. You don't need
an actual Amplify app; you just need the Cognito resources to be created.

The repository provides a template file for you to add your own settings:

```sh
cp lib/amplifyconfigurationexample.dart lib/amplifyconfiguration.dart
```

You'll need to fill in:
- `CognitoUserPool:Default`
  - `PoolId`
  - `AppClientId`
  - `Region`
- `Auth:Default:OAuth`
  - `WebDomain`
  - `AppClientId`

Then build the app:

```sh
flutter build --debug
echo "flutter.minSdkVersion=24" >> android/local.properties
```
