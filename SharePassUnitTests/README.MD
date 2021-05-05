# ReadMe

## Start the service

1. Open project folder
2. Run console from the folder
3. Run command:

```yml
>docker-compose up
```

4. Open [http://localhost:8080](http://localhost:8080) in the browser

## Run tests

1. Open UnitTests project folder with Visual Studio or Visual Studio Code
2. Go To Test Explorer
3. Run the tests

## Server API

1. GetLink(formData) - gets formData (passed password), returns the link
2. GetPass(urlToken) - returns restored password

## Methodolody

RAD methodolody was selected. Allows in minimal time give the necessary functionality.

## Improvements

1. Encrypt password, and sent to the server encrypted value.
2. Implement SSL protocol
3. Save **salt** in separate db
4. Add db authentication
5. Store AesEncryptor token in separate place (db, registry with hight access level)